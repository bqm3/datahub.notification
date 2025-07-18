using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Web;
using OfficeOpenXml;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ClosedXML.Excel;
using microservice.mess.Models.Message;
using microservice.mess.Repositories;
using microservice.mess.Helpers;

namespace microservice.mess.Services
{
    public class SignetService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SignetRepository _signetRepository;
        private readonly string _gatewayUrl;

        public SignetService(HttpClient httpClient, IConfiguration configuration, SignetRepository signetRepository, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _signetRepository = signetRepository;
            _httpClientFactory = httpClientFactory;
            _gatewayUrl = configuration["Signet:Gateway"];
        }



        public async Task<ApiResponse<AllMessageTemplate>> UpdateTemplateAsync(string name, AllMessageTemplate template)
        {
            var existingTemplate = await GetTemplateByNameAsync(name);
            if (!existingTemplate.Success || existingTemplate.Data == null)
            {
                return ApiResponse<AllMessageTemplate>.ErrorResponse("Template not found.", 404);
            }

            template.CreatedAt = existingTemplate.Data.CreatedAt; // Keep original creation date
            var result = await _signetRepository.UpdateTemplateAsync(name, template);
            if (!result.Success)
            {
                return ApiResponse<AllMessageTemplate>.ErrorResponse(result.Message, result.Status);
            }

            return ApiResponse<AllMessageTemplate>.SuccessResponse(template, "Template updated successfully");
        }

        public async Task<SGIActionModel> GetActionByCallbackValueAsync(string callbackUrl)
        {
            // Extract value from callback URL if needed, assuming the value is part of the URL or a query parameter
            // For simplicity, this example assumes the callbackUrl contains the value directly or as a query parameter
            string value = callbackUrl;
            if (callbackUrl.Contains("?"))
            {
                var uri = new Uri(callbackUrl);
                var query = uri.Query;
                var queryParams = System.Web.HttpUtility.ParseQueryString(query);
                value = queryParams["value"] ?? callbackUrl;
            }

            return await _signetRepository.GetSGIActionByValueAsync(value);
        }

        public async Task<SGIActionModel> GetByCmdValueAsync(string cmdValue)
        {
            return await _signetRepository.GetByCmdValueAsync(cmdValue);
        }


        public async Task<string?> CreateSGIActionAsync(SGIActionModel template)
        {
            try
            {
                if (template == null)
                {
                    throw new ArgumentNullException(nameof(template));
                }

                // Save the template to the database
                await _signetRepository.CreateSGIActionAsync(template);

                return $"Lưu thành công với ID: {template.Id}";
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return $"Lỗi: {ex.Message}";
            }
        }

        public async Task<ApiResponse<SGIUploadFileHashModel>> CreateSGIUploadFileHashAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return ApiResponse<SGIUploadFileHashModel>.ErrorResponse("File không hợp lệ");

                using var form = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                form.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync($"{_gatewayUrl}/files/upload/single", form);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<SGIUploadFileHashModel>.ErrorResponse($"Lỗi upload file: {response.StatusCode} - {responseBody}");

                using var doc = JsonDocument.Parse(responseBody);
                var encodedFileValue = doc.RootElement.GetProperty("data").GetProperty("value").GetString();

                // Parse thành NameValueCollection
                var normalizedValue = PkceHelper.NormalizeFileValue(encodedFileValue!);

                var model = new SGIUploadFileHashModel
                {
                    FileName = file.FileName,
                    FileHash = normalizedValue,
                    FileType = file.ContentType,
                    CreatedAt = DateTime.UtcNow
                };

                await _signetRepository.UploadFileHashAsync(model);

                return ApiResponse<SGIUploadFileHashModel>.SuccessResponse(model, "Upload file thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<SGIUploadFileHashModel>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<List<SGIUploadFileHashModel>> GetAllUploadFileHashesAsync()
        {
            return await _signetRepository.GetAllUploadFileHashesAsync();
        }

        public async Task<SGIUploadFileHashModel?> GetUploadFileHashByIdAsync(string id)
        {
            return await _signetRepository.GetUploadFileHashByIdAsync(id);
        }

        public async Task<ApiResponse<AllMessageTemplate>> SaveTemplateAsync(AllMessageTemplate template)
        {
            try
            {
                if (template == null)
                    return ApiResponse<AllMessageTemplate>.ErrorResponse("Template không hợp lệ");

                await _signetRepository.SaveTemplateAsync(template);
                return ApiResponse<AllMessageTemplate>.SuccessResponse(template, "Lưu template thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<AllMessageTemplate>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<object>> SendTemplateMessageAsync(SendTemplateMessageRequest request)
        {
            var templateResult = await GetTemplateByNameAsync(request.TemplateName);
            if (!templateResult.Success || templateResult.Data == null)
                return ApiResponse<object>.ErrorResponse("Template not found.");

            var rawBlock = templateResult.Data.Block;
            foreach (var kv in request.Data)
            {
                rawBlock = rawBlock.Replace($"{{{{{kv.Key}}}}}", kv.Value); // "{{key}}" → value
            }

            var blockObject = JsonConvert.DeserializeObject<object>(rawBlock);

            var payload = new
            {
                receivers = templateResult.Data.Receivers,
                block = blockObject,
                request = new
                {
                    req_id = "",
                    url_path = ""
                }
            };

            using var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8036/api/bot-gateway/v1/chat/post-message", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<object>.ErrorResponse($"Failed to send message. {responseContent}");
            }

            return ApiResponse<object>.SuccessResponse(new { payload }, "Message sent successfully");
        }

        public async Task<ApiResponse<AllMessageTemplate>> GetTemplateByNameAsync(string name)
        {
            try
            {
                var template = await _signetRepository.GetTemplateByNameAsync(name);
                if (template == null)
                    return ApiResponse<AllMessageTemplate>.ErrorResponse("Template không tồn tại");

                return ApiResponse<AllMessageTemplate>.SuccessResponse(template, "Tìm thấy template");
            }
            catch (Exception ex)
            {
                return ApiResponse<AllMessageTemplate>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserAccountModel>>> UploadExcelFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return ApiResponse<List<UserAccountModel>>.ErrorResponse("File không hợp lệ");

                var processedUsers = new List<UserAccountModel>();
                using (var stream = file.OpenReadStream())
                {
                    using (var workbook = new ClosedXML.Excel.XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rowCount = worksheet.RowsUsed().Count();

                        for (int row = 2; row <= rowCount; row++) // Assuming first row is header
                        {
                            var userName = worksheet.Cell(row, 1).GetString();
                            var fullName = worksheet.Cell(row, 2).GetString();
                            var avatar = worksheet.Cell(row, 3).GetString();
                            var role = worksheet.Cell(row, 4).GetString();
                            var group = worksheet.Cell(row, 5).GetString();

                            if (string.IsNullOrEmpty(userName))
                                continue;

                            var existingUser = await _signetRepository.GetSignetUserByUserNameAsync(userName);
                            if (existingUser != null)
                            {
                                // If user exists, update group if different
                                if (existingUser.Group != group && !string.IsNullOrEmpty(group))
                                {
                                    existingUser.Group = group;
                                    await _signetRepository.UpdateSignetUserAsync(userName, existingUser);
                                    processedUsers.Add(existingUser);
                                }
                            }
                            else
                            {
                                // If user does not exist, create new
                                var newUser = new UserAccountModel
                                {
                                    UserName = userName,
                                    FullName = fullName,
                                    Avatar = avatar,
                                    Role = role,
                                    Group = group
                                };
                                await _signetRepository.CreateSignetUserAsync(newUser);
                                processedUsers.Add(newUser);
                            }
                        }
                    }
                }

                return ApiResponse<List<UserAccountModel>>.SuccessResponse(processedUsers, "Upload và xử lý file Excel thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<UserAccountModel>>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        // UserAccountModel CRUD Operations
        public async Task<ApiResponse<UserAccountModel>> CreateSignetUserAsync(UserAccountModel user)
        {
            try
            {
                if (user == null)
                    return ApiResponse<UserAccountModel>.ErrorResponse("User không hợp lệ");

                var existingUser = await _signetRepository.GetSignetUserByUserNameAsync(user.UserName);
                if (existingUser != null)
                    return ApiResponse<UserAccountModel>.ErrorResponse("UserName đã tồn tại");

                await _signetRepository.CreateSignetUserAsync(user);
                return ApiResponse<UserAccountModel>.SuccessResponse(user, "Tạo user thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserAccountModel>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<List<UserAccountModel>> GetAllSignetUsersAsync()
        {
            return await _signetRepository.GetAllSignetUsersAsync();
        }

        public async Task<UserAccountModel?> GetSignetUserByUserNameAsync(string userName)
        {
            return await _signetRepository.GetSignetUserByUserNameAsync(userName);
        }

        public async Task<List<UserAccountModel>> GetSignetUsersByGroupAsync(string group)
        {
            return await _signetRepository.GetSignetUsersByGroupAsync(group);
        }

        public async Task<ApiResponse<UserAccountModel>> UpdateSignetUserAsync(string userName, UserAccountModel user)
        {
            try
            {
                var existingUser = await _signetRepository.GetSignetUserByUserNameAsync(userName);
                if (existingUser == null)
                    return ApiResponse<UserAccountModel>.ErrorResponse("User không tồn tại");

                // Check if the new username is already taken by another user
                if (user.UserName != userName)
                {
                    var userWithNewName = await _signetRepository.GetSignetUserByUserNameAsync(user.UserName);
                    if (userWithNewName != null)
                        return ApiResponse<UserAccountModel>.ErrorResponse("UserName mới đã tồn tại");
                }

                await _signetRepository.UpdateSignetUserAsync(userName, user);
                return ApiResponse<UserAccountModel>.SuccessResponse(user, "Cập nhật user thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserAccountModel>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<string>> DeleteSignetUserAsync(string userName)
        {
            try
            {
                var existingUser = await _signetRepository.GetSignetUserByUserNameAsync(userName);
                if (existingUser == null)
                    return ApiResponse<string>.ErrorResponse("User không tồn tại");

                await _signetRepository.DeleteSignetUserAsync(userName);
                return ApiResponse<string>.SuccessResponse(userName, "Xóa user thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResponse($"Lỗi: {ex.Message}", 500);
            }
        }
    }
}

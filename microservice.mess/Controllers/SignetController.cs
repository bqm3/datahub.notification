using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using microservice.mess.Services;
using microservice.mess.Repositories;
using microservice.mess.Models.Message;
using microservice.mess.Models;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignetController : ControllerBase
    {
        private readonly ILogger<SignetController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SignetService _signetService;
        private readonly SignetRepository _signetRepository;

        public SignetController(ILogger<SignetController> logger, IHttpClientFactory httpClientFactory, SignetService signetService, SignetRepository signetRepository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _signetService = signetService;
            _signetRepository = signetRepository;
        }

        [HttpPost("sgi-action")]
        public async Task<IActionResult> CreateSGIAction([FromBody] SGIActionModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Type) || string.IsNullOrWhiteSpace(model.Value))
                return BadRequest(new { success = false, message = "Invalid action model." });

            var result = await _signetService.CreateSGIActionAsync(model);
            if (result == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Quick menu not found."));

            return Ok(ApiResponse<string>.SuccessResponse(result, "Lưu thành công"));
        }

        [HttpPost("callback")]
        public async Task<IActionResult> HandleCallback([FromBody] CallbackRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.CallbackUrl))
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid callback request."));

            var action = await _signetService.GetActionByCallbackValueAsync(request.CallbackUrl);
            if (action == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Action not found for the provided callback value."));

            return Ok(ApiResponse<SGIActionModel>.SuccessResponse(action, "Action retrieved successfully"));
        }

        #region 
        [HttpGet("generate")]
        public async Task<IActionResult> GeneratePdf()
        {
            try
            {
                var filePath = await _signetRepository.GeneratePdfWithQuestAsync();
                return Ok(ApiResponse<string>.SuccessResponse(filePath, "Upload file thành công"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo PDF");
                return StatusCode(500, ApiResponse<string>.ErrorResponse(ex.Message, 500));
            }
        }
        #endregion

        #region SGI Upload File
        [HttpPost("upload-file-hash")]
        public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileDto dto, [FromServices] SignetService signetService)
        {
            try
            {
                var result = await _signetService.CreateSGIUploadFileHashAsync(dto.File);

                if (!result.Success)
                    return BadRequest(ApiResponse<string>.ErrorResponse("Invalid callback request."));

                return Ok(ApiResponse<string>.SuccessResponse("Upload file thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(ex.Message, 500));
            }
        }


        [HttpGet("upload-file-hashes")]
        public async Task<IActionResult> GetAllUploadFileHashesAsync([FromServices] SignetService signetService)
        {
            var result = await signetService.GetAllUploadFileHashesAsync();
            return Ok(ApiResponse<List<SGIUploadFileHashModel>>.SuccessResponse(result, "Lấy danh sách file thành công"));
        }

        [HttpGet("upload-file-hash/{id}")]
        public async Task<IActionResult> GetUploadFileHashByIdAsync(string id, [FromServices] SignetService signetService)
        {
            var result = await signetService.GetUploadFileHashByIdAsync(id);
            if (result == null)
                return NotFound(ApiResponse<string>.ErrorResponse("File not found."));

            return Ok(ApiResponse<SGIUploadFileHashModel>.SuccessResponse(result, "Tìm thấy file."));
        }
        #endregion

        #region SGI Action Endpoints
        // [HttpPost("webhook/on-command")]
        // public async Task<IActionResult> OnSgiCommand()
        // {
        //     using var reader = new StreamReader(Request.Body);
        //     var raw = await reader.ReadToEndAsync();

        //     _logger.LogInformation("Raw webhook payload: {0}", raw);

        //     return Ok();
        // }


        [HttpPost("webhook/on-command")]
        public async Task<IActionResult> OnSgiCommand([FromBody] JObject payload)
        {
            _logger.LogInformation("Webhook received: {json}", payload.ToString());

            var cmdValue = payload["command"]?.ToString();
            var receiver = payload["receiver"]?.ToString();
            var msgId = payload["parameter"]?["msg_id"]?.ToString();

            _logger.LogInformation("Nhận webhook từ user {user} với command: {cmd}", receiver, cmdValue);

            if (string.IsNullOrEmpty(cmdValue) || string.IsNullOrEmpty(receiver) || string.IsNullOrEmpty(msgId))
                return BadRequest("Thiếu thông tin bắt buộc");

            var actionTemplate = await _signetService.GetByCmdValueAsync(cmdValue);
            if (actionTemplate == null)
            {
                _logger.LogWarning("Không tìm thấy template cho command: {cmd}", cmdValue);
                return Ok(); // Không làm gì cả
            }

            var responsePayload = new
            {
                req_id = msgId,
                receiver = "dawndev",
                resp_body = new[]
                {
            new
            {
                type = "notify",
                data = new
                {
                    text = actionTemplate.Message,
                    type = "default"
                }
            }
        }
            };

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(responsePayload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://127.0.0.1:8036/api/bot-gateway/v1/chat/post-response", content);
            var respText = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Đã gửi notify: {msg}", actionTemplate.Message);
                return Ok(new { message = "Đã nhận thành công", cmd = cmdValue });
            }

            _logger.LogError("Gửi notify thất bại: {0}", respText);
            return StatusCode(500, respText);
        }


        #endregion

        #region SGI Message Template Endpoints
        [HttpPost("template")]
        public async Task<IActionResult> CreateTemplate([FromBody] SgiMessageTemplateDto request)
        {
            string jsonBlock = JsonConvert.SerializeObject(request.Block, Formatting.None);
            var template = new SgiMessageTemplate
            {
                Name = request.Name,
                Receivers = request.Receivers,
                Skip_Receiver_Error = request.Skip_Receiver_Error,
                BlockJson = jsonBlock
            };

            var result = await _signetService.SaveTemplateAsync(template);
            return Ok(result);
        }


        [HttpPost("send-template")]
        public async Task<IActionResult> SendTemplateMessage([FromBody] SendTemplateMessageRequest request)
        {
            var result = await _signetService.SendTemplateMessageAsync(request);
            if (!result.Success)
                return StatusCode(400, result);

            return Ok(result);
        }


        [HttpPut("template/{name}")]
        public async Task<IActionResult> UpdateTemplate(string name, [FromBody] SgiMessageTemplateDto request)
        {
            string jsonBlock = JsonConvert.SerializeObject(request.Block, Formatting.None);
            var template = new SgiMessageTemplate
            {
                Name = request.Name,
                Receivers = request.Receivers,
                Skip_Receiver_Error = request.Skip_Receiver_Error,
                BlockJson = jsonBlock
            };

            var result = await _signetService.UpdateTemplateAsync(name, template);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<SgiMessageTemplate>.SuccessResponse(result.Data, "Cập nhật template thành công"));
        }

        #endregion


        [HttpPost("upload-excel-users")]
        public async Task<IActionResult> UploadExcelUsersAsync([FromForm] UploadFileDto dto)
        {
            try
            {
                var result = await _signetService.UploadExcelFileAsync(dto.File);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(ex.Message, 500));
            }
        }

        // SignetUserComponent CRUD Endpoints
        [HttpPost("user")]
        public async Task<IActionResult> CreateSignetUser([FromBody] SignetUserComponent user)
        {
            if (user == null)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid user data."));

            var result = await _signetService.CreateSignetUserAsync(user);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllSignetUsers()
        {
            var result = await _signetService.GetAllSignetUsersAsync();
            return Ok(ApiResponse<List<SignetUserComponent>>.SuccessResponse(result, "Lấy danh sách user thành công"));
        }

        [HttpGet("user/{userName}")]
        public async Task<IActionResult> GetSignetUserByUserName(string userName)
        {
            var result = await _signetService.GetSignetUserByUserNameAsync(userName);
            if (result == null)
                return NotFound(ApiResponse<string>.ErrorResponse("User not found."));

            return Ok(ApiResponse<SignetUserComponent>.SuccessResponse(result, "Tìm thấy user."));
        }

        [HttpGet("users/group/{group}")]
        public async Task<IActionResult> GetSignetUsersByGroup(string group)
        {
            var result = await _signetService.GetSignetUsersByGroupAsync(group);
            return Ok(ApiResponse<List<SignetUserComponent>>.SuccessResponse(result, $"Lấy danh sách user theo group {group} thành công"));
        }

        [HttpPut("user/{userName}")]
        public async Task<IActionResult> UpdateSignetUser(string userName, [FromBody] SignetUserComponent user)
        {
            if (user == null)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid user data."));

            var result = await _signetService.UpdateSignetUserAsync(userName, user);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("user/{userName}")]
        public async Task<IActionResult> DeleteSignetUser(string userName)
        {
            var result = await _signetService.DeleteSignetUserAsync(userName);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using MongoDB.Bson;
using Minio;
using Minio.Exceptions;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
// using iText.Kernel.Pdf;
// using iText.Layout;
// using iText.Layout.Element;
// using iText.IO.Image;
// using iText.IO;
// using iText.Kernel.Font;
// using iText.IO.Font.Constants;
// using QuestPDF.Fluent;
using STJ = System.Text.Json.JsonSerializer;
using Aspose.Words;
using Aspose.Words.Reporting;
using iTextDocument = iText.Layout.Document;
using AsposeDocument = Aspose.Words.Document;
using microservice.mess.Models;
using microservice.mess.Documents;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;
using microservice.mess.Documents;
using microservice.mess.Models.Message;

namespace microservice.mess.Repositories
{
    public class SignetRepository
    {
        private readonly IMongoCollection<SGIActionModel> _templateSGIAction;
        private readonly IMongoCollection<SGIUploadFileHashModel> _templateUploadFileHash;
        private readonly IMongoCollection<SignetUserComponent> _signetUsers;
        private readonly IMongoCollection<SgiMessageTemplate> _signetTemplates;
        private readonly IMongoCollection<SgiDataPdfTemplate> _signetCrawlData;
        private readonly ILogger<SignetRepository> _logger;

        private readonly IMinioClient _minioClient;
        public SignetRepository(IOptions<MongoSettings> mongoOptions, ILogger<SignetRepository> logger, IMongoClient mongoClient, IMinioClient minioClient)
        {
            _logger = logger;
            _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _templateSGIAction = db.GetCollection<SGIActionModel>("sgi_action_templates");
            _templateUploadFileHash = db.GetCollection<SGIUploadFileHashModel>("sgi_upload_file_hashes");
            _signetUsers = db.GetCollection<SignetUserComponent>("signet_users");
            _signetTemplates = db.GetCollection<SgiMessageTemplate>("signet_templates");
            _signetCrawlData = db.GetCollection<SgiDataPdfTemplate>("EXT-SOCIAL-AGI-TTXH");
        }

        #region MinIO File PDF

        // public async Task<string> UploadToMinioAsync(string filePath)
        // {
        //     string bucketName = "notify-exports";
        //     string objectName = Path.GetFileName(filePath);
        //     string contentType = "application/pdf";

        //     try
        //     {
        //         // Tạo bucket nếu chưa có
        //         bool exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        //         if (!exists)
        //         {
        //             await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        //         }

        //         // Mở stream và upload
        //         await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //         await _minioClient.PutObjectAsync(new PutObjectArgs()
        //             .WithBucket(bucketName)
        //             .WithObject(objectName)
        //             .WithStreamData(stream)
        //             .WithObjectSize(stream.Length)
        //             .WithContentType(contentType));

        //         return $"http://localhost:9000/{bucketName}/{objectName}";
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Lỗi khi upload file lên MinIO.");
        //         throw;
        //     }
        // }

        // public async Task<string> GeneratePdfWithQuestAsync()
        // {
        //     string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "templates");
        //     Directory.CreateDirectory(folderPath);

        //     string localFilePath = Path.Combine(folderPath, $"questpdf_{DateTime.Now:yyyyMMddHHmmss}.pdf");

        //     var records = await _signetCrawlData.Find(_ => true)
        //                                         .SortByDescending(x => x.Timestamp)
        //                                         .Limit(10)
        //                                         .ToListAsync();

        //     var doc = new SgiPdfDocument(records);

        //     try
        //     {
        //         // Ghi vào memory stream trước
        //         using var memoryStream = new MemoryStream();
        //         doc.GeneratePdf(memoryStream);

        //         // Ghi memory stream ra file
        //         await File.WriteAllBytesAsync(localFilePath, memoryStream.ToArray());

        //         // Upload to MinIO
        //         string fileUrl = await UploadToMinioAsync(localFilePath);

        //         if (File.Exists(localFilePath))
        //         {
        //             File.Delete(localFilePath);
        //         }

        //         return fileUrl;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogWarning(ex, "Không thể xoá file PDF sau khi upload hoặc lỗi tạo PDF: {File}", localFilePath);
        //         throw;
        //     }
        // }

        public async Task<string> GeneratePdfWithAsposeAsync()
        {
            var records = await _signetCrawlData.Find(_ => true)
                                                .SortByDescending(x => x.Timestamp)
                                                .Limit(5)
                                                .ToListAsync();

            var chuyenDeGroups = records
                .GroupBy(r => r.Message.ChuyenDe ?? "Không rõ")
                .Select(g => new ChuyenDeExportDto
                {
                    ChuyenDe = g.Key,
                    Records = g.Select(r =>
                    {
                        var props = r.Message.Properties;

                        return new RecordExportDto
                        {
                            Title = props.TryGetValue("title_s_srcha", out var titleList) && titleList?.Count > 0 ? titleList[0] : "",
                            Author = props.TryGetValue("authorName_s_srcha", out var authorList) && authorList?.Count > 0 ? authorList[0] : "",
                            CreatedAt = props.TryGetValue("createdAt_dt_srcha", out var createdList) && createdList?.Count > 0 && DateTime.TryParse(createdList[0], out var dt)
                                            ? dt.ToString("dd/MM/yyyy HH:mm")
                                            : "",
                            Content = props.TryGetValue("content_s_srcha", out var contentList) && contentList?.Count > 0 ? contentList[0] : "",
                            Link = props.TryGetValue("articleUrl_s_srcha", out var linkList) && linkList?.Count > 0 ? linkList[0] : ""

                        };
                    }).ToList()
                })
                .ToList();


            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "templates", "foreach.docx");
            var doc = new Document(templatePath);

            _logger.LogInformation("Full chuyenDeGroups: {Json}", STJ.Serialize(chuyenDeGroups));

            var engine = new ReportingEngine();
            engine.BuildReport(doc, chuyenDeGroups, "ChuyenDes");

            // Lưu ra file PDF
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "templates");
            Directory.CreateDirectory(folderPath);
            string localFilePath = Path.Combine(folderPath, $"sgi_{DateTime.Now:yyyyMMddHHmmss}.docx");

            // doc.Save(localFilePath, SaveFormat.Pdf);
            doc.Save(localFilePath, SaveFormat.Docx);
            // doc.Save("test-output.docx", SaveFormat.Docx);


            // string fileUrl = await UploadToMinioAsync(localFilePath);

            // if (File.Exists(localFilePath))
            //     File.Delete(localFilePath);

            return "123";
        }


        #endregion

        public async Task<ApiResponse<SgiMessageTemplate>> UpdateTemplateAsync(string name, SgiMessageTemplate template)
        {
            try
            {
                var filter = Builders<SgiMessageTemplate>.Filter.Eq(t => t.Name, name);
                var update = Builders<SgiMessageTemplate>.Update
                    .Set(t => t.Receivers, template.Receivers)
                    .Set(t => t.Skip_Receiver_Error, template.Skip_Receiver_Error)
                    .Set(t => t.BlockJson, template.BlockJson);

                var result = await _signetTemplates.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                {
                    return ApiResponse<SgiMessageTemplate>.ErrorResponse("No changes made", 400);
                }

                return ApiResponse<SgiMessageTemplate>.SuccessResponse(template, "Updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<SgiMessageTemplate>.ErrorResponse($"Error: {ex.Message}", 500);
            }
        }


        public async Task SaveTemplateAsync(SgiMessageTemplate template)
        {
            template.CreatedAt = DateTime.UtcNow;
            if (string.IsNullOrEmpty(template.Id))
            {
                template.Id = ObjectId.GenerateNewId().ToString();
            }
            var filter = Builders<SgiMessageTemplate>.Filter.Eq(x => x.Name, template.Name);
            await _signetTemplates.ReplaceOneAsync(filter, template, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<SgiMessageTemplate?> GetTemplateByNameAsync(string name)
        {
            var filter = Builders<SgiMessageTemplate>.Filter.Regex(
                x => x.Name,
                new MongoDB.Bson.BsonRegularExpression($"^{Regex.Escape(name)}$", "i")
            );
            return await _signetTemplates.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<SGIActionModel> GetSGIActionByValueAsync(string value)
        {
            return await _templateSGIAction.Find(x => x.Value == value).FirstOrDefaultAsync();
        }

        public async Task<SGIActionModel> GetByCmdValueAsync(string cmdValue)
        {
            return await _templateSGIAction.Find(x => x.Value == cmdValue).FirstOrDefaultAsync();
        }

        public async Task CreateSGIActionAsync(SGIActionModel model)
        {
            model.CreatedAt = DateTime.UtcNow;
            await _templateSGIAction.InsertOneAsync(model);
        }

        public async Task UploadFileHashAsync(SGIUploadFileHashModel model)
        {
            model.CreatedAt = DateTime.UtcNow;
            await _templateUploadFileHash.InsertOneAsync(model);
        }
        public async Task<List<SGIActionModel>> GetAllSGIActionsAsync()
        {
            return await _templateSGIAction.Find(_ => true).ToListAsync();
        }

        public async Task<List<SGIUploadFileHashModel>> GetAllUploadFileHashesAsync()
        {
            return await _templateUploadFileHash.Find(_ => true).ToListAsync();
        }

        public async Task<SGIUploadFileHashModel?> GetUploadFileHashByIdAsync(string id)
        {
            return await _templateUploadFileHash.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // SignetUserComponent CRUD Operations
        public async Task CreateSignetUserAsync(SignetUserComponent user)
        {
            await _signetUsers.InsertOneAsync(user);
        }

        public async Task<List<SignetUserComponent>> GetAllSignetUsersAsync()
        {
            return await _signetUsers.Find(_ => true).ToListAsync();
        }

        public async Task<SignetUserComponent?> GetSignetUserByUserNameAsync(string userName)
        {
            return await _signetUsers.Find(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<List<SignetUserComponent>> GetSignetUsersByGroupAsync(string group)
        {
            return await _signetUsers.Find(x => x.Group == group).ToListAsync();
        }

        public async Task UpdateSignetUserAsync(string userName, SignetUserComponent user)
        {
            await _signetUsers.ReplaceOneAsync(x => x.UserName == userName, user);
        }

        public async Task DeleteSignetUserAsync(string userName)
        {
            await _signetUsers.DeleteOneAsync(x => x.UserName == userName);
        }
    }
}

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
using microservice.mess.Models;

namespace microservice.mess.Repositories
{
    public class SignetRepository
    {
        private readonly IMongoCollection<SGIActionModel> _templateSGIAction;
        private readonly IMongoCollection<SGIUploadFileHashModel> _templateUploadFileHash;
        private readonly IMongoCollection<UserAccountModel> _signetUsers;
        private readonly IMongoCollection<AllMessageTemplate> _signetTemplates;
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
            _signetUsers = db.GetCollection<UserAccountModel>("all_accounts");
            _signetTemplates = db.GetCollection<AllMessageTemplate>("all_templates");
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

        #endregion

        public async Task<ApiResponse<AllMessageTemplate>> UpdateTemplateAsync(string name, AllMessageTemplate template)
        {
            try
            {
                var filter = Builders<AllMessageTemplate>.Filter.Eq(t => t.Name, name);
                var update = Builders<AllMessageTemplate>.Update
                    .Set(t => t.Receivers, template.Receivers)
                    .Set(t => t.Placeholders, template.Placeholders)
                    .Set(t => t.SkipReceiverError, template.SkipReceiverError)
                    .Set(t => t.Block, template.Block);

                var result = await _signetTemplates.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                {
                    return ApiResponse<AllMessageTemplate>.ErrorResponse("No changes made", 400);
                }

                return ApiResponse<AllMessageTemplate>.SuccessResponse(template, "Updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<AllMessageTemplate>.ErrorResponse($"Error: {ex.Message}", 500);
            }
        }


        public async Task SaveTemplateAsync(AllMessageTemplate template)
        {
            template.CreatedAt = DateTime.UtcNow;
            if (string.IsNullOrEmpty(template.Id))
            {
                template.Id = ObjectId.GenerateNewId().ToString();
            }
            var filter = Builders<AllMessageTemplate>.Filter.Eq(x => x.Name, template.Name);
            await _signetTemplates.ReplaceOneAsync(filter, template, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<AllMessageTemplate?> GetTemplateByNameAsync(string name)
        {
            var filter = Builders<AllMessageTemplate>.Filter.Regex(
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

        // UserAccountModel CRUD Operations
        public async Task CreateSignetUserAsync(UserAccountModel user)
        {
            await _signetUsers.InsertOneAsync(user);
        }

        public async Task<List<UserAccountModel>> GetAllSignetUsersAsync()
        {
            return await _signetUsers.Find(_ => true).ToListAsync();
        }

        public async Task<UserAccountModel?> GetSignetUserByUserNameAsync(string userName)
        {
            return await _signetUsers.Find(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<List<UserAccountModel>> GetSignetUsersByGroupAsync(string group)
        {
            return await _signetUsers.Find(x => x.Group == group).ToListAsync();
        }

        public async Task UpdateSignetUserAsync(string userName, UserAccountModel user)
        {
            await _signetUsers.ReplaceOneAsync(x => x.UserName == userName, user);
        }

        public async Task DeleteSignetUserAsync(string userName)
        {
            await _signetUsers.DeleteOneAsync(x => x.UserName == userName);
        }
    }
}

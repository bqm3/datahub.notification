using microservice.mess.Interfaces;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace microservice.mess.Repositories
{
    public class LogMessageRepository : ILogMessage
    {
        private readonly IMongoCollection<MessageLog> _logCollection;
        private readonly IMongoCollection<MessageErrorLog> _errorCollection;
        private readonly ILogger<LogMessageRepository> _logger;

        public LogMessageRepository(IConfiguration config, IMongoClient mongoClient,ILogger<LogMessageRepository> logger, IOptions<MongoSettings> mongoOptions)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);

            _logCollection = db.GetCollection<MessageLog>("message_logs");
            _errorCollection = db.GetCollection<MessageErrorLog>("message_error_logs");
        }


        public async Task InsertAsync(MessageLog log)
        {
            try
            {
                await _logCollection.InsertOneAsync(log);
                _logger.LogInformation("Đã ghi log message thành công: {action}", log.Action);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi ghi log message.");
            }
        }

        public async Task InsertErrorAsync(MessageErrorLog errorLog)
        {
            try
            {
                await _errorCollection.InsertOneAsync(errorLog);
                _logger.LogInformation("Đã ghi log lỗi message.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi ghi log lỗi message.");
            }
        }
    }
}

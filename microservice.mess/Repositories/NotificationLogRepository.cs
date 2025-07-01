using microservice.mess.Models;
using MongoDB.Driver;

namespace microservice.mess.Repositories
{
    public class NotificationLogRepository
    {
        private readonly IMongoCollection<NotificationLog> _collection;
        

        public NotificationLogRepository(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase("messaging");
            _collection = db.GetCollection<NotificationLog>("notification_logs");
        }

        public async Task InsertLogAsync(NotificationLog log)
        {
            await _collection.InsertOneAsync(log);
        }
    }
}

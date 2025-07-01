using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Configurations;

namespace microservice.mess.Repositories
{
    public class ZaloEventRepository
    {
        private readonly IMongoCollection<ZaloEvent> _collection;

        public ZaloEventRepository(IMongoClient mongoClient, IOptions<MongoSettings> mongoOptions)
        {
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _collection = db.GetCollection<ZaloEvent>("events");
        }

        public async Task InsertEventAsync(ZaloEvent ev)
        {
            await _collection.InsertOneAsync(ev);
        }
    }
}

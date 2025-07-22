using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class ScheduledAllRepository
    {
        private readonly IMongoCollection<ScheduledAllModel> _collection;

        public ScheduledAllRepository(IMongoClient client, IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var db = client.GetDatabase(settings.ZaloDatabase);
            _collection = db.GetCollection<ScheduledAllModel>("all_scheduled");
        }

        public async Task<List<ScheduledAllModel>> GetDueSchedulesAsync(DateTime now)
        {
            var filter = Builders<ScheduledAllModel>.Filter.Or(
                Builders<ScheduledAllModel>.Filter.And(
                    Builders<ScheduledAllModel>.Filter.Eq(e => e.Recurrence, "ONCE"),
                    Builders<ScheduledAllModel>.Filter.Eq(e => e.IsSent, false),
                    Builders<ScheduledAllModel>.Filter.Lte(e => e.ScheduledTime, now)
                ),
                Builders<ScheduledAllModel>.Filter.In(e => e.Recurrence, new[] {
                    "DAILY",
                    "WEEKLY",
                    "CUSTOM"
                })
            );

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(string id, ScheduledAllModel model)
        {
            model.Id = id;
            var filter = Builders<ScheduledAllModel>.Filter.Eq(e => e.Id, id);
            await _collection.ReplaceOneAsync(filter, model);
        }

        public async Task<List<ScheduledAllModel>> GetAllScheduledAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task MarkAsSentAsync(string id)
        {
            var update = Builders<ScheduledAllModel>.Update.Set(x => x.IsSent, true);
            await _collection.UpdateOneAsync(x => x.Id == id, update);
        }

        public async Task AddOrUpdateAsync(ScheduledAllModel model)
        {
            var existing = await _collection.Find(x => x.Name == model.Name).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new InvalidOperationException($"Tên lịch '{model.Name}' đã tồn tại.");
            }

            await _collection.InsertOneAsync(model);
        }

    }

}
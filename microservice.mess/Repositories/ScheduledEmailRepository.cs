using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using microservice.mess.Models;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class ScheduledEmailRepository
    {
        private readonly IMongoCollection<ScheduledEmailModel> _collection;

        public ScheduledEmailRepository(IMongoClient client, IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var db = client.GetDatabase(settings.ZaloDatabase);
            _collection = db.GetCollection<ScheduledEmailModel>("email_scheduled");
        }

        public async Task<List<ScheduledEmailModel>> GetDueEmailsAsync(DateTime now)
        {
            var filter = Builders<ScheduledEmailModel>.Filter.Or(
                Builders<ScheduledEmailModel>.Filter.And(
                    Builders<ScheduledEmailModel>.Filter.Eq(e => e.Recurrence, RecurrenceType.Once),
                    Builders<ScheduledEmailModel>.Filter.Eq(e => e.IsSent, false),
                    Builders<ScheduledEmailModel>.Filter.Lte(e => e.ScheduledTime, now)
                ),
                Builders<ScheduledEmailModel>.Filter.In(e => e.Recurrence, new[] {
                    RecurrenceType.Daily,
                    RecurrenceType.Weekly,
                    RecurrenceType.Custom
                })
            );

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(string id, ScheduledEmailModel model)
        {
            model.Id = id;
            var filter = Builders<ScheduledEmailModel>.Filter.Eq(e => e.Id, id);
            await _collection.ReplaceOneAsync(filter, model);
        }



        public async Task<List<ScheduledEmailModel>> GetAllScheduledEmailsAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task MarkAsSentAsync(string id)
        {
            var update = Builders<ScheduledEmailModel>.Update.Set(x => x.IsSent, true);
            await _collection.UpdateOneAsync(x => x.Id == id, update);
        }

        public async Task AddAsync(ScheduledEmailModel model)
            => await _collection.InsertOneAsync(model);
    }

}
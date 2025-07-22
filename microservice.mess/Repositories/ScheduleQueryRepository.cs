using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class ScheduleQueryRepository
    {
        private readonly IMongoCollection<AllMessageTemplate> _allTemplated;
        private readonly IMongoCollection<DataScheduleModel> _dataScheduled;
        private readonly ILogger<ScheduleQueryRepository> _logger;

        public ScheduleQueryRepository(IOptions<MongoSettings> mongoOptions, ILogger<ScheduleQueryRepository> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _allTemplated = db.GetCollection<AllMessageTemplate>("all_templates");
            _dataScheduled = db.GetCollection<DataScheduleModel>("data_scheduled");
        }

        public async Task<List<DataScheduleModel?>> GetBySchdeduleAsync(string objID)
        {
            return await _dataScheduled.Find(x => x.ObjID == objID).ToListAsync();
        }

        public async Task<List<DataScheduleModel>> GetAllScheduledAsync()
        {
            return await _dataScheduled.Find(_ => true).ToListAsync();
        }

        public async Task CreateDataScheduleAsync(DataScheduleModel model)
        {
            await _dataScheduled.InsertOneAsync(model);
        }

        // public async Task<List<AllMessageTemplate>> GetAllAsync()
        // {
        //     return await _templates.Find(_ => true).ToListAsync();
        // }

        public async Task<AllMessageTemplate?> GetByNameAsync(string name)
        {
            var filter = Builders<AllMessageTemplate>.Filter.Regex("Name", new BsonRegularExpression($"^{Regex.Escape(name)}$", "i"));
            return await _allTemplated.Find(filter).FirstOrDefaultAsync();
        }
    }
}
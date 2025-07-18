using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class ScheduleQueryRepository
    {
        private readonly IMongoCollection<AllMessageTemplate> _allScheduled;
        private readonly IMongoCollection<DataScheduleModel> _dataScheduled;
        private readonly ILogger<ScheduleQueryRepository> _logger;

        public ScheduleQueryRepository(IOptions<MongoSettings> mongoOptions, ILogger<ScheduleQueryRepository> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _allScheduled = db.GetCollection<AllMessageTemplate>("all_scheduled");
            _dataScheduled= db.GetCollection<DataScheduleModel>("data_scheduled");
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

        // public async Task<AllMessageTemplate?> GetByNameAsync(string name)
        // {
        //     return await _templates.Find(t => t.Name == name).FirstOrDefaultAsync();
        // }
    }
}
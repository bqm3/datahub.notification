using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class ScheduleFieldQueryRepository : IScheduleFieldQueryRepository
    {
        private readonly IMongoCollection<DataScheduleModel> _collection;

        public ScheduleFieldQueryRepository(IMongoClient client, IOptions<MongoSettings> options)
        {
            var db = client.GetDatabase(options.Value.ZaloDatabase);
            _collection = db.GetCollection<DataScheduleModel>("data_scheduled");
        }

        public async Task<List<DataScheduleModel>> GetByObjIdAsync(string objId)
        {
            return await _collection.Find(x => x.ObjID == objId).ToListAsync();
        }
    }

}
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Configurations;
namespace microservice.mess.Repositories
{
    public class ZaloMemberRepository
    {
        private readonly IMongoCollection<GroupMember> _collection;

        public ZaloMemberRepository(IMongoClient client, IOptions<MongoSettings> mongoOptions)
        {
            var settings = mongoOptions.Value;
            var database = client.GetDatabase(settings.ZaloDatabase);
            _collection = database.GetCollection<GroupMember>("zalo_members");
        }

        public async Task UpsertUserAsync(string userId, string status, string eventName)
        {
            var filter = Builders<GroupMember>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<GroupMember>.Update
                .Set(u => u.Status, status)
                .Set(u => u.EventName, eventName)
                .Set(u => u.LastUpdated, DateTime.UtcNow);

            await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task<List<string>> ListUserAsync()
        {
            var filter = Builders<GroupMember>.Filter.Empty; // Lấy tất cả
            var projection = Builders<GroupMember>.Projection.Include(x => x.UserId).Exclude("_id");

            var users = await _collection
                .Find(filter)
                .Project<GroupMember>(projection)
                .ToListAsync();

            return users.Select(u => u.UserId).ToList();
        }


    }
}
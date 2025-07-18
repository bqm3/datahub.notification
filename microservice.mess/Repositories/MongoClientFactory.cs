using microservice.mess.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace microservice.mess.Repositories
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IMongoClient _mongoClient;

        public MongoClientFactory(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IMongoDatabase GetDatabase(string source)
        {
            return _mongoClient.GetDatabase(source);
        }
    }

}
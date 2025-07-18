using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace microservice.mess.Interfaces
{
    public interface IMongoClientFactory
    {
        IMongoDatabase GetDatabase(string source);
    }

}
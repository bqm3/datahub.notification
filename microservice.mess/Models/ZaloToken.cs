using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace microservice.mess.Models
{
    public class ZaloToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OAID { get; set; }            // Chính là oa_id
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}

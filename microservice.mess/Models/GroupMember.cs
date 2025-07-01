using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models
{
    public class GroupMember
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("event_name")]
        public string EventName { get; set; }

        [BsonElement("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}

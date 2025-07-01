using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models
{
    public class NotificationLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }
        public string Message { get; set; } = "";
        public string Channel { get; set; } = ""; // signalR, zalo, email
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

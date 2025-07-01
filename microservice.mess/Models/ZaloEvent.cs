using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models
{
    public class ZaloEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string OAId { get; set; }           // ID của OA (sender)
        public string UserId { get; set; }         // ID người dùng Zalo
        public string Event { get; set; }          // Tên sự kiện (message, follow,...)
        public string MessageType { get; set; }    // text, image, file, sticker,...
        public string MessageContent { get; set; } // Nội dung tin nhắn hoặc raw JSON
        public DateTime Timestamp { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models
{
    public class MailPayload
    {
        public List<string> To { get; set; } = new();
        public string Subject { get; set; } = string.Empty;
        public string BodyHtml { get; set; } = string.Empty;
        public string? From { get; set; } = null; // optional
    }

    public class SendMailByNameRequest
    {
        public List<string> To { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> Data { get; set; } = new(); // để bind vào template
        public string? From { get; set; }
        public string? Subject { get; set; } // có thể override
        public List<EmailAttachment>? Attachments { get; set; }
    }

    public class EmailAttachment
    {
        public string FileName { get; set; } = string.Empty;        // Tên file hiển thị
        public string ContentBase64 { get; set; } = string.Empty;   // Nội dung file dưới dạng base64
        public string MimeType { get; set; } = "application/pdf";   // Kiểu MIME
    }

    public enum RecurrenceType
    {
        Once,       // Gửi 1 lần
        Daily,      // Gửi mỗi ngày
        Weekly,     // Gửi mỗi tuần
        Hourly,      // Gửi theo giờ
        Custom
    }
    public class ScheduledEmailModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<string> To { get; set; } = new();
        public Dictionary<string, string> Data { get; set; } = new();
        public string? Subject { get; set; }
        public string? From { get; set; }
        public DateTime ScheduledTime { get; set; }  // Thời gian lần đầu gửi
        public bool IsSent { get; set; } = false;
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.Once;

        public List<DayOfWeek>? DaysOfWeek { get; set; } = null; // Với Weekly hoặc Custom
        public List<TimeSpan>? TimesOfDay { get; set; } = null;  // Giờ cụ thể trong ngày

        public DateTime? LastSentAt { get; set; }    // Thời gian gửi gần nhất
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
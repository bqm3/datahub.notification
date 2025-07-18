using microservice.mess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace microservice.mess.Models.Message
{
    public class MessageRequest
    {
        public MessageHeaders? Headers { get; set; } = new();
        public List<MessageBodyItem>? Body { get; set; } = new();
    }

    public class MessageHeaders
    {
        public string Action { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }

    public class MessageBodyItem
    {
        public SendMailByNameRequest? Email { get; set; }
        public SignalRPayload? SignalR { get; set; }
        public SlackPayload? Slack { get; set; }
        public ZaloPayload? Zalo { get; set; }
        public SendTemplateMessageRequest? Signet { get; set; }
        // Các payload khác như SMS, Signet...
    }


    public class SignalRPayload
    {
        public string Message { get; set; } = string.Empty;
    }
    public class SlackPayload
    {
        public string Message { get; set; } = string.Empty;
        // public string Text { get; set; } = string.Empty; 
    }
    public class ZaloPayload
    {
        public string AccessToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? Tag { get; set; }

        // Gửi template promotion
        public ZaloSendMessagePayload? SendMessagePayload { get; set; }

        // Tạo chiến dịch khuyến mãi
        public ZaloPromotionRequest? PromotionRequest { get; set; }

        // Dữ liệu Webhook callback từ Zalo
        public ZaloWebhookRequest? WebhookRequest { get; set; }

        // Sự kiện realtime từ Zalo
        public ZaloEvent? Event { get; set; }

        // Token của OA
        public ZaloToken? Token { get; set; }

        // Callback OAuth2
        public ZaloCallbackRequest? CallbackRequest { get; set; }
    }


    public class MessageLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Action { get; set; }
        public string Channel { get; set; }
        public string Payload { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class MessageErrorLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RawPayload { get; set; }
        public string Error { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    // Template
    public class AllMessageTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty; // cho email
        public string BodyHtml { get; set; } = string.Empty; // cho email hoặc mô tả
        public string LogoUrl { get; set; } = string.Empty; // optional
        public List<string> Placeholders { get; set; } = new();
        public bool SkipReceiverError { get; set; } = false;
        public List<string> Receivers { get; set; } = new();
        public string Block { get; set; } = "{}";
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


    // Account model
    public class UserAccountModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        // Thông tin người dùng
        public string UserName { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public string? Group { get; set; }
        public string? DisplayName { get; set; }

        [BsonRequired]
        public string? FromEmail { get; set; }

        [BsonRequired]
        public string? Password { get; set; }

        public string SmtpClient { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
    }

    // Field Data Query model
    public class DataScheduleModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string ObjID { get; set; }
        public string? Name { get; set; }
        public string? Key { get; set; }
        public string? FieldData { get; set; }
        public string? QueryType { get; set; }
        public string? Query { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class ScheduleContext
    {
        // public JObject RawData { get; set; }
        // public string? FilePath { get; set; }
        // public string? FileHash { get; set; }
        // public string? FileValue { get; set; }
        // public string? Channel { get; set; }
        // public string? TemplateName { get; set; }
        // public JObject Config { get; set; } // chứa JSON schedule
        // public Dictionary<string, object> Items { get; } = new(); // cho các step custom gắn thêm data
        public ScheduledAllModel Schedule { get; set; } = default!;
        public ChannelType Channel { get; set; }
        public Dictionary<string, object> Items { get; } = new(); // cho dữ liệu giữa các step
        public IServiceProvider Services { get; set; } = default!;
    }


    public class ScheduledAllModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<ChannelType> Channels { get; set; } = new();

        public Dictionary<string, List<string>> Steps { get; set; } = new();

        public SharedConfig Shared { get; set; } = new();

        public List<string> To { get; set; } = new();

        public DataConfig Data { get; set; } = new();

        public string? Subject { get; set; }

        public string? From { get; set; }

        public DateTime ScheduledTime { get; set; }

        public bool IsSent { get; set; } = false;

        public RecurrenceType Recurrence { get; set; } = RecurrenceType.Once;

        public List<DayOfWeek>? DaysOfWeek { get; set; }

        public List<TimeSpan>? TimesOfDay { get; set; }

        public Dictionary<string, bool> ChannelStatus { get; set; } = new();

        public DateTime? LastSentAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class SharedConfig
    {
        public QueryDataModel QueryData { get; set; } = new();
    }


    public class DataConfig
    {
        public string? DataMode { get; set; } = "static";        // db_query, api,...
        public string? DataSource { get; set; }                 // SGI_DB
        public string? DataApiUrl { get; set; }                 // nếu gọi API
        public string? DataSourceType { get; set; }             // mongo, sql
        public string? DataSourceKey { get; set; }              // EXT-SOCIAL-AGI-TTXH
        public string? DataQuery { get; set; }                  // new-one-day
        public bool? IsRealtime { get; set; } = false;
        public string? File { get; set; }
        public string? OutFile { get; set; }
        public bool? DataJson { get; set; } = false;

        [BsonExtraElements]
        public BsonDocument? ExtraElements { get; set; }  // giữ lại các trường lạ
    }

    public class QueryDataModel
{
    public string Type { get; set; } = "mongo";
    public string Source { get; set; } = string.Empty;
    public string Collection { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public string File { get; set; }
}


    public class ShareSocial
    {
        public string ARTICLE_ID { get; set; }
        public string ARTICLE_URL { get; set; }
        public string AUTHOR_ID { get; set; }
        public string AUTHOR_NAME { get; set; }
        public string CATALOG_ID { get; set; }
        public string CHANNEL_NAME { get; set; }
        public string CONTENT { get; set; }
        public string LOCATION { get; set; }
        public string TITLE { get; set; }
        public int SENTIMENT { get; set; }
        public string IMAGE { get; set; }
        public DateTime CREATED_AT { get; set; }
    }

    public enum ChannelType
    {
        Email,
        Zalo,
        Signet,
        SignalR,
        Slack
    }

}

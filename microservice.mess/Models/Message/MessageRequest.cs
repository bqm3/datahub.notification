using microservice.mess.Models;
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
        public SendMailByTagRequest? Email { get; set; }
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

}

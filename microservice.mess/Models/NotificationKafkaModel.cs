using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models
{
    public class NotificationKafkaEnvelope
    {
        public string Action { get; set; }
        public string MessageType { get; set; }
        public string CreatedAt { get; set; }
        public string Payload { get; set; }
    }

    public class ZaloCallbackRequest
    {
        public string Code { get; set; }
        public string State { get; set; }
        public string CodeVerifier { get; set; }
    }

    public class ZaloPromotionRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string? AccessToken { get; set; }
        public string Tag { get; set; }
        public List<ZaloElement> Elements { get; set; } = new();
        public List<ZaloButton> Buttons { get; set; } = new();
    }

    public class ZaloPromotionBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("tag")]
        public string? Tag { get; set; }

        [BsonElement("elements")]
        public List<ZaloElementBson> Elements { get; set; } = new();

        [BsonElement("buttons")]
        public List<ZaloButtonBson> Buttons { get; set; } = new();
    }


    public class ZaloEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string OAId { get; set; }           // ID của OA (sender)
        public string UserId { get; set; }         // ID người dùng Zalo
        public string Event { get; set; }
        public string MessageType { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
    }
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

    public class ZaloButtonBson
    {
        // [BsonElement("title")]
        public string Title { get; set; }

        // [BsonElement("image_icon")]
        public string ImageIcon { get; set; }

        // [BsonElement("type")]
        public string Type { get; set; }

        // [BsonElement("payload")]
        public string Payload { get; set; } // dạng JSON string
    }
    public class ZaloElement
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [BsonElement("attachmentId")]
        [JsonProperty("attachmentId", NullValueHandling = NullValueHandling.Ignore)]
        public string? AttachmentId { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string? Content { get; set; }

        [JsonProperty("align", NullValueHandling = NullValueHandling.Ignore)]
        public string? Align { get; set; }

        [JsonProperty("contentTable", NullValueHandling = NullValueHandling.Ignore)]
        public List<ZaloTableRow>? ContentTable { get; set; }
    }

    public class ZaloElementBson
    {
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [BsonElement("attachment_id")]
        public string? AttachmentId { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("align")]
        public string? Align { get; set; }

        [BsonElement("contentTable")]
        public List<ZaloTableContent>? ContentTable { get; set; }
    }

    public class ZaloTableContent
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

    public class ZaloTableRow
    {
        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }
    }

    public class ZaloButton
    {
        public string Title { get; set; }

        public string ImageIcon { get; set; } = "default";

        public string Type { get; set; }

        public object Payload { get; set; }

        // Chuyển Payload về chuỗi JSON để lưu vào Bson
        public string ToBsonPayload()
        {
            return JsonConvert.SerializeObject(this.Payload);
        }

        // Load lại Payload từ chuỗi JSON trong Bson
        public void LoadFromBsonPayload(string payloadJson)
        {
            if (string.IsNullOrEmpty(payloadJson)) return;

            try
            {
                this.Payload = JsonConvert.DeserializeObject<object>(payloadJson);
            }
            catch (Exception ex)
            {
                // Log nếu cần thiết
                this.Payload = null;
            }
        }
    }

    public class ZaloMember
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

    public class ZaloSendMessagePayload
    {
        [JsonProperty("recipient")]
        public ZaloRecipient Recipient { get; set; }

        [JsonProperty("message")]
        public ZaloMessage Message { get; set; }
    }

    public class ZaloRecipient
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public class ZaloMessage
    {
        [JsonProperty("attachment")]
        public ZaloAttachment Attachment { get; set; }
    }

    public class ZaloAttachment
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "template";

        [JsonProperty("payload")]
        public ZaloAttachmentPayload Payload { get; set; }
    }

    public class ZaloAttachmentPayload
    {
        [JsonProperty("template_type")]
        public string TemplateType { get; set; } = "promotion";

        [JsonProperty("elements")]
        public List<object> Elements { get; set; }

        [JsonProperty("buttons")]
        public List<ZaloButton> Buttons { get; set; }
    }

    public class ZaloPromotionCreateRequest
    {
        public ZaloPromotionRequest Promotion { get; set; }
    }

    public class ZaloPromotionGetRequest
    {
        public string Id { get; set; }
    }

    public class ZaloPromotionUpdateRequest
    {
        public string Id { get; set; }
        public ZaloPromotionRequest Promotion { get; set; }
    }

    public class ZaloPromotionDeleteRequest
    {
        public string Id { get; set; }
    }

    public class ZaloWebhookRequest
    {
        public string EventName { get; set; }
        public Dictionary<string, object> Sender { get; set; }
        public Dictionary<string, object> Recipient { get; set; }
        public Dictionary<string, object> Follower { get; set; }
        public Dictionary<string, object> Message { get; set; }
    }
}

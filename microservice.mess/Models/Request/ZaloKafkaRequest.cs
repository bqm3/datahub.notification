using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Models.Request
{
    public class ZaloKafkaRequest
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string Header { get; set; }
        public List<string> Texts { get; set; }
        public List<ZaloButton> Buttons { get; set; }
        public List<string> BannerAttachmentIds { get; set; }
    }
}
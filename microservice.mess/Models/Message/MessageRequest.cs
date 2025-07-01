

namespace microservice.mess.Models.Message
{
    public class MessageRequest
    {
        public List<string> Channels { get; set; } = new();
        public string Action { get; set; } 
        public string Payload { get; set; } = string.Empty;
    }

}
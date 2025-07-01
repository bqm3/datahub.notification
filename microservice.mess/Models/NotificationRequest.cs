namespace microservice.mess.Models
{
    public class NotificationRequest
    {
        public string? UserId { get; set; }         
        public string Message { get; set; } = "";
    }
}

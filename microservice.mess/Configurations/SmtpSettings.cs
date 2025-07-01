namespace microservice.mess.Configurations
{
    public class SmtpSettings
    {
        public string StmpClient { get; set; } = "";
        public int Port { get; set; }
        public string From { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

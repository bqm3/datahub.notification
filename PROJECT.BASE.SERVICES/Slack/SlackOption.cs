namespace PROJECT.BASE.SERVICES
{
    public class SlackOption
    {
        public const string Key = "SlaskOptions";
        public string BaseUrl { get; set; }
        public string SendMessageEndpoint { get; set; }
    }
}
namespace PROJECT.BASE.SERVICES
{
    public class SmsOption
    {
        public const string Key = "SmsOptions";
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string AccountBalanceEndpoint { get; set; }
        public string SendSmsEndpoint { get; set; }
        public string BranchName { get; set; }
        public string SmsType { get; set; }
    }
    public class SmsOtpOption
    {
        public const string Key = "SmsOtpOptions";
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string AccountBalanceEndpoint { get; set; }
        public string SendSmsEndpoint { get; set; }
        public string BranchName { get; set; }
        public string SmsType { get; set; }
    }
}
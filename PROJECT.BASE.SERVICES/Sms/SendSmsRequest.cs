namespace PROJECT.BASE.SERVICES
{
    public class SendSmsRequest
    {
        public string ApiKey { get; set;}
        public string Content { get; set;}
        public string Phone { get; set;}
        public string SecretKey { get; set;}
        public string IsUnicode { get; set;}
        public string Brandname { get; set;}
        public string SmsType { get; set;}
        public string RequestId { get; set;}
        public string CallbackUrl { get; set;}
        public string campaignid { get; set;}
        public int SmsContentType { get; set; }
    }
}
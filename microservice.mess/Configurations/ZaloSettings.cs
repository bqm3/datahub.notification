namespace microservice.mess.Configurations
{
    public class ZaloSettings
    {
        public string app_id { get; set; } = "";
        public string oa_id { get; set; } = "";
        public string redirect_uri { get; set; }
        public string secret_key { get; set; } = "";
        public string grant_type { get; set; } = "";
        public string state { get; set; } = "";
        public string code_verifier { get; set; } = "";
        public string code_challenge { get; set; } = "";
    }
}

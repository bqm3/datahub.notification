namespace microservice.mess.Models
{
    public class ZaloTokenResponse
    {
        public int error { get; set; } 
        public string? error_name { get; set; }
        public string? message { get; set; }
        public string? error_reason { get; set; }
        public string? error_description { get; set; }
        public string? ref_doc { get; set; }

        public string? access_token { get; set; }
        public string? refresh_token { get; set; }
        public string? expires_in { get; set; }
        public string? oa_id { get; set; }
    }



}

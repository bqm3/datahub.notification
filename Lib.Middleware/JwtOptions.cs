using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Lib.Middleware
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class JwtToken
    {
        [DataMember]
        [DisplayName("access_token")]
        public string access_token { get; set; }
        [DataMember]
        [DisplayName("expires_in")]
        public int? expires_in { get; set; }
        [DataMember]
        [DisplayName("refresh_expires_in")]
        public int? refresh_expires_in { get; set; }
        [DataMember]
        [DisplayName("refresh_token")]
        public string refresh_token { get; set; }
        [DataMember]
        [DisplayName("token_type")]
        public string token_type { get; set; }
        [DataMember]
        [DisplayName("id_token")]
        public string id_token { get; set; }
        [DataMember]
        [DisplayName("session_state")]
        public string session_state { get; set; }
        [DataMember]
        [DisplayName("scope")]
        public string scope { get; set; }


    }

    [Serializable]
    [DataContract]
    public partial class JwtRefreshToken
    {

        [DataMember]
        [DisplayName("client_id")]
        public string client_id { get; set; }
        [DataMember]
        [DisplayName("client_secret")]
        public string client_secret { get; set; }
        [DataMember]
        [DisplayName("refresh_token")]
        public string refresh_token { get; set; }
    }
}

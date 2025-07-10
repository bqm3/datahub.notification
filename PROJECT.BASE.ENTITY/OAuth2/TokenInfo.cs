using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class TokenInfo
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
        [DisplayName("user_id")]
        public string user_id { get; set; }
        [DataMember]
        [DisplayName("session_state")]
        public string session_state { get; set; }
        [DataMember]
        [DisplayName("scope")]
        public string scope { get; set; }
        [DataMember]
        [DisplayName("token_api")]
        public string token_api { get; set; }

    }
 
}

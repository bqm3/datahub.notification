using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class OAuth2ProviderInfo
    {
        [DataMember]
        [DisplayName("ClientId")]
        public string ClientId { get; set; }
        [DataMember]
        [DisplayName("ClientSecret")]
        public string ClientSecret { get; set; }
        [DataMember]
        [DisplayName("AuthUri")]
        public string AuthUri { get; set; }
        [DataMember]
        [DisplayName("AccessTokenUri")]
        public string AccessTokenUri { get; set; }
        [DataMember]
        [DisplayName("UserInfoUri")]
        public string UserInfoUri { get; set; }
        [DataMember]
        [DisplayName("Scope")]
        public string Scope { get; set; }
        [DataMember]
        [DisplayName("State")]
        public string State { get; set; }        
        public bool Offline = false;
    }
}

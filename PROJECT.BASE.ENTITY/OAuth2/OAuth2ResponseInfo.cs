using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class OAuth2ResponseInfo
    {
        [DataMember]
        [DisplayName("AccessToken")]
        public string AccessToken { get; set; }
        [DataMember]
        [DisplayName("RefreshToken")]
        public string RefreshToken { get; set; }
        [DataMember]
        [DisplayName("Expires")]
        public DateTime? Expires { get; set; }
        [DataMember]
        [DisplayName("State")]
        public string State { get; set; }
        [DataMember]
        [DisplayName("State")]
        public UserInfo SessionInfo { get; set; }
    }
}

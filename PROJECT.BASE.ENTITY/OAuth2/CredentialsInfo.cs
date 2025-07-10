using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class CredentialsInfo
    {
        [DataMember]
        [DisplayName("type")]
        public string type { get; set; }
        [DataMember]
        [DisplayName("value")]
        public string value { get; set; }
        [DataMember]
        [DisplayName("temporary")]
        public bool? temporary { get; set; }
    }
}

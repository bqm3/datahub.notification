using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class RoleInfo
    {
        [DataMember]
        [DisplayName("id")]
        public string id { get; set; }
        [DataMember]
        [DisplayName("name")]
        public string name { get; set; }
        [DataMember]
        [DisplayName("description")]
        public string description { get; set; }
        [DataMember]
        [DisplayName("composite")]
        public bool? composite { get; set; }
        [DataMember]
        [DisplayName("clientRole")]
        public bool? clientRole { get; set; }
        [DataMember]
        [DisplayName("containerId")]
        public string containerId { get; set; }
        [DataMember]
        [DisplayName("attributes")]
        public Dictionary<string, string[]> attributes { get; set; }
    }
}
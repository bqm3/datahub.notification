using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_AUDITInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }
        [DataMember]
        [DisplayName("APPLICATION")]
        public string APPLICATION { get; set; }
        [DataMember]
        [DisplayName("MODULE")]
        public string MODULE { get; set; }
        [DataMember]
        [DisplayName("FUNCTION")]
        public string FUNCTION { get; set; }
        [DataMember]
        [DisplayName("FIELD ")]
        public string FIELD { get; set; }
        [DataMember]
        [DisplayName("VALUE_OLD ")]
        public string VALUE_OLD { get; set; }
        [DataMember]
        [DisplayName("VALUE_NEW ")]
        public string VALUE_NEW { get; set; }
        [DataMember]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
        [DataMember]
        [DisplayName("CDATE")]
        public long? CDATE { get; set; }
        [DataMember]
        [DisplayName("LUSER")]
        public string LUSER { get; set; }
        [DataMember]
        [DisplayName("LDATE")]
        public long? LDATE { get; set; }

    }
    [Serializable]
    [DataContract]
    public partial class AuditInfo
    {
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }
        [DataMember]
        [DisplayName("APPLICATION")]
        public string APPLICATION { get; set; }
        [DataMember]
        [DisplayName("MODULE")]
        public string MODULE { get; set; }
        [DataMember]
        [DisplayName("FUNCTION")]
        public string FUNCTION { get; set; }
        [DataMember]
        [DisplayName("OBJECT_OLD")]
        public Dictionary<string,string> OBJECT_OLD { get; set; }
        [DataMember]
        [DisplayName("OBJECT_NEW")]
        public Dictionary<string, string> OBJECT_NEW { get; set; }

    }
    [Serializable]
    [DataContract]
    public partial class FIELDInfo
    {        
        [DataMember]
        [DisplayName("FIELD ")]
        public string FIELD { get; set; }
        [DataMember]
        [DisplayName("VALUE_OLD ")]
        public string VALUE_OLD { get; set; }
        [DataMember]
        [DisplayName("VALUE_NEW ")]
        public string VALUE_NEW { get; set; }

    }
}

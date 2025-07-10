using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_LOGINInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }        
        [DataMember]
        [DisplayName("REALMS")]
        public string REALMS { get; set; }
        [DataMember]
        [DisplayName("IP")]
        public string IP { get; set; }
        [DataMember]
        [DisplayName("COUNTRY")]
        public string COUNTRY { get; set; }
        [DataMember]
        [DisplayName("USER_AGENT")]
        public string USER_AGENT { get; set; }        
        [DataMember]
        [DisplayName("CLIENT_ID")]
        public string CLIENT_ID { get; set; }
        [DataMember]
        [DisplayName("USER_ID")]
        public string USER_ID { get; set; }
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }
        [DataMember]
        [DisplayName("AUTH_TIME")]
        public long? AUTH_TIME { get; set; }
        [DataMember]
        [DisplayName("EXP_TIME")]
        public long? EXP_TIME { get; set; }
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
    public partial class LogLoginInfo
    {        
        [DataMember]
        [DisplayName("REALMS")]
        public string REALMS { get; set; }
        [DataMember]
        [DisplayName("IP")]
        public string IP { get; set; }
        [DataMember]
        [DisplayName("COUNTRY")]
        public string COUNTRY { get; set; }
        [DataMember]
        [DisplayName("USER_AGENT")]
        public string USER_AGENT { get; set; }
        [DataMember]
        [DisplayName("CLIENT_ID")]
        public string CLIENT_ID { get; set; }
        [DataMember]
        [DisplayName("USER_ID")]
        public string USER_ID { get; set; }
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }

    }

}

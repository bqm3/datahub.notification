using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_VERSIONInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }        
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("VERSION")]
        public string VERSION { get; set; }    
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
    public partial class LOG_VERSION
    {
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("VERSION")]
        public string VERSION { get; set; }

    }

}

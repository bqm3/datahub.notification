using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_TRANSACTIONInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("APP_CODE")]
        public string APP_CODE { get; set; }        
        [DataMember]
        [DisplayName("TRANSACTION_CODE")]
        public string TRANSACTION_CODE { get; set; }
        [DataMember]
        [DisplayName("TRANSACTION_NAME")]
        public string TRANSACTION_NAME { get; set; }
        [DataMember]
        [DisplayName("STEP_CODE")]
        public string STEP_CODE { get; set; }
        [DataMember]
        [DisplayName("STEP_NAME")]
        public string STEP_NAME { get; set; }
        [DataMember]
        [DisplayName("TYPE_NAME")]
        public string TYPE_NAME { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public string STATUS { get; set; }
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
    public partial class TRANSACTIONInfo
    {
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("APP_CODE")]
        public string APP_CODE { get; set; }
        [DataMember]
        [DisplayName("TRANSACTION_CODE")]
        public string TRANSACTION_CODE { get; set; }
        [DataMember]
        [DisplayName("TRANSACTION_NAME")]
        public string TRANSACTION_NAME { get; set; }
        [DataMember]
        [DisplayName("STEP_CODE")]
        public string STEP_CODE { get; set; }
        [DataMember]
        [DisplayName("STEP_NAME")]
        public string STEP_NAME { get; set; }
        [DataMember]
        [DisplayName("TYPE_NAME")]
        public string TYPE_NAME { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public string STATUS { get; set; }
        [DataMember]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
    }
}

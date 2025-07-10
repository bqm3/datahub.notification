using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_UPLOADInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("FILE_ID")]
        public string FILE_ID { get; set; }
        [DataMember]
        [DisplayName("HASH_CODE ")]
        public string HASH_CODE { get; set; }
        [DataMember]
        [DisplayName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        [DataMember]
        [DisplayName("EXTENSION")]
        public string EXTENSION { get; set; }
        [DataMember]
        [DisplayName("DESCRIPTION ")]
        public string DESCRIPTION { get; set; }
        [DataMember]
        [DisplayName("SOURCE ")]
        public string SOURCE { get; set; }
        [DataMember]
        [DisplayName("SIZE ")]
        public int? SIZE { get; set; }        
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
    public partial class UploadInfo
    {
        [DataMember]
        [DisplayName("FILE_ID")]
        public string FILE_ID { get; set; }
        [DataMember]
        [DisplayName("HASH_CODE ")]
        public string HASH_CODE { get; set; }
        [DataMember]
        [DisplayName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        [DataMember]
        [DisplayName("EXTENSION")]
        public string EXTENSION { get; set; }
        [DataMember]
        [DisplayName("DESCRIPTION ")]
        public string DESCRIPTION { get; set; }
        [DataMember]
        [DisplayName("SOURCE ")]
        public string SOURCE { get; set; }
        [DataMember]
        [DisplayName("VALUE_NEW ")]
        public int? SIZE { get; set; }

    }
}

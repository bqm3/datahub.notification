using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public class SendSMS
    {
        [DataMember]
        [DisplayName("ToPhone")]
        public string ToPhone { get; set; }
        
        [DataMember]
        [DisplayName("SmsContentType")]
        public int SmsContentType { get; set; }

        [DataMember]
        [DisplayName("Parameters")]
        public Dictionary<string,string> Parameters { get; set; }

    }
    public class SEND_SMSInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("TO_PHONE")]
        public string TO_PHONE { get; set; }
        [DataMember]
        [DisplayName("MESSAGE")]
        public string MESSAGE { get; set; }
        [DataMember]
        [DisplayName("SMS_CONTENT_TYPE")]
        public string SMS_CONTENT_TYPE { get; set; }

        [DataMember]
        [DisplayName("VERIFY_CODE")]
        public string VERIFY_CODE { get; set; }

        [DataMember]
        [DisplayName("TIMESTAMP")]
        public string TIMESTAMP { get; set; }
        [DataMember]
        [DisplayName("TYPE_SMS")]
        public string TYPE_SMS { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public int? STATUS { get; set; }
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
}

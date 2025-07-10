using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public class MessegeQueue
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string ToChannel { get; set; }
        public object Message { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; }
        

    }
    public class MessageInfo
    {
        [DataMember]
        [DisplayName("From")]
        public string From { get; set; }
        [DataMember]
        [DisplayName("ToChannel")]
        public string ToChannel { get; set; }
        [DataMember]
        [DisplayName("Message")]
        public string Message { get; set; }
        [DataMember]
        [DisplayName("WarningType")]
        public string WarningType { get; set; }
        [DataMember]
        [DisplayName("Timestamp")]
        public string Timestamp { get; set; }
        
    }    
    public class ReceiveInfo
    {
        [DataMember]
        [DisplayName("Code")]
        public string Code { get; set; }
        [DataMember]
        [DisplayName("From")]
        public string From { get; set; }
        [DataMember]
        [DisplayName("ToChannel")]
        public string ToChannel { get; set; }
        [DataMember]
        [DisplayName("Message")]
        public string Message { get; set; }
        [DataMember]
        [DisplayName("WarningType")]
        public string WarningType { get; set; }
        [DataMember]
        [DisplayName("Timestamp")]
        public string Timestamp { get; set; }
    }
    public class NOTIFICATIONInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("FROM")]
        public string FROM { get; set; }
        [DataMember]
        [DisplayName("TO_CHANNEL")]
        public string TO_CHANNEL { get; set; }
        [DataMember]
        [DisplayName("MESSAGE")]
        public MessageInfo MESSAGE { get; set; }
        [DataMember]
        [DisplayName("TIMESTAMP")]
        public string TIMESTAMP { get; set; }
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
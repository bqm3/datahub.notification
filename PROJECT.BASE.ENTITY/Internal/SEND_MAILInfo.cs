using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public class FileAttachInfo
    {
        [DataMember]
        [DisplayName("FileName")]
        public string FileName { get; set; }        
        [DataMember]
        [DisplayName("Content")]
        public string Content { get; set; }        
    }
    public class SendMail
    {        
        [DataMember]
        [DisplayName("ToEmail")]
        public string ToEmail { get; set; }
        [DataMember]
        [DisplayName("CCEmail")]
        public string CCEmail { get; set; }
        [DataMember]
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [DataMember]
        [DisplayName("Content")]
        public Dictionary<string,string> Content { get; set; }
        [DataMember]
        [DisplayName("Attachment")]
        public List<FileAttachInfo> Attachment { get; set; }
        [DataMember]
        [DisplayName("WarningType")]
        public string WarningType { get; set; }
    }
    public class SEND_MAILInfo
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
        [DisplayName("TO_EMAIL")]
        public string TO_EMAIL { get; set; }
        [DataMember]
        [DisplayName("CC_EMAIL")]
        public string CC_EMAIL { get; set; }
        [DataMember]
        [DisplayName("SUBJECT")]
        public string SUBJECT { get; set; }
        [DataMember]
        [DisplayName("CONTENT")]
        public Dictionary<string, string> CONTENT { get; set; }
        [DataMember]
        [DisplayName("ATTACHMENT")]
        public string ATTACHMENT { get; set; }        
        [DataMember]
        [DisplayName("TYPE_MAIL")]
        public string TYPE_MAIL { get; set; }
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

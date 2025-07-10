using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public partial class AMS_ACTIONSInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("ACTION_NAME")]
        public string ACTION_NAME { get; set; }
        [DataMember]
        [DisplayName("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public bool? STATUS { get; set; }
        [DataMember]
        [DisplayName("IS_DELETE")]
        public bool? IS_DELETE { get; set; }
        [DataMember]
        [DisplayName("CDATE")]
        public long? CDATE { get; set; }
        [DataMember]
        [DisplayName("LDATE")]
        public long? LDATE { get; set; }
        [DataMember]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
        [DataMember]
        [DisplayName("LUSER")]
        public string LUSER { get; set; }		
        
        

        
    }
}

using System.ComponentModel;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PROJECT.BASE.ENTITY
{

    public partial class AMS_FUNCTIONSInfo 
    {

        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("FUNCTION_NAME")]
        public string FUNCTION_NAME { get; set; }
        [DataMember]
        [DisplayName("FUNCTION_ICON")]
        public string FUNCTION_ICON { get; set; }
        [DataMember]
        [DisplayName("FUNCTION_URL")]
        public string FUNCTION_URL { get; set; }
        [DataMember]
        [DisplayName("PARENT_CODE")]
        public string PARENT_CODE { get; set; }
        [DataMember]
        [DisplayName("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [DataMember]
        [DisplayName("INCLUDE_MENU")]
        public bool? INCLUDE_MENU { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public bool? STATUS { get; set; }
        [DataMember]
        [DisplayName("IS_DELETE")]
        public bool? IS_DELETE { get; set; }
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

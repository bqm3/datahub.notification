using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public class SendFireBase
    {
        [DataMember]
        [DisplayName("UserID")]
        public string UserID { get; set; }
        [DataMember]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DataMember]
        [DisplayName("Title")]
        public string Title { get; set; }
        [DataMember]
        [DisplayName("Message")]
        public string Message { get; set; }
        [DataMember]
        [DisplayName("NotiType")]
        public string NotiType { get; set; }
        [DisplayName("IconType")]
        public string IconType { get; set; }
        [DataMember]
        [DisplayName("TransCode")]
        public string TransCode { get; set; }
    }
    public class CONFIG_FIRE_BASE
    {        
        [DataMember]
        [DisplayName("UserID")]
        public string UserID { get; set; }
        [DataMember]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DataMember]
        [DisplayName("TokenKey")]
        public string TokenKey { get; set; }
        [DataMember]
        [DisplayName("AppType")]
        public string AppType { get; set; }
    }
    public class CONFIG_FIRE_BASEInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("USER_ID")]
        public string USER_ID { get; set; }
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }
        [DataMember]
        [DisplayName("TOKEN_KEY")]
        public string TOKEN_KEY { get; set; }
        [DataMember]
        [DisplayName("APP_TYPE")]
        public string APP_TYPE { get; set; }
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
    public class FirebaseModel
    {
        public List<string> ListTokens { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotiType { get; set; }
        public string TransCode { get; set; }

    }
    public class SEND_FIRE_BASE_Update_Request
    {
        public string Code { get; set; }
        public bool IsRead { get; set; }
    }

    public class UpdateIsReadModel
    {
        public string UserId { get; set; }
        //public bool IsRead { get; set; }
    }

    public class SEND_FIRE_BASEInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("USER_ID")]
        public string USER_ID { get; set; }
        [DataMember]
        [DisplayName("USER_NAME")]
        public string USER_NAME { get; set; }        
        [DataMember]
        [DisplayName("TITLE")]
        public string TITLE { get; set; }
        [DataMember]
        [DisplayName("NOTI_TYPE")]
        public string NOTI_TYPE { get; set; }
        [DataMember]
        [DisplayName("ICON_TYPE")]
        public string ICON_TYPE { get; set; }
        [DataMember]
        [DisplayName("TRANS_CODE")]
        public string TRANS_CODE { get; set; }
        [DataMember]
        [DisplayName("IS_READ")]
        public bool IS_READ { get; set; }
        [DataMember]
        [DisplayName("MESSAGE")]
        public string MESSAGE { get; set; }
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
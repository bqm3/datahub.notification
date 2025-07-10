using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class UserInfo
    {
        [DataMember]
        [DisplayName("id")]
        public string id { get; set; }
        [DataMember]
        [DisplayName("createdTimestamp")]
        public long? createdTimestamp { get; set; }
        [DataMember]
        [DisplayName("username")]
        public string username { get; set; }
        [DataMember]
        [DisplayName("enabled")]
        public bool? enabled { get; set; }
        [DataMember]
        [DisplayName("emailVerified")]
        public bool? emailVerified { get; set; }
        [DataMember]
        [DisplayName("email")]
        public string email { get; set; }
        [DataMember]
        [DisplayName("firstName")]
        public string firstName { get; set; }
        [DataMember]
        [DisplayName("lastName")]
        public string lastName { get; set; }
        [DataMember]
        [DisplayName("attributes")]
        public Dictionary<string, string[]> attributes { get; set; }

    }
    [Serializable]
    [DataContract]
    public partial class UserPostInfo
    {
        [DataMember]
        [DisplayName("username")]
        public string username { get; set; }
        [DataMember]
        [DisplayName("enabled")]
        public bool? enabled { get; set; }
        [DataMember]
        [DisplayName("emailVerified")]
        public bool? emailVerified { get; set; }
        [DataMember]
        [DisplayName("email")]
        public string email { get; set; }
        [DataMember]
        [DisplayName("firstName")]
        public string firstName { get; set; }
        [DataMember]
        [DisplayName("lastName")]
        public string lastName { get; set; }
        [DataMember]
        [DisplayName("credentials")]
        public List<CredentialsInfo> credentials { get; set; }
        [DataMember]
        [DisplayName("attributes")]
        public Dictionary<string, string[]> attributes { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class LoginInfo
    {
        [DataMember]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DataMember]
        [DisplayName("PassWord")]
        public string PassWord { get; set; }
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("ClientID")]
        public string ClientID { get; set; }
        [DataMember]
        [DisplayName("AppCode")]
        public string AppCode { get; set; }
    }
    [Serializable]
    [DataContract]
    public class RefreshTokenInfo
    {
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("ClientID")]
        public string ClientID { get; set; }
        [DataMember]
        [DisplayName("RefreshToken")]
        public string RefreshToken { get; set; }
        [DataMember]
        [DisplayName("AppCode")]
        public string AppCode { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class ChangePasswordInfo
    {
        [DataMember]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DataMember]
        [DisplayName("CurentPassWord")]
        public string CurentPassWord { get; set; }
        [DataMember]
        [DisplayName("NewPassWord")]
        public string NewPassWord { get; set; }
        [DataMember]
        [DisplayName("ConfirmPassWord")]
        public string ConfirmPassWord { get; set; }
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("ClientID")]
        public string ClientID { get; set; }


    }
    //[Serializable]
    //[DataContract]
    //public partial class CorpInfo
    //{          
    //    [DataMember]
    //    [DisplayName("CORP_CODE")]
    //    public string CORP_CODE { get; set; }
    //    [DataMember]
    //    [DisplayName("EXPIRED")]
    //    public string EXPIRED { get; set; }
    //    [DataMember]
    //    [DisplayName("CORP_NAME")]
    //    public string CORP_NAME { get; set; }
    //    [DataMember]
    //    [DisplayName("ADDRESS")]
    //    public string ADDRESS { get; set; }
    //    [DataMember]
    //    [DisplayName("CORP_PHONE")]
    //    public string CORP_PHONE { get; set; }
    //    [DataMember]
    //    [DisplayName("EMAIL")]
    //    public string EMAIL { get; set; }
    //}    
}

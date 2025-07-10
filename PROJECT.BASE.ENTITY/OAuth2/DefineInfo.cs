using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class CreateRedirectInfo
    {
        [DataMember]
        [DisplayName("Client_Id")]
        public string Client_Id { get; set; }
        [DataMember]
        [DisplayName("Client_Secret")]
        public string Client_Secret { get; set; }
        //[DataMember]
        //[DisplayName("Realms")]
        //public string Realms { get; set; }
        [DataMember]
        [DisplayName("RedirectUrl")]
        public string RedirectUrl { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class AuthenticateCodeInfo
    {
        [DataMember]
        [DisplayName("Client_Id")]
        public string Client_Id { get; set; }
        [DataMember]
        [DisplayName("Client_Secret")]
        public string Client_Secret { get; set; }        
        [DataMember]
        [DisplayName("RedirectUrl")]
        public string RedirectUrl { get; set; }
        [DataMember]
        [DisplayName("Code")]
        public string Code { get; set; }
    }
    
    [Serializable]
    [DataContract]
    public partial class ValidateTokenInfo
    {
        [DataMember]
        [DisplayName("Client_Id")]
        public string Client_Id { get; set; }
        [DataMember]
        [DisplayName("Client_Secret")]
        public string Client_Secret { get; set; }
        //[DataMember]
        //[DisplayName("Realms")]
        //public string Realms { get; set; }
        [DataMember]
        [DisplayName("Access_Token")]
        public string Access_Token { get; set; }
    }

    [Serializable]
    [DataContract]
    public partial class PostOAuth2Info<T> where T : new()
    {
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("DataPost")]
        public T DataPost { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class PostOAuth2ByID<T> where T : new()
    {
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("Id")]
        public string Id { get; set; }
        [DataMember]
        [DisplayName("DataPost")]
        public List<T> DataPost { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class ResetPassWordInfo
    {
        [DataMember]
        [DisplayName("Realms")]
        public string Realms { get; set; }
        [DataMember]
        [DisplayName("Id")]
        public string Id { get; set; }
        [DataMember]
        [DisplayName("DataPost")]
        public CredentialsInfo DataPost { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class ClientInfo
    {
        [DataMember]
        [DisplayName("Client_Id")]
        public string Client_Id { get; set; }
        [DataMember]
        [DisplayName("Client_Secret")]
        public string Client_Secret { get; set; }        
    }
    public partial class RealmInfo
    {
        [DataMember]
        [DisplayName("Name")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
    //public partial class KeyValueInfo
    //{
    //    [DataMember]
    //    [DisplayName("Key")]
    //    public string Key { get; set; }
    //    [DataMember]
    //    [DisplayName("Value")]
    //    public string Value { get; set; }
    //}
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PROJECT.BASE.CORE;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{

    //[Serializable]
    //[DataContract]
    //public partial class CONFIGURATIONInfo
    //{        
    //    [DataMember]
    //    [DisplayName("FILE_NAME")]
    //    public string FILE_NAME { get; set; }
    //    [DataMember]
    //    [DisplayName("ROOT_PATH")]
    //    public string PATH_FILE { get; set; }
    //    [DataMember]
    //    [DisplayName("CONTENT")]
    //    public string CONTENT { get; set; }

    //}

    //[Serializable]
    //[DataContract]
    //public partial class APPLICATIONInfo
    //{
    //    [DataMember]
    //    [DisplayName("APP_NAME")]
    //    public string APP_NAME { get; set; }
    //    [DataMember]
    //    [DisplayName("VERSION")]
    //    public string VERSION { get; set; }
    //    [DataMember]
    //    [DisplayName("STATUS")]
    //    public int? STATUS { get; set; }
    //    [DataMember]
    //    [DisplayName("START_DATE")]
    //    public string START_DATE { get; set; }
    //    [DataMember]
    //    [DisplayName("RESTART_DATE")]
    //    public string RESTART_DATE { get; set; }

    //}
    //[Serializable]
    //[DataContract]
    //public partial class DYNAMICInfo
    //{
    //    [DataMember]
    //    [DisplayName("TOTAL_RECORD")]
    //    public long TOTAL_RECORD { get; set; }
    //    [DataMember]
    //    [DisplayName("DATA")]
    //    //public List<Dictionary<string, object>> DATA { get; set; }
    //    public List<OrderedDictionary> DATA { get; set; }


    //}
    [Serializable]
    [DataContract]
    public partial class CONFIGURATION_FIELDInfo
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("STORAGE")]
        public string STORAGE { get; set; }
        [DataMember]
        [DisplayName("COLLECTIONS")]
        public string COLLECTIONS { get; set; }
        [DataMember]
        [DisplayName("FIELD_NAME")]
        public string FIELD_NAME { get; set; }
        
        [DataMember]
        [DisplayName("FIELD_SEARCH")]
        public bool? FIELD_SEARCH { get; set; }
        [DataMember]
        [DisplayName("FIELD_VIEW")]
        public bool? FIELD_VIEW { get; set; }        
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
    public partial class SERVICE_STATUSInfo
    {
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public string STATUS { get; set; }

    }
    [Serializable]
    [DataContract]
    public partial class COLLECTIONInfo
    {
        [DataMember]
        [DisplayName("NAME")]
        public string NAME { get; set; }
        [DataMember]
        [DisplayName("TYPE")]
        public string TYPE { get; set; }
        [DataMember]
        [DisplayName("STORAGE")]
        public string STORAGE { get; set; }
        [DataMember]
        [DisplayName("TOTAL_RECORD")]
        public long? TOTAL_RECORD { get; set; }
    }

    //[Serializable]
    //[DataContract]
    //public partial class DATA_FIELDInfo
    //{
    //    [DataMember]
    //    [DisplayName("FIELD_NAME")]
    //    public string FIELD_NAME { get; set; }
    //    [DataMember]
    //    [DisplayName("DATA_TYPE")]
    //    public string DATA_TYPE { get; set; }
    //    [DataMember]
    //    [DisplayName("FIELD_VALUE")]
    //    public object FIELD_VALUE { get; set; }

    //}

    [Serializable]
    [DataContract]
    public partial class DYNAMIC_POSTInfo
    {
        [DataMember]
        [DisplayName("STORAGE")]
        public string STORAGE { get; set; }
        [DataMember]
        [DisplayName("COLLECTION_NAME")]
        public string COLLECTION_NAME { get; set; }
        [DataMember]
        [DisplayName("DATA")]
        public Dictionary<string, string> DATA { get; set; }
    }
    [Serializable]
    [DataContract]
    public partial class DYNAMIC_PUTInfo
    {
        [DataMember]
        [DisplayName("STORAGE")]
        public string STORAGE { get; set; }
        [DataMember]
        [DisplayName("COLLECTION_NAME")]
        public string COLLECTION_NAME { get; set; }
        [DataMember]
        [DisplayName("DATA")]
        public Dictionary<string, string> DATA { get; set; }
        [DataMember]
        [DisplayName("KEY_VALUE_FILLTER")]
        public KeyValuePair<string, string> KEY_VALUE_FILLTER { get; set; }
    }

    [Serializable]
    [DataContract]
    public partial class DYNAMIC_POST_GET_LISTInfo
    {
        [DataMember]
        [DisplayName("STORAGE")]
        public string STORAGE { get; set; }
        [DataMember]
        [DisplayName("COLLECTION_NAME")]
        public string COLLECTION_NAME { get; set; }
        [DataMember]
        [DisplayName("LIST_FILLTER")]
        public List<DATA_FIELDInfo> LIST_FILLTER { get; set; }        
        [DataMember]
        [DisplayName("PAGE_INDEX")]
        public int PAGE_INDEX { get; set; }
        [DataMember]
        [DisplayName("PAGE_SIZE")]
        public int PAGE_SIZE { get; set; }

        
    }
}

using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class SYS_CONFIG_API
    {
        [DataMember]
        [SolrUniqueKey("ID")]
        [DisplayName("ID")]
        public decimal? ID { get; set; }
        [DataMember]
        [SolrField("CODE")]
        [DisplayName("CODE")]
        public string CODE { get; set; }

        [DataMember]
        [SolrField("API_NAME")]
        [DisplayName("API_NAME")]
        public string API_NAME { get; set; }
        [DataMember]
        [SolrField("API_URL")]
        [DisplayName("API_URL")]
        public string API_URL { get; set; }
        [DataMember]
        [SolrField("METHORD")]
        [DisplayName("METHORD")]
        public string METHORD { get; set; }
        [DataMember]
        [SolrField("ADD_HEADER")]
        [DisplayName("ADD_HEADER")]
        public string ADD_HEADER { get; set; }
        [DataMember]
        [SolrField("ADD_PARAMETER")]
        [DisplayName("ADD_PARAMETER")]
        public string ADD_PARAMETER { get; set; }
        [DataMember]
        [SolrField("API_TYPE")]
        [DisplayName("API_TYPE")]
        public string API_TYPE { get; set; }
        [DataMember]
        [SolrField("PARENT_CODE")]
        [DisplayName("PARENT_CODE")]
        public string PARENT_CODE { get; set; }
        [DataMember]
        [SolrField("PARENT_NAME")]
        [DisplayName("PARENT_NAME")]
        public string PARENT_NAME { get; set; }
        [DataMember]
        [SolrField("IS_ACTIVE")]
        [DisplayName("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
        [DataMember]
        [SolrField("STATUS")]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }     
		[DataMember]
		[SolrField("IS_DELETE")]
		[DisplayName("IS_DELETE")]
		public short? IS_DELETE { get; set; }
        [DataMember]
        [SolrField("CDATE")]
        [DisplayName("CDATE")]
        public DateTime? CDATE { get; set; }
        [DataMember]
        [SolrField("LDATE")]
        [DisplayName("LDATE")]
        public DateTime? LDATE { get; set; }
        [DataMember]
        [SolrField("CUSER")]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
        [DataMember]
        [SolrField("LUSER")]
        [DisplayName("LUSER")]
        public string LUSER { get; set; }
       
    }    
   
    public partial class SYS_CONFIG_API_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("API_NAME")]
        public string API_NAME { get; set; }
        [DataMember]
        [DisplayName("API_URL")]
        public string API_URL { get; set; }
        [DataMember]
        [DisplayName("METHORD")]
        public string METHORD { get; set; }
        [DataMember]
        [DisplayName("ADD_HEADER")]
        public string ADD_HEADER { get; set; }
        [DataMember]
        [DisplayName("ADD_PARAMETER")]
        public string ADD_PARAMETER { get; set; }
        [DataMember]
        [DisplayName("API_TYPE")]
        public string API_TYPE { get; set; }
        [DataMember]
        [DisplayName("PARENT_CODE")]
        public string PARENT_CODE { get; set; }
        [DataMember]
        [DisplayName("PARENT_NAME")]
        public string PARENT_NAME { get; set; }
        [DataMember]
        [DisplayName("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class SYS_CONFIG_API_Search
    {
        
        [DataMember]        
        [DisplayName("SearchField")]
        public Dictionary<string,object> SearchField { get; set; }
        [DataMember]        
        [DisplayName("CDATE_START")]
        public string CDATE_START { get; set; }
        [DataMember]        
        [DisplayName("CDATE_END")]
        public string CDATE_END { get; set; }
        [DataMember]        
        [DisplayName("PageIndex")]
        public int? PageIndex { get; set; }
        [DataMember]        
        [DisplayName("PageSize")]
        public int? PageSize { get; set; }

    }

}

using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class SYS_RESOURCE_AUTHOR
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
        [SolrField("ROLE_CODE")]
        [DisplayName("ROLE_CODE")]
        public string ROLE_CODE { get; set; }
        [DataMember]
        [SolrField("ACCOUNT")]
        [DisplayName("ACCOUNT")]
        public string ACCOUNT { get; set; }
        [DataMember]
        [SolrField("RESOURCE_CODE")]
        [DisplayName("RESOURCE_CODE")]
        public string RESOURCE_CODE { get; set; }
        [DataMember]
        [SolrField("RESOURCE_NAME")]
        [DisplayName("RESOURCE_NAME")]
        public string RESOURCE_NAME { get; set; }
        [DataMember]
        [SolrField("PARENT_CODE")]
        [DisplayName("PARENT_CODE")]
        public string PARENT_CODE { get; set; }
        [DataMember]
        [SolrField("PARENT_NAME")]
        [DisplayName("PARENT_NAME")]
        public string PARENT_NAME { get; set; }
        [DataMember]
        [SolrField("LINK")]
        [DisplayName("LINK")]
        public string LINK { get; set; }
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
   
    public partial class SYS_RESOURCE_AUTHOR_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("ROLE_CODE")]
        public string ROLE_CODE { get; set; }
        [DataMember]
        [DisplayName("ACCOUNT")]
        public string ACCOUNT { get; set; }
        [DataMember]
        [DisplayName("RESOURCE_CODE")]
        public string RESOURCE_CODE { get; set; }
        [DataMember]
        [DisplayName("RESOURCE_NAME")]
        public string RESOURCE_NAME { get; set; }
        [DataMember]
        [DisplayName("PARENT_CODE")]
        public string PARENT_CODE { get; set; }
        [DataMember]
        [DisplayName("PARENT_NAME")]
        public string PARENT_NAME { get; set; }
        [DataMember]
        [DisplayName("LINK")]
        public string LINK { get; set; }
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

    public partial class SYS_RESOURCE_AUTHOR_Search
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

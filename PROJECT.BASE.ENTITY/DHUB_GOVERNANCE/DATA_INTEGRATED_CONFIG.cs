using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class DATA_INTEGRATED_CONFIG
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
        [SolrField("URL")]
        [DisplayName("URL")]
        public string URL { get; set; }
        [DataMember]
        [SolrField("FILE_NAME")]
        [DisplayName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        [DataMember]
        [SolrField("DATA_SOURCE")]
        [DisplayName("DATA_SOURCE")]
        public string DATA_SOURCE { get; set; }
        [DataMember]
        [SolrField("FIELD")]
        [DisplayName("FIELD")]
        public string FIELD { get; set; }
        [DataMember]
        [SolrField("INTEGRATED_TYPE")]
        [DisplayName("INTEGRATED_TYPE")]
        public decimal? INTEGRATED_TYPE { get; set; }
        [DataMember]
        [SolrField("GROUP_ID")]
        [DisplayName("GROUP_ID")]
        public string GROUP_ID { get; set; }
        [DataMember]
        [SolrField("FILE_TYPE")]
        [DisplayName("FILE_TYPE")]
        public string FILE_TYPE { get; set; }     
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
   
    public partial class DATA_INTEGRATED_CONFIG_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("URL")]
        public string URL { get; set; }
        [DataMember]
        [DisplayName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        [DataMember]
        [DisplayName("DATA_SOURCE")]
        public string DATA_SOURCE { get; set; }
        [DataMember]
        [DisplayName("FIELD")]
        public string FIELD { get; set; }
        [DataMember]
        [DisplayName("INTEGRATED_TYPE")]
        public decimal? INTEGRATED_TYPE { get; set; }
        [DataMember]
        [DisplayName("GROUP_ID")]
        public string GROUP_ID { get; set; }
        [DataMember]
        [DisplayName("FILE_TYPE")]
        public string FILE_TYPE { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class DATA_INTEGRATED_CONFIG_Search
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

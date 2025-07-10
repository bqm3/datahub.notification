using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class LOG_ACTION
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
        [SolrField("OBJECT_NAME")]
        [DisplayName("OBJECT_NAME")]
        public string OBJECT_NAME { get; set; }
        [DataMember]
        [SolrField("OBJECT_ID")]
        [DisplayName("OBJECT_ID")]
        public decimal? OBJECT_ID { get; set; }
        [DataMember]
        [SolrField("LOG_TYPE_CODE")]
        [DisplayName("LOG_TYPE_CODE")]
        public string LOG_TYPE_CODE { get; set; }
        [DataMember]
        [SolrField("MESSAGE_LOG")]
        [DisplayName("MESSAGE_LOG")]
        public string MESSAGE_LOG { get; set; }
        [DataMember]
        [SolrField("MESSAGE_DETAIL")]
        [DisplayName("MESSAGE_DETAIL")]
        public string MESSAGE_DETAIL { get; set; }
        [DataMember]
        [SolrField("WORKING_SESSION")]
        [DisplayName("WORKING_SESSION")]
        public DateTime? WORKING_SESSION { get; set; }
        [DataMember]
        [SolrField("START_DATE")]
        [DisplayName("START_DATE")]
        public DateTime? START_DATE { get; set; }
        [DataMember]
        [SolrField("END_DATE")]
        [DisplayName("END_DATE")]
        public DateTime? END_DATE { get; set; }
        [DataMember]
        [SolrField("CONFIG_PIPELINE_CODE")]
        [DisplayName("CONFIG_PIPELINE_CODE")]
        public string CONFIG_PIPELINE_CODE { get; set; }
        [DataMember]
        [SolrField("RESULT_VALUE")]
        [DisplayName("RESULT_VALUE")]
        public string RESULT_VALUE { get; set; }
        [DataMember]
        [SolrField("RESULT_ETL_RDS")]
        [DisplayName("RESULT_ETL_RDS")]
        public long? RESULT_ETL_RDS { get; set; }
        [DataMember]
        [SolrField("RESULT_ETL_RIS")]
        [DisplayName("RESULT_ETL_RIS")]
        public long? RESULT_ETL_RIS { get; set; }
        [DataMember]
        [SolrField("PARENT_LOG_ID")]
        [DisplayName("PARENT_LOG_ID")]
        public decimal? PARENT_LOG_ID { get; set; }
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
   
    public partial class LOG_ACTION_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("OBJECT_NAME")]
        public string OBJECT_NAME { get; set; }
        [DataMember]
        [DisplayName("OBJECT_ID")]
        public decimal? OBJECT_ID { get; set; }
        [DataMember]
        [DisplayName("LOG_TYPE_CODE")]
        public string LOG_TYPE_CODE { get; set; }
        [DataMember]
        [DisplayName("MESSAGE_LOG")]
        public string MESSAGE_LOG { get; set; }
        [DataMember]
        [DisplayName("MESSAGE_DETAIL")]
        public string MESSAGE_DETAIL { get; set; }
        [DataMember]
        [DisplayName("WORKING_SESSION")]
        public string WORKING_SESSION { get; set; }
        [DataMember]
        [DisplayName("START_DATE")]
        public string START_DATE { get; set; }
        [DataMember]
        [DisplayName("END_DATE")]
        public string END_DATE { get; set; }
        [DataMember]
        [DisplayName("CONFIG_PIPELINE_CODE")]
        public string CONFIG_PIPELINE_CODE { get; set; }
        [DataMember]
        [DisplayName("RESULT_VALUE")]
        public string RESULT_VALUE { get; set; }
        [DataMember]
        [DisplayName("RESULT_ETL_RDS")]
        public long? RESULT_ETL_RDS { get; set; }
        [DataMember]
        [DisplayName("RESULT_ETL_RIS")]
        public long? RESULT_ETL_RIS { get; set; }
        [DataMember]
        [DisplayName("PARENT_LOG_ID")]
        public decimal? PARENT_LOG_ID { get; set; }
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

    public partial class LOG_ACTION_Search
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

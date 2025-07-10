using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class ETL_DATA_CONFIG
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
        [SolrField("SOURCE_OBJECT_NAME")]
        [DisplayName("SOURCE_OBJECT_NAME")]
        public string SOURCE_OBJECT_NAME { get; set; }
        [DataMember]
        [SolrField("OBJECT_ID")]
        [DisplayName("OBJECT_ID")]
        public decimal? OBJECT_ID { get; set; }
        [DataMember]
        [SolrField("ELTNAME")]
        [DisplayName("ELTNAME")]
        public string ELTNAME { get; set; }
        [DataMember]
        [SolrField("OBJECT_OWNER")]
        [DisplayName("OBJECT_OWNER")]
        public string OBJECT_OWNER { get; set; }
        [DataMember]
        [SolrField("LINK_OWNER")]
        [DisplayName("LINK_OWNER")]
        public string LINK_OWNER { get; set; }
        [DataMember]
        [SolrField("ETL_CONNECT")]
        [DisplayName("ETL_CONNECT")]
        public string ETL_CONNECT { get; set; }
        [DataMember]
        [SolrField("DATA_ZONE_CODE_START")]
        [DisplayName("DATA_ZONE_CODE_START")]
        public string DATA_ZONE_CODE_START { get; set; }
        [DataMember]
        [SolrField("DATA_ZONE_CODE_END")]
        [DisplayName("DATA_ZONE_CODE_END")]
        public string DATA_ZONE_CODE_END { get; set; }
        [DataMember]
        [SolrField("SOURCE_OBJECT")]
        [DisplayName("SOURCE_OBJECT")]
        public string SOURCE_OBJECT { get; set; }
        [DataMember]
        [SolrField("SOURCE_FIELDS_NAME")]
        [DisplayName("SOURCE_FIELDS_NAME")]
        public string SOURCE_FIELDS_NAME { get; set; }
        [DataMember]
        [SolrField("DEST_OBJECT")]
        [DisplayName("DEST_OBJECT")]
        public string DEST_OBJECT { get; set; }
        [DataMember]
        [SolrField("DEST_FIELDS_NAME")]
        [DisplayName("DEST_FIELDS_NAME")]
        public string DEST_FIELDS_NAME { get; set; }
        [DataMember]
        [SolrField("IS_ACTION")]
        [DisplayName("IS_ACTION")]
        public short? IS_ACTION { get; set; }
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
   
    public partial class ETL_DATA_CONFIG_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("SOURCE_OBJECT_NAME")]
        public string SOURCE_OBJECT_NAME { get; set; }
        [DataMember]
        [DisplayName("OBJECT_ID")]
        public decimal? OBJECT_ID { get; set; }
        [DataMember]
        [DisplayName("ELTNAME")]
        public string ELTNAME { get; set; }
        [DataMember]
        [DisplayName("OBJECT_OWNER")]
        public string OBJECT_OWNER { get; set; }
        [DataMember]
        [DisplayName("LINK_OWNER")]
        public string LINK_OWNER { get; set; }
        [DataMember]
        [DisplayName("ETL_CONNECT")]
        public string ETL_CONNECT { get; set; }
        [DataMember]
        [DisplayName("DATA_ZONE_CODE_START")]
        public string DATA_ZONE_CODE_START { get; set; }
        [DataMember]
        [DisplayName("DATA_ZONE_CODE_END")]
        public string DATA_ZONE_CODE_END { get; set; }
        [DataMember]
        [DisplayName("SOURCE_OBJECT")]
        public string SOURCE_OBJECT { get; set; }
        [DataMember]
        [DisplayName("SOURCE_FIELDS_NAME")]
        public string SOURCE_FIELDS_NAME { get; set; }
        [DataMember]
        [DisplayName("DEST_OBJECT")]
        public string DEST_OBJECT { get; set; }
        [DataMember]
        [DisplayName("DEST_FIELDS_NAME")]
        public string DEST_FIELDS_NAME { get; set; }
        [DataMember]
        [DisplayName("IS_ACTION")]
        public short? IS_ACTION { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class ETL_DATA_CONFIG_Search
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

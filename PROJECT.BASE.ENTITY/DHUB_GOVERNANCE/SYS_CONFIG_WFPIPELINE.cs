using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class SYS_CONFIG_WFPIPELINE
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
        [SolrField("NOTE")]
        [DisplayName("NOTE")]
        public string NOTE { get; set; }
        [DataMember]
        [SolrField("TIMESTART")]
        [DisplayName("TIMESTART")]
        public DateTime? TIMESTART { get; set; }
        [DataMember]
        [SolrField("TIMEEND")]
        [DisplayName("TIMEEND")]
        public DateTime? TIMEEND { get; set; }
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
   
    public partial class SYS_CONFIG_WFPIPELINE_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("NOTE")]
        public string NOTE { get; set; }
        [DataMember]
        [DisplayName("TIMESTART")]
        public string TIMESTART { get; set; }
        [DataMember]
        [DisplayName("TIMEEND")]
        public string TIMEEND { get; set; }
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

    public partial class SYS_CONFIG_WFPIPELINE_Search
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

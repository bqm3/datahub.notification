using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class CHARTS
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
        [SolrField("TITLE")]
        [DisplayName("TITLE")]
        public string TITLE { get; set; }
        [DataMember]
        [SolrField("X")]
        [DisplayName("X")]
        public decimal? X { get; set; }
        [DataMember]
        [SolrField("Y")]
        [DisplayName("Y")]
        public decimal? Y { get; set; }
        [DataMember]
        [SolrField("WIDTH")]
        [DisplayName("WIDTH")]
        public decimal? WIDTH { get; set; }
        [DataMember]
        [SolrField("HEIGHT")]
        [DisplayName("HEIGHT")]
        public decimal? HEIGHT { get; set; }
        [DataMember]
        [SolrField("POLL_INTERVAL")]
        [DisplayName("POLL_INTERVAL")]
        public decimal? POLL_INTERVAL { get; set; }
        [DataMember]
        [SolrField("CHART_TYPE")]
        [DisplayName("CHART_TYPE")]
        public string CHART_TYPE { get; set; }
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
   
    public partial class CHARTS_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("TITLE")]
        public string TITLE { get; set; }
        [DataMember]
        [DisplayName("X")]
        public decimal? X { get; set; }
        [DataMember]
        [DisplayName("Y")]
        public decimal? Y { get; set; }
        [DataMember]
        [DisplayName("WIDTH")]
        public decimal? WIDTH { get; set; }
        [DataMember]
        [DisplayName("HEIGHT")]
        public decimal? HEIGHT { get; set; }
        [DataMember]
        [DisplayName("POLL_INTERVAL")]
        public decimal? POLL_INTERVAL { get; set; }
        [DataMember]
        [DisplayName("CHART_TYPE")]
        public string CHART_TYPE { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class CHARTS_Search
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

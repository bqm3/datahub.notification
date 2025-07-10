using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class SYS_SHARE_CORP
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
        [SolrField("CORP_NAME")]
        [DisplayName("CORP_NAME")]
        public string CORP_NAME { get; set; }
        [DataMember]
        [SolrField("TAX_CODE")]
        [DisplayName("TAX_CODE")]
        public string TAX_CODE { get; set; }
        [DataMember]
        [SolrField("ADDRESS")]
        [DisplayName("ADDRESS")]
        public string ADDRESS { get; set; }
        [DataMember]
        [SolrField("CORP_PHONE")]
        [DisplayName("CORP_PHONE")]
        public string CORP_PHONE { get; set; }
        [DataMember]
        [SolrField("EMAIL")]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [SolrField("FAX")]
        [DisplayName("FAX")]
        public string FAX { get; set; }
        [DataMember]
        [SolrField("WEBSITE")]
        [DisplayName("WEBSITE")]
        public string WEBSITE { get; set; }
        [DataMember]
        [SolrField("REPRESENTATIVE")]
        [DisplayName("REPRESENTATIVE")]
        public string REPRESENTATIVE { get; set; }
        [DataMember]
        [SolrField("POSITION")]
        [DisplayName("POSITION")]
        public string POSITION { get; set; }
        [DataMember]
        [SolrField("REP_PHONE")]
        [DisplayName("REP_PHONE")]
        public string REP_PHONE { get; set; }
        [DataMember]
        [SolrField("REP_EMAIL")]
        [DisplayName("REP_EMAIL")]
        public string REP_EMAIL { get; set; }
        [DataMember]
        [SolrField("GROUP_CODE")]
        [DisplayName("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
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
   
    public partial class SYS_SHARE_CORP_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("CORP_NAME")]
        public string CORP_NAME { get; set; }
        [DataMember]
        [DisplayName("TAX_CODE")]
        public string TAX_CODE { get; set; }
        [DataMember]
        [DisplayName("ADDRESS")]
        public string ADDRESS { get; set; }
        [DataMember]
        [DisplayName("CORP_PHONE")]
        public string CORP_PHONE { get; set; }
        [DataMember]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [DisplayName("FAX")]
        public string FAX { get; set; }
        [DataMember]
        [DisplayName("WEBSITE")]
        public string WEBSITE { get; set; }
        [DataMember]
        [DisplayName("REPRESENTATIVE")]
        public string REPRESENTATIVE { get; set; }
        [DataMember]
        [DisplayName("POSITION")]
        public string POSITION { get; set; }
        [DataMember]
        [DisplayName("REP_PHONE")]
        public string REP_PHONE { get; set; }
        [DataMember]
        [DisplayName("REP_EMAIL")]
        public string REP_EMAIL { get; set; }
        [DataMember]
        [DisplayName("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
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

    public partial class SYS_SHARE_CORP_Search
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

using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_DM_DBHC
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
        [SolrField("ID_CHA")]
        [DisplayName("ID_CHA")]
        public decimal? ID_CHA { get; set; }
        [DataMember]
        [SolrField("LOAI")]
        [DisplayName("LOAI")]
        public decimal? LOAI { get; set; }
        [DataMember]
        [SolrField("MA_CHA")]
        [DisplayName("MA_CHA")]
        public string MA_CHA { get; set; }
        [DataMember]
        [SolrField("MA_HUYEN")]
        [DisplayName("MA_HUYEN")]
        public string MA_HUYEN { get; set; }
        [DataMember]
        [SolrField("MA_TINH")]
        [DisplayName("MA_TINH")]
        public string MA_TINH { get; set; }
        [DataMember]
        [SolrField("MA_XA")]
        [DisplayName("MA_XA")]
        public string MA_XA { get; set; }
        [DataMember]
        [SolrField("TEN")]
        [DisplayName("TEN")]
        public string TEN { get; set; }
        [DataMember]
        [SolrField("VERSION_XML")]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }     
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
    public partial class EHR_DM_DBHC_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_DM_DBHC_Information Information { get; set; }

    }
    public partial class EHR_DM_DBHC_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("ID_CHA")]
        public decimal? ID_CHA { get; set; }
        [DataMember]
        [DisplayName("LOAI")]
        public decimal? LOAI { get; set; }
        [DataMember]
        [DisplayName("MA_CHA")]
        public string MA_CHA { get; set; }
        [DataMember]
        [DisplayName("MA_HUYEN")]
        public string MA_HUYEN { get; set; }
        [DataMember]
        [DisplayName("MA_TINH")]
        public string MA_TINH { get; set; }
        [DataMember]
        [DisplayName("MA_XA")]
        public string MA_XA { get; set; }
        [DataMember]
        [DisplayName("TEN")]
        public string TEN { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_DM_DBHC_Search
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

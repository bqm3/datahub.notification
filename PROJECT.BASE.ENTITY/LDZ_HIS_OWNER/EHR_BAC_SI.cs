using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_BAC_SI
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
        [SolrField("CHUNG_CHI_HANH_NGHE")]
        [DisplayName("CHUNG_CHI_HANH_NGHE")]
        public string CHUNG_CHI_HANH_NGHE { get; set; }
        [DataMember]
        [SolrField("HO_TEN")]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [SolrField("MA_BAC_SI")]
        [DisplayName("MA_BAC_SI")]
        public string MA_BAC_SI { get; set; }
        [DataMember]
        [SolrField("MA_BAC_SI_DON_VI")]
        [DisplayName("MA_BAC_SI_DON_VI")]
        public string MA_BAC_SI_DON_VI { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
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
    public partial class EHR_BAC_SI_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_BAC_SI_Information Information { get; set; }

    }
    public partial class EHR_BAC_SI_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("CHUNG_CHI_HANH_NGHE")]
        public string CHUNG_CHI_HANH_NGHE { get; set; }
        [DataMember]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [DisplayName("MA_BAC_SI")]
        public string MA_BAC_SI { get; set; }
        [DataMember]
        [DisplayName("MA_BAC_SI_DON_VI")]
        public string MA_BAC_SI_DON_VI { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_BAC_SI_Search
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

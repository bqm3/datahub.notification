using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_CO_SO_Y_TE
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
        [SolrField("DIA_CHI")]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [SolrField("MA_CO_SO_Y_TE")]
        [DisplayName("MA_CO_SO_Y_TE")]
        public string MA_CO_SO_Y_TE { get; set; }
        [DataMember]
        [SolrField("MA_CO_SO_Y_TE_DON_VI")]
        [DisplayName("MA_CO_SO_Y_TE_DON_VI")]
        public string MA_CO_SO_Y_TE_DON_VI { get; set; }
        [DataMember]
        [SolrField("QUAN_HUYEN")]
        [DisplayName("QUAN_HUYEN")]
        public string QUAN_HUYEN { get; set; }
        [DataMember]
        [SolrField("TEN_CO_SO_Y_TE")]
        [DisplayName("TEN_CO_SO_Y_TE")]
        public string TEN_CO_SO_Y_TE { get; set; }
        [DataMember]
        [SolrField("TINH_TP")]
        [DisplayName("TINH_TP")]
        public string TINH_TP { get; set; }
        [DataMember]
        [SolrField("VERSION_XML")]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [SolrField("XA_PHUONG")]
        [DisplayName("XA_PHUONG")]
        public string XA_PHUONG { get; set; }     
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
    public partial class EHR_CO_SO_Y_TE_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_CO_SO_Y_TE_Information Information { get; set; }

    }
    public partial class EHR_CO_SO_Y_TE_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("MA_CO_SO_Y_TE")]
        public string MA_CO_SO_Y_TE { get; set; }
        [DataMember]
        [DisplayName("MA_CO_SO_Y_TE_DON_VI")]
        public string MA_CO_SO_Y_TE_DON_VI { get; set; }
        [DataMember]
        [DisplayName("QUAN_HUYEN")]
        public string QUAN_HUYEN { get; set; }
        [DataMember]
        [DisplayName("TEN_CO_SO_Y_TE")]
        public string TEN_CO_SO_Y_TE { get; set; }
        [DataMember]
        [DisplayName("TINH_TP")]
        public string TINH_TP { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [DisplayName("XA_PHUONG")]
        public string XA_PHUONG { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_CO_SO_Y_TE_Search
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

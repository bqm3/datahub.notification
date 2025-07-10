using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_DON_THUOC
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
        [SolrField("LOAI_BENH_NHAN")]
        [DisplayName("LOAI_BENH_NHAN")]
        public string LOAI_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_BAC_SI")]
        [DisplayName("MA_BAC_SI")]
        public string MA_BAC_SI { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_CO_SO_Y_TE")]
        [DisplayName("MA_CO_SO_Y_TE")]
        public string MA_CO_SO_Y_TE { get; set; }
        [DataMember]
        [SolrField("MA_DON_THUOC")]
        [DisplayName("MA_DON_THUOC")]
        public string MA_DON_THUOC { get; set; }
        [DataMember]
        [SolrField("MA_DON_THUOC_DON_VI")]
        [DisplayName("MA_DON_THUOC_DON_VI")]
        public string MA_DON_THUOC_DON_VI { get; set; }
        [DataMember]
        [SolrField("MA_HSSK")]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [SolrField("MA_KQ_KCB")]
        [DisplayName("MA_KQ_KCB")]
        public string MA_KQ_KCB { get; set; }
        [DataMember]
        [SolrField("THOI_GIAN_KE_THUOC")]
        [DisplayName("THOI_GIAN_KE_THUOC")]
        public DateTime? THOI_GIAN_KE_THUOC { get; set; }
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
    public partial class EHR_DON_THUOC_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_DON_THUOC_Information Information { get; set; }

    }
    public partial class EHR_DON_THUOC_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("LOAI_BENH_NHAN")]
        public string LOAI_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_BAC_SI")]
        public string MA_BAC_SI { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_CO_SO_Y_TE")]
        public string MA_CO_SO_Y_TE { get; set; }
        [DataMember]
        [DisplayName("MA_DON_THUOC")]
        public string MA_DON_THUOC { get; set; }
        [DataMember]
        [DisplayName("MA_DON_THUOC_DON_VI")]
        public string MA_DON_THUOC_DON_VI { get; set; }
        [DataMember]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [DisplayName("MA_KQ_KCB")]
        public string MA_KQ_KCB { get; set; }
        [DataMember]
        [DisplayName("THOI_GIAN_KE_THUOC")]
        public string THOI_GIAN_KE_THUOC { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_DON_THUOC_Search
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

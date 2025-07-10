using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_KQ_KCB_SINH_TON_VA_STH
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
        [SolrField("BMI")]
        [DisplayName("BMI")]
        public string BMI { get; set; }
        [DataMember]
        [SolrField("CAN_NANG")]
        [DisplayName("CAN_NANG")]
        public string CAN_NANG { get; set; }
        [DataMember]
        [SolrField("CHIEU_CAO")]
        [DisplayName("CHIEU_CAO")]
        public string CHIEU_CAO { get; set; }
        [DataMember]
        [SolrField("HUYET_AP_TAM_THU")]
        [DisplayName("HUYET_AP_TAM_THU")]
        public string HUYET_AP_TAM_THU { get; set; }
        [DataMember]
        [SolrField("HUYET_AP_TAM_TRUONG")]
        [DisplayName("HUYET_AP_TAM_TRUONG")]
        public string HUYET_AP_TAM_TRUONG { get; set; }
        [DataMember]
        [SolrField("MACH")]
        [DisplayName("MACH")]
        public string MACH { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_HSSK")]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [SolrField("MA_SINH_TON_VA_STH")]
        [DisplayName("MA_SINH_TON_VA_STH")]
        public string MA_SINH_TON_VA_STH { get; set; }
        [DataMember]
        [SolrField("MA_SINH_TON_VA_STH_DON_VI")]
        [DisplayName("MA_SINH_TON_VA_STH_DON_VI")]
        public string MA_SINH_TON_VA_STH_DON_VI { get; set; }
        [DataMember]
        [SolrField("NHIET_DO")]
        [DisplayName("NHIET_DO")]
        public string NHIET_DO { get; set; }
        [DataMember]
        [SolrField("NHIP_THO")]
        [DisplayName("NHIP_THO")]
        public string NHIP_THO { get; set; }
        [DataMember]
        [SolrField("VERSION_XML")]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [SolrField("VONG_BUNG")]
        [DisplayName("VONG_BUNG")]
        public string VONG_BUNG { get; set; }     
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
    public partial class EHR_KQ_KCB_SINH_TON_VA_STH_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_KQ_KCB_SINH_TON_VA_STH_Information Information { get; set; }

    }
    public partial class EHR_KQ_KCB_SINH_TON_VA_STH_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("BMI")]
        public string BMI { get; set; }
        [DataMember]
        [DisplayName("CAN_NANG")]
        public string CAN_NANG { get; set; }
        [DataMember]
        [DisplayName("CHIEU_CAO")]
        public string CHIEU_CAO { get; set; }
        [DataMember]
        [DisplayName("HUYET_AP_TAM_THU")]
        public string HUYET_AP_TAM_THU { get; set; }
        [DataMember]
        [DisplayName("HUYET_AP_TAM_TRUONG")]
        public string HUYET_AP_TAM_TRUONG { get; set; }
        [DataMember]
        [DisplayName("MACH")]
        public string MACH { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [DisplayName("MA_SINH_TON_VA_STH")]
        public string MA_SINH_TON_VA_STH { get; set; }
        [DataMember]
        [DisplayName("MA_SINH_TON_VA_STH_DON_VI")]
        public string MA_SINH_TON_VA_STH_DON_VI { get; set; }
        [DataMember]
        [DisplayName("NHIET_DO")]
        public string NHIET_DO { get; set; }
        [DataMember]
        [DisplayName("NHIP_THO")]
        public string NHIP_THO { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [DisplayName("VONG_BUNG")]
        public string VONG_BUNG { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_KQ_KCB_SINH_TON_VA_STH_Search
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

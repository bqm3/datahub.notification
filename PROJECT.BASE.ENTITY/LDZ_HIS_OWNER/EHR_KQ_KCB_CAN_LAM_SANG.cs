using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_KQ_KCB_CAN_LAM_SANG
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
        [SolrField("GIA_TRI")]
        [DisplayName("GIA_TRI")]
        public string GIA_TRI { get; set; }
        [DataMember]
        [SolrField("KET_LUAN")]
        [DisplayName("KET_LUAN")]
        public string KET_LUAN { get; set; }
        [DataMember]
        [SolrField("MAU_XET_NGHIEM")]
        [DisplayName("MAU_XET_NGHIEM")]
        public string MAU_XET_NGHIEM { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_CHI_SO")]
        [DisplayName("MA_CHI_SO")]
        public string MA_CHI_SO { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_HSSK")]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [SolrField("MA_KQ_CAN_LAM_SANG")]
        [DisplayName("MA_KQ_CAN_LAM_SANG")]
        public string MA_KQ_CAN_LAM_SANG { get; set; }
        [DataMember]
        [SolrField("MA_KQ_CAN_LAM_SANG_DON_VI")]
        [DisplayName("MA_KQ_CAN_LAM_SANG_DON_VI")]
        public string MA_KQ_CAN_LAM_SANG_DON_VI { get; set; }
        [DataMember]
        [SolrField("MA_KQ_KCB")]
        [DisplayName("MA_KQ_KCB")]
        public string MA_KQ_KCB { get; set; }
        [DataMember]
        [SolrField("MA_MAY")]
        [DisplayName("MA_MAY")]
        public string MA_MAY { get; set; }
        [DataMember]
        [SolrField("MO_TA")]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [SolrField("NGAY_KQ")]
        [DisplayName("NGAY_KQ")]
        public DateTime? NGAY_KQ { get; set; }
        [DataMember]
        [SolrField("NHOM_DICH_VU")]
        [DisplayName("NHOM_DICH_VU")]
        public string NHOM_DICH_VU { get; set; }
        [DataMember]
        [SolrField("TEN_CHI_SO")]
        [DisplayName("TEN_CHI_SO")]
        public string TEN_CHI_SO { get; set; }
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
    public partial class EHR_KQ_KCB_CAN_LAM_SANG_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_KQ_KCB_CAN_LAM_SANG_Information Information { get; set; }

    }
    public partial class EHR_KQ_KCB_CAN_LAM_SANG_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("GIA_TRI")]
        public string GIA_TRI { get; set; }
        [DataMember]
        [DisplayName("KET_LUAN")]
        public string KET_LUAN { get; set; }
        [DataMember]
        [DisplayName("MAU_XET_NGHIEM")]
        public string MAU_XET_NGHIEM { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CHI_SO")]
        public string MA_CHI_SO { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [DisplayName("MA_KQ_CAN_LAM_SANG")]
        public string MA_KQ_CAN_LAM_SANG { get; set; }
        [DataMember]
        [DisplayName("MA_KQ_CAN_LAM_SANG_DON_VI")]
        public string MA_KQ_CAN_LAM_SANG_DON_VI { get; set; }
        [DataMember]
        [DisplayName("MA_KQ_KCB")]
        public string MA_KQ_KCB { get; set; }
        [DataMember]
        [DisplayName("MA_MAY")]
        public string MA_MAY { get; set; }
        [DataMember]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [DisplayName("NGAY_KQ")]
        public string NGAY_KQ { get; set; }
        [DataMember]
        [DisplayName("NHOM_DICH_VU")]
        public string NHOM_DICH_VU { get; set; }
        [DataMember]
        [DisplayName("TEN_CHI_SO")]
        public string TEN_CHI_SO { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_KQ_KCB_CAN_LAM_SANG_Search
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

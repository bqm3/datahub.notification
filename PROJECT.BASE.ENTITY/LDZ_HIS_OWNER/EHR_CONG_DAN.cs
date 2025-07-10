using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_CONG_DAN
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
        [SolrField("CCCD")]
        [DisplayName("CCCD")]
        public string CCCD { get; set; }
        [DataMember]
        [SolrField("CCCD_NGAY_CAP")]
        [DisplayName("CCCD_NGAY_CAP")]
        public DateTime? CCCD_NGAY_CAP { get; set; }
        [DataMember]
        [SolrField("CCCD_NOI_CAP")]
        [DisplayName("CCCD_NOI_CAP")]
        public string CCCD_NOI_CAP { get; set; }
        [DataMember]
        [SolrField("CMND")]
        [DisplayName("CMND")]
        public string CMND { get; set; }
        [DataMember]
        [SolrField("CMND_NGAY_CAP")]
        [DisplayName("CMND_NGAY_CAP")]
        public DateTime? CMND_NGAY_CAP { get; set; }
        [DataMember]
        [SolrField("CMND_NOI_CAP")]
        [DisplayName("CMND_NOI_CAP")]
        public string CMND_NOI_CAP { get; set; }
        [DataMember]
        [SolrField("DAN_TOC")]
        [DisplayName("DAN_TOC")]
        public string DAN_TOC { get; set; }
        [DataMember]
        [SolrField("GIOI_TINH")]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [SolrField("HKTT_DIA_CHI")]
        [DisplayName("HKTT_DIA_CHI")]
        public string HKTT_DIA_CHI { get; set; }
        [DataMember]
        [SolrField("HKTT_QUAN_HUYEN")]
        [DisplayName("HKTT_QUAN_HUYEN")]
        public string HKTT_QUAN_HUYEN { get; set; }
        [DataMember]
        [SolrField("HKTT_TINH_TP")]
        [DisplayName("HKTT_TINH_TP")]
        public string HKTT_TINH_TP { get; set; }
        [DataMember]
        [SolrField("HKTT_XA_PHUONG")]
        [DisplayName("HKTT_XA_PHUONG")]
        public string HKTT_XA_PHUONG { get; set; }
        [DataMember]
        [SolrField("HO_TEN")]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_HO_GD")]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [SolrField("MOI_QUAN_HE")]
        [DisplayName("MOI_QUAN_HE")]
        public string MOI_QUAN_HE { get; set; }
        [DataMember]
        [SolrField("NGAY_SINH")]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [SolrField("NGHE_NGHIEP")]
        [DisplayName("NGHE_NGHIEP")]
        public string NGHE_NGHIEP { get; set; }
        [DataMember]
        [SolrField("NHOM_MAU_HE_ABO")]
        [DisplayName("NHOM_MAU_HE_ABO")]
        public string NHOM_MAU_HE_ABO { get; set; }
        [DataMember]
        [SolrField("NHOM_MAU_HE_RH")]
        [DisplayName("NHOM_MAU_HE_RH")]
        public string NHOM_MAU_HE_RH { get; set; }
        [DataMember]
        [SolrField("NOHT_DIA_CHI")]
        [DisplayName("NOHT_DIA_CHI")]
        public string NOHT_DIA_CHI { get; set; }
        [DataMember]
        [SolrField("NOHT_QUAN_HUYEN")]
        [DisplayName("NOHT_QUAN_HUYEN")]
        public string NOHT_QUAN_HUYEN { get; set; }
        [DataMember]
        [SolrField("NOHT_TINH_TP")]
        [DisplayName("NOHT_TINH_TP")]
        public string NOHT_TINH_TP { get; set; }
        [DataMember]
        [SolrField("NOHT_XA_PHUONG")]
        [DisplayName("NOHT_XA_PHUONG")]
        public string NOHT_XA_PHUONG { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_CO_DINH")]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_DI_DONG")]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [SolrField("TON_GIAO")]
        [DisplayName("TON_GIAO")]
        public string TON_GIAO { get; set; }
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
    public partial class EHR_CONG_DAN_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_CONG_DAN_Information Information { get; set; }

    }
    public partial class EHR_CONG_DAN_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("CCCD")]
        public string CCCD { get; set; }
        [DataMember]
        [DisplayName("CCCD_NGAY_CAP")]
        public string CCCD_NGAY_CAP { get; set; }
        [DataMember]
        [DisplayName("CCCD_NOI_CAP")]
        public string CCCD_NOI_CAP { get; set; }
        [DataMember]
        [DisplayName("CMND")]
        public string CMND { get; set; }
        [DataMember]
        [DisplayName("CMND_NGAY_CAP")]
        public string CMND_NGAY_CAP { get; set; }
        [DataMember]
        [DisplayName("CMND_NOI_CAP")]
        public string CMND_NOI_CAP { get; set; }
        [DataMember]
        [DisplayName("DAN_TOC")]
        public string DAN_TOC { get; set; }
        [DataMember]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [DisplayName("HKTT_DIA_CHI")]
        public string HKTT_DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("HKTT_QUAN_HUYEN")]
        public string HKTT_QUAN_HUYEN { get; set; }
        [DataMember]
        [DisplayName("HKTT_TINH_TP")]
        public string HKTT_TINH_TP { get; set; }
        [DataMember]
        [DisplayName("HKTT_XA_PHUONG")]
        public string HKTT_XA_PHUONG { get; set; }
        [DataMember]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [DisplayName("MOI_QUAN_HE")]
        public string MOI_QUAN_HE { get; set; }
        [DataMember]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [DisplayName("NGHE_NGHIEP")]
        public string NGHE_NGHIEP { get; set; }
        [DataMember]
        [DisplayName("NHOM_MAU_HE_ABO")]
        public string NHOM_MAU_HE_ABO { get; set; }
        [DataMember]
        [DisplayName("NHOM_MAU_HE_RH")]
        public string NHOM_MAU_HE_RH { get; set; }
        [DataMember]
        [DisplayName("NOHT_DIA_CHI")]
        public string NOHT_DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("NOHT_QUAN_HUYEN")]
        public string NOHT_QUAN_HUYEN { get; set; }
        [DataMember]
        [DisplayName("NOHT_TINH_TP")]
        public string NOHT_TINH_TP { get; set; }
        [DataMember]
        [DisplayName("NOHT_XA_PHUONG")]
        public string NOHT_XA_PHUONG { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [DisplayName("TON_GIAO")]
        public string TON_GIAO { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_CONG_DAN_Search
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

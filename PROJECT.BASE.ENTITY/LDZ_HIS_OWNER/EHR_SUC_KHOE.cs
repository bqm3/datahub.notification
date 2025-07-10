using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_SUC_KHOE
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
        [SolrField("BI_NGAT_LUC_DE")]
        [DisplayName("BI_NGAT_LUC_DE")]
        public string BI_NGAT_LUC_DE { get; set; }
        [DataMember]
        [SolrField("CAN_NANG_KHI_DE")]
        [DisplayName("CAN_NANG_KHI_DE")]
        public decimal? CAN_NANG_KHI_DE { get; set; }
        [DataMember]
        [SolrField("CHIEU_DAI_KHI_DE")]
        [DisplayName("CHIEU_DAI_KHI_DE")]
        public decimal? CHIEU_DAI_KHI_DE { get; set; }
        [DataMember]
        [SolrField("DE_MO")]
        [DisplayName("DE_MO")]
        public string DE_MO { get; set; }
        [DataMember]
        [SolrField("DE_THIEU_THANG")]
        [DisplayName("DE_THIEU_THANG")]
        public string DE_THIEU_THANG { get; set; }
        [DataMember]
        [SolrField("DE_THUONG")]
        [DisplayName("DE_THUONG")]
        public string DE_THUONG { get; set; }
        [DataMember]
        [SolrField("DI_TAT_BAM_SINH")]
        [DisplayName("DI_TAT_BAM_SINH")]
        public string DI_TAT_BAM_SINH { get; set; }
        [DataMember]
        [SolrField("HOAT_DONG_THE_LUC_CO_KHONG")]
        [DisplayName("HOAT_DONG_THE_LUC_CO_KHONG")]
        public string HOAT_DONG_THE_LUC_CO_KHONG { get; set; }
        [DataMember]
        [SolrField("HOAT_DONG_THE_LUC_THUONG_XUYEN")]
        [DisplayName("HOAT_DONG_THE_LUC_THUONG_XUYEN")]
        public string HOAT_DONG_THE_LUC_THUONG_XUYEN { get; set; }
        [DataMember]
        [SolrField("HUT_THUOC_CO_KHONG")]
        [DisplayName("HUT_THUOC_CO_KHONG")]
        public string HUT_THUOC_CO_KHONG { get; set; }
        [DataMember]
        [SolrField("HUT_THUOC_DA_BO")]
        [DisplayName("HUT_THUOC_DA_BO")]
        public string HUT_THUOC_DA_BO { get; set; }
        [DataMember]
        [SolrField("HUT_THUOC_THUONG_XUYEN")]
        [DisplayName("HUT_THUOC_THUONG_XUYEN")]
        public string HUT_THUOC_THUONG_XUYEN { get; set; }
        [DataMember]
        [SolrField("LOAI_TOILET")]
        [DisplayName("LOAI_TOILET")]
        public string LOAI_TOILET { get; set; }
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
        [SolrField("MA_SUC_KHOE")]
        [DisplayName("MA_SUC_KHOE")]
        public string MA_SUC_KHOE { get; set; }
        [DataMember]
        [SolrField("MA_SUC_KHOE_DON_VI")]
        [DisplayName("MA_SUC_KHOE_DON_VI")]
        public string MA_SUC_KHOE_DON_VI { get; set; }
        [DataMember]
        [SolrField("MA_TUY_CO_KHONG")]
        [DisplayName("MA_TUY_CO_KHONG")]
        public string MA_TUY_CO_KHONG { get; set; }
        [DataMember]
        [SolrField("MA_TUY_DA_BO")]
        [DisplayName("MA_TUY_DA_BO")]
        public string MA_TUY_DA_BO { get; set; }
        [DataMember]
        [SolrField("MA_TUY_THUONG_XUYEN")]
        [DisplayName("MA_TUY_THUONG_XUYEN")]
        public string MA_TUY_THUONG_XUYEN { get; set; }
        [DataMember]
        [SolrField("NGUY_CO_KHAC")]
        [DisplayName("NGUY_CO_KHAC")]
        public string NGUY_CO_KHAC { get; set; }
        [DataMember]
        [SolrField("RUOU_BIA_CO_KHONG")]
        [DisplayName("RUOU_BIA_CO_KHONG")]
        public string RUOU_BIA_CO_KHONG { get; set; }
        [DataMember]
        [SolrField("RUOU_BIA_DA_BO")]
        [DisplayName("RUOU_BIA_DA_BO")]
        public string RUOU_BIA_DA_BO { get; set; }
        [DataMember]
        [SolrField("RUOU_BIA_SO_LY_COC_MOI_NGAY")]
        [DisplayName("RUOU_BIA_SO_LY_COC_MOI_NGAY")]
        public decimal? RUOU_BIA_SO_LY_COC_MOI_NGAY { get; set; }
        [DataMember]
        [SolrField("VAN_DE_KHAC")]
        [DisplayName("VAN_DE_KHAC")]
        public string VAN_DE_KHAC { get; set; }
        [DataMember]
        [SolrField("VERSION_XML")]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [SolrField("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        [DisplayName("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        public string YEU_TO_NGHE_NGHIEP_MOI_TRUONG { get; set; }     
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
    public partial class EHR_SUC_KHOE_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_SUC_KHOE_Information Information { get; set; }

    }
    public partial class EHR_SUC_KHOE_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("BI_NGAT_LUC_DE")]
        public string BI_NGAT_LUC_DE { get; set; }
        [DataMember]
        [DisplayName("CAN_NANG_KHI_DE")]
        public decimal? CAN_NANG_KHI_DE { get; set; }
        [DataMember]
        [DisplayName("CHIEU_DAI_KHI_DE")]
        public decimal? CHIEU_DAI_KHI_DE { get; set; }
        [DataMember]
        [DisplayName("DE_MO")]
        public string DE_MO { get; set; }
        [DataMember]
        [DisplayName("DE_THIEU_THANG")]
        public string DE_THIEU_THANG { get; set; }
        [DataMember]
        [DisplayName("DE_THUONG")]
        public string DE_THUONG { get; set; }
        [DataMember]
        [DisplayName("DI_TAT_BAM_SINH")]
        public string DI_TAT_BAM_SINH { get; set; }
        [DataMember]
        [DisplayName("HOAT_DONG_THE_LUC_CO_KHONG")]
        public string HOAT_DONG_THE_LUC_CO_KHONG { get; set; }
        [DataMember]
        [DisplayName("HOAT_DONG_THE_LUC_THUONG_XUYEN")]
        public string HOAT_DONG_THE_LUC_THUONG_XUYEN { get; set; }
        [DataMember]
        [DisplayName("HUT_THUOC_CO_KHONG")]
        public string HUT_THUOC_CO_KHONG { get; set; }
        [DataMember]
        [DisplayName("HUT_THUOC_DA_BO")]
        public string HUT_THUOC_DA_BO { get; set; }
        [DataMember]
        [DisplayName("HUT_THUOC_THUONG_XUYEN")]
        public string HUT_THUOC_THUONG_XUYEN { get; set; }
        [DataMember]
        [DisplayName("LOAI_TOILET")]
        public string LOAI_TOILET { get; set; }
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
        [DisplayName("MA_SUC_KHOE")]
        public string MA_SUC_KHOE { get; set; }
        [DataMember]
        [DisplayName("MA_SUC_KHOE_DON_VI")]
        public string MA_SUC_KHOE_DON_VI { get; set; }
        [DataMember]
        [DisplayName("MA_TUY_CO_KHONG")]
        public string MA_TUY_CO_KHONG { get; set; }
        [DataMember]
        [DisplayName("MA_TUY_DA_BO")]
        public string MA_TUY_DA_BO { get; set; }
        [DataMember]
        [DisplayName("MA_TUY_THUONG_XUYEN")]
        public string MA_TUY_THUONG_XUYEN { get; set; }
        [DataMember]
        [DisplayName("NGUY_CO_KHAC")]
        public string NGUY_CO_KHAC { get; set; }
        [DataMember]
        [DisplayName("RUOU_BIA_CO_KHONG")]
        public string RUOU_BIA_CO_KHONG { get; set; }
        [DataMember]
        [DisplayName("RUOU_BIA_DA_BO")]
        public string RUOU_BIA_DA_BO { get; set; }
        [DataMember]
        [DisplayName("RUOU_BIA_SO_LY_COC_MOI_NGAY")]
        public decimal? RUOU_BIA_SO_LY_COC_MOI_NGAY { get; set; }
        [DataMember]
        [DisplayName("VAN_DE_KHAC")]
        public string VAN_DE_KHAC { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }
        [DataMember]
        [DisplayName("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        public string YEU_TO_NGHE_NGHIEP_MOI_TRUONG { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_SUC_KHOE_Search
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

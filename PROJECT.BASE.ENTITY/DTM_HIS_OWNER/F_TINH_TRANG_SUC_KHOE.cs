using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class F_TINH_TRANG_SUC_KHOE
    {        

        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
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
        [SolrField("TT_SINH")]
        [DisplayName("TT_SINH")]
        public string TT_SINH { get; set; }
        [DataMember]
        [SolrField("CAN_NANG_KHI_DE")]
        [DisplayName("CAN_NANG_KHI_DE")]
        public string CAN_NANG_KHI_DE { get; set; }
        [DataMember]
        [SolrField("CHIEU_DAI_KHI_DE")]
        [DisplayName("CHIEU_DAI_KHI_DE")]
        public string CHIEU_DAI_KHI_DE { get; set; }
        [DataMember]
        [SolrField("DI_TAT_BAM_SINH")]
        [DisplayName("DI_TAT_BAM_SINH")]
        public string DI_TAT_BAM_SINH { get; set; }
        [DataMember]
        [SolrField("VAN_DE_KHAC")]
        [DisplayName("VAN_DE_KHAC")]
        public string VAN_DE_KHAC { get; set; }
        [DataMember]
        [SolrField("THUOC_LA")]
        [DisplayName("THUOC_LA")]
        public string THUOC_LA { get; set; }
        [DataMember]
        [SolrField("RUOU_BIA")]
        [DisplayName("RUOU_BIA")]
        public string RUOU_BIA { get; set; }
        [DataMember]
        [SolrField("MA_TUY")]
        [DisplayName("MA_TUY")]
        public string MA_TUY { get; set; }
        [DataMember]
        [SolrField("THE_DUC")]
        [DisplayName("THE_DUC")]
        public string THE_DUC { get; set; }
        [DataMember]
        [SolrField("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        [DisplayName("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        public string YEU_TO_NGHE_NGHIEP_MOI_TRUONG { get; set; }
        [DataMember]
        [SolrField("THOI_GIAN_TIEP_XUC")]
        [DisplayName("THOI_GIAN_TIEP_XUC")]
        public string THOI_GIAN_TIEP_XUC { get; set; }
        [DataMember]
        [SolrField("LOAI_TOILET")]
        [DisplayName("LOAI_TOILET")]
        public string LOAI_TOILET { get; set; }
        [DataMember]
        [SolrField("NGUY_CO_KHAC")]
        [DisplayName("NGUY_CO_KHAC")]
        public string NGUY_CO_KHAC { get; set; }
        [DataMember]
        [SolrField("MO_TA_KHUYET_TAT")]
        [DisplayName("MO_TA_KHUYET_TAT")]
        public string MO_TA_KHUYET_TAT { get; set; }
        [DataMember]
        [SolrField("BIEN_PHAP_TRANH_THAI")]
        [DisplayName("BIEN_PHAP_TRANH_THAI")]
        public string BIEN_PHAP_TRANH_THAI { get; set; }
        [DataMember]
        [SolrField("KY_THAI_CUOI")]
        [DisplayName("KY_THAI_CUOI")]
        public string KY_THAI_CUOI { get; set; }
        [DataMember]
        [SolrField("SO_LAN_CO_THAI")]
        [DisplayName("SO_LAN_CO_THAI")]
        public string SO_LAN_CO_THAI { get; set; }
        [DataMember]
        [SolrField("SO_LAN_SAY_THAI")]
        [DisplayName("SO_LAN_SAY_THAI")]
        public string SO_LAN_SAY_THAI { get; set; }
        [DataMember]
        [SolrField("SL_SINH_DE")]
        [DisplayName("SL_SINH_DE")]
        public decimal? SL_SINH_DE { get; set; }
        [DataMember]
        [SolrField("SO_CON_HIEN_SONG")]
        [DisplayName("SO_CON_HIEN_SONG")]
        public decimal? SO_CON_HIEN_SONG { get; set; }
        [DataMember]
        [SolrField("BENH_PHU_KHOA")]
        [DisplayName("BENH_PHU_KHOA")]
        public string BENH_PHU_KHOA { get; set; }     
		
       
    }    
    public partial class F_TINH_TRANG_SUC_KHOE_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public F_TINH_TRANG_SUC_KHOE_Information Information { get; set; }

    }
    public partial class F_TINH_TRANG_SUC_KHOE_Information
    {
		
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
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
        [DisplayName("TT_SINH")]
        public string TT_SINH { get; set; }
        [DataMember]
        [DisplayName("CAN_NANG_KHI_DE")]
        public string CAN_NANG_KHI_DE { get; set; }
        [DataMember]
        [DisplayName("CHIEU_DAI_KHI_DE")]
        public string CHIEU_DAI_KHI_DE { get; set; }
        [DataMember]
        [DisplayName("DI_TAT_BAM_SINH")]
        public string DI_TAT_BAM_SINH { get; set; }
        [DataMember]
        [DisplayName("VAN_DE_KHAC")]
        public string VAN_DE_KHAC { get; set; }
        [DataMember]
        [DisplayName("THUOC_LA")]
        public string THUOC_LA { get; set; }
        [DataMember]
        [DisplayName("RUOU_BIA")]
        public string RUOU_BIA { get; set; }
        [DataMember]
        [DisplayName("MA_TUY")]
        public string MA_TUY { get; set; }
        [DataMember]
        [DisplayName("THE_DUC")]
        public string THE_DUC { get; set; }
        [DataMember]
        [DisplayName("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")]
        public string YEU_TO_NGHE_NGHIEP_MOI_TRUONG { get; set; }
        [DataMember]
        [DisplayName("THOI_GIAN_TIEP_XUC")]
        public string THOI_GIAN_TIEP_XUC { get; set; }
        [DataMember]
        [DisplayName("LOAI_TOILET")]
        public string LOAI_TOILET { get; set; }
        [DataMember]
        [DisplayName("NGUY_CO_KHAC")]
        public string NGUY_CO_KHAC { get; set; }
        [DataMember]
        [DisplayName("MO_TA_KHUYET_TAT")]
        public string MO_TA_KHUYET_TAT { get; set; }
        [DataMember]
        [DisplayName("BIEN_PHAP_TRANH_THAI")]
        public string BIEN_PHAP_TRANH_THAI { get; set; }
        [DataMember]
        [DisplayName("KY_THAI_CUOI")]
        public string KY_THAI_CUOI { get; set; }
        [DataMember]
        [DisplayName("SO_LAN_CO_THAI")]
        public string SO_LAN_CO_THAI { get; set; }
        [DataMember]
        [DisplayName("SO_LAN_SAY_THAI")]
        public string SO_LAN_SAY_THAI { get; set; }
        [DataMember]
        [DisplayName("SL_SINH_DE")]
        public decimal? SL_SINH_DE { get; set; }
        [DataMember]
        [DisplayName("SO_CON_HIEN_SONG")]
        public decimal? SO_CON_HIEN_SONG { get; set; }
        [DataMember]
        [DisplayName("BENH_PHU_KHOA")]
        public string BENH_PHU_KHOA { get; set; }		
		
        
    }

    public partial class F_TINH_TRANG_SUC_KHOE_Search
    {
        
        [DataMember]        
        [DisplayName("SearchField")]
        public Dictionary<string,object> SearchField { get; set; }
       
        [DataMember]        
        [DisplayName("PageIndex")]
        public int? PageIndex { get; set; }
        [DataMember]        
        [DisplayName("PageSize")]
        public int? PageSize { get; set; }

    }

}

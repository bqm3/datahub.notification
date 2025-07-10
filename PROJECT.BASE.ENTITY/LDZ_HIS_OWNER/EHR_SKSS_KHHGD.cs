using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_SKSS_KHHGD
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
        [SolrField("BENH_PHU_KHOA")]
        [DisplayName("BENH_PHU_KHOA")]
        public string BENH_PHU_KHOA { get; set; }
        [DataMember]
        [SolrField("BIEN_PHAP_TRANH_THAI")]
        [DisplayName("BIEN_PHAP_TRANH_THAI")]
        public string BIEN_PHAP_TRANH_THAI { get; set; }
        [DataMember]
        [SolrField("KY_THAI_CUOI")]
        [DisplayName("KY_THAI_CUOI")]
        public string KY_THAI_CUOI { get; set; }
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
        [SolrField("MA_SKSS_KHHGD")]
        [DisplayName("MA_SKSS_KHHGD")]
        public string MA_SKSS_KHHGD { get; set; }
        [DataMember]
        [SolrField("MA_SKSS_KHHGD_DON_VI")]
        [DisplayName("MA_SKSS_KHHGD_DON_VI")]
        public string MA_SKSS_KHHGD_DON_VI { get; set; }
        [DataMember]
        [SolrField("SL_DE_DU_THANG")]
        [DisplayName("SL_DE_DU_THANG")]
        public decimal? SL_DE_DU_THANG { get; set; }
        [DataMember]
        [SolrField("SL_DE_KHO")]
        [DisplayName("SL_DE_KHO")]
        public decimal? SL_DE_KHO { get; set; }
        [DataMember]
        [SolrField("SL_DE_MO")]
        [DisplayName("SL_DE_MO")]
        public decimal? SL_DE_MO { get; set; }
        [DataMember]
        [SolrField("SL_DE_NON")]
        [DisplayName("SL_DE_NON")]
        public decimal? SL_DE_NON { get; set; }
        [DataMember]
        [SolrField("SL_DE_THUONG")]
        [DisplayName("SL_DE_THUONG")]
        public decimal? SL_DE_THUONG { get; set; }
        [DataMember]
        [SolrField("SL_SINH_DE")]
        [DisplayName("SL_SINH_DE")]
        public decimal? SL_SINH_DE { get; set; }
        [DataMember]
        [SolrField("SO_CON_HIEN_SONG")]
        [DisplayName("SO_CON_HIEN_SONG")]
        public decimal? SO_CON_HIEN_SONG { get; set; }
        [DataMember]
        [SolrField("SO_LAN_CO_THAI")]
        [DisplayName("SO_LAN_CO_THAI")]
        public string SO_LAN_CO_THAI { get; set; }
        [DataMember]
        [SolrField("SO_LAN_PHA_THAI")]
        [DisplayName("SO_LAN_PHA_THAI")]
        public string SO_LAN_PHA_THAI { get; set; }
        [DataMember]
        [SolrField("SO_LAN_SAY_THAI")]
        [DisplayName("SO_LAN_SAY_THAI")]
        public string SO_LAN_SAY_THAI { get; set; }
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
    public partial class EHR_SKSS_KHHGD_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_SKSS_KHHGD_Information Information { get; set; }

    }
    public partial class EHR_SKSS_KHHGD_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("BENH_PHU_KHOA")]
        public string BENH_PHU_KHOA { get; set; }
        [DataMember]
        [DisplayName("BIEN_PHAP_TRANH_THAI")]
        public string BIEN_PHAP_TRANH_THAI { get; set; }
        [DataMember]
        [DisplayName("KY_THAI_CUOI")]
        public string KY_THAI_CUOI { get; set; }
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
        [DisplayName("MA_SKSS_KHHGD")]
        public string MA_SKSS_KHHGD { get; set; }
        [DataMember]
        [DisplayName("MA_SKSS_KHHGD_DON_VI")]
        public string MA_SKSS_KHHGD_DON_VI { get; set; }
        [DataMember]
        [DisplayName("SL_DE_DU_THANG")]
        public decimal? SL_DE_DU_THANG { get; set; }
        [DataMember]
        [DisplayName("SL_DE_KHO")]
        public decimal? SL_DE_KHO { get; set; }
        [DataMember]
        [DisplayName("SL_DE_MO")]
        public decimal? SL_DE_MO { get; set; }
        [DataMember]
        [DisplayName("SL_DE_NON")]
        public decimal? SL_DE_NON { get; set; }
        [DataMember]
        [DisplayName("SL_DE_THUONG")]
        public decimal? SL_DE_THUONG { get; set; }
        [DataMember]
        [DisplayName("SL_SINH_DE")]
        public decimal? SL_SINH_DE { get; set; }
        [DataMember]
        [DisplayName("SO_CON_HIEN_SONG")]
        public decimal? SO_CON_HIEN_SONG { get; set; }
        [DataMember]
        [DisplayName("SO_LAN_CO_THAI")]
        public string SO_LAN_CO_THAI { get; set; }
        [DataMember]
        [DisplayName("SO_LAN_PHA_THAI")]
        public string SO_LAN_PHA_THAI { get; set; }
        [DataMember]
        [DisplayName("SO_LAN_SAY_THAI")]
        public string SO_LAN_SAY_THAI { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_SKSS_KHHGD_Search
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

using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_TIEM_CHUNG
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
        [SolrField("DA_CHUNG_NGUA_NGAY")]
        [DisplayName("DA_CHUNG_NGUA_NGAY")]
        public DateTime? DA_CHUNG_NGUA_NGAY { get; set; }
        [DataMember]
        [SolrField("LAN_TIEM_CHUNG")]
        [DisplayName("LAN_TIEM_CHUNG")]
        public decimal? LAN_TIEM_CHUNG { get; set; }
        [DataMember]
        [SolrField("LOAI_TIEM_CHUNG")]
        [DisplayName("LOAI_TIEM_CHUNG")]
        public string LOAI_TIEM_CHUNG { get; set; }
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
        [SolrField("MA_LOAI_TIEM_CHUNG")]
        [DisplayName("MA_LOAI_TIEM_CHUNG")]
        public string MA_LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [SolrField("MA_VAC_XIN")]
        [DisplayName("MA_VAC_XIN")]
        public string MA_VAC_XIN { get; set; }
        [DataMember]
        [SolrField("PHAN_UNG")]
        [DisplayName("PHAN_UNG")]
        public string PHAN_UNG { get; set; }
        [DataMember]
        [SolrField("THANG_THAI")]
        [DisplayName("THANG_THAI")]
        public decimal? THANG_THAI { get; set; }
        [DataMember]
        [SolrField("VAC_XIN")]
        [DisplayName("VAC_XIN")]
        public string VAC_XIN { get; set; }
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
    public partial class EHR_TIEM_CHUNG_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_TIEM_CHUNG_Information Information { get; set; }

    }
    public partial class EHR_TIEM_CHUNG_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("DA_CHUNG_NGUA_NGAY")]
        public string DA_CHUNG_NGUA_NGAY { get; set; }
        [DataMember]
        [DisplayName("LAN_TIEM_CHUNG")]
        public decimal? LAN_TIEM_CHUNG { get; set; }
        [DataMember]
        [DisplayName("LOAI_TIEM_CHUNG")]
        public string LOAI_TIEM_CHUNG { get; set; }
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
        [DisplayName("MA_LOAI_TIEM_CHUNG")]
        public string MA_LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [DisplayName("MA_VAC_XIN")]
        public string MA_VAC_XIN { get; set; }
        [DataMember]
        [DisplayName("PHAN_UNG")]
        public string PHAN_UNG { get; set; }
        [DataMember]
        [DisplayName("THANG_THAI")]
        public decimal? THANG_THAI { get; set; }
        [DataMember]
        [DisplayName("VAC_XIN")]
        public string VAC_XIN { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_TIEM_CHUNG_Search
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

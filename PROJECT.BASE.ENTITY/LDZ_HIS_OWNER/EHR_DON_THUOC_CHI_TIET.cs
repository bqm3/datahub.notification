using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_DON_THUOC_CHI_TIET
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
        [SolrField("DON_VI_TINH")]
        [DisplayName("DON_VI_TINH")]
        public string DON_VI_TINH { get; set; }
        [DataMember]
        [SolrField("HAM_LUONG")]
        [DisplayName("HAM_LUONG")]
        public string HAM_LUONG { get; set; }
        [DataMember]
        [SolrField("LIEU_DUNG")]
        [DisplayName("LIEU_DUNG")]
        public string LIEU_DUNG { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_DON_THUOC")]
        [DisplayName("MA_DON_THUOC")]
        public string MA_DON_THUOC { get; set; }
        [DataMember]
        [SolrField("MA_DON_THUOC_CHI_TIET")]
        [DisplayName("MA_DON_THUOC_CHI_TIET")]
        public string MA_DON_THUOC_CHI_TIET { get; set; }
        [DataMember]
        [SolrField("MA_HSSK")]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [SolrField("MA_THUOC")]
        [DisplayName("MA_THUOC")]
        public string MA_THUOC { get; set; }
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
    public partial class EHR_DON_THUOC_CHI_TIET_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_DON_THUOC_CHI_TIET_Information Information { get; set; }

    }
    public partial class EHR_DON_THUOC_CHI_TIET_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("DON_VI_TINH")]
        public string DON_VI_TINH { get; set; }
        [DataMember]
        [DisplayName("HAM_LUONG")]
        public string HAM_LUONG { get; set; }
        [DataMember]
        [DisplayName("LIEU_DUNG")]
        public string LIEU_DUNG { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_DON_THUOC")]
        public string MA_DON_THUOC { get; set; }
        [DataMember]
        [DisplayName("MA_DON_THUOC_CHI_TIET")]
        public string MA_DON_THUOC_CHI_TIET { get; set; }
        [DataMember]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [DisplayName("MA_THUOC")]
        public string MA_THUOC { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_DON_THUOC_CHI_TIET_Search
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

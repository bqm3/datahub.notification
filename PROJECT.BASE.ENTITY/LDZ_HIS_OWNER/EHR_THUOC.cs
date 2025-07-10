using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_THUOC
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
        [SolrField("DUONG_DUNG")]
        [DisplayName("DUONG_DUNG")]
        public string DUONG_DUNG { get; set; }
        [DataMember]
        [SolrField("HOAT_CHAT")]
        [DisplayName("HOAT_CHAT")]
        public string HOAT_CHAT { get; set; }
        [DataMember]
        [SolrField("MA_LOAI_THUOC")]
        [DisplayName("MA_LOAI_THUOC")]
        public string MA_LOAI_THUOC { get; set; }
        [DataMember]
        [SolrField("MA_THUOC")]
        [DisplayName("MA_THUOC")]
        public string MA_THUOC { get; set; }
        [DataMember]
        [SolrField("MA_THUOC_DON_VI")]
        [DisplayName("MA_THUOC_DON_VI")]
        public string MA_THUOC_DON_VI { get; set; }
        [DataMember]
        [SolrField("SO_DANG_KY")]
        [DisplayName("SO_DANG_KY")]
        public string SO_DANG_KY { get; set; }
        [DataMember]
        [SolrField("TEN_THUOC")]
        [DisplayName("TEN_THUOC")]
        public string TEN_THUOC { get; set; }
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
    public partial class EHR_THUOC_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_THUOC_Information Information { get; set; }

    }
    public partial class EHR_THUOC_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("DUONG_DUNG")]
        public string DUONG_DUNG { get; set; }
        [DataMember]
        [DisplayName("HOAT_CHAT")]
        public string HOAT_CHAT { get; set; }
        [DataMember]
        [DisplayName("MA_LOAI_THUOC")]
        public string MA_LOAI_THUOC { get; set; }
        [DataMember]
        [DisplayName("MA_THUOC")]
        public string MA_THUOC { get; set; }
        [DataMember]
        [DisplayName("MA_THUOC_DON_VI")]
        public string MA_THUOC_DON_VI { get; set; }
        [DataMember]
        [DisplayName("SO_DANG_KY")]
        public string SO_DANG_KY { get; set; }
        [DataMember]
        [DisplayName("TEN_THUOC")]
        public string TEN_THUOC { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_THUOC_Search
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

using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_BENH_TAT
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
        [SolrField("BIEU_HIEN")]
        [DisplayName("BIEU_HIEN")]
        public string BIEU_HIEN { get; set; }
        [DataMember]
        [SolrField("GHI_CHU")]
        [DisplayName("GHI_CHU")]
        public string GHI_CHU { get; set; }
        [DataMember]
        [SolrField("LOAI_BENH_TAT")]
        [DisplayName("LOAI_BENH_TAT")]
        public string LOAI_BENH_TAT { get; set; }
        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_BENH_TAT")]
        [DisplayName("MA_BENH_TAT")]
        public string MA_BENH_TAT { get; set; }
        [DataMember]
        [SolrField("MA_BENH_TAT_DON_VI")]
        [DisplayName("MA_BENH_TAT_DON_VI")]
        public string MA_BENH_TAT_DON_VI { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_HSSK")]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [SolrField("MO_TA")]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [SolrField("MUC_DO")]
        [DisplayName("MUC_DO")]
        public string MUC_DO { get; set; }
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
    public partial class EHR_BENH_TAT_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_BENH_TAT_Information Information { get; set; }

    }
    public partial class EHR_BENH_TAT_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("BIEU_HIEN")]
        public string BIEU_HIEN { get; set; }
        [DataMember]
        [DisplayName("GHI_CHU")]
        public string GHI_CHU { get; set; }
        [DataMember]
        [DisplayName("LOAI_BENH_TAT")]
        public string LOAI_BENH_TAT { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_TAT")]
        public string MA_BENH_TAT { get; set; }
        [DataMember]
        [DisplayName("MA_BENH_TAT_DON_VI")]
        public string MA_BENH_TAT_DON_VI { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_HSSK")]
        public string MA_HSSK { get; set; }
        [DataMember]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [DisplayName("MUC_DO")]
        public string MUC_DO { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_BENH_TAT_Search
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

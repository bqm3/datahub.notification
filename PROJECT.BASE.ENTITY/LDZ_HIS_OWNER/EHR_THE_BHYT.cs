using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_THE_BHYT
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
        [SolrField("GIOI_TINH")]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [SolrField("HO_TEN")]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [SolrField("KICH_HOAT")]
        [DisplayName("KICH_HOAT")]
        public string KICH_HOAT { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_THE_BHYT")]
        [DisplayName("MA_THE_BHYT")]
        public string MA_THE_BHYT { get; set; }
        [DataMember]
        [SolrField("NGAY_SINH")]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [SolrField("NOI_DK_KCBBD")]
        [DisplayName("NOI_DK_KCBBD")]
        public string NOI_DK_KCBBD { get; set; }
        [DataMember]
        [SolrField("SO_THE_BHYT")]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
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
    public partial class EHR_THE_BHYT_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_THE_BHYT_Information Information { get; set; }

    }
    public partial class EHR_THE_BHYT_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [DisplayName("KICH_HOAT")]
        public string KICH_HOAT { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_THE_BHYT")]
        public string MA_THE_BHYT { get; set; }
        [DataMember]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [DisplayName("NOI_DK_KCBBD")]
        public string NOI_DK_KCBBD { get; set; }
        [DataMember]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_THE_BHYT_Search
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

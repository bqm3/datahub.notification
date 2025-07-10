using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class EHR_HO_GIA_DINH
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
        [SolrField("DIA_CHI")]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [SolrField("MA_HO_GD")]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_CO_DINH")]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_DI_DONG")]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [SolrField("TEN_HO_GD")]
        [DisplayName("TEN_HO_GD")]
        public string TEN_HO_GD { get; set; }
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
    public partial class EHR_HO_GIA_DINH_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public EHR_HO_GIA_DINH_Information Information { get; set; }

    }
    public partial class EHR_HO_GIA_DINH_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [DisplayName("TEN_HO_GD")]
        public string TEN_HO_GD { get; set; }
        [DataMember]
        [DisplayName("VERSION_XML")]
        public string VERSION_XML { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class EHR_HO_GIA_DINH_Search
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

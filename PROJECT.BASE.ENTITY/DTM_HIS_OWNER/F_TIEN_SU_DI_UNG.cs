using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class F_TIEN_SU_DI_UNG
    {
        

        [DataMember]
        [SolrField("MA_BENH_NHAN")]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_DI_UNG")]
        [DisplayName("MA_DI_UNG")]
        public string MA_DI_UNG { get; set; }
        [DataMember]
        [SolrField("LOAI_DI_UNG")]
        [DisplayName("LOAI_DI_UNG")]
        public string LOAI_DI_UNG { get; set; }
        [DataMember]
        [SolrField("MUC_DO")]
        [DisplayName("MUC_DO")]
        public string MUC_DO { get; set; }
        [DataMember]
        [SolrField("BIEU_HIEN")]
        [DisplayName("BIEU_HIEN")]
        public string BIEU_HIEN { get; set; }
        [DataMember]
        [SolrField("MO_TA")]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [SolrField("GHI_CHU")]
        [DisplayName("GHI_CHU")]
        public string GHI_CHU { get; set; }     
		
       
    }    
    public partial class F_TIEN_SU_DI_UNG_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public F_TIEN_SU_DI_UNG_Information Information { get; set; }

    }
    public partial class F_TIEN_SU_DI_UNG_Information
    {
		

        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_DI_UNG")]
        public string MA_DI_UNG { get; set; }
        [DataMember]
        [DisplayName("LOAI_DI_UNG")]
        public string LOAI_DI_UNG { get; set; }
        [DataMember]
        [DisplayName("MUC_DO")]
        public string MUC_DO { get; set; }
        [DataMember]
        [DisplayName("BIEU_HIEN")]
        public string BIEU_HIEN { get; set; }
        [DataMember]
        [DisplayName("MO_TA")]
        public string MO_TA { get; set; }
        [DataMember]
        [DisplayName("GHI_CHU")]
        public string GHI_CHU { get; set; }		
		
	
        
    }

    public partial class F_TIEN_SU_DI_UNG_Search
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

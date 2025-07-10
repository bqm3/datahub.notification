using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class F_TONG_QUAN
    {        

        [DataMember]
        [SolrField("DIA_CHI")]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI")]
        [DisplayName("SO_DIEN_THOAI")]
        public string SO_DIEN_THOAI { get; set; }
        [DataMember]
        [SolrField("EMAIL")]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [SolrField("TUOI")]
        [DisplayName("TUOI")]
        public string TUOI { get; set; }
        [DataMember]
        [SolrField("NHOM_MAU")]
        [DisplayName("NHOM_MAU")]
        public string NHOM_MAU { get; set; }
        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("SO_THE_BHYT")]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
        [DataMember]
        [SolrField("HO_TEN")]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [SolrField("GIOI_TINH")]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [SolrField("NGAY_SINH")]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [SolrField("CHIEU_CAO")]
        [DisplayName("CHIEU_CAO")]
        public string CHIEU_CAO { get; set; }
        [DataMember]
        [SolrField("CAN_NANG")]
        [DisplayName("CAN_NANG")]
        public string CAN_NANG { get; set; }
        [DataMember]
        [SolrField("HUYET_AP_TAM_THU")]
        [DisplayName("HUYET_AP_TAM_THU")]
        public string HUYET_AP_TAM_THU { get; set; }
        [DataMember]
        [SolrField("NHIP_THO")]
        [DisplayName("NHIP_THO")]
        public string NHIP_THO { get; set; }
        [DataMember]
        [SolrField("NHIET_DO")]
        [DisplayName("NHIET_DO")]
        public string NHIET_DO { get; set; }
        [DataMember]
        [SolrField("NHIP_TIM")]
        [DisplayName("NHIP_TIM")]
        public string NHIP_TIM { get; set; }
        [DataMember]
        [SolrField("BMI")]
        [DisplayName("BMI")]
        public string BMI { get; set; }
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
		
       
    }    
    public partial class F_TONG_QUAN_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public F_TONG_QUAN_Information Information { get; set; }

    }
    public partial class F_TONG_QUAN_Information
    {		

        [DataMember]
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI")]
        public string SO_DIEN_THOAI { get; set; }
        [DataMember]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [DisplayName("TUOI")]
        public string TUOI { get; set; }
        [DataMember]
        [DisplayName("NHOM_MAU")]
        public string NHOM_MAU { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
        [DataMember]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [DisplayName("CHIEU_CAO")]
        public string CHIEU_CAO { get; set; }
        [DataMember]
        [DisplayName("CAN_NANG")]
        public string CAN_NANG { get; set; }
        [DataMember]
        [DisplayName("HUYET_AP_TAM_THU")]
        public string HUYET_AP_TAM_THU { get; set; }
        [DataMember]
        [DisplayName("NHIP_THO")]
        public string NHIP_THO { get; set; }
        [DataMember]
        [DisplayName("NHIET_DO")]
        public string NHIET_DO { get; set; }
        [DataMember]
        [DisplayName("NHIP_TIM")]
        public string NHIP_TIM { get; set; }
        [DataMember]
        [DisplayName("BMI")]
        public string BMI { get; set; }
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
		
        
    }

    public partial class F_TONG_QUAN_Search
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

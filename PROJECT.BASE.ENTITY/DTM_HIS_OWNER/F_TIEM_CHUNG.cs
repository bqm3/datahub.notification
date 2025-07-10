using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class F_TIEM_CHUNG
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
        [SolrField("MA_LOAI_TIEM_CHUNG")]
        [DisplayName("MA_LOAI_TIEM_CHUNG")]
        public string MA_LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [SolrField("LOAI_TIEM_CHUNG")]
        [DisplayName("LOAI_TIEM_CHUNG")]
        public string LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [SolrField("VAC_XIN")]
        [DisplayName("VAC_XIN")]
        public string VAC_XIN { get; set; }
        [DataMember]
        [SolrField("DA_CHUNG_NGUA_NGAY")]
        [DisplayName("DA_CHUNG_NGUA_NGAY")]
        public DateTime? DA_CHUNG_NGUA_NGAY { get; set; }
        [DataMember]
        [SolrField("PHAN_UNG")]
        [DisplayName("PHAN_UNG")]
        public string PHAN_UNG { get; set; }     
		
       
    }    
    public partial class F_TIEM_CHUNG_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public F_TIEM_CHUNG_Information Information { get; set; }

    }
    public partial class F_TIEM_CHUNG_Information
    {
		
        [DataMember]
        [DisplayName("MA_BENH_NHAN")]
        public string MA_BENH_NHAN { get; set; }
        [DataMember]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_LOAI_TIEM_CHUNG")]
        public string MA_LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [DisplayName("LOAI_TIEM_CHUNG")]
        public string LOAI_TIEM_CHUNG { get; set; }
        [DataMember]
        [DisplayName("VAC_XIN")]
        public string VAC_XIN { get; set; }
        [DataMember]
        [DisplayName("DA_CHUNG_NGUA_NGAY")]
        public string DA_CHUNG_NGUA_NGAY { get; set; }
        [DataMember]
        [DisplayName("PHAN_UNG")]
        public string PHAN_UNG { get; set; }		
		
        
    }

    public partial class F_TIEM_CHUNG_Search
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

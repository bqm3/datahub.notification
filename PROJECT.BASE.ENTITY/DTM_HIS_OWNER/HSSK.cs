using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class HSSK
    {
        [DataMember]
        [DisplayName("EID")]
        public string EID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public HSSK_Information Information { get; set; }

    }
    public partial class HSSK_Information
    {
        public F_TONG_QUAN TONG_QUAN { get; set; }
        public F_HANH_CHINH HANH_CHINH { get; set; }        
        public F_TIEM_CHUNG TIEM_CHUNG { get; set; }
        public F_TIEN_SU_BENH_TAT TIEN_SU_BENH_TAT { get; set; }
        public F_TIEN_SU_DI_UNG TIEN_SU_DI_UNG { get; set; }        
        public F_TINH_TRANG_SUC_KHOE TINH_TRANG_SUC_KHOE { get; set; }

    }   

}

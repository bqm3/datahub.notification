using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class F_HANH_CHINH
    {

        [DataMember]
        [SolrField("MA_CONG_DAN")]
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [SolrField("MA_HO_GD")]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [SolrField("HO_TEN")]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [SolrField("MOI_QUAN_HE_CH")]
        [DisplayName("MOI_QUAN_HE_CH")]
        public string MOI_QUAN_HE_CH { get; set; }
        [DataMember]
        [SolrField("GIOI_TINH")]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [SolrField("NHOM_MAU")]
        [DisplayName("NHOM_MAU")]
        public string NHOM_MAU { get; set; }
        [DataMember]
        [SolrField("NGAY_SINH")]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [SolrField("DAN_TOC")]
        [DisplayName("DAN_TOC")]
        public string DAN_TOC { get; set; }
        [DataMember]
        [SolrField("QUOC_TICH")]
        [DisplayName("QUOC_TICH")]
        public string QUOC_TICH { get; set; }
        [DataMember]
        [SolrField("TON_GIAO")]
        [DisplayName("TON_GIAO")]
        public string TON_GIAO { get; set; }
        [DataMember]
        [SolrField("NGHE_NGHIEP")]
        [DisplayName("NGHE_NGHIEP")]
        public string NGHE_NGHIEP { get; set; }
        [DataMember]
        [SolrField("CMND")]
        [DisplayName("CMND")]
        public string CMND { get; set; }
        [DataMember]
        [SolrField("CMND_NGAY_CAP")]
        [DisplayName("CMND_NGAY_CAP")]
        public DateTime? CMND_NGAY_CAP { get; set; }
        [DataMember]
        [SolrField("CMND_NOI_CAP")]
        [DisplayName("CMND_NOI_CAP")]
        public string CMND_NOI_CAP { get; set; }
        [DataMember]
        [SolrField("CCCD")]
        [DisplayName("CCCD")]
        public string CCCD { get; set; }
        [DataMember]
        [SolrField("CCCD_NGAY_CAP")]
        [DisplayName("CCCD_NGAY_CAP")]
        public DateTime? CCCD_NGAY_CAP { get; set; }
        [DataMember]
        [SolrField("CCCD_NOI_CAP")]
        [DisplayName("CCCD_NOI_CAP")]
        public string CCCD_NOI_CAP { get; set; }
        [DataMember]
        [SolrField("SO_THE_BHYT")]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
        [DataMember]
        [SolrField("HKTT_DIA_CHI")]
        [DisplayName("HKTT_DIA_CHI")]
        public string HKTT_DIA_CHI { get; set; }
        [DataMember]
        [SolrField("HKTT_XA_PHUONG")]
        [DisplayName("HKTT_XA_PHUONG")]
        public string HKTT_XA_PHUONG { get; set; }
        [DataMember]
        [SolrField("HKTT_QUAN_HUYEN")]
        [DisplayName("HKTT_QUAN_HUYEN")]
        public string HKTT_QUAN_HUYEN { get; set; }
        [DataMember]
        [SolrField("HKTT_TINH_TP")]
        [DisplayName("HKTT_TINH_TP")]
        public string HKTT_TINH_TP { get; set; }
        [DataMember]
        [SolrField("NOHT_DIA_CHI")]
        [DisplayName("NOHT_DIA_CHI")]
        public string NOHT_DIA_CHI { get; set; }
        [DataMember]
        [SolrField("NOHT_XA_PHUONG")]
        [DisplayName("NOHT_XA_PHUONG")]
        public string NOHT_XA_PHUONG { get; set; }
        [DataMember]
        [SolrField("NOHT_QUAN_HUYEN")]
        [DisplayName("NOHT_QUAN_HUYEN")]
        public string NOHT_QUAN_HUYEN { get; set; }
        [DataMember]
        [SolrField("NOHT_TINH_TP")]
        [DisplayName("NOHT_TINH_TP")]
        public string NOHT_TINH_TP { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_CO_DINH")]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [SolrField("SO_DIEN_THOAI_DI_DONG")]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [SolrField("EMAIL")]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [SolrField("HO_TEN_BO")]
        [DisplayName("HO_TEN_BO")]
        public string HO_TEN_BO { get; set; }
        [DataMember]
        [SolrField("HO_TEN_ME")]
        [DisplayName("HO_TEN_ME")]
        public string HO_TEN_ME { get; set; }
        [DataMember]
        [SolrField("HO_TEN_NCSC")]
        [DisplayName("HO_TEN_NCSC")]
        public string HO_TEN_NCSC { get; set; }
        [DataMember]
        [SolrField("MOI_QUAN_HE")]
        [DisplayName("MOI_QUAN_HE")]
        public string MOI_QUAN_HE { get; set; }
        [DataMember]
        [SolrField("SO_DTCD")]
        [DisplayName("SO_DTCD")]
        public string SO_DTCD { get; set; }
        [DataMember]
        [SolrField("DTDD")]
        [DisplayName("DTDD")]
        public string DTDD { get; set; }


    }
    public partial class F_HANH_CHINH_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public F_HANH_CHINH_Information Information { get; set; }

    }
    public partial class F_HANH_CHINH_Information
    {

        [DataMember]        
        [DisplayName("MA_CONG_DAN")]
        public string MA_CONG_DAN { get; set; }
        [DataMember]
        [DisplayName("MA_HO_GD")]
        public string MA_HO_GD { get; set; }
        [DataMember]
        [DisplayName("HO_TEN")]
        public string HO_TEN { get; set; }
        [DataMember]
        [DisplayName("MOI_QUAN_HE_CH")]
        public string MOI_QUAN_HE_CH { get; set; }
        [DataMember]
        [DisplayName("GIOI_TINH")]
        public string GIOI_TINH { get; set; }
        [DataMember]
        [DisplayName("NHOM_MAU")]
        public string NHOM_MAU { get; set; }
        [DataMember]
        [DisplayName("NGAY_SINH")]
        public string NGAY_SINH { get; set; }
        [DataMember]
        [DisplayName("DAN_TOC")]
        public string DAN_TOC { get; set; }
        [DataMember]
        [DisplayName("QUOC_TICH")]
        public string QUOC_TICH { get; set; }
        [DataMember]
        [DisplayName("TON_GIAO")]
        public string TON_GIAO { get; set; }
        [DataMember]
        [DisplayName("NGHE_NGHIEP")]
        public string NGHE_NGHIEP { get; set; }
        [DataMember]
        [DisplayName("CMND")]
        public string CMND { get; set; }
        [DataMember]
        [DisplayName("CMND_NGAY_CAP")]
        public string CMND_NGAY_CAP { get; set; }
        [DataMember]
        [DisplayName("CMND_NOI_CAP")]
        public string CMND_NOI_CAP { get; set; }
        [DataMember]
        [DisplayName("CCCD")]
        public string CCCD { get; set; }
        [DataMember]
        [DisplayName("CCCD_NGAY_CAP")]
        public string CCCD_NGAY_CAP { get; set; }
        [DataMember]
        [DisplayName("CCCD_NOI_CAP")]
        public string CCCD_NOI_CAP { get; set; }
        [DataMember]
        [DisplayName("SO_THE_BHYT")]
        public string SO_THE_BHYT { get; set; }
        [DataMember]
        [DisplayName("HKTT_DIA_CHI")]
        public string HKTT_DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("HKTT_XA_PHUONG")]
        public string HKTT_XA_PHUONG { get; set; }
        [DataMember]
        [DisplayName("HKTT_QUAN_HUYEN")]
        public string HKTT_QUAN_HUYEN { get; set; }
        [DataMember]
        [DisplayName("HKTT_TINH_TP")]
        public string HKTT_TINH_TP { get; set; }
        [DataMember]
        [DisplayName("NOHT_DIA_CHI")]
        public string NOHT_DIA_CHI { get; set; }
        [DataMember]
        [DisplayName("NOHT_XA_PHUONG")]
        public string NOHT_XA_PHUONG { get; set; }
        [DataMember]
        [DisplayName("NOHT_QUAN_HUYEN")]
        public string NOHT_QUAN_HUYEN { get; set; }
        [DataMember]
        [DisplayName("NOHT_TINH_TP")]
        public string NOHT_TINH_TP { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_CO_DINH")]
        public string SO_DIEN_THOAI_CO_DINH { get; set; }
        [DataMember]
        [DisplayName("SO_DIEN_THOAI_DI_DONG")]
        public string SO_DIEN_THOAI_DI_DONG { get; set; }
        [DataMember]
        [DisplayName("EMAIL")]
        public string EMAIL { get; set; }
        [DataMember]
        [DisplayName("HO_TEN_BO")]
        public string HO_TEN_BO { get; set; }
        [DataMember]
        [DisplayName("HO_TEN_ME")]
        public string HO_TEN_ME { get; set; }
        [DataMember]
        [DisplayName("HO_TEN_NCSC")]
        public string HO_TEN_NCSC { get; set; }
        [DataMember]
        [DisplayName("MOI_QUAN_HE")]
        public string MOI_QUAN_HE { get; set; }
        [DataMember]
        [DisplayName("SO_DTCD")]
        public string SO_DTCD { get; set; }
        [DataMember]
        [DisplayName("DTDD")]
        public string DTDD { get; set; }


    }

    public partial class F_HANH_CHINH_Search
    {

        [DataMember]
        [DisplayName("SearchField")]
        public Dictionary<string, object> SearchField { get; set; }

        [DataMember]
        [DisplayName("PageIndex")]
        public int? PageIndex { get; set; }
        [DataMember]
        [DisplayName("PageSize")]
        public int? PageSize { get; set; }

    }

}

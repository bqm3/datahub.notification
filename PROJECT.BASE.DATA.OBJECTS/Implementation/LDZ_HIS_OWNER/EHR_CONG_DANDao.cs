using System;
using System.Collections.Generic;
using SolrNet;
using PROJECT.BASE.ENTITY;
using PROJECT.BASE.CORE;
using PROJECT.BASE.CORE.Solr;
using Lib.Utility;
using Lib.Setting;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;

namespace PROJECT.BASE.DAO
{    
    public class EHR_CONG_DANDao : OracleBaseImpl<EHR_CONG_DAN>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_CONG_DAN";
            PackageName = "PK_EHR_CONG_DAN";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_CONG_DAN_Information info)
        {        
			var paramValue = new EHR_CONG_DAN
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                CCCD = (info != null && !string.IsNullOrEmpty(info.CCCD)) ? info.CCCD : null,
                CCCD_NGAY_CAP = (info != null && !string.IsNullOrEmpty(info.CCCD_NGAY_CAP)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.CCCD_NGAY_CAP, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                CCCD_NOI_CAP = (info != null && !string.IsNullOrEmpty(info.CCCD_NOI_CAP)) ? info.CCCD_NOI_CAP : null,
                CMND = (info != null && !string.IsNullOrEmpty(info.CMND)) ? info.CMND : null,
                CMND_NGAY_CAP = (info != null && !string.IsNullOrEmpty(info.CMND_NGAY_CAP)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.CMND_NGAY_CAP, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                CMND_NOI_CAP = (info != null && !string.IsNullOrEmpty(info.CMND_NOI_CAP)) ? info.CMND_NOI_CAP : null,
                DAN_TOC = (info != null && !string.IsNullOrEmpty(info.DAN_TOC)) ? info.DAN_TOC : null,
                GIOI_TINH = (info != null && !string.IsNullOrEmpty(info.GIOI_TINH)) ? info.GIOI_TINH : null,
                HKTT_DIA_CHI = (info != null && !string.IsNullOrEmpty(info.HKTT_DIA_CHI)) ? info.HKTT_DIA_CHI : null,
                HKTT_QUAN_HUYEN = (info != null && !string.IsNullOrEmpty(info.HKTT_QUAN_HUYEN)) ? info.HKTT_QUAN_HUYEN : null,
                HKTT_TINH_TP = (info != null && !string.IsNullOrEmpty(info.HKTT_TINH_TP)) ? info.HKTT_TINH_TP : null,
                HKTT_XA_PHUONG = (info != null && !string.IsNullOrEmpty(info.HKTT_XA_PHUONG)) ? info.HKTT_XA_PHUONG : null,
                HO_TEN = (info != null && !string.IsNullOrEmpty(info.HO_TEN)) ? info.HO_TEN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HO_GD = (info != null && !string.IsNullOrEmpty(info.MA_HO_GD)) ? info.MA_HO_GD : null,
                MOI_QUAN_HE = (info != null && !string.IsNullOrEmpty(info.MOI_QUAN_HE)) ? info.MOI_QUAN_HE : null,
                NGAY_SINH = (info != null && !string.IsNullOrEmpty(info.NGAY_SINH)) ? info.NGAY_SINH : null,
                NGHE_NGHIEP = (info != null && !string.IsNullOrEmpty(info.NGHE_NGHIEP)) ? info.NGHE_NGHIEP : null,
                NHOM_MAU_HE_ABO = (info != null && !string.IsNullOrEmpty(info.NHOM_MAU_HE_ABO)) ? info.NHOM_MAU_HE_ABO : null,
                NHOM_MAU_HE_RH = (info != null && !string.IsNullOrEmpty(info.NHOM_MAU_HE_RH)) ? info.NHOM_MAU_HE_RH : null,
                NOHT_DIA_CHI = (info != null && !string.IsNullOrEmpty(info.NOHT_DIA_CHI)) ? info.NOHT_DIA_CHI : null,
                NOHT_QUAN_HUYEN = (info != null && !string.IsNullOrEmpty(info.NOHT_QUAN_HUYEN)) ? info.NOHT_QUAN_HUYEN : null,
                NOHT_TINH_TP = (info != null && !string.IsNullOrEmpty(info.NOHT_TINH_TP)) ? info.NOHT_TINH_TP : null,
                NOHT_XA_PHUONG = (info != null && !string.IsNullOrEmpty(info.NOHT_XA_PHUONG)) ? info.NOHT_XA_PHUONG : null,
                SO_DIEN_THOAI_CO_DINH = (info != null && !string.IsNullOrEmpty(info.SO_DIEN_THOAI_CO_DINH)) ? info.SO_DIEN_THOAI_CO_DINH : null,
                SO_DIEN_THOAI_DI_DONG = (info != null && !string.IsNullOrEmpty(info.SO_DIEN_THOAI_DI_DONG)) ? info.SO_DIEN_THOAI_DI_DONG : null,
                TON_GIAO = (info != null && !string.IsNullOrEmpty(info.TON_GIAO)) ? info.TON_GIAO : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_CONG_DAN> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_CCCD_NGAY_CAP_START", (dictParam != null && dictParam.ContainsKey("CCCD_NGAY_CAP_START")) ? dictParam["CCCD_NGAY_CAP_START"] : null),
                new OracleParameter("p_CCCD_NGAY_CAP_END", (dictParam != null && dictParam.ContainsKey("CCCD_NGAY_CAP_END")) ? dictParam["CCCD_NGAY_CAP_END"] : null),
                new OracleParameter("p_CCCD_NOI_CAP",(dictParam != null && dictParam.ContainsKey("CCCD_NOI_CAP")) ? dictParam["CCCD_NOI_CAP"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null),
                new OracleParameter("p_CMND_NGAY_CAP_START", (dictParam != null && dictParam.ContainsKey("CMND_NGAY_CAP_START")) ? dictParam["CMND_NGAY_CAP_START"] : null),
                new OracleParameter("p_CMND_NGAY_CAP_END", (dictParam != null && dictParam.ContainsKey("CMND_NGAY_CAP_END")) ? dictParam["CMND_NGAY_CAP_END"] : null),
                new OracleParameter("p_CMND_NOI_CAP",(dictParam != null && dictParam.ContainsKey("CMND_NOI_CAP")) ? dictParam["CMND_NOI_CAP"] : null),
                new OracleParameter("p_DAN_TOC",(dictParam != null && dictParam.ContainsKey("DAN_TOC")) ? dictParam["DAN_TOC"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_HKTT_DIA_CHI",(dictParam != null && dictParam.ContainsKey("HKTT_DIA_CHI")) ? dictParam["HKTT_DIA_CHI"] : null),
                new OracleParameter("p_HKTT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("HKTT_QUAN_HUYEN")) ? dictParam["HKTT_QUAN_HUYEN"] : null),
                new OracleParameter("p_HKTT_TINH_TP",(dictParam != null && dictParam.ContainsKey("HKTT_TINH_TP")) ? dictParam["HKTT_TINH_TP"] : null),
                new OracleParameter("p_HKTT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("HKTT_XA_PHUONG")) ? dictParam["HKTT_XA_PHUONG"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_MOI_QUAN_HE",(dictParam != null && dictParam.ContainsKey("MOI_QUAN_HE")) ? dictParam["MOI_QUAN_HE"] : null),
                new OracleParameter("p_NGAY_SINH",(dictParam != null && dictParam.ContainsKey("NGAY_SINH")) ? dictParam["NGAY_SINH"] : null),
                new OracleParameter("p_NGHE_NGHIEP",(dictParam != null && dictParam.ContainsKey("NGHE_NGHIEP")) ? dictParam["NGHE_NGHIEP"] : null),
                new OracleParameter("p_NHOM_MAU_HE_ABO",(dictParam != null && dictParam.ContainsKey("NHOM_MAU_HE_ABO")) ? dictParam["NHOM_MAU_HE_ABO"] : null),
                new OracleParameter("p_NHOM_MAU_HE_RH",(dictParam != null && dictParam.ContainsKey("NHOM_MAU_HE_RH")) ? dictParam["NHOM_MAU_HE_RH"] : null),
                new OracleParameter("p_NOHT_DIA_CHI",(dictParam != null && dictParam.ContainsKey("NOHT_DIA_CHI")) ? dictParam["NOHT_DIA_CHI"] : null),
                new OracleParameter("p_NOHT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("NOHT_QUAN_HUYEN")) ? dictParam["NOHT_QUAN_HUYEN"] : null),
                new OracleParameter("p_NOHT_TINH_TP",(dictParam != null && dictParam.ContainsKey("NOHT_TINH_TP")) ? dictParam["NOHT_TINH_TP"] : null),
                new OracleParameter("p_NOHT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("NOHT_XA_PHUONG")) ? dictParam["NOHT_XA_PHUONG"] : null),
                new OracleParameter("p_SO_DIEN_THOAI_CO_DINH",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI_CO_DINH")) ? dictParam["SO_DIEN_THOAI_CO_DINH"] : null),
                new OracleParameter("p_SO_DIEN_THOAI_DI_DONG",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI_DI_DONG")) ? dictParam["SO_DIEN_THOAI_DI_DONG"] : null),
                new OracleParameter("p_TON_GIAO",(dictParam != null && dictParam.ContainsKey("TON_GIAO")) ? dictParam["TON_GIAO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_CONG_DAN> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_CCCD_NGAY_CAP_START", (dictParam != null && dictParam.ContainsKey("CCCD_NGAY_CAP_START")) ? dictParam["CCCD_NGAY_CAP_START"] : null),
                new OracleParameter("p_CCCD_NGAY_CAP_END", (dictParam != null && dictParam.ContainsKey("CCCD_NGAY_CAP_END")) ? dictParam["CCCD_NGAY_CAP_END"] : null),
                new OracleParameter("p_CCCD_NOI_CAP",(dictParam != null && dictParam.ContainsKey("CCCD_NOI_CAP")) ? dictParam["CCCD_NOI_CAP"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null),
                new OracleParameter("p_CMND_NGAY_CAP_START", (dictParam != null && dictParam.ContainsKey("CMND_NGAY_CAP_START")) ? dictParam["CMND_NGAY_CAP_START"] : null),
                new OracleParameter("p_CMND_NGAY_CAP_END", (dictParam != null && dictParam.ContainsKey("CMND_NGAY_CAP_END")) ? dictParam["CMND_NGAY_CAP_END"] : null),
                new OracleParameter("p_CMND_NOI_CAP",(dictParam != null && dictParam.ContainsKey("CMND_NOI_CAP")) ? dictParam["CMND_NOI_CAP"] : null),
                new OracleParameter("p_DAN_TOC",(dictParam != null && dictParam.ContainsKey("DAN_TOC")) ? dictParam["DAN_TOC"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_HKTT_DIA_CHI",(dictParam != null && dictParam.ContainsKey("HKTT_DIA_CHI")) ? dictParam["HKTT_DIA_CHI"] : null),
                new OracleParameter("p_HKTT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("HKTT_QUAN_HUYEN")) ? dictParam["HKTT_QUAN_HUYEN"] : null),
                new OracleParameter("p_HKTT_TINH_TP",(dictParam != null && dictParam.ContainsKey("HKTT_TINH_TP")) ? dictParam["HKTT_TINH_TP"] : null),
                new OracleParameter("p_HKTT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("HKTT_XA_PHUONG")) ? dictParam["HKTT_XA_PHUONG"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_MOI_QUAN_HE",(dictParam != null && dictParam.ContainsKey("MOI_QUAN_HE")) ? dictParam["MOI_QUAN_HE"] : null),
                new OracleParameter("p_NGAY_SINH",(dictParam != null && dictParam.ContainsKey("NGAY_SINH")) ? dictParam["NGAY_SINH"] : null),
                new OracleParameter("p_NGHE_NGHIEP",(dictParam != null && dictParam.ContainsKey("NGHE_NGHIEP")) ? dictParam["NGHE_NGHIEP"] : null),
                new OracleParameter("p_NHOM_MAU_HE_ABO",(dictParam != null && dictParam.ContainsKey("NHOM_MAU_HE_ABO")) ? dictParam["NHOM_MAU_HE_ABO"] : null),
                new OracleParameter("p_NHOM_MAU_HE_RH",(dictParam != null && dictParam.ContainsKey("NHOM_MAU_HE_RH")) ? dictParam["NHOM_MAU_HE_RH"] : null),
                new OracleParameter("p_NOHT_DIA_CHI",(dictParam != null && dictParam.ContainsKey("NOHT_DIA_CHI")) ? dictParam["NOHT_DIA_CHI"] : null),
                new OracleParameter("p_NOHT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("NOHT_QUAN_HUYEN")) ? dictParam["NOHT_QUAN_HUYEN"] : null),
                new OracleParameter("p_NOHT_TINH_TP",(dictParam != null && dictParam.ContainsKey("NOHT_TINH_TP")) ? dictParam["NOHT_TINH_TP"] : null),
                new OracleParameter("p_NOHT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("NOHT_XA_PHUONG")) ? dictParam["NOHT_XA_PHUONG"] : null),
                new OracleParameter("p_SO_DIEN_THOAI_CO_DINH",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI_CO_DINH")) ? dictParam["SO_DIEN_THOAI_CO_DINH"] : null),
                new OracleParameter("p_SO_DIEN_THOAI_DI_DONG",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI_DI_DONG")) ? dictParam["SO_DIEN_THOAI_DI_DONG"] : null),
                new OracleParameter("p_TON_GIAO",(dictParam != null && dictParam.ContainsKey("TON_GIAO")) ? dictParam["TON_GIAO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_CONG_DAN> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_CONG_DAN QuerySolr_GetInfo(string _ID, string _CODE)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();
            if (!string.IsNullOrEmpty(_ID))
            {
                lstFilter.Add(new SolrQuery("ID:" + _ID));
            }            
			if (!string.IsNullOrEmpty(_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + _CODE));
			}
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","CCCD","CCCD_NGAY_CAP","CCCD_NOI_CAP","CMND","CMND_NGAY_CAP","CMND_NOI_CAP","DAN_TOC","GIOI_TINH","HKTT_DIA_CHI","HKTT_QUAN_HUYEN","HKTT_TINH_TP","HKTT_XA_PHUONG","HO_TEN","MA_CONG_DAN","MA_HO_GD","MOI_QUAN_HE","NGAY_SINH","NGHE_NGHIEP","NHOM_MAU_HE_ABO","NHOM_MAU_HE_RH","NOHT_DIA_CHI","NOHT_QUAN_HUYEN","NOHT_TINH_TP","NOHT_XA_PHUONG","SO_DIEN_THOAI_CO_DINH","SO_DIEN_THOAI_DI_DONG","TON_GIAO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_CONG_DAN> results = QuerySolrBase<EHR_CONG_DAN>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_CONG_DAN/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_CONG_DAN> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_CCCD,			DateTime? p_CCCD_NGAY_CAP_START,			DateTime? p_CCCD_NGAY_CAP_END,			string p_CCCD_NOI_CAP,			string p_CMND,			DateTime? p_CMND_NGAY_CAP_START,			DateTime? p_CMND_NGAY_CAP_END,			string p_CMND_NOI_CAP,			string p_DAN_TOC,			string p_GIOI_TINH,			string p_HKTT_DIA_CHI,			string p_HKTT_QUAN_HUYEN,			string p_HKTT_TINH_TP,			string p_HKTT_XA_PHUONG,			string p_HO_TEN,			string p_MA_CONG_DAN,			string p_MA_HO_GD,			string p_MOI_QUAN_HE,			string p_NGAY_SINH,			string p_NGHE_NGHIEP,			string p_NHOM_MAU_HE_ABO,			string p_NHOM_MAU_HE_RH,			string p_NOHT_DIA_CHI,			string p_NOHT_QUAN_HUYEN,			string p_NOHT_TINH_TP,			string p_NOHT_XA_PHUONG,			string p_SO_DIEN_THOAI_CO_DINH,			string p_SO_DIEN_THOAI_DI_DONG,			string p_TON_GIAO,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_CCCD))
			{
			    lstFilter.Add(new SolrQuery("CCCD:" + p_CCCD));
			}
			if (p_CCCD_NGAY_CAP_START != null && p_CCCD_NGAY_CAP_END != null)
			{
			    lstFilter.Add(new SolrQuery("CCCD_NGAY_CAP:[" + Utility.GetJSONZFromUserDateTime(p_CCCD_NGAY_CAP_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CCCD_NGAY_CAP_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_CCCD_NOI_CAP))
			{
			    lstFilter.Add(new SolrQuery("CCCD_NOI_CAP:" + p_CCCD_NOI_CAP));
			}
			if (!string.IsNullOrEmpty(p_CMND))
			{
			    lstFilter.Add(new SolrQuery("CMND:" + p_CMND));
			}
			if (p_CMND_NGAY_CAP_START != null && p_CMND_NGAY_CAP_END != null)
			{
			    lstFilter.Add(new SolrQuery("CMND_NGAY_CAP:[" + Utility.GetJSONZFromUserDateTime(p_CMND_NGAY_CAP_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CMND_NGAY_CAP_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_CMND_NOI_CAP))
			{
			    lstFilter.Add(new SolrQuery("CMND_NOI_CAP:" + p_CMND_NOI_CAP));
			}
			if (!string.IsNullOrEmpty(p_DAN_TOC))
			{
			    lstFilter.Add(new SolrQuery("DAN_TOC:" + p_DAN_TOC));
			}
			if (!string.IsNullOrEmpty(p_GIOI_TINH))
			{
			    lstFilter.Add(new SolrQuery("GIOI_TINH:" + p_GIOI_TINH));
			}
			if (!string.IsNullOrEmpty(p_HKTT_DIA_CHI))
			{
			    lstFilter.Add(new SolrQuery("HKTT_DIA_CHI:" + p_HKTT_DIA_CHI));
			}
			if (!string.IsNullOrEmpty(p_HKTT_QUAN_HUYEN))
			{
			    lstFilter.Add(new SolrQuery("HKTT_QUAN_HUYEN:" + p_HKTT_QUAN_HUYEN));
			}
			if (!string.IsNullOrEmpty(p_HKTT_TINH_TP))
			{
			    lstFilter.Add(new SolrQuery("HKTT_TINH_TP:" + p_HKTT_TINH_TP));
			}
			if (!string.IsNullOrEmpty(p_HKTT_XA_PHUONG))
			{
			    lstFilter.Add(new SolrQuery("HKTT_XA_PHUONG:" + p_HKTT_XA_PHUONG));
			}
			if (!string.IsNullOrEmpty(p_HO_TEN))
			{
			    lstFilter.Add(new SolrQuery("HO_TEN:" + p_HO_TEN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HO_GD))
			{
			    lstFilter.Add(new SolrQuery("MA_HO_GD:" + p_MA_HO_GD));
			}
			if (!string.IsNullOrEmpty(p_MOI_QUAN_HE))
			{
			    lstFilter.Add(new SolrQuery("MOI_QUAN_HE:" + p_MOI_QUAN_HE));
			}
			if (!string.IsNullOrEmpty(p_NGAY_SINH))
			{
			    lstFilter.Add(new SolrQuery("NGAY_SINH:" + p_NGAY_SINH));
			}
			if (!string.IsNullOrEmpty(p_NGHE_NGHIEP))
			{
			    lstFilter.Add(new SolrQuery("NGHE_NGHIEP:" + p_NGHE_NGHIEP));
			}
			if (!string.IsNullOrEmpty(p_NHOM_MAU_HE_ABO))
			{
			    lstFilter.Add(new SolrQuery("NHOM_MAU_HE_ABO:" + p_NHOM_MAU_HE_ABO));
			}
			if (!string.IsNullOrEmpty(p_NHOM_MAU_HE_RH))
			{
			    lstFilter.Add(new SolrQuery("NHOM_MAU_HE_RH:" + p_NHOM_MAU_HE_RH));
			}
			if (!string.IsNullOrEmpty(p_NOHT_DIA_CHI))
			{
			    lstFilter.Add(new SolrQuery("NOHT_DIA_CHI:" + p_NOHT_DIA_CHI));
			}
			if (!string.IsNullOrEmpty(p_NOHT_QUAN_HUYEN))
			{
			    lstFilter.Add(new SolrQuery("NOHT_QUAN_HUYEN:" + p_NOHT_QUAN_HUYEN));
			}
			if (!string.IsNullOrEmpty(p_NOHT_TINH_TP))
			{
			    lstFilter.Add(new SolrQuery("NOHT_TINH_TP:" + p_NOHT_TINH_TP));
			}
			if (!string.IsNullOrEmpty(p_NOHT_XA_PHUONG))
			{
			    lstFilter.Add(new SolrQuery("NOHT_XA_PHUONG:" + p_NOHT_XA_PHUONG));
			}
			if (!string.IsNullOrEmpty(p_SO_DIEN_THOAI_CO_DINH))
			{
			    lstFilter.Add(new SolrQuery("SO_DIEN_THOAI_CO_DINH:" + p_SO_DIEN_THOAI_CO_DINH));
			}
			if (!string.IsNullOrEmpty(p_SO_DIEN_THOAI_DI_DONG))
			{
			    lstFilter.Add(new SolrQuery("SO_DIEN_THOAI_DI_DONG:" + p_SO_DIEN_THOAI_DI_DONG));
			}
			if (!string.IsNullOrEmpty(p_TON_GIAO))
			{
			    lstFilter.Add(new SolrQuery("TON_GIAO:" + p_TON_GIAO));
			}
			if (!string.IsNullOrEmpty(p_VERSION_XML))
			{
			    lstFilter.Add(new SolrQuery("VERSION_XML:" + p_VERSION_XML));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","CCCD","CCCD_NGAY_CAP","CCCD_NOI_CAP","CMND","CMND_NGAY_CAP","CMND_NOI_CAP","DAN_TOC","GIOI_TINH","HKTT_DIA_CHI","HKTT_QUAN_HUYEN","HKTT_TINH_TP","HKTT_XA_PHUONG","HO_TEN","MA_CONG_DAN","MA_HO_GD","MOI_QUAN_HE","NGAY_SINH","NGHE_NGHIEP","NHOM_MAU_HE_ABO","NHOM_MAU_HE_RH","NOHT_DIA_CHI","NOHT_QUAN_HUYEN","NOHT_TINH_TP","NOHT_XA_PHUONG","SO_DIEN_THOAI_CO_DINH","SO_DIEN_THOAI_DI_DONG","TON_GIAO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_CONG_DAN>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_CONG_DAN/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_CONG_DAN> listAddRange = new List<EHR_CONG_DAN>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

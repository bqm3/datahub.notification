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
    public class EHR_SKSS_KHHGDDao : OracleBaseImpl<EHR_SKSS_KHHGD>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_SKSS_KHHGD";
            PackageName = "PK_EHR_SKSS_KHHGD";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_SKSS_KHHGD_Information info)
        {        
			var paramValue = new EHR_SKSS_KHHGD
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BENH_PHU_KHOA = (info != null && !string.IsNullOrEmpty(info.BENH_PHU_KHOA)) ? info.BENH_PHU_KHOA : null,
                BIEN_PHAP_TRANH_THAI = (info != null && !string.IsNullOrEmpty(info.BIEN_PHAP_TRANH_THAI)) ? info.BIEN_PHAP_TRANH_THAI : null,
                KY_THAI_CUOI = (info != null && !string.IsNullOrEmpty(info.KY_THAI_CUOI)) ? info.KY_THAI_CUOI : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_SKSS_KHHGD = (info != null && !string.IsNullOrEmpty(info.MA_SKSS_KHHGD)) ? info.MA_SKSS_KHHGD : null,
                MA_SKSS_KHHGD_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_SKSS_KHHGD_DON_VI)) ? info.MA_SKSS_KHHGD_DON_VI : null,
                SL_DE_DU_THANG = (info != null && info.SL_DE_DU_THANG != null) ? info.SL_DE_DU_THANG.Value : (decimal?)null,
                SL_DE_KHO = (info != null && info.SL_DE_KHO != null) ? info.SL_DE_KHO.Value : (decimal?)null,
                SL_DE_MO = (info != null && info.SL_DE_MO != null) ? info.SL_DE_MO.Value : (decimal?)null,
                SL_DE_NON = (info != null && info.SL_DE_NON != null) ? info.SL_DE_NON.Value : (decimal?)null,
                SL_DE_THUONG = (info != null && info.SL_DE_THUONG != null) ? info.SL_DE_THUONG.Value : (decimal?)null,
                SL_SINH_DE = (info != null && info.SL_SINH_DE != null) ? info.SL_SINH_DE.Value : (decimal?)null,
                SO_CON_HIEN_SONG = (info != null && info.SO_CON_HIEN_SONG != null) ? info.SO_CON_HIEN_SONG.Value : (decimal?)null,
                SO_LAN_CO_THAI = (info != null && !string.IsNullOrEmpty(info.SO_LAN_CO_THAI)) ? info.SO_LAN_CO_THAI : null,
                SO_LAN_PHA_THAI = (info != null && !string.IsNullOrEmpty(info.SO_LAN_PHA_THAI)) ? info.SO_LAN_PHA_THAI : null,
                SO_LAN_SAY_THAI = (info != null && !string.IsNullOrEmpty(info.SO_LAN_SAY_THAI)) ? info.SO_LAN_SAY_THAI : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_SKSS_KHHGD> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BENH_PHU_KHOA",(dictParam != null && dictParam.ContainsKey("BENH_PHU_KHOA")) ? dictParam["BENH_PHU_KHOA"] : null),
                new OracleParameter("p_BIEN_PHAP_TRANH_THAI",(dictParam != null && dictParam.ContainsKey("BIEN_PHAP_TRANH_THAI")) ? dictParam["BIEN_PHAP_TRANH_THAI"] : null),
                new OracleParameter("p_KY_THAI_CUOI",(dictParam != null && dictParam.ContainsKey("KY_THAI_CUOI")) ? dictParam["KY_THAI_CUOI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SKSS_KHHGD",(dictParam != null && dictParam.ContainsKey("MA_SKSS_KHHGD")) ? dictParam["MA_SKSS_KHHGD"] : null),
                new OracleParameter("p_MA_SKSS_KHHGD_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SKSS_KHHGD_DON_VI")) ? dictParam["MA_SKSS_KHHGD_DON_VI"] : null),
                new OracleParameter("p_SL_DE_DU_THANG",(dictParam != null && dictParam.ContainsKey("SL_DE_DU_THANG")) ? dictParam["SL_DE_DU_THANG"] : null),
                new OracleParameter("p_SL_DE_KHO",(dictParam != null && dictParam.ContainsKey("SL_DE_KHO")) ? dictParam["SL_DE_KHO"] : null),
                new OracleParameter("p_SL_DE_MO",(dictParam != null && dictParam.ContainsKey("SL_DE_MO")) ? dictParam["SL_DE_MO"] : null),
                new OracleParameter("p_SL_DE_NON",(dictParam != null && dictParam.ContainsKey("SL_DE_NON")) ? dictParam["SL_DE_NON"] : null),
                new OracleParameter("p_SL_DE_THUONG",(dictParam != null && dictParam.ContainsKey("SL_DE_THUONG")) ? dictParam["SL_DE_THUONG"] : null),
                new OracleParameter("p_SL_SINH_DE",(dictParam != null && dictParam.ContainsKey("SL_SINH_DE")) ? dictParam["SL_SINH_DE"] : null),
                new OracleParameter("p_SO_CON_HIEN_SONG",(dictParam != null && dictParam.ContainsKey("SO_CON_HIEN_SONG")) ? dictParam["SO_CON_HIEN_SONG"] : null),
                new OracleParameter("p_SO_LAN_CO_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_CO_THAI")) ? dictParam["SO_LAN_CO_THAI"] : null),
                new OracleParameter("p_SO_LAN_PHA_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_PHA_THAI")) ? dictParam["SO_LAN_PHA_THAI"] : null),
                new OracleParameter("p_SO_LAN_SAY_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_SAY_THAI")) ? dictParam["SO_LAN_SAY_THAI"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_SKSS_KHHGD> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BENH_PHU_KHOA",(dictParam != null && dictParam.ContainsKey("BENH_PHU_KHOA")) ? dictParam["BENH_PHU_KHOA"] : null),
                new OracleParameter("p_BIEN_PHAP_TRANH_THAI",(dictParam != null && dictParam.ContainsKey("BIEN_PHAP_TRANH_THAI")) ? dictParam["BIEN_PHAP_TRANH_THAI"] : null),
                new OracleParameter("p_KY_THAI_CUOI",(dictParam != null && dictParam.ContainsKey("KY_THAI_CUOI")) ? dictParam["KY_THAI_CUOI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SKSS_KHHGD",(dictParam != null && dictParam.ContainsKey("MA_SKSS_KHHGD")) ? dictParam["MA_SKSS_KHHGD"] : null),
                new OracleParameter("p_MA_SKSS_KHHGD_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SKSS_KHHGD_DON_VI")) ? dictParam["MA_SKSS_KHHGD_DON_VI"] : null),
                new OracleParameter("p_SL_DE_DU_THANG",(dictParam != null && dictParam.ContainsKey("SL_DE_DU_THANG")) ? dictParam["SL_DE_DU_THANG"] : null),
                new OracleParameter("p_SL_DE_KHO",(dictParam != null && dictParam.ContainsKey("SL_DE_KHO")) ? dictParam["SL_DE_KHO"] : null),
                new OracleParameter("p_SL_DE_MO",(dictParam != null && dictParam.ContainsKey("SL_DE_MO")) ? dictParam["SL_DE_MO"] : null),
                new OracleParameter("p_SL_DE_NON",(dictParam != null && dictParam.ContainsKey("SL_DE_NON")) ? dictParam["SL_DE_NON"] : null),
                new OracleParameter("p_SL_DE_THUONG",(dictParam != null && dictParam.ContainsKey("SL_DE_THUONG")) ? dictParam["SL_DE_THUONG"] : null),
                new OracleParameter("p_SL_SINH_DE",(dictParam != null && dictParam.ContainsKey("SL_SINH_DE")) ? dictParam["SL_SINH_DE"] : null),
                new OracleParameter("p_SO_CON_HIEN_SONG",(dictParam != null && dictParam.ContainsKey("SO_CON_HIEN_SONG")) ? dictParam["SO_CON_HIEN_SONG"] : null),
                new OracleParameter("p_SO_LAN_CO_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_CO_THAI")) ? dictParam["SO_LAN_CO_THAI"] : null),
                new OracleParameter("p_SO_LAN_PHA_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_PHA_THAI")) ? dictParam["SO_LAN_PHA_THAI"] : null),
                new OracleParameter("p_SO_LAN_SAY_THAI",(dictParam != null && dictParam.ContainsKey("SO_LAN_SAY_THAI")) ? dictParam["SO_LAN_SAY_THAI"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_SKSS_KHHGD> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_SKSS_KHHGD QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BENH_PHU_KHOA","BIEN_PHAP_TRANH_THAI","KY_THAI_CUOI","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SKSS_KHHGD","MA_SKSS_KHHGD_DON_VI","SL_DE_DU_THANG","SL_DE_KHO","SL_DE_MO","SL_DE_NON","SL_DE_THUONG","SL_SINH_DE","SO_CON_HIEN_SONG","SO_LAN_CO_THAI","SO_LAN_PHA_THAI","SO_LAN_SAY_THAI","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_SKSS_KHHGD> results = QuerySolrBase<EHR_SKSS_KHHGD>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_SKSS_KHHGD/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_SKSS_KHHGD> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BENH_PHU_KHOA,			string p_BIEN_PHAP_TRANH_THAI,			string p_KY_THAI_CUOI,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_SKSS_KHHGD,			string p_MA_SKSS_KHHGD_DON_VI,			string p_SL_DE_DU_THANG,			string p_SL_DE_KHO,			string p_SL_DE_MO,			string p_SL_DE_NON,			string p_SL_DE_THUONG,			string p_SL_SINH_DE,			string p_SO_CON_HIEN_SONG,			string p_SO_LAN_CO_THAI,			string p_SO_LAN_PHA_THAI,			string p_SO_LAN_SAY_THAI,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BENH_PHU_KHOA))
			{
			    lstFilter.Add(new SolrQuery("BENH_PHU_KHOA:" + p_BENH_PHU_KHOA));
			}
			if (!string.IsNullOrEmpty(p_BIEN_PHAP_TRANH_THAI))
			{
			    lstFilter.Add(new SolrQuery("BIEN_PHAP_TRANH_THAI:" + p_BIEN_PHAP_TRANH_THAI));
			}
			if (!string.IsNullOrEmpty(p_KY_THAI_CUOI))
			{
			    lstFilter.Add(new SolrQuery("KY_THAI_CUOI:" + p_KY_THAI_CUOI));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_SKSS_KHHGD))
			{
			    lstFilter.Add(new SolrQuery("MA_SKSS_KHHGD:" + p_MA_SKSS_KHHGD));
			}
			if (!string.IsNullOrEmpty(p_MA_SKSS_KHHGD_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_SKSS_KHHGD_DON_VI:" + p_MA_SKSS_KHHGD_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_SL_DE_DU_THANG))
			{
			    lstFilter.Add(new SolrQuery("SL_DE_DU_THANG:" + p_SL_DE_DU_THANG));
			}
			if (!string.IsNullOrEmpty(p_SL_DE_KHO))
			{
			    lstFilter.Add(new SolrQuery("SL_DE_KHO:" + p_SL_DE_KHO));
			}
			if (!string.IsNullOrEmpty(p_SL_DE_MO))
			{
			    lstFilter.Add(new SolrQuery("SL_DE_MO:" + p_SL_DE_MO));
			}
			if (!string.IsNullOrEmpty(p_SL_DE_NON))
			{
			    lstFilter.Add(new SolrQuery("SL_DE_NON:" + p_SL_DE_NON));
			}
			if (!string.IsNullOrEmpty(p_SL_DE_THUONG))
			{
			    lstFilter.Add(new SolrQuery("SL_DE_THUONG:" + p_SL_DE_THUONG));
			}
			if (!string.IsNullOrEmpty(p_SL_SINH_DE))
			{
			    lstFilter.Add(new SolrQuery("SL_SINH_DE:" + p_SL_SINH_DE));
			}
			if (!string.IsNullOrEmpty(p_SO_CON_HIEN_SONG))
			{
			    lstFilter.Add(new SolrQuery("SO_CON_HIEN_SONG:" + p_SO_CON_HIEN_SONG));
			}
			if (!string.IsNullOrEmpty(p_SO_LAN_CO_THAI))
			{
			    lstFilter.Add(new SolrQuery("SO_LAN_CO_THAI:" + p_SO_LAN_CO_THAI));
			}
			if (!string.IsNullOrEmpty(p_SO_LAN_PHA_THAI))
			{
			    lstFilter.Add(new SolrQuery("SO_LAN_PHA_THAI:" + p_SO_LAN_PHA_THAI));
			}
			if (!string.IsNullOrEmpty(p_SO_LAN_SAY_THAI))
			{
			    lstFilter.Add(new SolrQuery("SO_LAN_SAY_THAI:" + p_SO_LAN_SAY_THAI));
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
            string[] fieldSelect = { "ID","CODE","BENH_PHU_KHOA","BIEN_PHAP_TRANH_THAI","KY_THAI_CUOI","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SKSS_KHHGD","MA_SKSS_KHHGD_DON_VI","SL_DE_DU_THANG","SL_DE_KHO","SL_DE_MO","SL_DE_NON","SL_DE_THUONG","SL_SINH_DE","SO_CON_HIEN_SONG","SO_LAN_CO_THAI","SO_LAN_PHA_THAI","SO_LAN_SAY_THAI","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_SKSS_KHHGD>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_SKSS_KHHGD/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_SKSS_KHHGD> listAddRange = new List<EHR_SKSS_KHHGD>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

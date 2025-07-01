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
    public class EHR_KQ_KCB_SINH_TON_VA_STHDao : OracleBaseImpl<EHR_KQ_KCB_SINH_TON_VA_STH>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_KQ_KCB_SINH_TON_VA_STH";
            PackageName = "PK_EHR_KQ_KCB_SINH_TON_VA_STH";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_KQ_KCB_SINH_TON_VA_STH_Information info)
        {        
			var paramValue = new EHR_KQ_KCB_SINH_TON_VA_STH
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BMI = (info != null && !string.IsNullOrEmpty(info.BMI)) ? info.BMI : null,
                CAN_NANG = (info != null && !string.IsNullOrEmpty(info.CAN_NANG)) ? info.CAN_NANG : null,
                CHIEU_CAO = (info != null && !string.IsNullOrEmpty(info.CHIEU_CAO)) ? info.CHIEU_CAO : null,
                HUYET_AP_TAM_THU = (info != null && !string.IsNullOrEmpty(info.HUYET_AP_TAM_THU)) ? info.HUYET_AP_TAM_THU : null,
                HUYET_AP_TAM_TRUONG = (info != null && !string.IsNullOrEmpty(info.HUYET_AP_TAM_TRUONG)) ? info.HUYET_AP_TAM_TRUONG : null,
                MACH = (info != null && !string.IsNullOrEmpty(info.MACH)) ? info.MACH : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_SINH_TON_VA_STH = (info != null && !string.IsNullOrEmpty(info.MA_SINH_TON_VA_STH)) ? info.MA_SINH_TON_VA_STH : null,
                MA_SINH_TON_VA_STH_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_SINH_TON_VA_STH_DON_VI)) ? info.MA_SINH_TON_VA_STH_DON_VI : null,
                NHIET_DO = (info != null && !string.IsNullOrEmpty(info.NHIET_DO)) ? info.NHIET_DO : null,
                NHIP_THO = (info != null && !string.IsNullOrEmpty(info.NHIP_THO)) ? info.NHIP_THO : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
                VONG_BUNG = (info != null && !string.IsNullOrEmpty(info.VONG_BUNG)) ? info.VONG_BUNG : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_KQ_KCB_SINH_TON_VA_STH> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BMI",(dictParam != null && dictParam.ContainsKey("BMI")) ? dictParam["BMI"] : null),
                new OracleParameter("p_CAN_NANG",(dictParam != null && dictParam.ContainsKey("CAN_NANG")) ? dictParam["CAN_NANG"] : null),
                new OracleParameter("p_CHIEU_CAO",(dictParam != null && dictParam.ContainsKey("CHIEU_CAO")) ? dictParam["CHIEU_CAO"] : null),
                new OracleParameter("p_HUYET_AP_TAM_THU",(dictParam != null && dictParam.ContainsKey("HUYET_AP_TAM_THU")) ? dictParam["HUYET_AP_TAM_THU"] : null),
                new OracleParameter("p_HUYET_AP_TAM_TRUONG",(dictParam != null && dictParam.ContainsKey("HUYET_AP_TAM_TRUONG")) ? dictParam["HUYET_AP_TAM_TRUONG"] : null),
                new OracleParameter("p_MACH",(dictParam != null && dictParam.ContainsKey("MACH")) ? dictParam["MACH"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH")) ? dictParam["MA_SINH_TON_VA_STH"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH_DON_VI")) ? dictParam["MA_SINH_TON_VA_STH_DON_VI"] : null),
                new OracleParameter("p_NHIET_DO",(dictParam != null && dictParam.ContainsKey("NHIET_DO")) ? dictParam["NHIET_DO"] : null),
                new OracleParameter("p_NHIP_THO",(dictParam != null && dictParam.ContainsKey("NHIP_THO")) ? dictParam["NHIP_THO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_VONG_BUNG",(dictParam != null && dictParam.ContainsKey("VONG_BUNG")) ? dictParam["VONG_BUNG"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_KQ_KCB_SINH_TON_VA_STH> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BMI",(dictParam != null && dictParam.ContainsKey("BMI")) ? dictParam["BMI"] : null),
                new OracleParameter("p_CAN_NANG",(dictParam != null && dictParam.ContainsKey("CAN_NANG")) ? dictParam["CAN_NANG"] : null),
                new OracleParameter("p_CHIEU_CAO",(dictParam != null && dictParam.ContainsKey("CHIEU_CAO")) ? dictParam["CHIEU_CAO"] : null),
                new OracleParameter("p_HUYET_AP_TAM_THU",(dictParam != null && dictParam.ContainsKey("HUYET_AP_TAM_THU")) ? dictParam["HUYET_AP_TAM_THU"] : null),
                new OracleParameter("p_HUYET_AP_TAM_TRUONG",(dictParam != null && dictParam.ContainsKey("HUYET_AP_TAM_TRUONG")) ? dictParam["HUYET_AP_TAM_TRUONG"] : null),
                new OracleParameter("p_MACH",(dictParam != null && dictParam.ContainsKey("MACH")) ? dictParam["MACH"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH")) ? dictParam["MA_SINH_TON_VA_STH"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH_DON_VI")) ? dictParam["MA_SINH_TON_VA_STH_DON_VI"] : null),
                new OracleParameter("p_NHIET_DO",(dictParam != null && dictParam.ContainsKey("NHIET_DO")) ? dictParam["NHIET_DO"] : null),
                new OracleParameter("p_NHIP_THO",(dictParam != null && dictParam.ContainsKey("NHIP_THO")) ? dictParam["NHIP_THO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_VONG_BUNG",(dictParam != null && dictParam.ContainsKey("VONG_BUNG")) ? dictParam["VONG_BUNG"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_KQ_KCB_SINH_TON_VA_STH> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_KQ_KCB_SINH_TON_VA_STH QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BMI","CAN_NANG","CHIEU_CAO","HUYET_AP_TAM_THU","HUYET_AP_TAM_TRUONG","MACH","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SINH_TON_VA_STH","MA_SINH_TON_VA_STH_DON_VI","NHIET_DO","NHIP_THO","VERSION_XML","VONG_BUNG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_KQ_KCB_SINH_TON_VA_STH> results = QuerySolrBase<EHR_KQ_KCB_SINH_TON_VA_STH>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_SINH_TON_VA_STH/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_KQ_KCB_SINH_TON_VA_STH> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BMI,			string p_CAN_NANG,			string p_CHIEU_CAO,			string p_HUYET_AP_TAM_THU,			string p_HUYET_AP_TAM_TRUONG,			string p_MACH,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_SINH_TON_VA_STH,			string p_MA_SINH_TON_VA_STH_DON_VI,			string p_NHIET_DO,			string p_NHIP_THO,			string p_VERSION_XML,			string p_VONG_BUNG,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BMI))
			{
			    lstFilter.Add(new SolrQuery("BMI:" + p_BMI));
			}
			if (!string.IsNullOrEmpty(p_CAN_NANG))
			{
			    lstFilter.Add(new SolrQuery("CAN_NANG:" + p_CAN_NANG));
			}
			if (!string.IsNullOrEmpty(p_CHIEU_CAO))
			{
			    lstFilter.Add(new SolrQuery("CHIEU_CAO:" + p_CHIEU_CAO));
			}
			if (!string.IsNullOrEmpty(p_HUYET_AP_TAM_THU))
			{
			    lstFilter.Add(new SolrQuery("HUYET_AP_TAM_THU:" + p_HUYET_AP_TAM_THU));
			}
			if (!string.IsNullOrEmpty(p_HUYET_AP_TAM_TRUONG))
			{
			    lstFilter.Add(new SolrQuery("HUYET_AP_TAM_TRUONG:" + p_HUYET_AP_TAM_TRUONG));
			}
			if (!string.IsNullOrEmpty(p_MACH))
			{
			    lstFilter.Add(new SolrQuery("MACH:" + p_MACH));
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
			if (!string.IsNullOrEmpty(p_MA_SINH_TON_VA_STH))
			{
			    lstFilter.Add(new SolrQuery("MA_SINH_TON_VA_STH:" + p_MA_SINH_TON_VA_STH));
			}
			if (!string.IsNullOrEmpty(p_MA_SINH_TON_VA_STH_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_SINH_TON_VA_STH_DON_VI:" + p_MA_SINH_TON_VA_STH_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_NHIET_DO))
			{
			    lstFilter.Add(new SolrQuery("NHIET_DO:" + p_NHIET_DO));
			}
			if (!string.IsNullOrEmpty(p_NHIP_THO))
			{
			    lstFilter.Add(new SolrQuery("NHIP_THO:" + p_NHIP_THO));
			}
			if (!string.IsNullOrEmpty(p_VERSION_XML))
			{
			    lstFilter.Add(new SolrQuery("VERSION_XML:" + p_VERSION_XML));
			}
			if (!string.IsNullOrEmpty(p_VONG_BUNG))
			{
			    lstFilter.Add(new SolrQuery("VONG_BUNG:" + p_VONG_BUNG));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","BMI","CAN_NANG","CHIEU_CAO","HUYET_AP_TAM_THU","HUYET_AP_TAM_TRUONG","MACH","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SINH_TON_VA_STH","MA_SINH_TON_VA_STH_DON_VI","NHIET_DO","NHIP_THO","VERSION_XML","VONG_BUNG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_KQ_KCB_SINH_TON_VA_STH>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_SINH_TON_VA_STH/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_KQ_KCB_SINH_TON_VA_STH> listAddRange = new List<EHR_KQ_KCB_SINH_TON_VA_STH>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class EHR_KQ_KCBDao : OracleBaseImpl<EHR_KQ_KCB>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_KQ_KCB";
            PackageName = "PK_EHR_KQ_KCB";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_KQ_KCB_Information info)
        {        
			var paramValue = new EHR_KQ_KCB
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BENH_SU = (info != null && !string.IsNullOrEmpty(info.BENH_SU)) ? info.BENH_SU : null,
                MA_BAC_SI = (info != null && !string.IsNullOrEmpty(info.MA_BAC_SI)) ? info.MA_BAC_SI : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_CO_SO_Y_TE = (info != null && !string.IsNullOrEmpty(info.MA_CO_SO_Y_TE)) ? info.MA_CO_SO_Y_TE : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_KQ_KCB = (info != null && !string.IsNullOrEmpty(info.MA_KQ_KCB)) ? info.MA_KQ_KCB : null,
                MA_KQ_KCB_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_KQ_KCB_DON_VI)) ? info.MA_KQ_KCB_DON_VI : null,
                MA_SINH_TON_VA_STH = (info != null && !string.IsNullOrEmpty(info.MA_SINH_TON_VA_STH)) ? info.MA_SINH_TON_VA_STH : null,
                MA_THI_LUC = (info != null && !string.IsNullOrEmpty(info.MA_THI_LUC)) ? info.MA_THI_LUC : null,
                MA_TOAN_THAN = (info != null && !string.IsNullOrEmpty(info.MA_TOAN_THAN)) ? info.MA_TOAN_THAN : null,
                TU_VAN = (info != null && !string.IsNullOrEmpty(info.TU_VAN)) ? info.TU_VAN : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_KQ_KCB> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BENH_SU",(dictParam != null && dictParam.ContainsKey("BENH_SU")) ? dictParam["BENH_SU"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_MA_KQ_KCB_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB_DON_VI")) ? dictParam["MA_KQ_KCB_DON_VI"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH")) ? dictParam["MA_SINH_TON_VA_STH"] : null),
                new OracleParameter("p_MA_THI_LUC",(dictParam != null && dictParam.ContainsKey("MA_THI_LUC")) ? dictParam["MA_THI_LUC"] : null),
                new OracleParameter("p_MA_TOAN_THAN",(dictParam != null && dictParam.ContainsKey("MA_TOAN_THAN")) ? dictParam["MA_TOAN_THAN"] : null),
                new OracleParameter("p_TU_VAN",(dictParam != null && dictParam.ContainsKey("TU_VAN")) ? dictParam["TU_VAN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_KQ_KCB> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BENH_SU",(dictParam != null && dictParam.ContainsKey("BENH_SU")) ? dictParam["BENH_SU"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_MA_KQ_KCB_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB_DON_VI")) ? dictParam["MA_KQ_KCB_DON_VI"] : null),
                new OracleParameter("p_MA_SINH_TON_VA_STH",(dictParam != null && dictParam.ContainsKey("MA_SINH_TON_VA_STH")) ? dictParam["MA_SINH_TON_VA_STH"] : null),
                new OracleParameter("p_MA_THI_LUC",(dictParam != null && dictParam.ContainsKey("MA_THI_LUC")) ? dictParam["MA_THI_LUC"] : null),
                new OracleParameter("p_MA_TOAN_THAN",(dictParam != null && dictParam.ContainsKey("MA_TOAN_THAN")) ? dictParam["MA_TOAN_THAN"] : null),
                new OracleParameter("p_TU_VAN",(dictParam != null && dictParam.ContainsKey("TU_VAN")) ? dictParam["TU_VAN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_KQ_KCB> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_KQ_KCB QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BENH_SU","MA_BAC_SI","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_SO_Y_TE","MA_HSSK","MA_KQ_KCB","MA_KQ_KCB_DON_VI","MA_SINH_TON_VA_STH","MA_THI_LUC","MA_TOAN_THAN","TU_VAN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_KQ_KCB> results = QuerySolrBase<EHR_KQ_KCB>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_KQ_KCB> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BENH_SU,			string p_MA_BAC_SI,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_CO_SO_Y_TE,			string p_MA_HSSK,			string p_MA_KQ_KCB,			string p_MA_KQ_KCB_DON_VI,			string p_MA_SINH_TON_VA_STH,			string p_MA_THI_LUC,			string p_MA_TOAN_THAN,			string p_TU_VAN,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BENH_SU))
			{
			    lstFilter.Add(new SolrQuery("BENH_SU:" + p_BENH_SU));
			}
			if (!string.IsNullOrEmpty(p_MA_BAC_SI))
			{
			    lstFilter.Add(new SolrQuery("MA_BAC_SI:" + p_MA_BAC_SI));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CO_SO_Y_TE))
			{
			    lstFilter.Add(new SolrQuery("MA_CO_SO_Y_TE:" + p_MA_CO_SO_Y_TE));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_KCB))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_KCB:" + p_MA_KQ_KCB));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_KCB_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_KCB_DON_VI:" + p_MA_KQ_KCB_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_SINH_TON_VA_STH))
			{
			    lstFilter.Add(new SolrQuery("MA_SINH_TON_VA_STH:" + p_MA_SINH_TON_VA_STH));
			}
			if (!string.IsNullOrEmpty(p_MA_THI_LUC))
			{
			    lstFilter.Add(new SolrQuery("MA_THI_LUC:" + p_MA_THI_LUC));
			}
			if (!string.IsNullOrEmpty(p_MA_TOAN_THAN))
			{
			    lstFilter.Add(new SolrQuery("MA_TOAN_THAN:" + p_MA_TOAN_THAN));
			}
			if (!string.IsNullOrEmpty(p_TU_VAN))
			{
			    lstFilter.Add(new SolrQuery("TU_VAN:" + p_TU_VAN));
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
            string[] fieldSelect = { "ID","CODE","BENH_SU","MA_BAC_SI","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_SO_Y_TE","MA_HSSK","MA_KQ_KCB","MA_KQ_KCB_DON_VI","MA_SINH_TON_VA_STH","MA_THI_LUC","MA_TOAN_THAN","TU_VAN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_KQ_KCB>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_KQ_KCB> listAddRange = new List<EHR_KQ_KCB>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

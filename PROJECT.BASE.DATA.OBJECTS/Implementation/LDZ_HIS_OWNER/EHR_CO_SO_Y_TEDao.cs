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
    public class EHR_CO_SO_Y_TEDao : OracleBaseImpl<EHR_CO_SO_Y_TE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_CO_SO_Y_TE";
            PackageName = "PK_EHR_CO_SO_Y_TE";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_CO_SO_Y_TE_Information info)
        {        
			var paramValue = new EHR_CO_SO_Y_TE
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DIA_CHI = (info != null && !string.IsNullOrEmpty(info.DIA_CHI)) ? info.DIA_CHI : null,
                MA_CO_SO_Y_TE = (info != null && !string.IsNullOrEmpty(info.MA_CO_SO_Y_TE)) ? info.MA_CO_SO_Y_TE : null,
                MA_CO_SO_Y_TE_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_CO_SO_Y_TE_DON_VI)) ? info.MA_CO_SO_Y_TE_DON_VI : null,
                QUAN_HUYEN = (info != null && !string.IsNullOrEmpty(info.QUAN_HUYEN)) ? info.QUAN_HUYEN : null,
                TEN_CO_SO_Y_TE = (info != null && !string.IsNullOrEmpty(info.TEN_CO_SO_Y_TE)) ? info.TEN_CO_SO_Y_TE : null,
                TINH_TP = (info != null && !string.IsNullOrEmpty(info.TINH_TP)) ? info.TINH_TP : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
                XA_PHUONG = (info != null && !string.IsNullOrEmpty(info.XA_PHUONG)) ? info.XA_PHUONG : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_CO_SO_Y_TE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE_DON_VI")) ? dictParam["MA_CO_SO_Y_TE_DON_VI"] : null),
                new OracleParameter("p_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("QUAN_HUYEN")) ? dictParam["QUAN_HUYEN"] : null),
                new OracleParameter("p_TEN_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("TEN_CO_SO_Y_TE")) ? dictParam["TEN_CO_SO_Y_TE"] : null),
                new OracleParameter("p_TINH_TP",(dictParam != null && dictParam.ContainsKey("TINH_TP")) ? dictParam["TINH_TP"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("XA_PHUONG")) ? dictParam["XA_PHUONG"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_CO_SO_Y_TE> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE_DON_VI")) ? dictParam["MA_CO_SO_Y_TE_DON_VI"] : null),
                new OracleParameter("p_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("QUAN_HUYEN")) ? dictParam["QUAN_HUYEN"] : null),
                new OracleParameter("p_TEN_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("TEN_CO_SO_Y_TE")) ? dictParam["TEN_CO_SO_Y_TE"] : null),
                new OracleParameter("p_TINH_TP",(dictParam != null && dictParam.ContainsKey("TINH_TP")) ? dictParam["TINH_TP"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("XA_PHUONG")) ? dictParam["XA_PHUONG"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_CO_SO_Y_TE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_CO_SO_Y_TE QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DIA_CHI","MA_CO_SO_Y_TE","MA_CO_SO_Y_TE_DON_VI","QUAN_HUYEN","TEN_CO_SO_Y_TE","TINH_TP","VERSION_XML","XA_PHUONG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_CO_SO_Y_TE> results = QuerySolrBase<EHR_CO_SO_Y_TE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_CO_SO_Y_TE/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_CO_SO_Y_TE> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DIA_CHI,			string p_MA_CO_SO_Y_TE,			string p_MA_CO_SO_Y_TE_DON_VI,			string p_QUAN_HUYEN,			string p_TEN_CO_SO_Y_TE,			string p_TINH_TP,			string p_VERSION_XML,			string p_XA_PHUONG,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DIA_CHI))
			{
			    lstFilter.Add(new SolrQuery("DIA_CHI:" + p_DIA_CHI));
			}
			if (!string.IsNullOrEmpty(p_MA_CO_SO_Y_TE))
			{
			    lstFilter.Add(new SolrQuery("MA_CO_SO_Y_TE:" + p_MA_CO_SO_Y_TE));
			}
			if (!string.IsNullOrEmpty(p_MA_CO_SO_Y_TE_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_CO_SO_Y_TE_DON_VI:" + p_MA_CO_SO_Y_TE_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_QUAN_HUYEN))
			{
			    lstFilter.Add(new SolrQuery("QUAN_HUYEN:" + p_QUAN_HUYEN));
			}
			if (!string.IsNullOrEmpty(p_TEN_CO_SO_Y_TE))
			{
			    lstFilter.Add(new SolrQuery("TEN_CO_SO_Y_TE:" + p_TEN_CO_SO_Y_TE));
			}
			if (!string.IsNullOrEmpty(p_TINH_TP))
			{
			    lstFilter.Add(new SolrQuery("TINH_TP:" + p_TINH_TP));
			}
			if (!string.IsNullOrEmpty(p_VERSION_XML))
			{
			    lstFilter.Add(new SolrQuery("VERSION_XML:" + p_VERSION_XML));
			}
			if (!string.IsNullOrEmpty(p_XA_PHUONG))
			{
			    lstFilter.Add(new SolrQuery("XA_PHUONG:" + p_XA_PHUONG));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","DIA_CHI","MA_CO_SO_Y_TE","MA_CO_SO_Y_TE_DON_VI","QUAN_HUYEN","TEN_CO_SO_Y_TE","TINH_TP","VERSION_XML","XA_PHUONG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_CO_SO_Y_TE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_CO_SO_Y_TE/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_CO_SO_Y_TE> listAddRange = new List<EHR_CO_SO_Y_TE>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

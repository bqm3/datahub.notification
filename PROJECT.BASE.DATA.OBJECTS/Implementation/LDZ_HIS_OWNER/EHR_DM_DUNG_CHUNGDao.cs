﻿using System;
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
    public class EHR_DM_DUNG_CHUNGDao : OracleBaseImpl<EHR_DM_DUNG_CHUNG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_DM_DUNG_CHUNG";
            PackageName = "PK_EHR_DM_DUNG_CHUNG";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_DM_DUNG_CHUNG_Information info)
        {        
			var paramValue = new EHR_DM_DUNG_CHUNG
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                MA = (info != null && !string.IsNullOrEmpty(info.MA)) ? info.MA : null,
                MA_CHA = (info != null && !string.IsNullOrEmpty(info.MA_CHA)) ? info.MA_CHA : null,
                TEN = (info != null && !string.IsNullOrEmpty(info.TEN)) ? info.TEN : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_DM_DUNG_CHUNG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MA",(dictParam != null && dictParam.ContainsKey("MA")) ? dictParam["MA"] : null),
                new OracleParameter("p_MA_CHA",(dictParam != null && dictParam.ContainsKey("MA_CHA")) ? dictParam["MA_CHA"] : null),
                new OracleParameter("p_TEN",(dictParam != null && dictParam.ContainsKey("TEN")) ? dictParam["TEN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_DM_DUNG_CHUNG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MA",(dictParam != null && dictParam.ContainsKey("MA")) ? dictParam["MA"] : null),
                new OracleParameter("p_MA_CHA",(dictParam != null && dictParam.ContainsKey("MA_CHA")) ? dictParam["MA_CHA"] : null),
                new OracleParameter("p_TEN",(dictParam != null && dictParam.ContainsKey("TEN")) ? dictParam["TEN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_DM_DUNG_CHUNG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_DM_DUNG_CHUNG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","MA","MA_CHA","TEN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_DM_DUNG_CHUNG> results = QuerySolrBase<EHR_DM_DUNG_CHUNG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DM_DUNG_CHUNG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_DM_DUNG_CHUNG> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_MA,			string p_MA_CHA,			string p_TEN,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_MA))
			{
			    lstFilter.Add(new SolrQuery("MA:" + p_MA));
			}
			if (!string.IsNullOrEmpty(p_MA_CHA))
			{
			    lstFilter.Add(new SolrQuery("MA_CHA:" + p_MA_CHA));
			}
			if (!string.IsNullOrEmpty(p_TEN))
			{
			    lstFilter.Add(new SolrQuery("TEN:" + p_TEN));
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
            string[] fieldSelect = { "ID","CODE","MA","MA_CHA","TEN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_DM_DUNG_CHUNG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DM_DUNG_CHUNG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_DM_DUNG_CHUNG> listAddRange = new List<EHR_DM_DUNG_CHUNG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

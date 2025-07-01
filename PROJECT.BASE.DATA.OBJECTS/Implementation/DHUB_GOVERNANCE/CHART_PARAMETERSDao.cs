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
    public class CHART_PARAMETERSDao : OracleBaseImpl<CHART_PARAMETERS>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "CHART_PARAMETERS";
            PackageName = "PK_CHART_PARAMETERS";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(CHART_PARAMETERS_Request request)
        {        
			var info = new CHART_PARAMETERS
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                CHART_ID = (request != null && request.CHART_ID != null) ? request.CHART_ID.Value : (decimal?)null,
                NAME = (request != null && !string.IsNullOrEmpty(request.NAME)) ? request.NAME : null,
                DEFAULT_VALUE = (request != null && !string.IsNullOrEmpty(request.DEFAULT_VALUE)) ? request.DEFAULT_VALUE : null,
                UI_HINT = (request != null && !string.IsNullOrEmpty(request.UI_HINT)) ? request.UI_HINT : null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<CHART_PARAMETERS_Request> request)
		{
			var list = new List<CHART_PARAMETERS>();
			foreach (var info in request)
			{
				list.Add(new CHART_PARAMETERS
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                CHART_ID = (info != null && info.CHART_ID != null) ? info.CHART_ID.Value : (decimal?)null,
                NAME = (info != null && !string.IsNullOrEmpty(info.NAME)) ? info.NAME : null,
                DEFAULT_VALUE = (info != null && !string.IsNullOrEmpty(info.DEFAULT_VALUE)) ? info.DEFAULT_VALUE : null,
                UI_HINT = (info != null && !string.IsNullOrEmpty(info.UI_HINT)) ? info.UI_HINT : null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<CHART_PARAMETERS> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<CHART_PARAMETERS> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<CHART_PARAMETERS> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CHART_ID",(dictParam != null && dictParam.ContainsKey("CHART_ID")) ? dictParam["CHART_ID"] : null),
                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<CHART_PARAMETERS> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CHART_ID",(dictParam != null && dictParam.ContainsKey("CHART_ID")) ? dictParam["CHART_ID"] : null),
                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<CHART_PARAMETERS> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public CHART_PARAMETERS QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","CHART_ID","NAME","DEFAULT_VALUE","UI_HINT","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<CHART_PARAMETERS> results = QuerySolrBase<CHART_PARAMETERS>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"CHART_PARAMETERS/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<CHART_PARAMETERS> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_CHART_ID,			string p_NAME,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_CHART_ID))
			{
			    lstFilter.Add(new SolrQuery("CHART_ID:" + p_CHART_ID));
			}
			if (!string.IsNullOrEmpty(p_NAME))
			{
			    lstFilter.Add(new SolrQuery("NAME:" + p_NAME));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","CHART_ID","NAME","DEFAULT_VALUE","UI_HINT","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<CHART_PARAMETERS>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"CHART_PARAMETERS/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<CHART_PARAMETERS> listAddRange = new List<CHART_PARAMETERS>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

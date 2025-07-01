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
    public class CHARTSDao : OracleBaseImpl<CHARTS>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "CHARTS";
            PackageName = "PK_CHARTS";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(CHARTS_Request request)
        {        
			var info = new CHARTS
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                TITLE = (request != null && !string.IsNullOrEmpty(request.TITLE)) ? request.TITLE : null,
                X = (request != null && request.X != null) ? request.X.Value : (decimal?)null,
                Y = (request != null && request.Y != null) ? request.Y.Value : (decimal?)null,
                WIDTH = (request != null && request.WIDTH != null) ? request.WIDTH.Value : (decimal?)null,
                HEIGHT = (request != null && request.HEIGHT != null) ? request.HEIGHT.Value : (decimal?)null,
                POLL_INTERVAL = (request != null && request.POLL_INTERVAL != null) ? request.POLL_INTERVAL.Value : (decimal?)null,
                CHART_TYPE = (request != null && !string.IsNullOrEmpty(request.CHART_TYPE)) ? request.CHART_TYPE : null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<CHARTS_Request> request)
		{
			var list = new List<CHARTS>();
			foreach (var info in request)
			{
				list.Add(new CHARTS
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                TITLE = (info != null && !string.IsNullOrEmpty(info.TITLE)) ? info.TITLE : null,
                X = (info != null && info.X != null) ? info.X.Value : (decimal?)null,
                Y = (info != null && info.Y != null) ? info.Y.Value : (decimal?)null,
                WIDTH = (info != null && info.WIDTH != null) ? info.WIDTH.Value : (decimal?)null,
                HEIGHT = (info != null && info.HEIGHT != null) ? info.HEIGHT.Value : (decimal?)null,
                POLL_INTERVAL = (info != null && info.POLL_INTERVAL != null) ? info.POLL_INTERVAL.Value : (decimal?)null,
                CHART_TYPE = (info != null && !string.IsNullOrEmpty(info.CHART_TYPE)) ? info.CHART_TYPE : null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<CHARTS> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<CHARTS> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<CHARTS> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_TITLE",(dictParam != null && dictParam.ContainsKey("TITLE")) ? dictParam["TITLE"] : null),
                new OracleParameter("p_CHART_TYPE",(dictParam != null && dictParam.ContainsKey("CHART_TYPE")) ? dictParam["CHART_TYPE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<CHARTS> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_TITLE",(dictParam != null && dictParam.ContainsKey("TITLE")) ? dictParam["TITLE"] : null),
                new OracleParameter("p_CHART_TYPE",(dictParam != null && dictParam.ContainsKey("CHART_TYPE")) ? dictParam["CHART_TYPE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<CHARTS> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public CHARTS QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","TITLE","X","Y","WIDTH","HEIGHT","POLL_INTERVAL","CHART_TYPE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<CHARTS> results = QuerySolrBase<CHARTS>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"CHARTS/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<CHARTS> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_TITLE,			string p_CHART_TYPE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_TITLE))
			{
			    lstFilter.Add(new SolrQuery("TITLE:" + p_TITLE));
			}
			if (!string.IsNullOrEmpty(p_CHART_TYPE))
			{
			    lstFilter.Add(new SolrQuery("CHART_TYPE:" + p_CHART_TYPE));
			}
			if (!string.IsNullOrEmpty(p_STATUS))
			{
			    lstFilter.Add(new SolrQuery("STATUS:" + p_STATUS));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","TITLE","X","Y","WIDTH","HEIGHT","POLL_INTERVAL","CHART_TYPE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<CHARTS>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"CHARTS/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<CHARTS> listAddRange = new List<CHARTS>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

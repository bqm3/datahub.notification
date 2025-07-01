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
    public class LOG_ERRORDao : OracleBaseImpl<LOG_ERROR>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "LOG_ERROR";
            PackageName = "PK_LOG_ERROR";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(LOG_ERROR_Request request)
        {        
			var info = new LOG_ERROR
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                LOG_TYPE_CODE = (request != null && !string.IsNullOrEmpty(request.LOG_TYPE_CODE)) ? request.LOG_TYPE_CODE : null,
                LOG_ACTION_ID = (request != null && request.LOG_ACTION_ID != null) ? request.LOG_ACTION_ID.Value : (long?)null,
                MESSAGE_LOG = (request != null && !string.IsNullOrEmpty(request.MESSAGE_LOG)) ? request.MESSAGE_LOG : null,
                MESSAGE_DETAIL = (request != null && !string.IsNullOrEmpty(request.MESSAGE_DETAIL)) ? request.MESSAGE_DETAIL : null,
                LOG_ACTION_CODE = (request != null && !string.IsNullOrEmpty(request.LOG_ACTION_CODE)) ? request.LOG_ACTION_CODE : null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<LOG_ERROR_Request> request)
		{
			var list = new List<LOG_ERROR>();
			foreach (var info in request)
			{
				list.Add(new LOG_ERROR
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                LOG_TYPE_CODE = (info != null && !string.IsNullOrEmpty(info.LOG_TYPE_CODE)) ? info.LOG_TYPE_CODE : null,
                LOG_ACTION_ID = (info != null && info.LOG_ACTION_ID != null) ? info.LOG_ACTION_ID.Value : (long?)null,
                MESSAGE_LOG = (info != null && !string.IsNullOrEmpty(info.MESSAGE_LOG)) ? info.MESSAGE_LOG : null,
                MESSAGE_DETAIL = (info != null && !string.IsNullOrEmpty(info.MESSAGE_DETAIL)) ? info.MESSAGE_DETAIL : null,
                LOG_ACTION_CODE = (info != null && !string.IsNullOrEmpty(info.LOG_ACTION_CODE)) ? info.LOG_ACTION_CODE : null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<LOG_ERROR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<LOG_ERROR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<LOG_ERROR> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOG_TYPE_CODE",(dictParam != null && dictParam.ContainsKey("LOG_TYPE_CODE")) ? dictParam["LOG_TYPE_CODE"] : null),
                new OracleParameter("p_LOG_ACTION_ID",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_ID")) ? dictParam["LOG_ACTION_ID"] : null),
                new OracleParameter("p_MESSAGE_LOG",(dictParam != null && dictParam.ContainsKey("MESSAGE_LOG")) ? dictParam["MESSAGE_LOG"] : null),
                new OracleParameter("p_MESSAGE_DETAIL",(dictParam != null && dictParam.ContainsKey("MESSAGE_DETAIL")) ? dictParam["MESSAGE_DETAIL"] : null),
                new OracleParameter("p_LOG_ACTION_CODE",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_CODE")) ? dictParam["LOG_ACTION_CODE"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<LOG_ERROR> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOG_TYPE_CODE",(dictParam != null && dictParam.ContainsKey("LOG_TYPE_CODE")) ? dictParam["LOG_TYPE_CODE"] : null),
                new OracleParameter("p_LOG_ACTION_ID",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_ID")) ? dictParam["LOG_ACTION_ID"] : null),
                new OracleParameter("p_MESSAGE_LOG",(dictParam != null && dictParam.ContainsKey("MESSAGE_LOG")) ? dictParam["MESSAGE_LOG"] : null),
                new OracleParameter("p_MESSAGE_DETAIL",(dictParam != null && dictParam.ContainsKey("MESSAGE_DETAIL")) ? dictParam["MESSAGE_DETAIL"] : null),
                new OracleParameter("p_LOG_ACTION_CODE",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_CODE")) ? dictParam["LOG_ACTION_CODE"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<LOG_ERROR> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public LOG_ERROR QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","LOG_TYPE_CODE","LOG_ACTION_ID","MESSAGE_LOG","MESSAGE_DETAIL","LOG_ACTION_CODE","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<LOG_ERROR> results = QuerySolrBase<LOG_ERROR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"LOG_ERROR/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<LOG_ERROR> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_LOG_TYPE_CODE,			string p_LOG_ACTION_ID,			string p_MESSAGE_LOG,			string p_MESSAGE_DETAIL,			string p_LOG_ACTION_CODE,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_LOG_TYPE_CODE))
			{
			    lstFilter.Add(new SolrQuery("LOG_TYPE_CODE:" + p_LOG_TYPE_CODE));
			}
			if (!string.IsNullOrEmpty(p_LOG_ACTION_ID))
			{
			    lstFilter.Add(new SolrQuery("LOG_ACTION_ID:" + p_LOG_ACTION_ID));
			}
			if (!string.IsNullOrEmpty(p_MESSAGE_LOG))
			{
			    lstFilter.Add(new SolrQuery("MESSAGE_LOG:" + p_MESSAGE_LOG));
			}
			if (!string.IsNullOrEmpty(p_MESSAGE_DETAIL))
			{
			    lstFilter.Add(new SolrQuery("MESSAGE_DETAIL:" + p_MESSAGE_DETAIL));
			}
			if (!string.IsNullOrEmpty(p_LOG_ACTION_CODE))
			{
			    lstFilter.Add(new SolrQuery("LOG_ACTION_CODE:" + p_LOG_ACTION_CODE));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","LOG_TYPE_CODE","LOG_ACTION_ID","MESSAGE_LOG","MESSAGE_DETAIL","LOG_ACTION_CODE","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<LOG_ERROR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"LOG_ERROR/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<LOG_ERROR> listAddRange = new List<LOG_ERROR>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class LOG_ACTIONDao : OracleBaseImpl<LOG_ACTION>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "LOG_ACTION";
            PackageName = "PK_LOG_ACTION";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(LOG_ACTION_Request request)
        {        
			var info = new LOG_ACTION
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                OBJECT_NAME = (request != null && !string.IsNullOrEmpty(request.OBJECT_NAME)) ? request.OBJECT_NAME : null,
                OBJECT_ID = (request != null && request.OBJECT_ID != null) ? request.OBJECT_ID.Value : (decimal?)null,
                LOG_TYPE_CODE = (request != null && !string.IsNullOrEmpty(request.LOG_TYPE_CODE)) ? request.LOG_TYPE_CODE : null,
                MESSAGE_LOG = (request != null && !string.IsNullOrEmpty(request.MESSAGE_LOG)) ? request.MESSAGE_LOG : null,
                MESSAGE_DETAIL = (request != null && !string.IsNullOrEmpty(request.MESSAGE_DETAIL)) ? request.MESSAGE_DETAIL : null,
                WORKING_SESSION = (request != null && !string.IsNullOrEmpty(request.WORKING_SESSION)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.WORKING_SESSION, "yyyy-MM-dd")? $"{request.WORKING_SESSION} 00:00:00" : request.WORKING_SESSION, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                START_DATE = (request != null && !string.IsNullOrEmpty(request.START_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.START_DATE, "yyyy-MM-dd")? $"{request.START_DATE} 00:00:00" : request.START_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                END_DATE = (request != null && !string.IsNullOrEmpty(request.END_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.END_DATE, "yyyy-MM-dd")? $"{request.END_DATE} 00:00:00" : request.END_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                CONFIG_PIPELINE_CODE = (request != null && !string.IsNullOrEmpty(request.CONFIG_PIPELINE_CODE)) ? request.CONFIG_PIPELINE_CODE : null,
                RESULT_VALUE = (request != null && !string.IsNullOrEmpty(request.RESULT_VALUE)) ? request.RESULT_VALUE : null,
                RESULT_ETL_RDS = (request != null && request.RESULT_ETL_RDS != null) ? request.RESULT_ETL_RDS.Value : (long?)null,
                RESULT_ETL_RIS = (request != null && request.RESULT_ETL_RIS != null) ? request.RESULT_ETL_RIS.Value : (long?)null,
                PARENT_LOG_ID = (request != null && request.PARENT_LOG_ID != null) ? request.PARENT_LOG_ID.Value : (decimal?)null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<LOG_ACTION_Request> request)
		{
			var list = new List<LOG_ACTION>();
			foreach (var info in request)
			{
				list.Add(new LOG_ACTION
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                OBJECT_NAME = (info != null && !string.IsNullOrEmpty(info.OBJECT_NAME)) ? info.OBJECT_NAME : null,
                OBJECT_ID = (info != null && info.OBJECT_ID != null) ? info.OBJECT_ID.Value : (decimal?)null,
                LOG_TYPE_CODE = (info != null && !string.IsNullOrEmpty(info.LOG_TYPE_CODE)) ? info.LOG_TYPE_CODE : null,
                MESSAGE_LOG = (info != null && !string.IsNullOrEmpty(info.MESSAGE_LOG)) ? info.MESSAGE_LOG : null,
                MESSAGE_DETAIL = (info != null && !string.IsNullOrEmpty(info.MESSAGE_DETAIL)) ? info.MESSAGE_DETAIL : null,
                WORKING_SESSION = (info != null && !string.IsNullOrEmpty(info.WORKING_SESSION)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.WORKING_SESSION, "yyyy-MM-dd")? $"{info.WORKING_SESSION} 00:00:00" : info.WORKING_SESSION, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                START_DATE = (info != null && !string.IsNullOrEmpty(info.START_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.START_DATE, "yyyy-MM-dd")? $"{info.START_DATE} 00:00:00" : info.START_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                END_DATE = (info != null && !string.IsNullOrEmpty(info.END_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.END_DATE, "yyyy-MM-dd")? $"{info.END_DATE} 00:00:00" : info.END_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                CONFIG_PIPELINE_CODE = (info != null && !string.IsNullOrEmpty(info.CONFIG_PIPELINE_CODE)) ? info.CONFIG_PIPELINE_CODE : null,
                RESULT_VALUE = (info != null && !string.IsNullOrEmpty(info.RESULT_VALUE)) ? info.RESULT_VALUE : null,
                RESULT_ETL_RDS = (info != null && info.RESULT_ETL_RDS != null) ? info.RESULT_ETL_RDS.Value : (long?)null,
                RESULT_ETL_RIS = (info != null && info.RESULT_ETL_RIS != null) ? info.RESULT_ETL_RIS.Value : (long?)null,
                PARENT_LOG_ID = (info != null && info.PARENT_LOG_ID != null) ? info.PARENT_LOG_ID.Value : (decimal?)null,
                IS_ACTIVE = (info != null && info.IS_ACTIVE != null) ? info.IS_ACTIVE.Value : (short?)null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<LOG_ACTION> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<LOG_ACTION> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<LOG_ACTION> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_OBJECT_NAME",(dictParam != null && dictParam.ContainsKey("OBJECT_NAME")) ? dictParam["OBJECT_NAME"] : null),
                new OracleParameter("p_OBJECT_ID",(dictParam != null && dictParam.ContainsKey("OBJECT_ID")) ? dictParam["OBJECT_ID"] : null),
                new OracleParameter("p_LOG_TYPE_CODE",(dictParam != null && dictParam.ContainsKey("LOG_TYPE_CODE")) ? dictParam["LOG_TYPE_CODE"] : null),
                new OracleParameter("p_MESSAGE_LOG",(dictParam != null && dictParam.ContainsKey("MESSAGE_LOG")) ? dictParam["MESSAGE_LOG"] : null),
                new OracleParameter("p_MESSAGE_DETAIL",(dictParam != null && dictParam.ContainsKey("MESSAGE_DETAIL")) ? dictParam["MESSAGE_DETAIL"] : null),
                new OracleParameter("p_WORKING_SESSION_START", (dictParam != null && dictParam.ContainsKey("WORKING_SESSION_START")) ? dictParam["WORKING_SESSION_START"] : null),
                new OracleParameter("p_WORKING_SESSION_END", (dictParam != null && dictParam.ContainsKey("WORKING_SESSION_END")) ? dictParam["WORKING_SESSION_END"] : null),
                new OracleParameter("p_CONFIG_PIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("CONFIG_PIPELINE_CODE")) ? dictParam["CONFIG_PIPELINE_CODE"] : null),
                new OracleParameter("p_PARENT_LOG_ID",(dictParam != null && dictParam.ContainsKey("PARENT_LOG_ID")) ? dictParam["PARENT_LOG_ID"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<LOG_ACTION> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_OBJECT_NAME",(dictParam != null && dictParam.ContainsKey("OBJECT_NAME")) ? dictParam["OBJECT_NAME"] : null),
                new OracleParameter("p_OBJECT_ID",(dictParam != null && dictParam.ContainsKey("OBJECT_ID")) ? dictParam["OBJECT_ID"] : null),
                new OracleParameter("p_LOG_TYPE_CODE",(dictParam != null && dictParam.ContainsKey("LOG_TYPE_CODE")) ? dictParam["LOG_TYPE_CODE"] : null),
                new OracleParameter("p_MESSAGE_LOG",(dictParam != null && dictParam.ContainsKey("MESSAGE_LOG")) ? dictParam["MESSAGE_LOG"] : null),
                new OracleParameter("p_MESSAGE_DETAIL",(dictParam != null && dictParam.ContainsKey("MESSAGE_DETAIL")) ? dictParam["MESSAGE_DETAIL"] : null),
                new OracleParameter("p_WORKING_SESSION_START", (dictParam != null && dictParam.ContainsKey("WORKING_SESSION_START")) ? dictParam["WORKING_SESSION_START"] : null),
                new OracleParameter("p_WORKING_SESSION_END", (dictParam != null && dictParam.ContainsKey("WORKING_SESSION_END")) ? dictParam["WORKING_SESSION_END"] : null),
                new OracleParameter("p_CONFIG_PIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("CONFIG_PIPELINE_CODE")) ? dictParam["CONFIG_PIPELINE_CODE"] : null),
                new OracleParameter("p_PARENT_LOG_ID",(dictParam != null && dictParam.ContainsKey("PARENT_LOG_ID")) ? dictParam["PARENT_LOG_ID"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<LOG_ACTION> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public LOG_ACTION QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","OBJECT_NAME","OBJECT_ID","LOG_TYPE_CODE","MESSAGE_LOG","MESSAGE_DETAIL","WORKING_SESSION","START_DATE","END_DATE","CONFIG_PIPELINE_CODE","RESULT_VALUE","RESULT_ETL_RDS","RESULT_ETL_RIS","PARENT_LOG_ID","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<LOG_ACTION> results = QuerySolrBase<LOG_ACTION>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"LOG_ACTION/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<LOG_ACTION> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_OBJECT_NAME,			string p_OBJECT_ID,			string p_LOG_TYPE_CODE,			string p_MESSAGE_LOG,			string p_MESSAGE_DETAIL,			DateTime? p_WORKING_SESSION_START,			DateTime? p_WORKING_SESSION_END,			string p_CONFIG_PIPELINE_CODE,			string p_PARENT_LOG_ID,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_OBJECT_NAME))
			{
			    lstFilter.Add(new SolrQuery("OBJECT_NAME:" + p_OBJECT_NAME));
			}
			if (!string.IsNullOrEmpty(p_OBJECT_ID))
			{
			    lstFilter.Add(new SolrQuery("OBJECT_ID:" + p_OBJECT_ID));
			}
			if (!string.IsNullOrEmpty(p_LOG_TYPE_CODE))
			{
			    lstFilter.Add(new SolrQuery("LOG_TYPE_CODE:" + p_LOG_TYPE_CODE));
			}
			if (!string.IsNullOrEmpty(p_MESSAGE_LOG))
			{
			    lstFilter.Add(new SolrQuery("MESSAGE_LOG:" + p_MESSAGE_LOG));
			}
			if (!string.IsNullOrEmpty(p_MESSAGE_DETAIL))
			{
			    lstFilter.Add(new SolrQuery("MESSAGE_DETAIL:" + p_MESSAGE_DETAIL));
			}
			if (p_WORKING_SESSION_START != null && p_WORKING_SESSION_END != null)
			{
			    lstFilter.Add(new SolrQuery("WORKING_SESSION:[" + Utility.GetJSONZFromUserDateTime(p_WORKING_SESSION_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_WORKING_SESSION_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_CONFIG_PIPELINE_CODE))
			{
			    lstFilter.Add(new SolrQuery("CONFIG_PIPELINE_CODE:" + p_CONFIG_PIPELINE_CODE));
			}
			if (!string.IsNullOrEmpty(p_PARENT_LOG_ID))
			{
			    lstFilter.Add(new SolrQuery("PARENT_LOG_ID:" + p_PARENT_LOG_ID));
			}
			if (!string.IsNullOrEmpty(p_IS_ACTIVE))
			{
			    lstFilter.Add(new SolrQuery("IS_ACTIVE:" + p_IS_ACTIVE));
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
            string[] fieldSelect = { "ID","CODE","OBJECT_NAME","OBJECT_ID","LOG_TYPE_CODE","MESSAGE_LOG","MESSAGE_DETAIL","WORKING_SESSION","START_DATE","END_DATE","CONFIG_PIPELINE_CODE","RESULT_VALUE","RESULT_ETL_RDS","RESULT_ETL_RIS","PARENT_LOG_ID","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<LOG_ACTION>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"LOG_ACTION/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<LOG_ACTION> listAddRange = new List<LOG_ACTION>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

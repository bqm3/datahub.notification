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
    public class SYS_CONFIG_WFPIPELINE_DTLDao : OracleBaseImpl<SYS_CONFIG_WFPIPELINE_DTL>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONFIG_WFPIPELINE_DTL";
            PackageName = "PK_SYS_CONFIG_WFPIPELINE_DTL";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONFIG_WFPIPELINE_DTL_Request request)
        {        
			var info = new SYS_CONFIG_WFPIPELINE_DTL
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                WFPIPELINE_CODE = (request != null && !string.IsNullOrEmpty(request.WFPIPELINE_CODE)) ? request.WFPIPELINE_CODE : null,
                STEP_ID = (request != null && request.STEP_ID != null) ? request.STEP_ID.Value : (decimal?)null,
                STEP_NEXT = (request != null && request.STEP_NEXT != null) ? request.STEP_NEXT.Value : (decimal?)null,
                POSITION = (request != null && request.POSITION != null) ? request.POSITION.Value : (short?)null,
                TIMESTART = (request != null && !string.IsNullOrEmpty(request.TIMESTART)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.TIMESTART, "yyyy-MM-dd")? $"{request.TIMESTART} 00:00:00" : request.TIMESTART, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                TIMEEND = (request != null && !string.IsNullOrEmpty(request.TIMEEND)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.TIMEEND, "yyyy-MM-dd")? $"{request.TIMEEND} 00:00:00" : request.TIMEEND, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                LOG_ACTION_ID = (request != null && request.LOG_ACTION_ID != null) ? request.LOG_ACTION_ID.Value : (decimal?)null,
                IS_ACTION = (request != null && request.IS_ACTION != null) ? request.IS_ACTION.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_CONFIG_WFPIPELINE_DTL_Request> request)
		{
			var list = new List<SYS_CONFIG_WFPIPELINE_DTL>();
			foreach (var info in request)
			{
				list.Add(new SYS_CONFIG_WFPIPELINE_DTL
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                WFPIPELINE_CODE = (info != null && !string.IsNullOrEmpty(info.WFPIPELINE_CODE)) ? info.WFPIPELINE_CODE : null,
                STEP_ID = (info != null && info.STEP_ID != null) ? info.STEP_ID.Value : (decimal?)null,
                STEP_NEXT = (info != null && info.STEP_NEXT != null) ? info.STEP_NEXT.Value : (decimal?)null,
                POSITION = (info != null && info.POSITION != null) ? info.POSITION.Value : (short?)null,
                TIMESTART = (info != null && !string.IsNullOrEmpty(info.TIMESTART)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.TIMESTART, "yyyy-MM-dd")? $"{info.TIMESTART} 00:00:00" : info.TIMESTART, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                TIMEEND = (info != null && !string.IsNullOrEmpty(info.TIMEEND)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.TIMEEND, "yyyy-MM-dd")? $"{info.TIMEEND} 00:00:00" : info.TIMEEND, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                LOG_ACTION_ID = (info != null && info.LOG_ACTION_ID != null) ? info.LOG_ACTION_ID.Value : (decimal?)null,
                IS_ACTION = (info != null && info.IS_ACTION != null) ? info.IS_ACTION.Value : (short?)null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<SYS_CONFIG_WFPIPELINE_DTL> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_CONFIG_WFPIPELINE_DTL> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_CONFIG_WFPIPELINE_DTL> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_WFPIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("WFPIPELINE_CODE")) ? dictParam["WFPIPELINE_CODE"] : null),
                new OracleParameter("p_LOG_ACTION_ID",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_ID")) ? dictParam["LOG_ACTION_ID"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONFIG_WFPIPELINE_DTL> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_WFPIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("WFPIPELINE_CODE")) ? dictParam["WFPIPELINE_CODE"] : null),
                new OracleParameter("p_LOG_ACTION_ID",(dictParam != null && dictParam.ContainsKey("LOG_ACTION_ID")) ? dictParam["LOG_ACTION_ID"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONFIG_WFPIPELINE_DTL> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONFIG_WFPIPELINE_DTL QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","WFPIPELINE_CODE","STEP_ID","STEP_NEXT","POSITION","TIMESTART","TIMEEND","LOG_ACTION_ID","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_CONFIG_WFPIPELINE_DTL> results = QuerySolrBase<SYS_CONFIG_WFPIPELINE_DTL>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFPIPELINE_DTL/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONFIG_WFPIPELINE_DTL> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_WFPIPELINE_CODE,			string p_LOG_ACTION_ID,			string p_IS_ACTION,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_WFPIPELINE_CODE))
			{
			    lstFilter.Add(new SolrQuery("WFPIPELINE_CODE:" + p_WFPIPELINE_CODE));
			}
			if (!string.IsNullOrEmpty(p_LOG_ACTION_ID))
			{
			    lstFilter.Add(new SolrQuery("LOG_ACTION_ID:" + p_LOG_ACTION_ID));
			}
			if (!string.IsNullOrEmpty(p_IS_ACTION))
			{
			    lstFilter.Add(new SolrQuery("IS_ACTION:" + p_IS_ACTION));
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
            string[] fieldSelect = { "ID","CODE","WFPIPELINE_CODE","STEP_ID","STEP_NEXT","POSITION","TIMESTART","TIMEEND","LOG_ACTION_ID","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_CONFIG_WFPIPELINE_DTL>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFPIPELINE_DTL/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONFIG_WFPIPELINE_DTL> listAddRange = new List<SYS_CONFIG_WFPIPELINE_DTL>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class SYS_CONFIG_PIPELINEDao : OracleBaseImpl<SYS_CONFIG_PIPELINE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONFIG_PIPELINE";
            PackageName = "PK_SYS_CONFIG_PIPELINE";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONFIG_PIPELINE_Request request)
        {        
			var info = new SYS_CONFIG_PIPELINE
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                DATASET_ID = (request != null && request.DATASET_ID != null) ? request.DATASET_ID.Value : (decimal?)null,
                CONFIG_WFLOW_CODE = (request != null && !string.IsNullOrEmpty(request.CONFIG_WFLOW_CODE)) ? request.CONFIG_WFLOW_CODE : null,
                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                RUNTYPE = (request != null && request.RUNTYPE != null) ? request.RUNTYPE.Value : (short?)null,
                JOBNAME = (request != null && !string.IsNullOrEmpty(request.JOBNAME)) ? request.JOBNAME : null,
                LOCATION = (request != null && !string.IsNullOrEmpty(request.LOCATION)) ? request.LOCATION : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_CONFIG_PIPELINE_Request> request)
		{
			var list = new List<SYS_CONFIG_PIPELINE>();
			foreach (var info in request)
			{
				list.Add(new SYS_CONFIG_PIPELINE
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DATASET_ID = (info != null && info.DATASET_ID != null) ? info.DATASET_ID.Value : (decimal?)null,
                CONFIG_WFLOW_CODE = (info != null && !string.IsNullOrEmpty(info.CONFIG_WFLOW_CODE)) ? info.CONFIG_WFLOW_CODE : null,
                NOTE = (info != null && !string.IsNullOrEmpty(info.NOTE)) ? info.NOTE : null,
                RUNTYPE = (info != null && info.RUNTYPE != null) ? info.RUNTYPE.Value : (short?)null,
                JOBNAME = (info != null && !string.IsNullOrEmpty(info.JOBNAME)) ? info.JOBNAME : null,
                LOCATION = (info != null && !string.IsNullOrEmpty(info.LOCATION)) ? info.LOCATION : null,
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
		public bool? UpdateList(List<SYS_CONFIG_PIPELINE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_CONFIG_PIPELINE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_CONFIG_PIPELINE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DATASET_ID",(dictParam != null && dictParam.ContainsKey("DATASET_ID")) ? dictParam["DATASET_ID"] : null),
                new OracleParameter("p_CONFIG_WFLOW_CODE",(dictParam != null && dictParam.ContainsKey("CONFIG_WFLOW_CODE")) ? dictParam["CONFIG_WFLOW_CODE"] : null),
                new OracleParameter("p_RUNTYPE",(dictParam != null && dictParam.ContainsKey("RUNTYPE")) ? dictParam["RUNTYPE"] : null),
                new OracleParameter("p_JOBNAME",(dictParam != null && dictParam.ContainsKey("JOBNAME")) ? dictParam["JOBNAME"] : null),
                new OracleParameter("p_LOCATION",(dictParam != null && dictParam.ContainsKey("LOCATION")) ? dictParam["LOCATION"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONFIG_PIPELINE> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DATASET_ID",(dictParam != null && dictParam.ContainsKey("DATASET_ID")) ? dictParam["DATASET_ID"] : null),
                new OracleParameter("p_CONFIG_WFLOW_CODE",(dictParam != null && dictParam.ContainsKey("CONFIG_WFLOW_CODE")) ? dictParam["CONFIG_WFLOW_CODE"] : null),
                new OracleParameter("p_RUNTYPE",(dictParam != null && dictParam.ContainsKey("RUNTYPE")) ? dictParam["RUNTYPE"] : null),
                new OracleParameter("p_JOBNAME",(dictParam != null && dictParam.ContainsKey("JOBNAME")) ? dictParam["JOBNAME"] : null),
                new OracleParameter("p_LOCATION",(dictParam != null && dictParam.ContainsKey("LOCATION")) ? dictParam["LOCATION"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONFIG_PIPELINE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONFIG_PIPELINE QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DATASET_ID","CONFIG_WFLOW_CODE","NOTE","RUNTYPE","JOBNAME","LOCATION","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_CONFIG_PIPELINE> results = QuerySolrBase<SYS_CONFIG_PIPELINE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_PIPELINE/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONFIG_PIPELINE> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DATASET_ID,			string p_CONFIG_WFLOW_CODE,			string p_RUNTYPE,			string p_JOBNAME,			string p_LOCATION,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DATASET_ID))
			{
			    lstFilter.Add(new SolrQuery("DATASET_ID:" + p_DATASET_ID));
			}
			if (!string.IsNullOrEmpty(p_CONFIG_WFLOW_CODE))
			{
			    lstFilter.Add(new SolrQuery("CONFIG_WFLOW_CODE:" + p_CONFIG_WFLOW_CODE));
			}
			if (!string.IsNullOrEmpty(p_RUNTYPE))
			{
			    lstFilter.Add(new SolrQuery("RUNTYPE:" + p_RUNTYPE));
			}
			if (!string.IsNullOrEmpty(p_JOBNAME))
			{
			    lstFilter.Add(new SolrQuery("JOBNAME:" + p_JOBNAME));
			}
			if (!string.IsNullOrEmpty(p_LOCATION))
			{
			    lstFilter.Add(new SolrQuery("LOCATION:" + p_LOCATION));
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
            string[] fieldSelect = { "ID","CODE","DATASET_ID","CONFIG_WFLOW_CODE","NOTE","RUNTYPE","JOBNAME","LOCATION","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_CONFIG_PIPELINE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_PIPELINE/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONFIG_PIPELINE> listAddRange = new List<SYS_CONFIG_PIPELINE>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

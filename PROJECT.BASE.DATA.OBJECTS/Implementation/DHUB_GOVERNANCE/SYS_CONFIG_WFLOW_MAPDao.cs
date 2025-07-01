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
    public class SYS_CONFIG_WFLOW_MAPDao : OracleBaseImpl<SYS_CONFIG_WFLOW_MAP>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONFIG_WFLOW_MAP";
            PackageName = "PK_SYS_CONFIG_WFLOW_MAP";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONFIG_WFLOW_MAP_Request request)
        {        
			var info = new SYS_CONFIG_WFLOW_MAP
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                PIPELINE_CODE = (request != null && !string.IsNullOrEmpty(request.PIPELINE_CODE)) ? request.PIPELINE_CODE : null,
                DATA_METHOD = (request != null && !string.IsNullOrEmpty(request.DATA_METHOD)) ? request.DATA_METHOD : null,
                DATASET_DTL_ID_SOURCE = (request != null && request.DATASET_DTL_ID_SOURCE != null) ? request.DATASET_DTL_ID_SOURCE.Value : (decimal?)null,
                DATASET_DTL_ID_DEST = (request != null && request.DATASET_DTL_ID_DEST != null) ? request.DATASET_DTL_ID_DEST.Value : (decimal?)null,
                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_CONFIG_WFLOW_MAP_Request> request)
		{
			var list = new List<SYS_CONFIG_WFLOW_MAP>();
			foreach (var info in request)
			{
				list.Add(new SYS_CONFIG_WFLOW_MAP
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                PIPELINE_CODE = (info != null && !string.IsNullOrEmpty(info.PIPELINE_CODE)) ? info.PIPELINE_CODE : null,
                DATA_METHOD = (info != null && !string.IsNullOrEmpty(info.DATA_METHOD)) ? info.DATA_METHOD : null,
                DATASET_DTL_ID_SOURCE = (info != null && info.DATASET_DTL_ID_SOURCE != null) ? info.DATASET_DTL_ID_SOURCE.Value : (decimal?)null,
                DATASET_DTL_ID_DEST = (info != null && info.DATASET_DTL_ID_DEST != null) ? info.DATASET_DTL_ID_DEST.Value : (decimal?)null,
                NOTE = (info != null && !string.IsNullOrEmpty(info.NOTE)) ? info.NOTE : null,
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
		public bool? UpdateList(List<SYS_CONFIG_WFLOW_MAP> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_CONFIG_WFLOW_MAP> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_CONFIG_WFLOW_MAP> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_PIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("PIPELINE_CODE")) ? dictParam["PIPELINE_CODE"] : null),
                new OracleParameter("p_DATA_METHOD",(dictParam != null && dictParam.ContainsKey("DATA_METHOD")) ? dictParam["DATA_METHOD"] : null),
                new OracleParameter("p_DATASET_DTL_ID_SOURCE",(dictParam != null && dictParam.ContainsKey("DATASET_DTL_ID_SOURCE")) ? dictParam["DATASET_DTL_ID_SOURCE"] : null),
                new OracleParameter("p_DATASET_DTL_ID_DEST",(dictParam != null && dictParam.ContainsKey("DATASET_DTL_ID_DEST")) ? dictParam["DATASET_DTL_ID_DEST"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONFIG_WFLOW_MAP> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_PIPELINE_CODE",(dictParam != null && dictParam.ContainsKey("PIPELINE_CODE")) ? dictParam["PIPELINE_CODE"] : null),
                new OracleParameter("p_DATA_METHOD",(dictParam != null && dictParam.ContainsKey("DATA_METHOD")) ? dictParam["DATA_METHOD"] : null),
                new OracleParameter("p_DATASET_DTL_ID_SOURCE",(dictParam != null && dictParam.ContainsKey("DATASET_DTL_ID_SOURCE")) ? dictParam["DATASET_DTL_ID_SOURCE"] : null),
                new OracleParameter("p_DATASET_DTL_ID_DEST",(dictParam != null && dictParam.ContainsKey("DATASET_DTL_ID_DEST")) ? dictParam["DATASET_DTL_ID_DEST"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONFIG_WFLOW_MAP> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONFIG_WFLOW_MAP QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","PIPELINE_CODE","DATA_METHOD","DATASET_DTL_ID_SOURCE","DATASET_DTL_ID_DEST","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_CONFIG_WFLOW_MAP> results = QuerySolrBase<SYS_CONFIG_WFLOW_MAP>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFLOW_MAP/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONFIG_WFLOW_MAP> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_PIPELINE_CODE,			string p_DATA_METHOD,			string p_DATASET_DTL_ID_SOURCE,			string p_DATASET_DTL_ID_DEST,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_PIPELINE_CODE))
			{
			    lstFilter.Add(new SolrQuery("PIPELINE_CODE:" + p_PIPELINE_CODE));
			}
			if (!string.IsNullOrEmpty(p_DATA_METHOD))
			{
			    lstFilter.Add(new SolrQuery("DATA_METHOD:" + p_DATA_METHOD));
			}
			if (!string.IsNullOrEmpty(p_DATASET_DTL_ID_SOURCE))
			{
			    lstFilter.Add(new SolrQuery("DATASET_DTL_ID_SOURCE:" + p_DATASET_DTL_ID_SOURCE));
			}
			if (!string.IsNullOrEmpty(p_DATASET_DTL_ID_DEST))
			{
			    lstFilter.Add(new SolrQuery("DATASET_DTL_ID_DEST:" + p_DATASET_DTL_ID_DEST));
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
            string[] fieldSelect = { "ID","CODE","PIPELINE_CODE","DATA_METHOD","DATASET_DTL_ID_SOURCE","DATASET_DTL_ID_DEST","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_CONFIG_WFLOW_MAP>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFLOW_MAP/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONFIG_WFLOW_MAP> listAddRange = new List<SYS_CONFIG_WFLOW_MAP>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

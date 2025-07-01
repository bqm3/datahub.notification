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
    public class SYS_CONFIG_WFLOWDao : OracleBaseImpl<SYS_CONFIG_WFLOW>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONFIG_WFLOW";
            PackageName = "PK_SYS_CONFIG_WFLOW";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONFIG_WFLOW_Request request)
        {        
			var info = new SYS_CONFIG_WFLOW
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                DATA_ZONE_CODE_START = (request != null && !string.IsNullOrEmpty(request.DATA_ZONE_CODE_START)) ? request.DATA_ZONE_CODE_START : null,
                DATA_ZONE_CODE_END = (request != null && !string.IsNullOrEmpty(request.DATA_ZONE_CODE_END)) ? request.DATA_ZONE_CODE_END : null,
                ETL_TYPE = (request != null && request.ETL_TYPE != null) ? request.ETL_TYPE.Value : (short?)null,
                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_CONFIG_WFLOW_Request> request)
		{
			var list = new List<SYS_CONFIG_WFLOW>();
			foreach (var info in request)
			{
				list.Add(new SYS_CONFIG_WFLOW
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DATA_ZONE_CODE_START = (info != null && !string.IsNullOrEmpty(info.DATA_ZONE_CODE_START)) ? info.DATA_ZONE_CODE_START : null,
                DATA_ZONE_CODE_END = (info != null && !string.IsNullOrEmpty(info.DATA_ZONE_CODE_END)) ? info.DATA_ZONE_CODE_END : null,
                ETL_TYPE = (info != null && info.ETL_TYPE != null) ? info.ETL_TYPE.Value : (short?)null,
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
		public bool? UpdateList(List<SYS_CONFIG_WFLOW> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_CONFIG_WFLOW> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_CONFIG_WFLOW> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DATA_ZONE_CODE_START",(dictParam != null && dictParam.ContainsKey("DATA_ZONE_CODE_START")) ? dictParam["DATA_ZONE_CODE_START"] : null),
                new OracleParameter("p_DATA_ZONE_CODE_END",(dictParam != null && dictParam.ContainsKey("DATA_ZONE_CODE_END")) ? dictParam["DATA_ZONE_CODE_END"] : null),
                new OracleParameter("p_ETL_TYPE",(dictParam != null && dictParam.ContainsKey("ETL_TYPE")) ? dictParam["ETL_TYPE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONFIG_WFLOW> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DATA_ZONE_CODE_START",(dictParam != null && dictParam.ContainsKey("DATA_ZONE_CODE_START")) ? dictParam["DATA_ZONE_CODE_START"] : null),
                new OracleParameter("p_DATA_ZONE_CODE_END",(dictParam != null && dictParam.ContainsKey("DATA_ZONE_CODE_END")) ? dictParam["DATA_ZONE_CODE_END"] : null),
                new OracleParameter("p_ETL_TYPE",(dictParam != null && dictParam.ContainsKey("ETL_TYPE")) ? dictParam["ETL_TYPE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONFIG_WFLOW> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONFIG_WFLOW QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DATA_ZONE_CODE_START","DATA_ZONE_CODE_END","ETL_TYPE","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_CONFIG_WFLOW> results = QuerySolrBase<SYS_CONFIG_WFLOW>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFLOW/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONFIG_WFLOW> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DATA_ZONE_CODE_START,			string p_DATA_ZONE_CODE_END,			string p_ETL_TYPE,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DATA_ZONE_CODE_START))
			{
			    lstFilter.Add(new SolrQuery("DATA_ZONE_CODE_START:" + p_DATA_ZONE_CODE_START));
			}
			if (!string.IsNullOrEmpty(p_DATA_ZONE_CODE_END))
			{
			    lstFilter.Add(new SolrQuery("DATA_ZONE_CODE_END:" + p_DATA_ZONE_CODE_END));
			}
			if (!string.IsNullOrEmpty(p_ETL_TYPE))
			{
			    lstFilter.Add(new SolrQuery("ETL_TYPE:" + p_ETL_TYPE));
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
            string[] fieldSelect = { "ID","CODE","DATA_ZONE_CODE_START","DATA_ZONE_CODE_END","ETL_TYPE","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_CONFIG_WFLOW>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFLOW/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONFIG_WFLOW> listAddRange = new List<SYS_CONFIG_WFLOW>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

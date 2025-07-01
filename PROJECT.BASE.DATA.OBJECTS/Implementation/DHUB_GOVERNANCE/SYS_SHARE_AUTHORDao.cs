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
    public class SYS_SHARE_AUTHORDao : OracleBaseImpl<SYS_SHARE_AUTHOR>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_SHARE_AUTHOR";
            PackageName = "PK_SYS_SHARE_AUTHOR";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_SHARE_AUTHOR_Request request)
        {        
			var info = new SYS_SHARE_AUTHOR
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                CORP_CODE = (request != null && !string.IsNullOrEmpty(request.CORP_CODE)) ? request.CORP_CODE : null,
                SCHEMAS = (request != null && !string.IsNullOrEmpty(request.SCHEMAS)) ? request.SCHEMAS : null,
                TABLES = (request != null && !string.IsNullOrEmpty(request.TABLES)) ? request.TABLES : null,
                COLUMNS = (request != null && !string.IsNullOrEmpty(request.COLUMNS)) ? request.COLUMNS : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_SHARE_AUTHOR_Request> request)
		{
			var list = new List<SYS_SHARE_AUTHOR>();
			foreach (var info in request)
			{
				list.Add(new SYS_SHARE_AUTHOR
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                CORP_CODE = (info != null && !string.IsNullOrEmpty(info.CORP_CODE)) ? info.CORP_CODE : null,
                SCHEMAS = (info != null && !string.IsNullOrEmpty(info.SCHEMAS)) ? info.SCHEMAS : null,
                TABLES = (info != null && !string.IsNullOrEmpty(info.TABLES)) ? info.TABLES : null,
                COLUMNS = (info != null && !string.IsNullOrEmpty(info.COLUMNS)) ? info.COLUMNS : null,
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
		public bool? UpdateList(List<SYS_SHARE_AUTHOR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_SHARE_AUTHOR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_SHARE_AUTHOR> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CORP_CODE",(dictParam != null && dictParam.ContainsKey("CORP_CODE")) ? dictParam["CORP_CODE"] : null),
                new OracleParameter("p_SCHEMAS",(dictParam != null && dictParam.ContainsKey("SCHEMAS")) ? dictParam["SCHEMAS"] : null),
                new OracleParameter("p_TABLES",(dictParam != null && dictParam.ContainsKey("TABLES")) ? dictParam["TABLES"] : null),
                new OracleParameter("p_COLUMNS",(dictParam != null && dictParam.ContainsKey("COLUMNS")) ? dictParam["COLUMNS"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_SHARE_AUTHOR> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CORP_CODE",(dictParam != null && dictParam.ContainsKey("CORP_CODE")) ? dictParam["CORP_CODE"] : null),
                new OracleParameter("p_SCHEMAS",(dictParam != null && dictParam.ContainsKey("SCHEMAS")) ? dictParam["SCHEMAS"] : null),
                new OracleParameter("p_TABLES",(dictParam != null && dictParam.ContainsKey("TABLES")) ? dictParam["TABLES"] : null),
                new OracleParameter("p_COLUMNS",(dictParam != null && dictParam.ContainsKey("COLUMNS")) ? dictParam["COLUMNS"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_SHARE_AUTHOR> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_SHARE_AUTHOR QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","CORP_CODE","SCHEMAS","TABLES","COLUMNS","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_SHARE_AUTHOR> results = QuerySolrBase<SYS_SHARE_AUTHOR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_SHARE_AUTHOR/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_SHARE_AUTHOR> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_CORP_CODE,			string p_SCHEMAS,			string p_TABLES,			string p_COLUMNS,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_CORP_CODE))
			{
			    lstFilter.Add(new SolrQuery("CORP_CODE:" + p_CORP_CODE));
			}
			if (!string.IsNullOrEmpty(p_SCHEMAS))
			{
			    lstFilter.Add(new SolrQuery("SCHEMAS:" + p_SCHEMAS));
			}
			if (!string.IsNullOrEmpty(p_TABLES))
			{
			    lstFilter.Add(new SolrQuery("TABLES:" + p_TABLES));
			}
			if (!string.IsNullOrEmpty(p_COLUMNS))
			{
			    lstFilter.Add(new SolrQuery("COLUMNS:" + p_COLUMNS));
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
            string[] fieldSelect = { "ID","CODE","CORP_CODE","SCHEMAS","TABLES","COLUMNS","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_SHARE_AUTHOR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_SHARE_AUTHOR/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_SHARE_AUTHOR> listAddRange = new List<SYS_SHARE_AUTHOR>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

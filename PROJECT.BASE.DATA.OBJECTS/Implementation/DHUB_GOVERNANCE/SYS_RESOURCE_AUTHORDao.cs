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
    public class SYS_RESOURCE_AUTHORDao : OracleBaseImpl<SYS_RESOURCE_AUTHOR>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_RESOURCE_AUTHOR";
            PackageName = "PK_SYS_RESOURCE_AUTHOR";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_RESOURCE_AUTHOR_Request request)
        {        
			var info = new SYS_RESOURCE_AUTHOR
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                ROLE_CODE = (request != null && !string.IsNullOrEmpty(request.ROLE_CODE)) ? request.ROLE_CODE : null,
                ACCOUNT = (request != null && !string.IsNullOrEmpty(request.ACCOUNT)) ? request.ACCOUNT : null,
                RESOURCE_CODE = (request != null && !string.IsNullOrEmpty(request.RESOURCE_CODE)) ? request.RESOURCE_CODE : null,
                RESOURCE_NAME = (request != null && !string.IsNullOrEmpty(request.RESOURCE_NAME)) ? request.RESOURCE_NAME : null,
                PARENT_CODE = (request != null && !string.IsNullOrEmpty(request.PARENT_CODE)) ? request.PARENT_CODE : null,
                PARENT_NAME = (request != null && !string.IsNullOrEmpty(request.PARENT_NAME)) ? request.PARENT_NAME : null,
                LINK = (request != null && !string.IsNullOrEmpty(request.LINK)) ? request.LINK : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_RESOURCE_AUTHOR_Request> request)
		{
			var list = new List<SYS_RESOURCE_AUTHOR>();
			foreach (var info in request)
			{
				list.Add(new SYS_RESOURCE_AUTHOR
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                ROLE_CODE = (info != null && !string.IsNullOrEmpty(info.ROLE_CODE)) ? info.ROLE_CODE : null,
                ACCOUNT = (info != null && !string.IsNullOrEmpty(info.ACCOUNT)) ? info.ACCOUNT : null,
                RESOURCE_CODE = (info != null && !string.IsNullOrEmpty(info.RESOURCE_CODE)) ? info.RESOURCE_CODE : null,
                RESOURCE_NAME = (info != null && !string.IsNullOrEmpty(info.RESOURCE_NAME)) ? info.RESOURCE_NAME : null,
                PARENT_CODE = (info != null && !string.IsNullOrEmpty(info.PARENT_CODE)) ? info.PARENT_CODE : null,
                PARENT_NAME = (info != null && !string.IsNullOrEmpty(info.PARENT_NAME)) ? info.PARENT_NAME : null,
                LINK = (info != null && !string.IsNullOrEmpty(info.LINK)) ? info.LINK : null,
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
		public bool? UpdateList(List<SYS_RESOURCE_AUTHOR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_RESOURCE_AUTHOR> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_RESOURCE_AUTHOR> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_ROLE_CODE",(dictParam != null && dictParam.ContainsKey("ROLE_CODE")) ? dictParam["ROLE_CODE"] : null),
                new OracleParameter("p_ACCOUNT",(dictParam != null && dictParam.ContainsKey("ACCOUNT")) ? dictParam["ACCOUNT"] : null),
                new OracleParameter("p_RESOURCE_CODE",(dictParam != null && dictParam.ContainsKey("RESOURCE_CODE")) ? dictParam["RESOURCE_CODE"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_RESOURCE_AUTHOR> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_ROLE_CODE",(dictParam != null && dictParam.ContainsKey("ROLE_CODE")) ? dictParam["ROLE_CODE"] : null),
                new OracleParameter("p_ACCOUNT",(dictParam != null && dictParam.ContainsKey("ACCOUNT")) ? dictParam["ACCOUNT"] : null),
                new OracleParameter("p_RESOURCE_CODE",(dictParam != null && dictParam.ContainsKey("RESOURCE_CODE")) ? dictParam["RESOURCE_CODE"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_RESOURCE_AUTHOR> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_RESOURCE_AUTHOR QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","ROLE_CODE","ACCOUNT","RESOURCE_CODE","RESOURCE_NAME","PARENT_CODE","PARENT_NAME","LINK","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_RESOURCE_AUTHOR> results = QuerySolrBase<SYS_RESOURCE_AUTHOR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_RESOURCE_AUTHOR/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_RESOURCE_AUTHOR> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_ROLE_CODE,			string p_ACCOUNT,			string p_RESOURCE_CODE,			string p_PARENT_CODE,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_ROLE_CODE))
			{
			    lstFilter.Add(new SolrQuery("ROLE_CODE:" + p_ROLE_CODE));
			}
			if (!string.IsNullOrEmpty(p_ACCOUNT))
			{
			    lstFilter.Add(new SolrQuery("ACCOUNT:" + p_ACCOUNT));
			}
			if (!string.IsNullOrEmpty(p_RESOURCE_CODE))
			{
			    lstFilter.Add(new SolrQuery("RESOURCE_CODE:" + p_RESOURCE_CODE));
			}
			if (!string.IsNullOrEmpty(p_PARENT_CODE))
			{
			    lstFilter.Add(new SolrQuery("PARENT_CODE:" + p_PARENT_CODE));
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
            string[] fieldSelect = { "ID","CODE","ROLE_CODE","ACCOUNT","RESOURCE_CODE","RESOURCE_NAME","PARENT_CODE","PARENT_NAME","LINK","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_RESOURCE_AUTHOR>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_RESOURCE_AUTHOR/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_RESOURCE_AUTHOR> listAddRange = new List<SYS_RESOURCE_AUTHOR>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

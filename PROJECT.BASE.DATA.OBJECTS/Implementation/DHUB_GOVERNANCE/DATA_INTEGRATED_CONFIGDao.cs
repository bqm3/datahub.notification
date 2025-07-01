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
    public class DATA_INTEGRATED_CONFIGDao : OracleBaseImpl<DATA_INTEGRATED_CONFIG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "DATA_INTEGRATED_CONFIG";
            PackageName = "PK_DATA_INTEGRATED_CONFIG";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(DATA_INTEGRATED_CONFIG_Request request)
        {        
			var info = new DATA_INTEGRATED_CONFIG
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                URL = (request != null && !string.IsNullOrEmpty(request.URL)) ? request.URL : null,
                FILE_NAME = (request != null && !string.IsNullOrEmpty(request.FILE_NAME)) ? request.FILE_NAME : null,
                DATA_SOURCE = (request != null && !string.IsNullOrEmpty(request.DATA_SOURCE)) ? request.DATA_SOURCE : null,
                FIELD = (request != null && !string.IsNullOrEmpty(request.FIELD)) ? request.FIELD : null,
                INTEGRATED_TYPE = (request != null && request.INTEGRATED_TYPE != null) ? request.INTEGRATED_TYPE.Value : (decimal?)null,
                GROUP_ID = (request != null && !string.IsNullOrEmpty(request.GROUP_ID)) ? request.GROUP_ID : null,
                FILE_TYPE = (request != null && !string.IsNullOrEmpty(request.FILE_TYPE)) ? request.FILE_TYPE : null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<DATA_INTEGRATED_CONFIG_Request> request)
		{
			var list = new List<DATA_INTEGRATED_CONFIG>();
			foreach (var info in request)
			{
				list.Add(new DATA_INTEGRATED_CONFIG
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                URL = (info != null && !string.IsNullOrEmpty(info.URL)) ? info.URL : null,
                FILE_NAME = (info != null && !string.IsNullOrEmpty(info.FILE_NAME)) ? info.FILE_NAME : null,
                DATA_SOURCE = (info != null && !string.IsNullOrEmpty(info.DATA_SOURCE)) ? info.DATA_SOURCE : null,
                FIELD = (info != null && !string.IsNullOrEmpty(info.FIELD)) ? info.FIELD : null,
                INTEGRATED_TYPE = (info != null && info.INTEGRATED_TYPE != null) ? info.INTEGRATED_TYPE.Value : (decimal?)null,
                GROUP_ID = (info != null && !string.IsNullOrEmpty(info.GROUP_ID)) ? info.GROUP_ID : null,
                FILE_TYPE = (info != null && !string.IsNullOrEmpty(info.FILE_TYPE)) ? info.FILE_TYPE : null,					
					IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
					CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
				});
			}
			var getPropertyNames = this.GetPropertyNames();
			var result = this.InsertList(list , getPropertyNames);            
			return result;
		}
		public bool? UpdateList(List<DATA_INTEGRATED_CONFIG> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<DATA_INTEGRATED_CONFIG> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<DATA_INTEGRATED_CONFIG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_URL",(dictParam != null && dictParam.ContainsKey("URL")) ? dictParam["URL"] : null),
                new OracleParameter("p_FILE_NAME",(dictParam != null && dictParam.ContainsKey("FILE_NAME")) ? dictParam["FILE_NAME"] : null),
                new OracleParameter("p_DATA_SOURCE",(dictParam != null && dictParam.ContainsKey("DATA_SOURCE")) ? dictParam["DATA_SOURCE"] : null),
                new OracleParameter("p_FIELD",(dictParam != null && dictParam.ContainsKey("FIELD")) ? dictParam["FIELD"] : null),
                new OracleParameter("p_INTEGRATED_TYPE",(dictParam != null && dictParam.ContainsKey("INTEGRATED_TYPE")) ? dictParam["INTEGRATED_TYPE"] : null),
                new OracleParameter("p_GROUP_ID",(dictParam != null && dictParam.ContainsKey("GROUP_ID")) ? dictParam["GROUP_ID"] : null),
                new OracleParameter("p_FILE_TYPE",(dictParam != null && dictParam.ContainsKey("FILE_TYPE")) ? dictParam["FILE_TYPE"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<DATA_INTEGRATED_CONFIG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_URL",(dictParam != null && dictParam.ContainsKey("URL")) ? dictParam["URL"] : null),
                new OracleParameter("p_FILE_NAME",(dictParam != null && dictParam.ContainsKey("FILE_NAME")) ? dictParam["FILE_NAME"] : null),
                new OracleParameter("p_DATA_SOURCE",(dictParam != null && dictParam.ContainsKey("DATA_SOURCE")) ? dictParam["DATA_SOURCE"] : null),
                new OracleParameter("p_FIELD",(dictParam != null && dictParam.ContainsKey("FIELD")) ? dictParam["FIELD"] : null),
                new OracleParameter("p_INTEGRATED_TYPE",(dictParam != null && dictParam.ContainsKey("INTEGRATED_TYPE")) ? dictParam["INTEGRATED_TYPE"] : null),
                new OracleParameter("p_GROUP_ID",(dictParam != null && dictParam.ContainsKey("GROUP_ID")) ? dictParam["GROUP_ID"] : null),
                new OracleParameter("p_FILE_TYPE",(dictParam != null && dictParam.ContainsKey("FILE_TYPE")) ? dictParam["FILE_TYPE"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<DATA_INTEGRATED_CONFIG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public DATA_INTEGRATED_CONFIG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","URL","FILE_NAME","DATA_SOURCE","FIELD","INTEGRATED_TYPE","GROUP_ID","FILE_TYPE","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<DATA_INTEGRATED_CONFIG> results = QuerySolrBase<DATA_INTEGRATED_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"DATA_INTEGRATED_CONFIG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<DATA_INTEGRATED_CONFIG> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_URL,			string p_FILE_NAME,			string p_DATA_SOURCE,			string p_FIELD,			string p_INTEGRATED_TYPE,			string p_GROUP_ID,			string p_FILE_TYPE,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_URL))
			{
			    lstFilter.Add(new SolrQuery("URL:" + p_URL));
			}
			if (!string.IsNullOrEmpty(p_FILE_NAME))
			{
			    lstFilter.Add(new SolrQuery("FILE_NAME:" + p_FILE_NAME));
			}
			if (!string.IsNullOrEmpty(p_DATA_SOURCE))
			{
			    lstFilter.Add(new SolrQuery("DATA_SOURCE:" + p_DATA_SOURCE));
			}
			if (!string.IsNullOrEmpty(p_FIELD))
			{
			    lstFilter.Add(new SolrQuery("FIELD:" + p_FIELD));
			}
			if (!string.IsNullOrEmpty(p_INTEGRATED_TYPE))
			{
			    lstFilter.Add(new SolrQuery("INTEGRATED_TYPE:" + p_INTEGRATED_TYPE));
			}
			if (!string.IsNullOrEmpty(p_GROUP_ID))
			{
			    lstFilter.Add(new SolrQuery("GROUP_ID:" + p_GROUP_ID));
			}
			if (!string.IsNullOrEmpty(p_FILE_TYPE))
			{
			    lstFilter.Add(new SolrQuery("FILE_TYPE:" + p_FILE_TYPE));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","URL","FILE_NAME","DATA_SOURCE","FIELD","INTEGRATED_TYPE","GROUP_ID","FILE_TYPE","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<DATA_INTEGRATED_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"DATA_INTEGRATED_CONFIG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<DATA_INTEGRATED_CONFIG> listAddRange = new List<DATA_INTEGRATED_CONFIG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class ETL_DATA_CONFIGDao : OracleBaseImpl<ETL_DATA_CONFIG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "ETL_DATA_CONFIG";
            PackageName = "PK_ETL_DATA_CONFIG";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(ETL_DATA_CONFIG_Request request)
        {        
			var info = new ETL_DATA_CONFIG
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                SOURCE_OBJECT_NAME = (request != null && !string.IsNullOrEmpty(request.SOURCE_OBJECT_NAME)) ? request.SOURCE_OBJECT_NAME : null,
                OBJECT_ID = (request != null && request.OBJECT_ID != null) ? request.OBJECT_ID.Value : (decimal?)null,
                ELTNAME = (request != null && !string.IsNullOrEmpty(request.ELTNAME)) ? request.ELTNAME : null,
                OBJECT_OWNER = (request != null && !string.IsNullOrEmpty(request.OBJECT_OWNER)) ? request.OBJECT_OWNER : null,
                LINK_OWNER = (request != null && !string.IsNullOrEmpty(request.LINK_OWNER)) ? request.LINK_OWNER : null,
                ETL_CONNECT = (request != null && !string.IsNullOrEmpty(request.ETL_CONNECT)) ? request.ETL_CONNECT : null,
                DATA_ZONE_CODE_START = (request != null && !string.IsNullOrEmpty(request.DATA_ZONE_CODE_START)) ? request.DATA_ZONE_CODE_START : null,
                DATA_ZONE_CODE_END = (request != null && !string.IsNullOrEmpty(request.DATA_ZONE_CODE_END)) ? request.DATA_ZONE_CODE_END : null,
                SOURCE_OBJECT = (request != null && !string.IsNullOrEmpty(request.SOURCE_OBJECT)) ? request.SOURCE_OBJECT : null,
                SOURCE_FIELDS_NAME = (request != null && !string.IsNullOrEmpty(request.SOURCE_FIELDS_NAME)) ? request.SOURCE_FIELDS_NAME : null,
                DEST_OBJECT = (request != null && !string.IsNullOrEmpty(request.DEST_OBJECT)) ? request.DEST_OBJECT : null,
                DEST_FIELDS_NAME = (request != null && !string.IsNullOrEmpty(request.DEST_FIELDS_NAME)) ? request.DEST_FIELDS_NAME : null,
                IS_ACTION = (request != null && request.IS_ACTION != null) ? request.IS_ACTION.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<ETL_DATA_CONFIG_Request> request)
		{
			var list = new List<ETL_DATA_CONFIG>();
			foreach (var info in request)
			{
				list.Add(new ETL_DATA_CONFIG
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                SOURCE_OBJECT_NAME = (info != null && !string.IsNullOrEmpty(info.SOURCE_OBJECT_NAME)) ? info.SOURCE_OBJECT_NAME : null,
                OBJECT_ID = (info != null && info.OBJECT_ID != null) ? info.OBJECT_ID.Value : (decimal?)null,
                ELTNAME = (info != null && !string.IsNullOrEmpty(info.ELTNAME)) ? info.ELTNAME : null,
                OBJECT_OWNER = (info != null && !string.IsNullOrEmpty(info.OBJECT_OWNER)) ? info.OBJECT_OWNER : null,
                LINK_OWNER = (info != null && !string.IsNullOrEmpty(info.LINK_OWNER)) ? info.LINK_OWNER : null,
                ETL_CONNECT = (info != null && !string.IsNullOrEmpty(info.ETL_CONNECT)) ? info.ETL_CONNECT : null,
                DATA_ZONE_CODE_START = (info != null && !string.IsNullOrEmpty(info.DATA_ZONE_CODE_START)) ? info.DATA_ZONE_CODE_START : null,
                DATA_ZONE_CODE_END = (info != null && !string.IsNullOrEmpty(info.DATA_ZONE_CODE_END)) ? info.DATA_ZONE_CODE_END : null,
                SOURCE_OBJECT = (info != null && !string.IsNullOrEmpty(info.SOURCE_OBJECT)) ? info.SOURCE_OBJECT : null,
                SOURCE_FIELDS_NAME = (info != null && !string.IsNullOrEmpty(info.SOURCE_FIELDS_NAME)) ? info.SOURCE_FIELDS_NAME : null,
                DEST_OBJECT = (info != null && !string.IsNullOrEmpty(info.DEST_OBJECT)) ? info.DEST_OBJECT : null,
                DEST_FIELDS_NAME = (info != null && !string.IsNullOrEmpty(info.DEST_FIELDS_NAME)) ? info.DEST_FIELDS_NAME : null,
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
		public bool? UpdateList(List<ETL_DATA_CONFIG> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<ETL_DATA_CONFIG> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<ETL_DATA_CONFIG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_SOURCE_OBJECT_NAME",(dictParam != null && dictParam.ContainsKey("SOURCE_OBJECT_NAME")) ? dictParam["SOURCE_OBJECT_NAME"] : null),
                new OracleParameter("p_OBJECT_ID",(dictParam != null && dictParam.ContainsKey("OBJECT_ID")) ? dictParam["OBJECT_ID"] : null),
                new OracleParameter("p_ELTNAME",(dictParam != null && dictParam.ContainsKey("ELTNAME")) ? dictParam["ELTNAME"] : null),
                new OracleParameter("p_OBJECT_OWNER",(dictParam != null && dictParam.ContainsKey("OBJECT_OWNER")) ? dictParam["OBJECT_OWNER"] : null),
                new OracleParameter("p_LINK_OWNER",(dictParam != null && dictParam.ContainsKey("LINK_OWNER")) ? dictParam["LINK_OWNER"] : null),
                new OracleParameter("p_ETL_CONNECT",(dictParam != null && dictParam.ContainsKey("ETL_CONNECT")) ? dictParam["ETL_CONNECT"] : null),
                new OracleParameter("p_SOURCE_OBJECT",(dictParam != null && dictParam.ContainsKey("SOURCE_OBJECT")) ? dictParam["SOURCE_OBJECT"] : null),
                new OracleParameter("p_SOURCE_FIELDS_NAME",(dictParam != null && dictParam.ContainsKey("SOURCE_FIELDS_NAME")) ? dictParam["SOURCE_FIELDS_NAME"] : null),
                new OracleParameter("p_DEST_OBJECT",(dictParam != null && dictParam.ContainsKey("DEST_OBJECT")) ? dictParam["DEST_OBJECT"] : null),
                new OracleParameter("p_DEST_FIELDS_NAME",(dictParam != null && dictParam.ContainsKey("DEST_FIELDS_NAME")) ? dictParam["DEST_FIELDS_NAME"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<ETL_DATA_CONFIG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_SOURCE_OBJECT_NAME",(dictParam != null && dictParam.ContainsKey("SOURCE_OBJECT_NAME")) ? dictParam["SOURCE_OBJECT_NAME"] : null),
                new OracleParameter("p_OBJECT_ID",(dictParam != null && dictParam.ContainsKey("OBJECT_ID")) ? dictParam["OBJECT_ID"] : null),
                new OracleParameter("p_ELTNAME",(dictParam != null && dictParam.ContainsKey("ELTNAME")) ? dictParam["ELTNAME"] : null),
                new OracleParameter("p_OBJECT_OWNER",(dictParam != null && dictParam.ContainsKey("OBJECT_OWNER")) ? dictParam["OBJECT_OWNER"] : null),
                new OracleParameter("p_LINK_OWNER",(dictParam != null && dictParam.ContainsKey("LINK_OWNER")) ? dictParam["LINK_OWNER"] : null),
                new OracleParameter("p_ETL_CONNECT",(dictParam != null && dictParam.ContainsKey("ETL_CONNECT")) ? dictParam["ETL_CONNECT"] : null),
                new OracleParameter("p_SOURCE_OBJECT",(dictParam != null && dictParam.ContainsKey("SOURCE_OBJECT")) ? dictParam["SOURCE_OBJECT"] : null),
                new OracleParameter("p_SOURCE_FIELDS_NAME",(dictParam != null && dictParam.ContainsKey("SOURCE_FIELDS_NAME")) ? dictParam["SOURCE_FIELDS_NAME"] : null),
                new OracleParameter("p_DEST_OBJECT",(dictParam != null && dictParam.ContainsKey("DEST_OBJECT")) ? dictParam["DEST_OBJECT"] : null),
                new OracleParameter("p_DEST_FIELDS_NAME",(dictParam != null && dictParam.ContainsKey("DEST_FIELDS_NAME")) ? dictParam["DEST_FIELDS_NAME"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<ETL_DATA_CONFIG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public ETL_DATA_CONFIG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","SOURCE_OBJECT_NAME","OBJECT_ID","ELTNAME","OBJECT_OWNER","LINK_OWNER","ETL_CONNECT","DATA_ZONE_CODE_START","DATA_ZONE_CODE_END","SOURCE_OBJECT","SOURCE_FIELDS_NAME","DEST_OBJECT","DEST_FIELDS_NAME","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<ETL_DATA_CONFIG> results = QuerySolrBase<ETL_DATA_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ETL_DATA_CONFIG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<ETL_DATA_CONFIG> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_SOURCE_OBJECT_NAME,			string p_OBJECT_ID,			string p_ELTNAME,			string p_OBJECT_OWNER,			string p_LINK_OWNER,			string p_ETL_CONNECT,			string p_SOURCE_OBJECT,			string p_SOURCE_FIELDS_NAME,			string p_DEST_OBJECT,			string p_DEST_FIELDS_NAME,			string p_IS_ACTION,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_SOURCE_OBJECT_NAME))
			{
			    lstFilter.Add(new SolrQuery("SOURCE_OBJECT_NAME:" + p_SOURCE_OBJECT_NAME));
			}
			if (!string.IsNullOrEmpty(p_OBJECT_ID))
			{
			    lstFilter.Add(new SolrQuery("OBJECT_ID:" + p_OBJECT_ID));
			}
			if (!string.IsNullOrEmpty(p_ELTNAME))
			{
			    lstFilter.Add(new SolrQuery("ELTNAME:" + p_ELTNAME));
			}
			if (!string.IsNullOrEmpty(p_OBJECT_OWNER))
			{
			    lstFilter.Add(new SolrQuery("OBJECT_OWNER:" + p_OBJECT_OWNER));
			}
			if (!string.IsNullOrEmpty(p_LINK_OWNER))
			{
			    lstFilter.Add(new SolrQuery("LINK_OWNER:" + p_LINK_OWNER));
			}
			if (!string.IsNullOrEmpty(p_ETL_CONNECT))
			{
			    lstFilter.Add(new SolrQuery("ETL_CONNECT:" + p_ETL_CONNECT));
			}
			if (!string.IsNullOrEmpty(p_SOURCE_OBJECT))
			{
			    lstFilter.Add(new SolrQuery("SOURCE_OBJECT:" + p_SOURCE_OBJECT));
			}
			if (!string.IsNullOrEmpty(p_SOURCE_FIELDS_NAME))
			{
			    lstFilter.Add(new SolrQuery("SOURCE_FIELDS_NAME:" + p_SOURCE_FIELDS_NAME));
			}
			if (!string.IsNullOrEmpty(p_DEST_OBJECT))
			{
			    lstFilter.Add(new SolrQuery("DEST_OBJECT:" + p_DEST_OBJECT));
			}
			if (!string.IsNullOrEmpty(p_DEST_FIELDS_NAME))
			{
			    lstFilter.Add(new SolrQuery("DEST_FIELDS_NAME:" + p_DEST_FIELDS_NAME));
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
            string[] fieldSelect = { "ID","CODE","SOURCE_OBJECT_NAME","OBJECT_ID","ELTNAME","OBJECT_OWNER","LINK_OWNER","ETL_CONNECT","DATA_ZONE_CODE_START","DATA_ZONE_CODE_END","SOURCE_OBJECT","SOURCE_FIELDS_NAME","DEST_OBJECT","DEST_FIELDS_NAME","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<ETL_DATA_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ETL_DATA_CONFIG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<ETL_DATA_CONFIG> listAddRange = new List<ETL_DATA_CONFIG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

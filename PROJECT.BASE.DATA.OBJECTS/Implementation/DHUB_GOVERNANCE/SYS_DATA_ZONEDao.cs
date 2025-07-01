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
    public class SYS_DATA_ZONEDao : OracleBaseImpl<SYS_DATA_ZONE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_DATA_ZONE";
            PackageName = "PK_SYS_DATA_ZONE";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_DATA_ZONE_Request request)
        {        
			var info = new SYS_DATA_ZONE
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                NAME = (request != null && !string.IsNullOrEmpty(request.NAME)) ? request.NAME : null,
                TYPE = (request != null && !string.IsNullOrEmpty(request.TYPE)) ? request.TYPE : null,
                IP = (request != null && !string.IsNullOrEmpty(request.IP)) ? request.IP : null,
                SERVICENAME = (request != null && !string.IsNullOrEmpty(request.SERVICENAME)) ? request.SERVICENAME : null,
                PORTNAME = (request != null && request.PORTNAME != null) ? request.PORTNAME.Value : (int?)null,
                DBLINKNAME = (request != null && !string.IsNullOrEmpty(request.DBLINKNAME)) ? request.DBLINKNAME : null,
                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_DATA_ZONE_Request> request)
		{
			var list = new List<SYS_DATA_ZONE>();
			foreach (var info in request)
			{
				list.Add(new SYS_DATA_ZONE
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                NAME = (info != null && !string.IsNullOrEmpty(info.NAME)) ? info.NAME : null,
                TYPE = (info != null && !string.IsNullOrEmpty(info.TYPE)) ? info.TYPE : null,
                IP = (info != null && !string.IsNullOrEmpty(info.IP)) ? info.IP : null,
                SERVICENAME = (info != null && !string.IsNullOrEmpty(info.SERVICENAME)) ? info.SERVICENAME : null,
                PORTNAME = (info != null && info.PORTNAME != null) ? info.PORTNAME.Value : (int?)null,
                DBLINKNAME = (info != null && !string.IsNullOrEmpty(info.DBLINKNAME)) ? info.DBLINKNAME : null,
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
		public bool? UpdateList(List<SYS_DATA_ZONE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_DATA_ZONE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_DATA_ZONE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_TYPE",(dictParam != null && dictParam.ContainsKey("TYPE")) ? dictParam["TYPE"] : null),
                new OracleParameter("p_IP",(dictParam != null && dictParam.ContainsKey("IP")) ? dictParam["IP"] : null),
                new OracleParameter("p_SERVICENAME",(dictParam != null && dictParam.ContainsKey("SERVICENAME")) ? dictParam["SERVICENAME"] : null),
                new OracleParameter("p_PORTNAME",(dictParam != null && dictParam.ContainsKey("PORTNAME")) ? dictParam["PORTNAME"] : null),
                new OracleParameter("p_DBLINKNAME",(dictParam != null && dictParam.ContainsKey("DBLINKNAME")) ? dictParam["DBLINKNAME"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_DATA_ZONE> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_TYPE",(dictParam != null && dictParam.ContainsKey("TYPE")) ? dictParam["TYPE"] : null),
                new OracleParameter("p_IP",(dictParam != null && dictParam.ContainsKey("IP")) ? dictParam["IP"] : null),
                new OracleParameter("p_SERVICENAME",(dictParam != null && dictParam.ContainsKey("SERVICENAME")) ? dictParam["SERVICENAME"] : null),
                new OracleParameter("p_PORTNAME",(dictParam != null && dictParam.ContainsKey("PORTNAME")) ? dictParam["PORTNAME"] : null),
                new OracleParameter("p_DBLINKNAME",(dictParam != null && dictParam.ContainsKey("DBLINKNAME")) ? dictParam["DBLINKNAME"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_DATA_ZONE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_DATA_ZONE QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","NAME","TYPE","IP","SERVICENAME","PORTNAME","DBLINKNAME","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_DATA_ZONE> results = QuerySolrBase<SYS_DATA_ZONE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_DATA_ZONE/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_DATA_ZONE> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_NAME,			string p_TYPE,			string p_IP,			string p_SERVICENAME,			string p_PORTNAME,			string p_DBLINKNAME,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_NAME))
			{
			    lstFilter.Add(new SolrQuery("NAME:" + p_NAME));
			}
			if (!string.IsNullOrEmpty(p_TYPE))
			{
			    lstFilter.Add(new SolrQuery("TYPE:" + p_TYPE));
			}
			if (!string.IsNullOrEmpty(p_IP))
			{
			    lstFilter.Add(new SolrQuery("IP:" + p_IP));
			}
			if (!string.IsNullOrEmpty(p_SERVICENAME))
			{
			    lstFilter.Add(new SolrQuery("SERVICENAME:" + p_SERVICENAME));
			}
			if (!string.IsNullOrEmpty(p_PORTNAME))
			{
			    lstFilter.Add(new SolrQuery("PORTNAME:" + p_PORTNAME));
			}
			if (!string.IsNullOrEmpty(p_DBLINKNAME))
			{
			    lstFilter.Add(new SolrQuery("DBLINKNAME:" + p_DBLINKNAME));
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
            string[] fieldSelect = { "ID","CODE","NAME","TYPE","IP","SERVICENAME","PORTNAME","DBLINKNAME","NOTE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_DATA_ZONE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_DATA_ZONE/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_DATA_ZONE> listAddRange = new List<SYS_DATA_ZONE>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

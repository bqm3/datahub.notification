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
    public class SYS_CONFIG_WFPIPELINEDao : OracleBaseImpl<SYS_CONFIG_WFPIPELINE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONFIG_WFPIPELINE";
            PackageName = "PK_SYS_CONFIG_WFPIPELINE";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONFIG_WFPIPELINE_Request request)
        {        
			var info = new SYS_CONFIG_WFPIPELINE
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                TIMESTART = (request != null && !string.IsNullOrEmpty(request.TIMESTART)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.TIMESTART, "yyyy-MM-dd")? $"{request.TIMESTART} 00:00:00" : request.TIMESTART, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                TIMEEND = (request != null && !string.IsNullOrEmpty(request.TIMEEND)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(request.TIMEEND, "yyyy-MM-dd")? $"{request.TIMEEND} 00:00:00" : request.TIMEEND, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                IS_ACTION = (request != null && request.IS_ACTION != null) ? request.IS_ACTION.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_CONFIG_WFPIPELINE_Request> request)
		{
			var list = new List<SYS_CONFIG_WFPIPELINE>();
			foreach (var info in request)
			{
				list.Add(new SYS_CONFIG_WFPIPELINE
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                NOTE = (info != null && !string.IsNullOrEmpty(info.NOTE)) ? info.NOTE : null,
                TIMESTART = (info != null && !string.IsNullOrEmpty(info.TIMESTART)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.TIMESTART, "yyyy-MM-dd")? $"{info.TIMESTART} 00:00:00" : info.TIMESTART, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                TIMEEND = (info != null && !string.IsNullOrEmpty(info.TIMEEND)) ? DateTime.SpecifyKind(DateTime.ParseExact(Valid.IsValidDate(info.TIMEEND, "yyyy-MM-dd")? $"{info.TIMEEND} 00:00:00" : info.TIMEEND, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
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
		public bool? UpdateList(List<SYS_CONFIG_WFPIPELINE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_CONFIG_WFPIPELINE> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_CONFIG_WFPIPELINE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NOTE",(dictParam != null && dictParam.ContainsKey("NOTE")) ? dictParam["NOTE"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONFIG_WFPIPELINE> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NOTE",(dictParam != null && dictParam.ContainsKey("NOTE")) ? dictParam["NOTE"] : null),
                new OracleParameter("p_IS_ACTION",(dictParam != null && dictParam.ContainsKey("IS_ACTION")) ? dictParam["IS_ACTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONFIG_WFPIPELINE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONFIG_WFPIPELINE QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","NOTE","TIMESTART","TIMEEND","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_CONFIG_WFPIPELINE> results = QuerySolrBase<SYS_CONFIG_WFPIPELINE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFPIPELINE/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONFIG_WFPIPELINE> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_NOTE,			string p_IS_ACTION,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_NOTE))
			{
			    lstFilter.Add(new SolrQuery("NOTE:" + p_NOTE));
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
            string[] fieldSelect = { "ID","CODE","NOTE","TIMESTART","TIMEEND","IS_ACTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_CONFIG_WFPIPELINE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONFIG_WFPIPELINE/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONFIG_WFPIPELINE> listAddRange = new List<SYS_CONFIG_WFPIPELINE>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

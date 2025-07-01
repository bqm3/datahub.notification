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
    public class ITG_MESSAGESDao : OracleBaseImpl<ITG_MESSAGES>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "ITG_MESSAGES";
            PackageName = "PK_ITG_MESSAGES";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_TRANS.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(ITG_MESSAGES_Information info)
        {        
			var paramValue = new ITG_MESSAGES
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                MESSAGE_NAME = (info != null && !string.IsNullOrEmpty(info.MESSAGE_NAME)) ? info.MESSAGE_NAME : null,
                PARENT_CODE = (info != null && !string.IsNullOrEmpty(info.PARENT_CODE)) ? info.PARENT_CODE : null,
                PARENT_NAME = (info != null && !string.IsNullOrEmpty(info.PARENT_NAME)) ? info.PARENT_NAME : null,
                DESCRIPTION = (info != null && !string.IsNullOrEmpty(info.DESCRIPTION)) ? info.DESCRIPTION : null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<ITG_MESSAGES> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MESSAGE_NAME",(dictParam != null && dictParam.ContainsKey("MESSAGE_NAME")) ? dictParam["MESSAGE_NAME"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_PARENT_NAME",(dictParam != null && dictParam.ContainsKey("PARENT_NAME")) ? dictParam["PARENT_NAME"] : null),
                new OracleParameter("p_DESCRIPTION",(dictParam != null && dictParam.ContainsKey("DESCRIPTION")) ? dictParam["DESCRIPTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<ITG_MESSAGES> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MESSAGE_NAME",(dictParam != null && dictParam.ContainsKey("MESSAGE_NAME")) ? dictParam["MESSAGE_NAME"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_PARENT_NAME",(dictParam != null && dictParam.ContainsKey("PARENT_NAME")) ? dictParam["PARENT_NAME"] : null),
                new OracleParameter("p_DESCRIPTION",(dictParam != null && dictParam.ContainsKey("DESCRIPTION")) ? dictParam["DESCRIPTION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<ITG_MESSAGES> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public ITG_MESSAGES QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","MESSAGE_NAME","PARENT_CODE","PARENT_NAME","DESCRIPTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<ITG_MESSAGES> results = QuerySolrBase<ITG_MESSAGES>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MESSAGES/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<ITG_MESSAGES> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_MESSAGE_NAME,			string p_PARENT_CODE,			string p_PARENT_NAME,			string p_DESCRIPTION,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_MESSAGE_NAME))
			{
			    lstFilter.Add(new SolrQuery("MESSAGE_NAME:" + p_MESSAGE_NAME));
			}
			if (!string.IsNullOrEmpty(p_PARENT_CODE))
			{
			    lstFilter.Add(new SolrQuery("PARENT_CODE:" + p_PARENT_CODE));
			}
			if (!string.IsNullOrEmpty(p_PARENT_NAME))
			{
			    lstFilter.Add(new SolrQuery("PARENT_NAME:" + p_PARENT_NAME));
			}
			if (!string.IsNullOrEmpty(p_DESCRIPTION))
			{
			    lstFilter.Add(new SolrQuery("DESCRIPTION:" + p_DESCRIPTION));
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
            string[] fieldSelect = { "ID","CODE","MESSAGE_NAME","PARENT_CODE","PARENT_NAME","DESCRIPTION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<ITG_MESSAGES>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MESSAGES/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<ITG_MESSAGES> listAddRange = new List<ITG_MESSAGES>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

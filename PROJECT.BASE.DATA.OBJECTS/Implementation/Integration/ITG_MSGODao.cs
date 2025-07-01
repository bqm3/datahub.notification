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
    public class ITG_MSGODao : OracleBaseImpl<ITG_MSGO>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "ITG_MSGO";
            PackageName = "PK_ITG_MSGO";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_TRANS.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(ITG_MSGO_Information info)
        {        
			var paramValue = new ITG_MSGO
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                REF_CODE = (info != null && !string.IsNullOrEmpty(info.REF_CODE)) ? info.REF_CODE : null,
                MSG_REFID = (info != null && !string.IsNullOrEmpty(info.MSG_REFID)) ? info.MSG_REFID : null,
                TRAN_CODE = (info != null && !string.IsNullOrEmpty(info.TRAN_CODE)) ? info.TRAN_CODE : null,
                TRAN_NAME = (info != null && !string.IsNullOrEmpty(info.TRAN_NAME)) ? info.TRAN_NAME : null,
                SENDER_CODE = (info != null && !string.IsNullOrEmpty(info.SENDER_CODE)) ? info.SENDER_CODE : null,
                SENDER_NAME = (info != null && !string.IsNullOrEmpty(info.SENDER_NAME)) ? info.SENDER_NAME : null,
                RECEIVER_CODE = (info != null && !string.IsNullOrEmpty(info.RECEIVER_CODE)) ? info.RECEIVER_CODE : null,
                RECEIVER_NAME = (info != null && !string.IsNullOrEmpty(info.RECEIVER_NAME)) ? info.RECEIVER_NAME : null,
                MSG_CONTENT = (info != null && !string.IsNullOrEmpty(info.MSG_CONTENT)) ? info.MSG_CONTENT : null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<ITG_MSGO> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_REF_CODE",(dictParam != null && dictParam.ContainsKey("REF_CODE")) ? dictParam["REF_CODE"] : null),
                new OracleParameter("p_MSG_REFID",(dictParam != null && dictParam.ContainsKey("MSG_REFID")) ? dictParam["MSG_REFID"] : null),
                new OracleParameter("p_TRAN_CODE",(dictParam != null && dictParam.ContainsKey("TRAN_CODE")) ? dictParam["TRAN_CODE"] : null),
                new OracleParameter("p_TRAN_NAME",(dictParam != null && dictParam.ContainsKey("TRAN_NAME")) ? dictParam["TRAN_NAME"] : null),
                new OracleParameter("p_SENDER_CODE",(dictParam != null && dictParam.ContainsKey("SENDER_CODE")) ? dictParam["SENDER_CODE"] : null),
                new OracleParameter("p_SENDER_NAME",(dictParam != null && dictParam.ContainsKey("SENDER_NAME")) ? dictParam["SENDER_NAME"] : null),
                new OracleParameter("p_RECEIVER_CODE",(dictParam != null && dictParam.ContainsKey("RECEIVER_CODE")) ? dictParam["RECEIVER_CODE"] : null),
                new OracleParameter("p_RECEIVER_NAME",(dictParam != null && dictParam.ContainsKey("RECEIVER_NAME")) ? dictParam["RECEIVER_NAME"] : null),
                new OracleParameter("p_MSG_CONTENT",(dictParam != null && dictParam.ContainsKey("MSG_CONTENT")) ? dictParam["MSG_CONTENT"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<ITG_MSGO> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_REF_CODE",(dictParam != null && dictParam.ContainsKey("REF_CODE")) ? dictParam["REF_CODE"] : null),
                new OracleParameter("p_MSG_REFID",(dictParam != null && dictParam.ContainsKey("MSG_REFID")) ? dictParam["MSG_REFID"] : null),
                new OracleParameter("p_TRAN_CODE",(dictParam != null && dictParam.ContainsKey("TRAN_CODE")) ? dictParam["TRAN_CODE"] : null),
                new OracleParameter("p_TRAN_NAME",(dictParam != null && dictParam.ContainsKey("TRAN_NAME")) ? dictParam["TRAN_NAME"] : null),
                new OracleParameter("p_SENDER_CODE",(dictParam != null && dictParam.ContainsKey("SENDER_CODE")) ? dictParam["SENDER_CODE"] : null),
                new OracleParameter("p_SENDER_NAME",(dictParam != null && dictParam.ContainsKey("SENDER_NAME")) ? dictParam["SENDER_NAME"] : null),
                new OracleParameter("p_RECEIVER_CODE",(dictParam != null && dictParam.ContainsKey("RECEIVER_CODE")) ? dictParam["RECEIVER_CODE"] : null),
                new OracleParameter("p_RECEIVER_NAME",(dictParam != null && dictParam.ContainsKey("RECEIVER_NAME")) ? dictParam["RECEIVER_NAME"] : null),
                new OracleParameter("p_MSG_CONTENT",(dictParam != null && dictParam.ContainsKey("MSG_CONTENT")) ? dictParam["MSG_CONTENT"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<ITG_MSGO> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public ITG_MSGO QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","REF_CODE","MSG_REFID","TRAN_CODE","TRAN_NAME","SENDER_CODE","SENDER_NAME","RECEIVER_CODE","RECEIVER_NAME","MSG_CONTENT","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<ITG_MSGO> results = QuerySolrBase<ITG_MSGO>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MSGO/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<ITG_MSGO> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_REF_CODE,			string p_MSG_REFID,			string p_TRAN_CODE,			string p_TRAN_NAME,			string p_SENDER_CODE,			string p_SENDER_NAME,			string p_RECEIVER_CODE,			string p_RECEIVER_NAME,			string p_MSG_CONTENT,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_REF_CODE))
			{
			    lstFilter.Add(new SolrQuery("REF_CODE:" + p_REF_CODE));
			}
			if (!string.IsNullOrEmpty(p_MSG_REFID))
			{
			    lstFilter.Add(new SolrQuery("MSG_REFID:" + p_MSG_REFID));
			}
			if (!string.IsNullOrEmpty(p_TRAN_CODE))
			{
			    lstFilter.Add(new SolrQuery("TRAN_CODE:" + p_TRAN_CODE));
			}
			if (!string.IsNullOrEmpty(p_TRAN_NAME))
			{
			    lstFilter.Add(new SolrQuery("TRAN_NAME:" + p_TRAN_NAME));
			}
			if (!string.IsNullOrEmpty(p_SENDER_CODE))
			{
			    lstFilter.Add(new SolrQuery("SENDER_CODE:" + p_SENDER_CODE));
			}
			if (!string.IsNullOrEmpty(p_SENDER_NAME))
			{
			    lstFilter.Add(new SolrQuery("SENDER_NAME:" + p_SENDER_NAME));
			}
			if (!string.IsNullOrEmpty(p_RECEIVER_CODE))
			{
			    lstFilter.Add(new SolrQuery("RECEIVER_CODE:" + p_RECEIVER_CODE));
			}
			if (!string.IsNullOrEmpty(p_RECEIVER_NAME))
			{
			    lstFilter.Add(new SolrQuery("RECEIVER_NAME:" + p_RECEIVER_NAME));
			}
			if (!string.IsNullOrEmpty(p_MSG_CONTENT))
			{
			    lstFilter.Add(new SolrQuery("MSG_CONTENT:" + p_MSG_CONTENT));
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
            string[] fieldSelect = { "ID","CODE","REF_CODE","MSG_REFID","TRAN_CODE","TRAN_NAME","SENDER_CODE","SENDER_NAME","RECEIVER_CODE","RECEIVER_NAME","MSG_CONTENT","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<ITG_MSGO>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MSGO/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<ITG_MSGO> listAddRange = new List<ITG_MSGO>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class ITG_MSGIDao : OracleBaseImpl<ITG_MSGI>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "ITG_MSGI";
            PackageName = "PK_ITG_MSGI";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_TRANS.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(ITG_MSGI_Information info)
        {        
			var paramValue = new ITG_MSGI
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                MSG_ID = (info != null && !string.IsNullOrEmpty(info.MSG_ID)) ? info.MSG_ID : null,
                MSG_REFID = (info != null && !string.IsNullOrEmpty(info.MSG_REFID)) ? info.MSG_REFID : null,
                MSG_CONTENT = (info != null && !string.IsNullOrEmpty(info.MSG_CONTENT)) ? info.MSG_CONTENT : null,
                TRAN_CODE = (info != null && !string.IsNullOrEmpty(info.TRAN_CODE)) ? info.TRAN_CODE : null,
                TRAN_NAME = (info != null && !string.IsNullOrEmpty(info.TRAN_NAME)) ? info.TRAN_NAME : null,
                SENDER_CODE = (info != null && !string.IsNullOrEmpty(info.SENDER_CODE)) ? info.SENDER_CODE : null,
                SENDER_NAME = (info != null && !string.IsNullOrEmpty(info.SENDER_NAME)) ? info.SENDER_NAME : null,
                RECEIVER_CODE = (info != null && !string.IsNullOrEmpty(info.RECEIVER_CODE)) ? info.RECEIVER_CODE : null,
                RECEIVER_NAME = (info != null && !string.IsNullOrEmpty(info.RECEIVER_NAME)) ? info.RECEIVER_NAME : null,
                DATA_TYPE = (info != null && !string.IsNullOrEmpty(info.DATA_TYPE)) ? info.DATA_TYPE : null,
                SEND_DATE = (info != null && !string.IsNullOrEmpty(info.SEND_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.SEND_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                ORIGINAL_CODE = (info != null && !string.IsNullOrEmpty(info.ORIGINAL_CODE)) ? info.ORIGINAL_CODE : null,
                ORIGINAL_NAME = (info != null && !string.IsNullOrEmpty(info.ORIGINAL_NAME)) ? info.ORIGINAL_NAME : null,
                ORIGINAL_DATE = (info != null && !string.IsNullOrEmpty(info.ORIGINAL_DATE)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.ORIGINAL_DATE, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                VERSION = (info != null && !string.IsNullOrEmpty(info.VERSION)) ? info.VERSION : null,
                STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<ITG_MSGI> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MSG_ID",(dictParam != null && dictParam.ContainsKey("MSG_ID")) ? dictParam["MSG_ID"] : null),
                new OracleParameter("p_MSG_REFID",(dictParam != null && dictParam.ContainsKey("MSG_REFID")) ? dictParam["MSG_REFID"] : null),
                new OracleParameter("p_MSG_CONTENT",(dictParam != null && dictParam.ContainsKey("MSG_CONTENT")) ? dictParam["MSG_CONTENT"] : null),
                new OracleParameter("p_TRAN_CODE",(dictParam != null && dictParam.ContainsKey("TRAN_CODE")) ? dictParam["TRAN_CODE"] : null),
                new OracleParameter("p_TRAN_NAME",(dictParam != null && dictParam.ContainsKey("TRAN_NAME")) ? dictParam["TRAN_NAME"] : null),
                new OracleParameter("p_SENDER_CODE",(dictParam != null && dictParam.ContainsKey("SENDER_CODE")) ? dictParam["SENDER_CODE"] : null),
                new OracleParameter("p_SENDER_NAME",(dictParam != null && dictParam.ContainsKey("SENDER_NAME")) ? dictParam["SENDER_NAME"] : null),
                new OracleParameter("p_RECEIVER_CODE",(dictParam != null && dictParam.ContainsKey("RECEIVER_CODE")) ? dictParam["RECEIVER_CODE"] : null),
                new OracleParameter("p_RECEIVER_NAME",(dictParam != null && dictParam.ContainsKey("RECEIVER_NAME")) ? dictParam["RECEIVER_NAME"] : null),
                new OracleParameter("p_DATA_TYPE",(dictParam != null && dictParam.ContainsKey("DATA_TYPE")) ? dictParam["DATA_TYPE"] : null),
                new OracleParameter("p_SEND_DATE_START", (dictParam != null && dictParam.ContainsKey("SEND_DATE_START")) ? dictParam["SEND_DATE_START"] : null),
                new OracleParameter("p_SEND_DATE_END", (dictParam != null && dictParam.ContainsKey("SEND_DATE_END")) ? dictParam["SEND_DATE_END"] : null),
                new OracleParameter("p_ORIGINAL_CODE",(dictParam != null && dictParam.ContainsKey("ORIGINAL_CODE")) ? dictParam["ORIGINAL_CODE"] : null),
                new OracleParameter("p_ORIGINAL_NAME",(dictParam != null && dictParam.ContainsKey("ORIGINAL_NAME")) ? dictParam["ORIGINAL_NAME"] : null),
                new OracleParameter("p_ORIGINAL_DATE_START", (dictParam != null && dictParam.ContainsKey("ORIGINAL_DATE_START")) ? dictParam["ORIGINAL_DATE_START"] : null),
                new OracleParameter("p_ORIGINAL_DATE_END", (dictParam != null && dictParam.ContainsKey("ORIGINAL_DATE_END")) ? dictParam["ORIGINAL_DATE_END"] : null),
                new OracleParameter("p_VERSION",(dictParam != null && dictParam.ContainsKey("VERSION")) ? dictParam["VERSION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<ITG_MSGI> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_MSG_ID",(dictParam != null && dictParam.ContainsKey("MSG_ID")) ? dictParam["MSG_ID"] : null),
                new OracleParameter("p_MSG_REFID",(dictParam != null && dictParam.ContainsKey("MSG_REFID")) ? dictParam["MSG_REFID"] : null),
                new OracleParameter("p_MSG_CONTENT",(dictParam != null && dictParam.ContainsKey("MSG_CONTENT")) ? dictParam["MSG_CONTENT"] : null),
                new OracleParameter("p_TRAN_CODE",(dictParam != null && dictParam.ContainsKey("TRAN_CODE")) ? dictParam["TRAN_CODE"] : null),
                new OracleParameter("p_TRAN_NAME",(dictParam != null && dictParam.ContainsKey("TRAN_NAME")) ? dictParam["TRAN_NAME"] : null),
                new OracleParameter("p_SENDER_CODE",(dictParam != null && dictParam.ContainsKey("SENDER_CODE")) ? dictParam["SENDER_CODE"] : null),
                new OracleParameter("p_SENDER_NAME",(dictParam != null && dictParam.ContainsKey("SENDER_NAME")) ? dictParam["SENDER_NAME"] : null),
                new OracleParameter("p_RECEIVER_CODE",(dictParam != null && dictParam.ContainsKey("RECEIVER_CODE")) ? dictParam["RECEIVER_CODE"] : null),
                new OracleParameter("p_RECEIVER_NAME",(dictParam != null && dictParam.ContainsKey("RECEIVER_NAME")) ? dictParam["RECEIVER_NAME"] : null),
                new OracleParameter("p_DATA_TYPE",(dictParam != null && dictParam.ContainsKey("DATA_TYPE")) ? dictParam["DATA_TYPE"] : null),
                new OracleParameter("p_SEND_DATE_START", (dictParam != null && dictParam.ContainsKey("SEND_DATE_START")) ? dictParam["SEND_DATE_START"] : null),
                new OracleParameter("p_SEND_DATE_END", (dictParam != null && dictParam.ContainsKey("SEND_DATE_END")) ? dictParam["SEND_DATE_END"] : null),
                new OracleParameter("p_ORIGINAL_CODE",(dictParam != null && dictParam.ContainsKey("ORIGINAL_CODE")) ? dictParam["ORIGINAL_CODE"] : null),
                new OracleParameter("p_ORIGINAL_NAME",(dictParam != null && dictParam.ContainsKey("ORIGINAL_NAME")) ? dictParam["ORIGINAL_NAME"] : null),
                new OracleParameter("p_ORIGINAL_DATE_START", (dictParam != null && dictParam.ContainsKey("ORIGINAL_DATE_START")) ? dictParam["ORIGINAL_DATE_START"] : null),
                new OracleParameter("p_ORIGINAL_DATE_END", (dictParam != null && dictParam.ContainsKey("ORIGINAL_DATE_END")) ? dictParam["ORIGINAL_DATE_END"] : null),
                new OracleParameter("p_VERSION",(dictParam != null && dictParam.ContainsKey("VERSION")) ? dictParam["VERSION"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<ITG_MSGI> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public ITG_MSGI QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","MSG_ID","MSG_REFID","MSG_CONTENT","TRAN_CODE","TRAN_NAME","SENDER_CODE","SENDER_NAME","RECEIVER_CODE","RECEIVER_NAME","DATA_TYPE","SEND_DATE","ORIGINAL_CODE","ORIGINAL_NAME","ORIGINAL_DATE","VERSION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<ITG_MSGI> results = QuerySolrBase<ITG_MSGI>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MSGI/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<ITG_MSGI> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_MSG_ID,			string p_MSG_REFID,			string p_MSG_CONTENT,			string p_TRAN_CODE,			string p_TRAN_NAME,			string p_SENDER_CODE,			string p_SENDER_NAME,			string p_RECEIVER_CODE,			string p_RECEIVER_NAME,			string p_DATA_TYPE,			DateTime? p_SEND_DATE_START,			DateTime? p_SEND_DATE_END,			string p_ORIGINAL_CODE,			string p_ORIGINAL_NAME,			DateTime? p_ORIGINAL_DATE_START,			DateTime? p_ORIGINAL_DATE_END,			string p_VERSION,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_MSG_ID))
			{
			    lstFilter.Add(new SolrQuery("MSG_ID:" + p_MSG_ID));
			}
			if (!string.IsNullOrEmpty(p_MSG_REFID))
			{
			    lstFilter.Add(new SolrQuery("MSG_REFID:" + p_MSG_REFID));
			}
			if (!string.IsNullOrEmpty(p_MSG_CONTENT))
			{
			    lstFilter.Add(new SolrQuery("MSG_CONTENT:" + p_MSG_CONTENT));
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
			if (!string.IsNullOrEmpty(p_DATA_TYPE))
			{
			    lstFilter.Add(new SolrQuery("DATA_TYPE:" + p_DATA_TYPE));
			}
			if (p_SEND_DATE_START != null && p_SEND_DATE_END != null)
			{
			    lstFilter.Add(new SolrQuery("SEND_DATE:[" + Utility.GetJSONZFromUserDateTime(p_SEND_DATE_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_SEND_DATE_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_ORIGINAL_CODE))
			{
			    lstFilter.Add(new SolrQuery("ORIGINAL_CODE:" + p_ORIGINAL_CODE));
			}
			if (!string.IsNullOrEmpty(p_ORIGINAL_NAME))
			{
			    lstFilter.Add(new SolrQuery("ORIGINAL_NAME:" + p_ORIGINAL_NAME));
			}
			if (p_ORIGINAL_DATE_START != null && p_ORIGINAL_DATE_END != null)
			{
			    lstFilter.Add(new SolrQuery("ORIGINAL_DATE:[" + Utility.GetJSONZFromUserDateTime(p_ORIGINAL_DATE_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_ORIGINAL_DATE_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_VERSION))
			{
			    lstFilter.Add(new SolrQuery("VERSION:" + p_VERSION));
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
            string[] fieldSelect = { "ID","CODE","MSG_ID","MSG_REFID","MSG_CONTENT","TRAN_CODE","TRAN_NAME","SENDER_CODE","SENDER_NAME","RECEIVER_CODE","RECEIVER_NAME","DATA_TYPE","SEND_DATE","ORIGINAL_CODE","ORIGINAL_NAME","ORIGINAL_DATE","VERSION","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<ITG_MSGI>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"ITG_MSGI/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<ITG_MSGI> listAddRange = new List<ITG_MSGI>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

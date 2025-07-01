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
    public class SYS_SHARE_CORPDao : OracleBaseImpl<SYS_SHARE_CORP>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_SHARE_CORP";
            PackageName = "PK_SYS_SHARE_CORP";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_SHARE_CORP_Request request)
        {        
			var info = new SYS_SHARE_CORP
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                CORP_NAME = (request != null && !string.IsNullOrEmpty(request.CORP_NAME)) ? request.CORP_NAME : null,
                TAX_CODE = (request != null && !string.IsNullOrEmpty(request.TAX_CODE)) ? request.TAX_CODE : null,
                ADDRESS = (request != null && !string.IsNullOrEmpty(request.ADDRESS)) ? request.ADDRESS : null,
                CORP_PHONE = (request != null && !string.IsNullOrEmpty(request.CORP_PHONE)) ? request.CORP_PHONE : null,
                EMAIL = (request != null && !string.IsNullOrEmpty(request.EMAIL)) ? request.EMAIL : null,
                FAX = (request != null && !string.IsNullOrEmpty(request.FAX)) ? request.FAX : null,
                WEBSITE = (request != null && !string.IsNullOrEmpty(request.WEBSITE)) ? request.WEBSITE : null,
                REPRESENTATIVE = (request != null && !string.IsNullOrEmpty(request.REPRESENTATIVE)) ? request.REPRESENTATIVE : null,
                POSITION = (request != null && !string.IsNullOrEmpty(request.POSITION)) ? request.POSITION : null,
                REP_PHONE = (request != null && !string.IsNullOrEmpty(request.REP_PHONE)) ? request.REP_PHONE : null,
                REP_EMAIL = (request != null && !string.IsNullOrEmpty(request.REP_EMAIL)) ? request.REP_EMAIL : null,
                GROUP_CODE = (request != null && !string.IsNullOrEmpty(request.GROUP_CODE)) ? request.GROUP_CODE : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_SHARE_CORP_Request> request)
		{
			var list = new List<SYS_SHARE_CORP>();
			foreach (var info in request)
			{
				list.Add(new SYS_SHARE_CORP
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                CORP_NAME = (info != null && !string.IsNullOrEmpty(info.CORP_NAME)) ? info.CORP_NAME : null,
                TAX_CODE = (info != null && !string.IsNullOrEmpty(info.TAX_CODE)) ? info.TAX_CODE : null,
                ADDRESS = (info != null && !string.IsNullOrEmpty(info.ADDRESS)) ? info.ADDRESS : null,
                CORP_PHONE = (info != null && !string.IsNullOrEmpty(info.CORP_PHONE)) ? info.CORP_PHONE : null,
                EMAIL = (info != null && !string.IsNullOrEmpty(info.EMAIL)) ? info.EMAIL : null,
                FAX = (info != null && !string.IsNullOrEmpty(info.FAX)) ? info.FAX : null,
                WEBSITE = (info != null && !string.IsNullOrEmpty(info.WEBSITE)) ? info.WEBSITE : null,
                REPRESENTATIVE = (info != null && !string.IsNullOrEmpty(info.REPRESENTATIVE)) ? info.REPRESENTATIVE : null,
                POSITION = (info != null && !string.IsNullOrEmpty(info.POSITION)) ? info.POSITION : null,
                REP_PHONE = (info != null && !string.IsNullOrEmpty(info.REP_PHONE)) ? info.REP_PHONE : null,
                REP_EMAIL = (info != null && !string.IsNullOrEmpty(info.REP_EMAIL)) ? info.REP_EMAIL : null,
                GROUP_CODE = (info != null && !string.IsNullOrEmpty(info.GROUP_CODE)) ? info.GROUP_CODE : null,
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
		public bool? UpdateList(List<SYS_SHARE_CORP> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_SHARE_CORP> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_SHARE_CORP> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CORP_NAME",(dictParam != null && dictParam.ContainsKey("CORP_NAME")) ? dictParam["CORP_NAME"] : null),
                new OracleParameter("p_TAX_CODE",(dictParam != null && dictParam.ContainsKey("TAX_CODE")) ? dictParam["TAX_CODE"] : null),
                new OracleParameter("p_ADDRESS",(dictParam != null && dictParam.ContainsKey("ADDRESS")) ? dictParam["ADDRESS"] : null),
                new OracleParameter("p_CORP_PHONE",(dictParam != null && dictParam.ContainsKey("CORP_PHONE")) ? dictParam["CORP_PHONE"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null),
                new OracleParameter("p_FAX",(dictParam != null && dictParam.ContainsKey("FAX")) ? dictParam["FAX"] : null),
                new OracleParameter("p_WEBSITE",(dictParam != null && dictParam.ContainsKey("WEBSITE")) ? dictParam["WEBSITE"] : null),
                new OracleParameter("p_REPRESENTATIVE",(dictParam != null && dictParam.ContainsKey("REPRESENTATIVE")) ? dictParam["REPRESENTATIVE"] : null),
                new OracleParameter("p_POSITION",(dictParam != null && dictParam.ContainsKey("POSITION")) ? dictParam["POSITION"] : null),
                new OracleParameter("p_REP_PHONE",(dictParam != null && dictParam.ContainsKey("REP_PHONE")) ? dictParam["REP_PHONE"] : null),
                new OracleParameter("p_REP_EMAIL",(dictParam != null && dictParam.ContainsKey("REP_EMAIL")) ? dictParam["REP_EMAIL"] : null),
                new OracleParameter("p_GROUP_CODE",(dictParam != null && dictParam.ContainsKey("GROUP_CODE")) ? dictParam["GROUP_CODE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_SHARE_CORP> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CORP_NAME",(dictParam != null && dictParam.ContainsKey("CORP_NAME")) ? dictParam["CORP_NAME"] : null),
                new OracleParameter("p_TAX_CODE",(dictParam != null && dictParam.ContainsKey("TAX_CODE")) ? dictParam["TAX_CODE"] : null),
                new OracleParameter("p_ADDRESS",(dictParam != null && dictParam.ContainsKey("ADDRESS")) ? dictParam["ADDRESS"] : null),
                new OracleParameter("p_CORP_PHONE",(dictParam != null && dictParam.ContainsKey("CORP_PHONE")) ? dictParam["CORP_PHONE"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null),
                new OracleParameter("p_FAX",(dictParam != null && dictParam.ContainsKey("FAX")) ? dictParam["FAX"] : null),
                new OracleParameter("p_WEBSITE",(dictParam != null && dictParam.ContainsKey("WEBSITE")) ? dictParam["WEBSITE"] : null),
                new OracleParameter("p_REPRESENTATIVE",(dictParam != null && dictParam.ContainsKey("REPRESENTATIVE")) ? dictParam["REPRESENTATIVE"] : null),
                new OracleParameter("p_POSITION",(dictParam != null && dictParam.ContainsKey("POSITION")) ? dictParam["POSITION"] : null),
                new OracleParameter("p_REP_PHONE",(dictParam != null && dictParam.ContainsKey("REP_PHONE")) ? dictParam["REP_PHONE"] : null),
                new OracleParameter("p_REP_EMAIL",(dictParam != null && dictParam.ContainsKey("REP_EMAIL")) ? dictParam["REP_EMAIL"] : null),
                new OracleParameter("p_GROUP_CODE",(dictParam != null && dictParam.ContainsKey("GROUP_CODE")) ? dictParam["GROUP_CODE"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_SHARE_CORP> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_SHARE_CORP QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","CORP_NAME","TAX_CODE","ADDRESS","CORP_PHONE","EMAIL","FAX","WEBSITE","REPRESENTATIVE","POSITION","REP_PHONE","REP_EMAIL","GROUP_CODE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_SHARE_CORP> results = QuerySolrBase<SYS_SHARE_CORP>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_SHARE_CORP/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_SHARE_CORP> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_CORP_NAME,			string p_TAX_CODE,			string p_ADDRESS,			string p_CORP_PHONE,			string p_EMAIL,			string p_FAX,			string p_WEBSITE,			string p_REPRESENTATIVE,			string p_POSITION,			string p_REP_PHONE,			string p_REP_EMAIL,			string p_GROUP_CODE,			string p_IS_ACTIVE,			string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_CORP_NAME))
			{
			    lstFilter.Add(new SolrQuery("CORP_NAME:" + p_CORP_NAME));
			}
			if (!string.IsNullOrEmpty(p_TAX_CODE))
			{
			    lstFilter.Add(new SolrQuery("TAX_CODE:" + p_TAX_CODE));
			}
			if (!string.IsNullOrEmpty(p_ADDRESS))
			{
			    lstFilter.Add(new SolrQuery("ADDRESS:" + p_ADDRESS));
			}
			if (!string.IsNullOrEmpty(p_CORP_PHONE))
			{
			    lstFilter.Add(new SolrQuery("CORP_PHONE:" + p_CORP_PHONE));
			}
			if (!string.IsNullOrEmpty(p_EMAIL))
			{
			    lstFilter.Add(new SolrQuery("EMAIL:" + p_EMAIL));
			}
			if (!string.IsNullOrEmpty(p_FAX))
			{
			    lstFilter.Add(new SolrQuery("FAX:" + p_FAX));
			}
			if (!string.IsNullOrEmpty(p_WEBSITE))
			{
			    lstFilter.Add(new SolrQuery("WEBSITE:" + p_WEBSITE));
			}
			if (!string.IsNullOrEmpty(p_REPRESENTATIVE))
			{
			    lstFilter.Add(new SolrQuery("REPRESENTATIVE:" + p_REPRESENTATIVE));
			}
			if (!string.IsNullOrEmpty(p_POSITION))
			{
			    lstFilter.Add(new SolrQuery("POSITION:" + p_POSITION));
			}
			if (!string.IsNullOrEmpty(p_REP_PHONE))
			{
			    lstFilter.Add(new SolrQuery("REP_PHONE:" + p_REP_PHONE));
			}
			if (!string.IsNullOrEmpty(p_REP_EMAIL))
			{
			    lstFilter.Add(new SolrQuery("REP_EMAIL:" + p_REP_EMAIL));
			}
			if (!string.IsNullOrEmpty(p_GROUP_CODE))
			{
			    lstFilter.Add(new SolrQuery("GROUP_CODE:" + p_GROUP_CODE));
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
            string[] fieldSelect = { "ID","CODE","CORP_NAME","TAX_CODE","ADDRESS","CORP_PHONE","EMAIL","FAX","WEBSITE","REPRESENTATIVE","POSITION","REP_PHONE","REP_EMAIL","GROUP_CODE","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_SHARE_CORP>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_SHARE_CORP/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_SHARE_CORP> listAddRange = new List<SYS_SHARE_CORP>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

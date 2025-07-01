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
    public class SYS_DATASET_DTLDao : OracleBaseImpl<SYS_DATASET_DTL>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_DATASET_DTL";
            PackageName = "PK_SYS_DATASET_DTL";         
			ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_DATASET_DTL_Request request)
        {        
			var info = new SYS_DATASET_DTL
			{
				CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                NAME = (request != null && !string.IsNullOrEmpty(request.NAME)) ? request.NAME : null,
                NOTE = (request != null && !string.IsNullOrEmpty(request.NOTE)) ? request.NOTE : null,
                DATASET_ID = (request != null && request.DATASET_ID != null) ? request.DATASET_ID.Value : (decimal?)null,
                DATA_TYPE = (request != null && !string.IsNullOrEmpty(request.DATA_TYPE)) ? request.DATA_TYPE : null,
                REQUIREMENT = (request != null && request.REQUIREMENT != null) ? request.REQUIREMENT.Value : (short?)null,
                VALUE_DEFAULT = (request != null && !string.IsNullOrEmpty(request.VALUE_DEFAULT)) ? request.VALUE_DEFAULT : null,
                CONSTRAINT = (request != null && !string.IsNullOrEmpty(request.CONSTRAINT)) ? request.CONSTRAINT : null,
                IS_ACTIVE = (request != null && request.IS_ACTIVE != null) ? request.IS_ACTIVE.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
				IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(info);
            return result;
        }
		public bool? Insert(List<SYS_DATASET_DTL_Request> request)
		{
			var list = new List<SYS_DATASET_DTL>();
			foreach (var info in request)
			{
				list.Add(new SYS_DATASET_DTL
				{
					CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                NAME = (info != null && !string.IsNullOrEmpty(info.NAME)) ? info.NAME : null,
                NOTE = (info != null && !string.IsNullOrEmpty(info.NOTE)) ? info.NOTE : null,
                DATASET_ID = (info != null && info.DATASET_ID != null) ? info.DATASET_ID.Value : (decimal?)null,
                DATA_TYPE = (info != null && !string.IsNullOrEmpty(info.DATA_TYPE)) ? info.DATA_TYPE : null,
                REQUIREMENT = (info != null && info.REQUIREMENT != null) ? info.REQUIREMENT.Value : (short?)null,
                VALUE_DEFAULT = (info != null && !string.IsNullOrEmpty(info.VALUE_DEFAULT)) ? info.VALUE_DEFAULT : null,
                CONSTRAINT = (info != null && !string.IsNullOrEmpty(info.CONSTRAINT)) ? info.CONSTRAINT : null,
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
		public bool? UpdateList(List<SYS_DATASET_DTL> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpdateList(list,"ID", getPropertyNames);
			return result;
		}
		public bool? UpInsertList(List<SYS_DATASET_DTL> list)
		{
			var getPropertyNames = this.GetPropertyNames();
			var result = this.UpInsertList(list, "ID", getPropertyNames);
			return result;
		}
		public List<SYS_DATASET_DTL> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_DATASET_ID",(dictParam != null && dictParam.ContainsKey("DATASET_ID")) ? dictParam["DATASET_ID"] : null),
                new OracleParameter("p_DATA_TYPE",(dictParam != null && dictParam.ContainsKey("DATA_TYPE")) ? dictParam["DATA_TYPE"] : null),
                new OracleParameter("p_REQUIREMENT",(dictParam != null && dictParam.ContainsKey("REQUIREMENT")) ? dictParam["REQUIREMENT"] : null),
                new OracleParameter("p_VALUE_DEFAULT",(dictParam != null && dictParam.ContainsKey("VALUE_DEFAULT")) ? dictParam["VALUE_DEFAULT"] : null),
                new OracleParameter("p_CONSTRAINT",(dictParam != null && dictParam.ContainsKey("CONSTRAINT")) ? dictParam["CONSTRAINT"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_DATASET_DTL> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_DATASET_ID",(dictParam != null && dictParam.ContainsKey("DATASET_ID")) ? dictParam["DATASET_ID"] : null),
                new OracleParameter("p_DATA_TYPE",(dictParam != null && dictParam.ContainsKey("DATA_TYPE")) ? dictParam["DATA_TYPE"] : null),
                new OracleParameter("p_REQUIREMENT",(dictParam != null && dictParam.ContainsKey("REQUIREMENT")) ? dictParam["REQUIREMENT"] : null),
                new OracleParameter("p_VALUE_DEFAULT",(dictParam != null && dictParam.ContainsKey("VALUE_DEFAULT")) ? dictParam["VALUE_DEFAULT"] : null),
                new OracleParameter("p_CONSTRAINT",(dictParam != null && dictParam.ContainsKey("CONSTRAINT")) ? dictParam["CONSTRAINT"] : null),
                new OracleParameter("p_IS_ACTIVE",(dictParam != null && dictParam.ContainsKey("IS_ACTIVE")) ? dictParam["IS_ACTIVE"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_DATASET_DTL> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_DATASET_DTL QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","NAME","NOTE","DATASET_ID","DATA_TYPE","REQUIREMENT","VALUE_DEFAULT","CONSTRAINT","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<SYS_DATASET_DTL> results = QuerySolrBase<SYS_DATASET_DTL>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_DATASET_DTL/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_DATASET_DTL> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_NAME,			string p_DATASET_ID,			string p_DATA_TYPE,			string p_REQUIREMENT,			string p_VALUE_DEFAULT,			string p_CONSTRAINT,			string p_IS_ACTIVE,			string p_STATUS,
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
			if (!string.IsNullOrEmpty(p_DATASET_ID))
			{
			    lstFilter.Add(new SolrQuery("DATASET_ID:" + p_DATASET_ID));
			}
			if (!string.IsNullOrEmpty(p_DATA_TYPE))
			{
			    lstFilter.Add(new SolrQuery("DATA_TYPE:" + p_DATA_TYPE));
			}
			if (!string.IsNullOrEmpty(p_REQUIREMENT))
			{
			    lstFilter.Add(new SolrQuery("REQUIREMENT:" + p_REQUIREMENT));
			}
			if (!string.IsNullOrEmpty(p_VALUE_DEFAULT))
			{
			    lstFilter.Add(new SolrQuery("VALUE_DEFAULT:" + p_VALUE_DEFAULT));
			}
			if (!string.IsNullOrEmpty(p_CONSTRAINT))
			{
			    lstFilter.Add(new SolrQuery("CONSTRAINT:" + p_CONSTRAINT));
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
            string[] fieldSelect = { "ID","CODE","NAME","NOTE","DATASET_ID","DATA_TYPE","REQUIREMENT","VALUE_DEFAULT","CONSTRAINT","IS_ACTIVE","STATUS","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<SYS_DATASET_DTL>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_DATASET_DTL/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_DATASET_DTL> listAddRange = new List<SYS_DATASET_DTL>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

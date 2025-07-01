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
    public class EHR_QUAN_HE_GIA_DINHDao : OracleBaseImpl<EHR_QUAN_HE_GIA_DINH>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_QUAN_HE_GIA_DINH";
            PackageName = "PK_EHR_QUAN_HE_GIA_DINH";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_QUAN_HE_GIA_DINH_Information info)
        {        
			var paramValue = new EHR_QUAN_HE_GIA_DINH
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DIA_CHI = (info != null && !string.IsNullOrEmpty(info.DIA_CHI)) ? info.DIA_CHI : null,
                MA_MOI_QH = (info != null && !string.IsNullOrEmpty(info.MA_MOI_QH)) ? info.MA_MOI_QH : null,
                TEN_MOI_QH = (info != null && !string.IsNullOrEmpty(info.TEN_MOI_QH)) ? info.TEN_MOI_QH : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_QUAN_HE_GIA_DINH> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_MA_MOI_QH",(dictParam != null && dictParam.ContainsKey("MA_MOI_QH")) ? dictParam["MA_MOI_QH"] : null),
                new OracleParameter("p_TEN_MOI_QH",(dictParam != null && dictParam.ContainsKey("TEN_MOI_QH")) ? dictParam["TEN_MOI_QH"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_QUAN_HE_GIA_DINH> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_MA_MOI_QH",(dictParam != null && dictParam.ContainsKey("MA_MOI_QH")) ? dictParam["MA_MOI_QH"] : null),
                new OracleParameter("p_TEN_MOI_QH",(dictParam != null && dictParam.ContainsKey("TEN_MOI_QH")) ? dictParam["TEN_MOI_QH"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_QUAN_HE_GIA_DINH> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_QUAN_HE_GIA_DINH QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DIA_CHI","MA_MOI_QH","TEN_MOI_QH","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_QUAN_HE_GIA_DINH> results = QuerySolrBase<EHR_QUAN_HE_GIA_DINH>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_QUAN_HE_GIA_DINH/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_QUAN_HE_GIA_DINH> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DIA_CHI,			string p_MA_MOI_QH,			string p_TEN_MOI_QH,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DIA_CHI))
			{
			    lstFilter.Add(new SolrQuery("DIA_CHI:" + p_DIA_CHI));
			}
			if (!string.IsNullOrEmpty(p_MA_MOI_QH))
			{
			    lstFilter.Add(new SolrQuery("MA_MOI_QH:" + p_MA_MOI_QH));
			}
			if (!string.IsNullOrEmpty(p_TEN_MOI_QH))
			{
			    lstFilter.Add(new SolrQuery("TEN_MOI_QH:" + p_TEN_MOI_QH));
			}
			if (!string.IsNullOrEmpty(p_VERSION_XML))
			{
			    lstFilter.Add(new SolrQuery("VERSION_XML:" + p_VERSION_XML));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","DIA_CHI","MA_MOI_QH","TEN_MOI_QH","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_QUAN_HE_GIA_DINH>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_QUAN_HE_GIA_DINH/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_QUAN_HE_GIA_DINH> listAddRange = new List<EHR_QUAN_HE_GIA_DINH>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

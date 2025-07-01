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
    public class EHR_BAC_SIDao : OracleBaseImpl<EHR_BAC_SI>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_BAC_SI";
            PackageName = "PK_EHR_BAC_SI";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_BAC_SI_Information info)
        {        
			var paramValue = new EHR_BAC_SI
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                CHUNG_CHI_HANH_NGHE = (info != null && !string.IsNullOrEmpty(info.CHUNG_CHI_HANH_NGHE)) ? info.CHUNG_CHI_HANH_NGHE : null,
                HO_TEN = (info != null && !string.IsNullOrEmpty(info.HO_TEN)) ? info.HO_TEN : null,
                MA_BAC_SI = (info != null && !string.IsNullOrEmpty(info.MA_BAC_SI)) ? info.MA_BAC_SI : null,
                MA_BAC_SI_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_BAC_SI_DON_VI)) ? info.MA_BAC_SI_DON_VI : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_BAC_SI> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CHUNG_CHI_HANH_NGHE",(dictParam != null && dictParam.ContainsKey("CHUNG_CHI_HANH_NGHE")) ? dictParam["CHUNG_CHI_HANH_NGHE"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BAC_SI_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI_DON_VI")) ? dictParam["MA_BAC_SI_DON_VI"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_BAC_SI> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_CHUNG_CHI_HANH_NGHE",(dictParam != null && dictParam.ContainsKey("CHUNG_CHI_HANH_NGHE")) ? dictParam["CHUNG_CHI_HANH_NGHE"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BAC_SI_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI_DON_VI")) ? dictParam["MA_BAC_SI_DON_VI"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_BAC_SI> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_BAC_SI QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","CHUNG_CHI_HANH_NGHE","HO_TEN","MA_BAC_SI","MA_BAC_SI_DON_VI","MA_CONG_DAN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_BAC_SI> results = QuerySolrBase<EHR_BAC_SI>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_BAC_SI/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_BAC_SI> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_CHUNG_CHI_HANH_NGHE,			string p_HO_TEN,			string p_MA_BAC_SI,			string p_MA_BAC_SI_DON_VI,			string p_MA_CONG_DAN,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_CHUNG_CHI_HANH_NGHE))
			{
			    lstFilter.Add(new SolrQuery("CHUNG_CHI_HANH_NGHE:" + p_CHUNG_CHI_HANH_NGHE));
			}
			if (!string.IsNullOrEmpty(p_HO_TEN))
			{
			    lstFilter.Add(new SolrQuery("HO_TEN:" + p_HO_TEN));
			}
			if (!string.IsNullOrEmpty(p_MA_BAC_SI))
			{
			    lstFilter.Add(new SolrQuery("MA_BAC_SI:" + p_MA_BAC_SI));
			}
			if (!string.IsNullOrEmpty(p_MA_BAC_SI_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_BAC_SI_DON_VI:" + p_MA_BAC_SI_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
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
            string[] fieldSelect = { "ID","CODE","CHUNG_CHI_HANH_NGHE","HO_TEN","MA_BAC_SI","MA_BAC_SI_DON_VI","MA_CONG_DAN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_BAC_SI>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_BAC_SI/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_BAC_SI> listAddRange = new List<EHR_BAC_SI>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

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
    public class EHR_KQ_KCB_CO_QUANDao : OracleBaseImpl<EHR_KQ_KCB_CO_QUAN>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_KQ_KCB_CO_QUAN";
            PackageName = "PK_EHR_KQ_KCB_CO_QUAN";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_KQ_KCB_CO_QUAN_Information info)
        {        
			var paramValue = new EHR_KQ_KCB_CO_QUAN
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BO_PHAN = (info != null && !string.IsNullOrEmpty(info.BO_PHAN)) ? info.BO_PHAN : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_CO_QUAN = (info != null && !string.IsNullOrEmpty(info.MA_CO_QUAN)) ? info.MA_CO_QUAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_KQ_KCB = (info != null && !string.IsNullOrEmpty(info.MA_KQ_KCB)) ? info.MA_KQ_KCB : null,
                NOI_DUNG_KHAM = (info != null && !string.IsNullOrEmpty(info.NOI_DUNG_KHAM)) ? info.NOI_DUNG_KHAM : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_KQ_KCB_CO_QUAN> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BO_PHAN",(dictParam != null && dictParam.ContainsKey("BO_PHAN")) ? dictParam["BO_PHAN"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_QUAN",(dictParam != null && dictParam.ContainsKey("MA_CO_QUAN")) ? dictParam["MA_CO_QUAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_NOI_DUNG_KHAM",(dictParam != null && dictParam.ContainsKey("NOI_DUNG_KHAM")) ? dictParam["NOI_DUNG_KHAM"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_KQ_KCB_CO_QUAN> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BO_PHAN",(dictParam != null && dictParam.ContainsKey("BO_PHAN")) ? dictParam["BO_PHAN"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_QUAN",(dictParam != null && dictParam.ContainsKey("MA_CO_QUAN")) ? dictParam["MA_CO_QUAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_NOI_DUNG_KHAM",(dictParam != null && dictParam.ContainsKey("NOI_DUNG_KHAM")) ? dictParam["NOI_DUNG_KHAM"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_KQ_KCB_CO_QUAN> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_KQ_KCB_CO_QUAN QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BO_PHAN","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_QUAN","MA_HSSK","MA_KQ_KCB","NOI_DUNG_KHAM","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_KQ_KCB_CO_QUAN> results = QuerySolrBase<EHR_KQ_KCB_CO_QUAN>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_CO_QUAN/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_KQ_KCB_CO_QUAN> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BO_PHAN,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_CO_QUAN,			string p_MA_HSSK,			string p_MA_KQ_KCB,			string p_NOI_DUNG_KHAM,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BO_PHAN))
			{
			    lstFilter.Add(new SolrQuery("BO_PHAN:" + p_BO_PHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CO_QUAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CO_QUAN:" + p_MA_CO_QUAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_KCB))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_KCB:" + p_MA_KQ_KCB));
			}
			if (!string.IsNullOrEmpty(p_NOI_DUNG_KHAM))
			{
			    lstFilter.Add(new SolrQuery("NOI_DUNG_KHAM:" + p_NOI_DUNG_KHAM));
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
            string[] fieldSelect = { "ID","CODE","BO_PHAN","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_QUAN","MA_HSSK","MA_KQ_KCB","NOI_DUNG_KHAM","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_KQ_KCB_CO_QUAN>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_CO_QUAN/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_KQ_KCB_CO_QUAN> listAddRange = new List<EHR_KQ_KCB_CO_QUAN>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

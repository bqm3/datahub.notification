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
    public class EHR_KHUYET_TATDao : OracleBaseImpl<EHR_KHUYET_TAT>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_KHUYET_TAT";
            PackageName = "PK_EHR_KHUYET_TAT";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_KHUYET_TAT_Information info)
        {        
			var paramValue = new EHR_KHUYET_TAT
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                LOAI_KHUYET_TAT = (info != null && !string.IsNullOrEmpty(info.LOAI_KHUYET_TAT)) ? info.LOAI_KHUYET_TAT : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_KHUYET_TAT = (info != null && !string.IsNullOrEmpty(info.MA_KHUYET_TAT)) ? info.MA_KHUYET_TAT : null,
                MA_SUC_KHOE = (info != null && !string.IsNullOrEmpty(info.MA_SUC_KHOE)) ? info.MA_SUC_KHOE : null,
                MO_TA_KHUYET_TAT = (info != null && !string.IsNullOrEmpty(info.MO_TA_KHUYET_TAT)) ? info.MO_TA_KHUYET_TAT : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_KHUYET_TAT> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOAI_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("LOAI_KHUYET_TAT")) ? dictParam["LOAI_KHUYET_TAT"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("MA_KHUYET_TAT")) ? dictParam["MA_KHUYET_TAT"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MO_TA_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("MO_TA_KHUYET_TAT")) ? dictParam["MO_TA_KHUYET_TAT"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_KHUYET_TAT> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOAI_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("LOAI_KHUYET_TAT")) ? dictParam["LOAI_KHUYET_TAT"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("MA_KHUYET_TAT")) ? dictParam["MA_KHUYET_TAT"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MO_TA_KHUYET_TAT",(dictParam != null && dictParam.ContainsKey("MO_TA_KHUYET_TAT")) ? dictParam["MO_TA_KHUYET_TAT"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_KHUYET_TAT> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_KHUYET_TAT QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","LOAI_KHUYET_TAT","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_KHUYET_TAT","MA_SUC_KHOE","MO_TA_KHUYET_TAT","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_KHUYET_TAT> results = QuerySolrBase<EHR_KHUYET_TAT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KHUYET_TAT/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_KHUYET_TAT> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_LOAI_KHUYET_TAT,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_KHUYET_TAT,			string p_MA_SUC_KHOE,			string p_MO_TA_KHUYET_TAT,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_LOAI_KHUYET_TAT))
			{
			    lstFilter.Add(new SolrQuery("LOAI_KHUYET_TAT:" + p_LOAI_KHUYET_TAT));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_KHUYET_TAT))
			{
			    lstFilter.Add(new SolrQuery("MA_KHUYET_TAT:" + p_MA_KHUYET_TAT));
			}
			if (!string.IsNullOrEmpty(p_MA_SUC_KHOE))
			{
			    lstFilter.Add(new SolrQuery("MA_SUC_KHOE:" + p_MA_SUC_KHOE));
			}
			if (!string.IsNullOrEmpty(p_MO_TA_KHUYET_TAT))
			{
			    lstFilter.Add(new SolrQuery("MO_TA_KHUYET_TAT:" + p_MO_TA_KHUYET_TAT));
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
            string[] fieldSelect = { "ID","CODE","LOAI_KHUYET_TAT","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_KHUYET_TAT","MA_SUC_KHOE","MO_TA_KHUYET_TAT","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_KHUYET_TAT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KHUYET_TAT/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_KHUYET_TAT> listAddRange = new List<EHR_KHUYET_TAT>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

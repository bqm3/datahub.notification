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
    public class EHR_BENH_TATDao : OracleBaseImpl<EHR_BENH_TAT>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_BENH_TAT";
            PackageName = "PK_EHR_BENH_TAT";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_BENH_TAT_Information info)
        {        
			var paramValue = new EHR_BENH_TAT
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BIEU_HIEN = (info != null && !string.IsNullOrEmpty(info.BIEU_HIEN)) ? info.BIEU_HIEN : null,
                GHI_CHU = (info != null && !string.IsNullOrEmpty(info.GHI_CHU)) ? info.GHI_CHU : null,
                LOAI_BENH_TAT = (info != null && !string.IsNullOrEmpty(info.LOAI_BENH_TAT)) ? info.LOAI_BENH_TAT : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_BENH_TAT = (info != null && !string.IsNullOrEmpty(info.MA_BENH_TAT)) ? info.MA_BENH_TAT : null,
                MA_BENH_TAT_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_BENH_TAT_DON_VI)) ? info.MA_BENH_TAT_DON_VI : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MO_TA = (info != null && !string.IsNullOrEmpty(info.MO_TA)) ? info.MO_TA : null,
                MUC_DO = (info != null && !string.IsNullOrEmpty(info.MUC_DO)) ? info.MUC_DO : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_BENH_TAT> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BIEU_HIEN",(dictParam != null && dictParam.ContainsKey("BIEU_HIEN")) ? dictParam["BIEU_HIEN"] : null),
                new OracleParameter("p_GHI_CHU",(dictParam != null && dictParam.ContainsKey("GHI_CHU")) ? dictParam["GHI_CHU"] : null),
                new OracleParameter("p_LOAI_BENH_TAT",(dictParam != null && dictParam.ContainsKey("LOAI_BENH_TAT")) ? dictParam["LOAI_BENH_TAT"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_BENH_TAT",(dictParam != null && dictParam.ContainsKey("MA_BENH_TAT")) ? dictParam["MA_BENH_TAT"] : null),
                new OracleParameter("p_MA_BENH_TAT_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_BENH_TAT_DON_VI")) ? dictParam["MA_BENH_TAT_DON_VI"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MO_TA",(dictParam != null && dictParam.ContainsKey("MO_TA")) ? dictParam["MO_TA"] : null),
                new OracleParameter("p_MUC_DO",(dictParam != null && dictParam.ContainsKey("MUC_DO")) ? dictParam["MUC_DO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_BENH_TAT> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BIEU_HIEN",(dictParam != null && dictParam.ContainsKey("BIEU_HIEN")) ? dictParam["BIEU_HIEN"] : null),
                new OracleParameter("p_GHI_CHU",(dictParam != null && dictParam.ContainsKey("GHI_CHU")) ? dictParam["GHI_CHU"] : null),
                new OracleParameter("p_LOAI_BENH_TAT",(dictParam != null && dictParam.ContainsKey("LOAI_BENH_TAT")) ? dictParam["LOAI_BENH_TAT"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_BENH_TAT",(dictParam != null && dictParam.ContainsKey("MA_BENH_TAT")) ? dictParam["MA_BENH_TAT"] : null),
                new OracleParameter("p_MA_BENH_TAT_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_BENH_TAT_DON_VI")) ? dictParam["MA_BENH_TAT_DON_VI"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MO_TA",(dictParam != null && dictParam.ContainsKey("MO_TA")) ? dictParam["MO_TA"] : null),
                new OracleParameter("p_MUC_DO",(dictParam != null && dictParam.ContainsKey("MUC_DO")) ? dictParam["MUC_DO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_BENH_TAT> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_BENH_TAT QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BIEU_HIEN","GHI_CHU","LOAI_BENH_TAT","MA_BENH_NHAN","MA_BENH_TAT","MA_BENH_TAT_DON_VI","MA_CONG_DAN","MA_HSSK","MO_TA","MUC_DO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_BENH_TAT> results = QuerySolrBase<EHR_BENH_TAT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_BENH_TAT/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_BENH_TAT> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BIEU_HIEN,			string p_GHI_CHU,			string p_LOAI_BENH_TAT,			string p_MA_BENH_NHAN,			string p_MA_BENH_TAT,			string p_MA_BENH_TAT_DON_VI,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MO_TA,			string p_MUC_DO,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BIEU_HIEN))
			{
			    lstFilter.Add(new SolrQuery("BIEU_HIEN:" + p_BIEU_HIEN));
			}
			if (!string.IsNullOrEmpty(p_GHI_CHU))
			{
			    lstFilter.Add(new SolrQuery("GHI_CHU:" + p_GHI_CHU));
			}
			if (!string.IsNullOrEmpty(p_LOAI_BENH_TAT))
			{
			    lstFilter.Add(new SolrQuery("LOAI_BENH_TAT:" + p_LOAI_BENH_TAT));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_TAT))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_TAT:" + p_MA_BENH_TAT));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_TAT_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_TAT_DON_VI:" + p_MA_BENH_TAT_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MO_TA))
			{
			    lstFilter.Add(new SolrQuery("MO_TA:" + p_MO_TA));
			}
			if (!string.IsNullOrEmpty(p_MUC_DO))
			{
			    lstFilter.Add(new SolrQuery("MUC_DO:" + p_MUC_DO));
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
            string[] fieldSelect = { "ID","CODE","BIEU_HIEN","GHI_CHU","LOAI_BENH_TAT","MA_BENH_NHAN","MA_BENH_TAT","MA_BENH_TAT_DON_VI","MA_CONG_DAN","MA_HSSK","MO_TA","MUC_DO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_BENH_TAT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_BENH_TAT/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_BENH_TAT> listAddRange = new List<EHR_BENH_TAT>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

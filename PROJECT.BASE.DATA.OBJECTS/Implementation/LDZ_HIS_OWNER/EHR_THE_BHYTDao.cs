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
    public class EHR_THE_BHYTDao : OracleBaseImpl<EHR_THE_BHYT>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_THE_BHYT";
            PackageName = "PK_EHR_THE_BHYT";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_THE_BHYT_Information info)
        {        
			var paramValue = new EHR_THE_BHYT
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                GIOI_TINH = (info != null && !string.IsNullOrEmpty(info.GIOI_TINH)) ? info.GIOI_TINH : null,
                HO_TEN = (info != null && !string.IsNullOrEmpty(info.HO_TEN)) ? info.HO_TEN : null,
                KICH_HOAT = (info != null && !string.IsNullOrEmpty(info.KICH_HOAT)) ? info.KICH_HOAT : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_THE_BHYT = (info != null && !string.IsNullOrEmpty(info.MA_THE_BHYT)) ? info.MA_THE_BHYT : null,
                NGAY_SINH = (info != null && !string.IsNullOrEmpty(info.NGAY_SINH)) ? info.NGAY_SINH : null,
                NOI_DK_KCBBD = (info != null && !string.IsNullOrEmpty(info.NOI_DK_KCBBD)) ? info.NOI_DK_KCBBD : null,
                SO_THE_BHYT = (info != null && !string.IsNullOrEmpty(info.SO_THE_BHYT)) ? info.SO_THE_BHYT : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_THE_BHYT> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_KICH_HOAT",(dictParam != null && dictParam.ContainsKey("KICH_HOAT")) ? dictParam["KICH_HOAT"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_THE_BHYT",(dictParam != null && dictParam.ContainsKey("MA_THE_BHYT")) ? dictParam["MA_THE_BHYT"] : null),
                new OracleParameter("p_NGAY_SINH",(dictParam != null && dictParam.ContainsKey("NGAY_SINH")) ? dictParam["NGAY_SINH"] : null),
                new OracleParameter("p_NOI_DK_KCBBD",(dictParam != null && dictParam.ContainsKey("NOI_DK_KCBBD")) ? dictParam["NOI_DK_KCBBD"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_THE_BHYT> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_KICH_HOAT",(dictParam != null && dictParam.ContainsKey("KICH_HOAT")) ? dictParam["KICH_HOAT"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_THE_BHYT",(dictParam != null && dictParam.ContainsKey("MA_THE_BHYT")) ? dictParam["MA_THE_BHYT"] : null),
                new OracleParameter("p_NGAY_SINH",(dictParam != null && dictParam.ContainsKey("NGAY_SINH")) ? dictParam["NGAY_SINH"] : null),
                new OracleParameter("p_NOI_DK_KCBBD",(dictParam != null && dictParam.ContainsKey("NOI_DK_KCBBD")) ? dictParam["NOI_DK_KCBBD"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_THE_BHYT> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_THE_BHYT QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","GIOI_TINH","HO_TEN","KICH_HOAT","MA_CONG_DAN","MA_THE_BHYT","NGAY_SINH","NOI_DK_KCBBD","SO_THE_BHYT","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_THE_BHYT> results = QuerySolrBase<EHR_THE_BHYT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_THE_BHYT/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_THE_BHYT> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_GIOI_TINH,			string p_HO_TEN,			string p_KICH_HOAT,			string p_MA_CONG_DAN,			string p_MA_THE_BHYT,			string p_NGAY_SINH,			string p_NOI_DK_KCBBD,			string p_SO_THE_BHYT,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_GIOI_TINH))
			{
			    lstFilter.Add(new SolrQuery("GIOI_TINH:" + p_GIOI_TINH));
			}
			if (!string.IsNullOrEmpty(p_HO_TEN))
			{
			    lstFilter.Add(new SolrQuery("HO_TEN:" + p_HO_TEN));
			}
			if (!string.IsNullOrEmpty(p_KICH_HOAT))
			{
			    lstFilter.Add(new SolrQuery("KICH_HOAT:" + p_KICH_HOAT));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_THE_BHYT))
			{
			    lstFilter.Add(new SolrQuery("MA_THE_BHYT:" + p_MA_THE_BHYT));
			}
			if (!string.IsNullOrEmpty(p_NGAY_SINH))
			{
			    lstFilter.Add(new SolrQuery("NGAY_SINH:" + p_NGAY_SINH));
			}
			if (!string.IsNullOrEmpty(p_NOI_DK_KCBBD))
			{
			    lstFilter.Add(new SolrQuery("NOI_DK_KCBBD:" + p_NOI_DK_KCBBD));
			}
			if (!string.IsNullOrEmpty(p_SO_THE_BHYT))
			{
			    lstFilter.Add(new SolrQuery("SO_THE_BHYT:" + p_SO_THE_BHYT));
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
            string[] fieldSelect = { "ID","CODE","GIOI_TINH","HO_TEN","KICH_HOAT","MA_CONG_DAN","MA_THE_BHYT","NGAY_SINH","NOI_DK_KCBBD","SO_THE_BHYT","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_THE_BHYT>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_THE_BHYT/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_THE_BHYT> listAddRange = new List<EHR_THE_BHYT>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

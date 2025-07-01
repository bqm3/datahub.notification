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
    public class EHR_DON_THUOCDao : OracleBaseImpl<EHR_DON_THUOC>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_DON_THUOC";
            PackageName = "PK_EHR_DON_THUOC";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_DON_THUOC_Information info)
        {        
			var paramValue = new EHR_DON_THUOC
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                LOAI_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.LOAI_BENH_NHAN)) ? info.LOAI_BENH_NHAN : null,
                MA_BAC_SI = (info != null && !string.IsNullOrEmpty(info.MA_BAC_SI)) ? info.MA_BAC_SI : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_CO_SO_Y_TE = (info != null && !string.IsNullOrEmpty(info.MA_CO_SO_Y_TE)) ? info.MA_CO_SO_Y_TE : null,
                MA_DON_THUOC = (info != null && !string.IsNullOrEmpty(info.MA_DON_THUOC)) ? info.MA_DON_THUOC : null,
                MA_DON_THUOC_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_DON_THUOC_DON_VI)) ? info.MA_DON_THUOC_DON_VI : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_KQ_KCB = (info != null && !string.IsNullOrEmpty(info.MA_KQ_KCB)) ? info.MA_KQ_KCB : null,
                THOI_GIAN_KE_THUOC = (info != null && !string.IsNullOrEmpty(info.THOI_GIAN_KE_THUOC)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.THOI_GIAN_KE_THUOC, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_DON_THUOC> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOAI_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("LOAI_BENH_NHAN")) ? dictParam["LOAI_BENH_NHAN"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_DON_THUOC",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC")) ? dictParam["MA_DON_THUOC"] : null),
                new OracleParameter("p_MA_DON_THUOC_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC_DON_VI")) ? dictParam["MA_DON_THUOC_DON_VI"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_THOI_GIAN_KE_THUOC_START", (dictParam != null && dictParam.ContainsKey("THOI_GIAN_KE_THUOC_START")) ? dictParam["THOI_GIAN_KE_THUOC_START"] : null),
                new OracleParameter("p_THOI_GIAN_KE_THUOC_END", (dictParam != null && dictParam.ContainsKey("THOI_GIAN_KE_THUOC_END")) ? dictParam["THOI_GIAN_KE_THUOC_END"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_DON_THUOC> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_LOAI_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("LOAI_BENH_NHAN")) ? dictParam["LOAI_BENH_NHAN"] : null),
                new OracleParameter("p_MA_BAC_SI",(dictParam != null && dictParam.ContainsKey("MA_BAC_SI")) ? dictParam["MA_BAC_SI"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_DON_THUOC",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC")) ? dictParam["MA_DON_THUOC"] : null),
                new OracleParameter("p_MA_DON_THUOC_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC_DON_VI")) ? dictParam["MA_DON_THUOC_DON_VI"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_THOI_GIAN_KE_THUOC_START", (dictParam != null && dictParam.ContainsKey("THOI_GIAN_KE_THUOC_START")) ? dictParam["THOI_GIAN_KE_THUOC_START"] : null),
                new OracleParameter("p_THOI_GIAN_KE_THUOC_END", (dictParam != null && dictParam.ContainsKey("THOI_GIAN_KE_THUOC_END")) ? dictParam["THOI_GIAN_KE_THUOC_END"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_DON_THUOC> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_DON_THUOC QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","LOAI_BENH_NHAN","MA_BAC_SI","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_SO_Y_TE","MA_DON_THUOC","MA_DON_THUOC_DON_VI","MA_HSSK","MA_KQ_KCB","THOI_GIAN_KE_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_DON_THUOC> results = QuerySolrBase<EHR_DON_THUOC>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DON_THUOC/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_DON_THUOC> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_LOAI_BENH_NHAN,			string p_MA_BAC_SI,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_CO_SO_Y_TE,			string p_MA_DON_THUOC,			string p_MA_DON_THUOC_DON_VI,			string p_MA_HSSK,			string p_MA_KQ_KCB,			DateTime? p_THOI_GIAN_KE_THUOC_START,			DateTime? p_THOI_GIAN_KE_THUOC_END,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_LOAI_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("LOAI_BENH_NHAN:" + p_LOAI_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_BAC_SI))
			{
			    lstFilter.Add(new SolrQuery("MA_BAC_SI:" + p_MA_BAC_SI));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CO_SO_Y_TE))
			{
			    lstFilter.Add(new SolrQuery("MA_CO_SO_Y_TE:" + p_MA_CO_SO_Y_TE));
			}
			if (!string.IsNullOrEmpty(p_MA_DON_THUOC))
			{
			    lstFilter.Add(new SolrQuery("MA_DON_THUOC:" + p_MA_DON_THUOC));
			}
			if (!string.IsNullOrEmpty(p_MA_DON_THUOC_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_DON_THUOC_DON_VI:" + p_MA_DON_THUOC_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_KCB))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_KCB:" + p_MA_KQ_KCB));
			}
			if (p_THOI_GIAN_KE_THUOC_START != null && p_THOI_GIAN_KE_THUOC_END != null)
			{
			    lstFilter.Add(new SolrQuery("THOI_GIAN_KE_THUOC:[" + Utility.GetJSONZFromUserDateTime(p_THOI_GIAN_KE_THUOC_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_THOI_GIAN_KE_THUOC_END.Value) + "]"));
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
            string[] fieldSelect = { "ID","CODE","LOAI_BENH_NHAN","MA_BAC_SI","MA_BENH_NHAN","MA_CONG_DAN","MA_CO_SO_Y_TE","MA_DON_THUOC","MA_DON_THUOC_DON_VI","MA_HSSK","MA_KQ_KCB","THOI_GIAN_KE_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_DON_THUOC>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DON_THUOC/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_DON_THUOC> listAddRange = new List<EHR_DON_THUOC>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

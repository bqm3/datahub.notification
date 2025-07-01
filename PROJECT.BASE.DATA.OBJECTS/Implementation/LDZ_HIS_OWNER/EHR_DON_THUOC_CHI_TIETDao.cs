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
    public class EHR_DON_THUOC_CHI_TIETDao : OracleBaseImpl<EHR_DON_THUOC_CHI_TIET>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_DON_THUOC_CHI_TIET";
            PackageName = "PK_EHR_DON_THUOC_CHI_TIET";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_DON_THUOC_CHI_TIET_Information info)
        {        
			var paramValue = new EHR_DON_THUOC_CHI_TIET
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DON_VI_TINH = (info != null && !string.IsNullOrEmpty(info.DON_VI_TINH)) ? info.DON_VI_TINH : null,
                HAM_LUONG = (info != null && !string.IsNullOrEmpty(info.HAM_LUONG)) ? info.HAM_LUONG : null,
                LIEU_DUNG = (info != null && !string.IsNullOrEmpty(info.LIEU_DUNG)) ? info.LIEU_DUNG : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_DON_THUOC = (info != null && !string.IsNullOrEmpty(info.MA_DON_THUOC)) ? info.MA_DON_THUOC : null,
                MA_DON_THUOC_CHI_TIET = (info != null && !string.IsNullOrEmpty(info.MA_DON_THUOC_CHI_TIET)) ? info.MA_DON_THUOC_CHI_TIET : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_THUOC = (info != null && !string.IsNullOrEmpty(info.MA_THUOC)) ? info.MA_THUOC : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_DON_THUOC_CHI_TIET> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DON_VI_TINH",(dictParam != null && dictParam.ContainsKey("DON_VI_TINH")) ? dictParam["DON_VI_TINH"] : null),
                new OracleParameter("p_HAM_LUONG",(dictParam != null && dictParam.ContainsKey("HAM_LUONG")) ? dictParam["HAM_LUONG"] : null),
                new OracleParameter("p_LIEU_DUNG",(dictParam != null && dictParam.ContainsKey("LIEU_DUNG")) ? dictParam["LIEU_DUNG"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_DON_THUOC",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC")) ? dictParam["MA_DON_THUOC"] : null),
                new OracleParameter("p_MA_DON_THUOC_CHI_TIET",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC_CHI_TIET")) ? dictParam["MA_DON_THUOC_CHI_TIET"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_THUOC",(dictParam != null && dictParam.ContainsKey("MA_THUOC")) ? dictParam["MA_THUOC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_DON_THUOC_CHI_TIET> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DON_VI_TINH",(dictParam != null && dictParam.ContainsKey("DON_VI_TINH")) ? dictParam["DON_VI_TINH"] : null),
                new OracleParameter("p_HAM_LUONG",(dictParam != null && dictParam.ContainsKey("HAM_LUONG")) ? dictParam["HAM_LUONG"] : null),
                new OracleParameter("p_LIEU_DUNG",(dictParam != null && dictParam.ContainsKey("LIEU_DUNG")) ? dictParam["LIEU_DUNG"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_DON_THUOC",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC")) ? dictParam["MA_DON_THUOC"] : null),
                new OracleParameter("p_MA_DON_THUOC_CHI_TIET",(dictParam != null && dictParam.ContainsKey("MA_DON_THUOC_CHI_TIET")) ? dictParam["MA_DON_THUOC_CHI_TIET"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_THUOC",(dictParam != null && dictParam.ContainsKey("MA_THUOC")) ? dictParam["MA_THUOC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_DON_THUOC_CHI_TIET> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_DON_THUOC_CHI_TIET QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DON_VI_TINH","HAM_LUONG","LIEU_DUNG","MA_BENH_NHAN","MA_CONG_DAN","MA_DON_THUOC","MA_DON_THUOC_CHI_TIET","MA_HSSK","MA_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_DON_THUOC_CHI_TIET> results = QuerySolrBase<EHR_DON_THUOC_CHI_TIET>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DON_THUOC_CHI_TIET/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_DON_THUOC_CHI_TIET> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DON_VI_TINH,			string p_HAM_LUONG,			string p_LIEU_DUNG,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_DON_THUOC,			string p_MA_DON_THUOC_CHI_TIET,			string p_MA_HSSK,			string p_MA_THUOC,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DON_VI_TINH))
			{
			    lstFilter.Add(new SolrQuery("DON_VI_TINH:" + p_DON_VI_TINH));
			}
			if (!string.IsNullOrEmpty(p_HAM_LUONG))
			{
			    lstFilter.Add(new SolrQuery("HAM_LUONG:" + p_HAM_LUONG));
			}
			if (!string.IsNullOrEmpty(p_LIEU_DUNG))
			{
			    lstFilter.Add(new SolrQuery("LIEU_DUNG:" + p_LIEU_DUNG));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_DON_THUOC))
			{
			    lstFilter.Add(new SolrQuery("MA_DON_THUOC:" + p_MA_DON_THUOC));
			}
			if (!string.IsNullOrEmpty(p_MA_DON_THUOC_CHI_TIET))
			{
			    lstFilter.Add(new SolrQuery("MA_DON_THUOC_CHI_TIET:" + p_MA_DON_THUOC_CHI_TIET));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_THUOC))
			{
			    lstFilter.Add(new SolrQuery("MA_THUOC:" + p_MA_THUOC));
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
            string[] fieldSelect = { "ID","CODE","DON_VI_TINH","HAM_LUONG","LIEU_DUNG","MA_BENH_NHAN","MA_CONG_DAN","MA_DON_THUOC","MA_DON_THUOC_CHI_TIET","MA_HSSK","MA_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_DON_THUOC_CHI_TIET>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_DON_THUOC_CHI_TIET/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_DON_THUOC_CHI_TIET> listAddRange = new List<EHR_DON_THUOC_CHI_TIET>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

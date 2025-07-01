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
    public class EHR_THUOCDao : OracleBaseImpl<EHR_THUOC>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_THUOC";
            PackageName = "PK_EHR_THUOC";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_THUOC_Information info)
        {        
			var paramValue = new EHR_THUOC
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DUONG_DUNG = (info != null && !string.IsNullOrEmpty(info.DUONG_DUNG)) ? info.DUONG_DUNG : null,
                HOAT_CHAT = (info != null && !string.IsNullOrEmpty(info.HOAT_CHAT)) ? info.HOAT_CHAT : null,
                MA_LOAI_THUOC = (info != null && !string.IsNullOrEmpty(info.MA_LOAI_THUOC)) ? info.MA_LOAI_THUOC : null,
                MA_THUOC = (info != null && !string.IsNullOrEmpty(info.MA_THUOC)) ? info.MA_THUOC : null,
                MA_THUOC_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_THUOC_DON_VI)) ? info.MA_THUOC_DON_VI : null,
                SO_DANG_KY = (info != null && !string.IsNullOrEmpty(info.SO_DANG_KY)) ? info.SO_DANG_KY : null,
                TEN_THUOC = (info != null && !string.IsNullOrEmpty(info.TEN_THUOC)) ? info.TEN_THUOC : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_THUOC> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DUONG_DUNG",(dictParam != null && dictParam.ContainsKey("DUONG_DUNG")) ? dictParam["DUONG_DUNG"] : null),
                new OracleParameter("p_HOAT_CHAT",(dictParam != null && dictParam.ContainsKey("HOAT_CHAT")) ? dictParam["HOAT_CHAT"] : null),
                new OracleParameter("p_MA_LOAI_THUOC",(dictParam != null && dictParam.ContainsKey("MA_LOAI_THUOC")) ? dictParam["MA_LOAI_THUOC"] : null),
                new OracleParameter("p_MA_THUOC",(dictParam != null && dictParam.ContainsKey("MA_THUOC")) ? dictParam["MA_THUOC"] : null),
                new OracleParameter("p_MA_THUOC_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_THUOC_DON_VI")) ? dictParam["MA_THUOC_DON_VI"] : null),
                new OracleParameter("p_SO_DANG_KY",(dictParam != null && dictParam.ContainsKey("SO_DANG_KY")) ? dictParam["SO_DANG_KY"] : null),
                new OracleParameter("p_TEN_THUOC",(dictParam != null && dictParam.ContainsKey("TEN_THUOC")) ? dictParam["TEN_THUOC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_THUOC> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DUONG_DUNG",(dictParam != null && dictParam.ContainsKey("DUONG_DUNG")) ? dictParam["DUONG_DUNG"] : null),
                new OracleParameter("p_HOAT_CHAT",(dictParam != null && dictParam.ContainsKey("HOAT_CHAT")) ? dictParam["HOAT_CHAT"] : null),
                new OracleParameter("p_MA_LOAI_THUOC",(dictParam != null && dictParam.ContainsKey("MA_LOAI_THUOC")) ? dictParam["MA_LOAI_THUOC"] : null),
                new OracleParameter("p_MA_THUOC",(dictParam != null && dictParam.ContainsKey("MA_THUOC")) ? dictParam["MA_THUOC"] : null),
                new OracleParameter("p_MA_THUOC_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_THUOC_DON_VI")) ? dictParam["MA_THUOC_DON_VI"] : null),
                new OracleParameter("p_SO_DANG_KY",(dictParam != null && dictParam.ContainsKey("SO_DANG_KY")) ? dictParam["SO_DANG_KY"] : null),
                new OracleParameter("p_TEN_THUOC",(dictParam != null && dictParam.ContainsKey("TEN_THUOC")) ? dictParam["TEN_THUOC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_THUOC> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_THUOC QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DUONG_DUNG","HOAT_CHAT","MA_LOAI_THUOC","MA_THUOC","MA_THUOC_DON_VI","SO_DANG_KY","TEN_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_THUOC> results = QuerySolrBase<EHR_THUOC>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_THUOC/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_THUOC> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_DUONG_DUNG,			string p_HOAT_CHAT,			string p_MA_LOAI_THUOC,			string p_MA_THUOC,			string p_MA_THUOC_DON_VI,			string p_SO_DANG_KY,			string p_TEN_THUOC,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_DUONG_DUNG))
			{
			    lstFilter.Add(new SolrQuery("DUONG_DUNG:" + p_DUONG_DUNG));
			}
			if (!string.IsNullOrEmpty(p_HOAT_CHAT))
			{
			    lstFilter.Add(new SolrQuery("HOAT_CHAT:" + p_HOAT_CHAT));
			}
			if (!string.IsNullOrEmpty(p_MA_LOAI_THUOC))
			{
			    lstFilter.Add(new SolrQuery("MA_LOAI_THUOC:" + p_MA_LOAI_THUOC));
			}
			if (!string.IsNullOrEmpty(p_MA_THUOC))
			{
			    lstFilter.Add(new SolrQuery("MA_THUOC:" + p_MA_THUOC));
			}
			if (!string.IsNullOrEmpty(p_MA_THUOC_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_THUOC_DON_VI:" + p_MA_THUOC_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_SO_DANG_KY))
			{
			    lstFilter.Add(new SolrQuery("SO_DANG_KY:" + p_SO_DANG_KY));
			}
			if (!string.IsNullOrEmpty(p_TEN_THUOC))
			{
			    lstFilter.Add(new SolrQuery("TEN_THUOC:" + p_TEN_THUOC));
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
            string[] fieldSelect = { "ID","CODE","DUONG_DUNG","HOAT_CHAT","MA_LOAI_THUOC","MA_THUOC","MA_THUOC_DON_VI","SO_DANG_KY","TEN_THUOC","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_THUOC>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_THUOC/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_THUOC> listAddRange = new List<EHR_THUOC>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

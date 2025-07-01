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
    public class EHR_TIEM_CHUNGDao : OracleBaseImpl<EHR_TIEM_CHUNG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_TIEM_CHUNG";
            PackageName = "PK_EHR_TIEM_CHUNG";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_TIEM_CHUNG_Information info)
        {        
			var paramValue = new EHR_TIEM_CHUNG
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                DA_CHUNG_NGUA_NGAY = (info != null && !string.IsNullOrEmpty(info.DA_CHUNG_NGUA_NGAY)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.DA_CHUNG_NGUA_NGAY, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                LAN_TIEM_CHUNG = (info != null && info.LAN_TIEM_CHUNG != null) ? info.LAN_TIEM_CHUNG.Value : (decimal?)null,
                LOAI_TIEM_CHUNG = (info != null && !string.IsNullOrEmpty(info.LOAI_TIEM_CHUNG)) ? info.LOAI_TIEM_CHUNG : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_LOAI_TIEM_CHUNG = (info != null && !string.IsNullOrEmpty(info.MA_LOAI_TIEM_CHUNG)) ? info.MA_LOAI_TIEM_CHUNG : null,
                MA_VAC_XIN = (info != null && !string.IsNullOrEmpty(info.MA_VAC_XIN)) ? info.MA_VAC_XIN : null,
                PHAN_UNG = (info != null && !string.IsNullOrEmpty(info.PHAN_UNG)) ? info.PHAN_UNG : null,
                THANG_THAI = (info != null && info.THANG_THAI != null) ? info.THANG_THAI.Value : (decimal?)null,
                VAC_XIN = (info != null && !string.IsNullOrEmpty(info.VAC_XIN)) ? info.VAC_XIN : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_TIEM_CHUNG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DA_CHUNG_NGUA_NGAY_START", (dictParam != null && dictParam.ContainsKey("DA_CHUNG_NGUA_NGAY_START")) ? dictParam["DA_CHUNG_NGUA_NGAY_START"] : null),
                new OracleParameter("p_DA_CHUNG_NGUA_NGAY_END", (dictParam != null && dictParam.ContainsKey("DA_CHUNG_NGUA_NGAY_END")) ? dictParam["DA_CHUNG_NGUA_NGAY_END"] : null),
                new OracleParameter("p_LAN_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("LAN_TIEM_CHUNG")) ? dictParam["LAN_TIEM_CHUNG"] : null),
                new OracleParameter("p_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("LOAI_TIEM_CHUNG")) ? dictParam["LOAI_TIEM_CHUNG"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("MA_LOAI_TIEM_CHUNG")) ? dictParam["MA_LOAI_TIEM_CHUNG"] : null),
                new OracleParameter("p_MA_VAC_XIN",(dictParam != null && dictParam.ContainsKey("MA_VAC_XIN")) ? dictParam["MA_VAC_XIN"] : null),
                new OracleParameter("p_PHAN_UNG",(dictParam != null && dictParam.ContainsKey("PHAN_UNG")) ? dictParam["PHAN_UNG"] : null),
                new OracleParameter("p_THANG_THAI",(dictParam != null && dictParam.ContainsKey("THANG_THAI")) ? dictParam["THANG_THAI"] : null),
                new OracleParameter("p_VAC_XIN",(dictParam != null && dictParam.ContainsKey("VAC_XIN")) ? dictParam["VAC_XIN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_TIEM_CHUNG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_DA_CHUNG_NGUA_NGAY_START", (dictParam != null && dictParam.ContainsKey("DA_CHUNG_NGUA_NGAY_START")) ? dictParam["DA_CHUNG_NGUA_NGAY_START"] : null),
                new OracleParameter("p_DA_CHUNG_NGUA_NGAY_END", (dictParam != null && dictParam.ContainsKey("DA_CHUNG_NGUA_NGAY_END")) ? dictParam["DA_CHUNG_NGUA_NGAY_END"] : null),
                new OracleParameter("p_LAN_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("LAN_TIEM_CHUNG")) ? dictParam["LAN_TIEM_CHUNG"] : null),
                new OracleParameter("p_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("LOAI_TIEM_CHUNG")) ? dictParam["LOAI_TIEM_CHUNG"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("MA_LOAI_TIEM_CHUNG")) ? dictParam["MA_LOAI_TIEM_CHUNG"] : null),
                new OracleParameter("p_MA_VAC_XIN",(dictParam != null && dictParam.ContainsKey("MA_VAC_XIN")) ? dictParam["MA_VAC_XIN"] : null),
                new OracleParameter("p_PHAN_UNG",(dictParam != null && dictParam.ContainsKey("PHAN_UNG")) ? dictParam["PHAN_UNG"] : null),
                new OracleParameter("p_THANG_THAI",(dictParam != null && dictParam.ContainsKey("THANG_THAI")) ? dictParam["THANG_THAI"] : null),
                new OracleParameter("p_VAC_XIN",(dictParam != null && dictParam.ContainsKey("VAC_XIN")) ? dictParam["VAC_XIN"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_TIEM_CHUNG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_TIEM_CHUNG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","DA_CHUNG_NGUA_NGAY","LAN_TIEM_CHUNG","LOAI_TIEM_CHUNG","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_LOAI_TIEM_CHUNG","MA_VAC_XIN","PHAN_UNG","THANG_THAI","VAC_XIN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_TIEM_CHUNG> results = QuerySolrBase<EHR_TIEM_CHUNG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_TIEM_CHUNG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_TIEM_CHUNG> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			DateTime? p_DA_CHUNG_NGUA_NGAY_START,			DateTime? p_DA_CHUNG_NGUA_NGAY_END,			string p_LAN_TIEM_CHUNG,			string p_LOAI_TIEM_CHUNG,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_LOAI_TIEM_CHUNG,			string p_MA_VAC_XIN,			string p_PHAN_UNG,			string p_THANG_THAI,			string p_VAC_XIN,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (p_DA_CHUNG_NGUA_NGAY_START != null && p_DA_CHUNG_NGUA_NGAY_END != null)
			{
			    lstFilter.Add(new SolrQuery("DA_CHUNG_NGUA_NGAY:[" + Utility.GetJSONZFromUserDateTime(p_DA_CHUNG_NGUA_NGAY_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_DA_CHUNG_NGUA_NGAY_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_LAN_TIEM_CHUNG))
			{
			    lstFilter.Add(new SolrQuery("LAN_TIEM_CHUNG:" + p_LAN_TIEM_CHUNG));
			}
			if (!string.IsNullOrEmpty(p_LOAI_TIEM_CHUNG))
			{
			    lstFilter.Add(new SolrQuery("LOAI_TIEM_CHUNG:" + p_LOAI_TIEM_CHUNG));
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
			if (!string.IsNullOrEmpty(p_MA_LOAI_TIEM_CHUNG))
			{
			    lstFilter.Add(new SolrQuery("MA_LOAI_TIEM_CHUNG:" + p_MA_LOAI_TIEM_CHUNG));
			}
			if (!string.IsNullOrEmpty(p_MA_VAC_XIN))
			{
			    lstFilter.Add(new SolrQuery("MA_VAC_XIN:" + p_MA_VAC_XIN));
			}
			if (!string.IsNullOrEmpty(p_PHAN_UNG))
			{
			    lstFilter.Add(new SolrQuery("PHAN_UNG:" + p_PHAN_UNG));
			}
			if (!string.IsNullOrEmpty(p_THANG_THAI))
			{
			    lstFilter.Add(new SolrQuery("THANG_THAI:" + p_THANG_THAI));
			}
			if (!string.IsNullOrEmpty(p_VAC_XIN))
			{
			    lstFilter.Add(new SolrQuery("VAC_XIN:" + p_VAC_XIN));
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
            string[] fieldSelect = { "ID","CODE","DA_CHUNG_NGUA_NGAY","LAN_TIEM_CHUNG","LOAI_TIEM_CHUNG","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_LOAI_TIEM_CHUNG","MA_VAC_XIN","PHAN_UNG","THANG_THAI","VAC_XIN","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_TIEM_CHUNG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_TIEM_CHUNG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_TIEM_CHUNG> listAddRange = new List<EHR_TIEM_CHUNG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

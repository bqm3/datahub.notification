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
    public class EHR_KQ_KCB_CAN_LAM_SANGDao : OracleBaseImpl<EHR_KQ_KCB_CAN_LAM_SANG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_KQ_KCB_CAN_LAM_SANG";
            PackageName = "PK_EHR_KQ_KCB_CAN_LAM_SANG";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_KQ_KCB_CAN_LAM_SANG_Information info)
        {        
			var paramValue = new EHR_KQ_KCB_CAN_LAM_SANG
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                GIA_TRI = (info != null && !string.IsNullOrEmpty(info.GIA_TRI)) ? info.GIA_TRI : null,
                KET_LUAN = (info != null && !string.IsNullOrEmpty(info.KET_LUAN)) ? info.KET_LUAN : null,
                MAU_XET_NGHIEM = (info != null && !string.IsNullOrEmpty(info.MAU_XET_NGHIEM)) ? info.MAU_XET_NGHIEM : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CHI_SO = (info != null && !string.IsNullOrEmpty(info.MA_CHI_SO)) ? info.MA_CHI_SO : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_KQ_CAN_LAM_SANG = (info != null && !string.IsNullOrEmpty(info.MA_KQ_CAN_LAM_SANG)) ? info.MA_KQ_CAN_LAM_SANG : null,
                MA_KQ_CAN_LAM_SANG_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_KQ_CAN_LAM_SANG_DON_VI)) ? info.MA_KQ_CAN_LAM_SANG_DON_VI : null,
                MA_KQ_KCB = (info != null && !string.IsNullOrEmpty(info.MA_KQ_KCB)) ? info.MA_KQ_KCB : null,
                MA_MAY = (info != null && !string.IsNullOrEmpty(info.MA_MAY)) ? info.MA_MAY : null,
                MO_TA = (info != null && !string.IsNullOrEmpty(info.MO_TA)) ? info.MO_TA : null,
                NGAY_KQ = (info != null && !string.IsNullOrEmpty(info.NGAY_KQ)) ? DateTime.SpecifyKind(DateTime.ParseExact(info.NGAY_KQ, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                NHOM_DICH_VU = (info != null && !string.IsNullOrEmpty(info.NHOM_DICH_VU)) ? info.NHOM_DICH_VU : null,
                TEN_CHI_SO = (info != null && !string.IsNullOrEmpty(info.TEN_CHI_SO)) ? info.TEN_CHI_SO : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_KQ_KCB_CAN_LAM_SANG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_GIA_TRI",(dictParam != null && dictParam.ContainsKey("GIA_TRI")) ? dictParam["GIA_TRI"] : null),
                new OracleParameter("p_KET_LUAN",(dictParam != null && dictParam.ContainsKey("KET_LUAN")) ? dictParam["KET_LUAN"] : null),
                new OracleParameter("p_MAU_XET_NGHIEM",(dictParam != null && dictParam.ContainsKey("MAU_XET_NGHIEM")) ? dictParam["MAU_XET_NGHIEM"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CHI_SO",(dictParam != null && dictParam.ContainsKey("MA_CHI_SO")) ? dictParam["MA_CHI_SO"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_CAN_LAM_SANG",(dictParam != null && dictParam.ContainsKey("MA_KQ_CAN_LAM_SANG")) ? dictParam["MA_KQ_CAN_LAM_SANG"] : null),
                new OracleParameter("p_MA_KQ_CAN_LAM_SANG_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_KQ_CAN_LAM_SANG_DON_VI")) ? dictParam["MA_KQ_CAN_LAM_SANG_DON_VI"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_MA_MAY",(dictParam != null && dictParam.ContainsKey("MA_MAY")) ? dictParam["MA_MAY"] : null),
                new OracleParameter("p_MO_TA",(dictParam != null && dictParam.ContainsKey("MO_TA")) ? dictParam["MO_TA"] : null),
                new OracleParameter("p_NGAY_KQ_START", (dictParam != null && dictParam.ContainsKey("NGAY_KQ_START")) ? dictParam["NGAY_KQ_START"] : null),
                new OracleParameter("p_NGAY_KQ_END", (dictParam != null && dictParam.ContainsKey("NGAY_KQ_END")) ? dictParam["NGAY_KQ_END"] : null),
                new OracleParameter("p_NHOM_DICH_VU",(dictParam != null && dictParam.ContainsKey("NHOM_DICH_VU")) ? dictParam["NHOM_DICH_VU"] : null),
                new OracleParameter("p_TEN_CHI_SO",(dictParam != null && dictParam.ContainsKey("TEN_CHI_SO")) ? dictParam["TEN_CHI_SO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_KQ_KCB_CAN_LAM_SANG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_GIA_TRI",(dictParam != null && dictParam.ContainsKey("GIA_TRI")) ? dictParam["GIA_TRI"] : null),
                new OracleParameter("p_KET_LUAN",(dictParam != null && dictParam.ContainsKey("KET_LUAN")) ? dictParam["KET_LUAN"] : null),
                new OracleParameter("p_MAU_XET_NGHIEM",(dictParam != null && dictParam.ContainsKey("MAU_XET_NGHIEM")) ? dictParam["MAU_XET_NGHIEM"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CHI_SO",(dictParam != null && dictParam.ContainsKey("MA_CHI_SO")) ? dictParam["MA_CHI_SO"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_KQ_CAN_LAM_SANG",(dictParam != null && dictParam.ContainsKey("MA_KQ_CAN_LAM_SANG")) ? dictParam["MA_KQ_CAN_LAM_SANG"] : null),
                new OracleParameter("p_MA_KQ_CAN_LAM_SANG_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_KQ_CAN_LAM_SANG_DON_VI")) ? dictParam["MA_KQ_CAN_LAM_SANG_DON_VI"] : null),
                new OracleParameter("p_MA_KQ_KCB",(dictParam != null && dictParam.ContainsKey("MA_KQ_KCB")) ? dictParam["MA_KQ_KCB"] : null),
                new OracleParameter("p_MA_MAY",(dictParam != null && dictParam.ContainsKey("MA_MAY")) ? dictParam["MA_MAY"] : null),
                new OracleParameter("p_MO_TA",(dictParam != null && dictParam.ContainsKey("MO_TA")) ? dictParam["MO_TA"] : null),
                new OracleParameter("p_NGAY_KQ_START", (dictParam != null && dictParam.ContainsKey("NGAY_KQ_START")) ? dictParam["NGAY_KQ_START"] : null),
                new OracleParameter("p_NGAY_KQ_END", (dictParam != null && dictParam.ContainsKey("NGAY_KQ_END")) ? dictParam["NGAY_KQ_END"] : null),
                new OracleParameter("p_NHOM_DICH_VU",(dictParam != null && dictParam.ContainsKey("NHOM_DICH_VU")) ? dictParam["NHOM_DICH_VU"] : null),
                new OracleParameter("p_TEN_CHI_SO",(dictParam != null && dictParam.ContainsKey("TEN_CHI_SO")) ? dictParam["TEN_CHI_SO"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_KQ_KCB_CAN_LAM_SANG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_KQ_KCB_CAN_LAM_SANG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","GIA_TRI","KET_LUAN","MAU_XET_NGHIEM","MA_BENH_NHAN","MA_CHI_SO","MA_CONG_DAN","MA_HSSK","MA_KQ_CAN_LAM_SANG","MA_KQ_CAN_LAM_SANG_DON_VI","MA_KQ_KCB","MA_MAY","MO_TA","NGAY_KQ","NHOM_DICH_VU","TEN_CHI_SO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_KQ_KCB_CAN_LAM_SANG> results = QuerySolrBase<EHR_KQ_KCB_CAN_LAM_SANG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_CAN_LAM_SANG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_KQ_KCB_CAN_LAM_SANG> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_GIA_TRI,			string p_KET_LUAN,			string p_MAU_XET_NGHIEM,			string p_MA_BENH_NHAN,			string p_MA_CHI_SO,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_KQ_CAN_LAM_SANG,			string p_MA_KQ_CAN_LAM_SANG_DON_VI,			string p_MA_KQ_KCB,			string p_MA_MAY,			string p_MO_TA,			DateTime? p_NGAY_KQ_START,			DateTime? p_NGAY_KQ_END,			string p_NHOM_DICH_VU,			string p_TEN_CHI_SO,			string p_VERSION_XML,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_GIA_TRI))
			{
			    lstFilter.Add(new SolrQuery("GIA_TRI:" + p_GIA_TRI));
			}
			if (!string.IsNullOrEmpty(p_KET_LUAN))
			{
			    lstFilter.Add(new SolrQuery("KET_LUAN:" + p_KET_LUAN));
			}
			if (!string.IsNullOrEmpty(p_MAU_XET_NGHIEM))
			{
			    lstFilter.Add(new SolrQuery("MAU_XET_NGHIEM:" + p_MAU_XET_NGHIEM));
			}
			if (!string.IsNullOrEmpty(p_MA_BENH_NHAN))
			{
			    lstFilter.Add(new SolrQuery("MA_BENH_NHAN:" + p_MA_BENH_NHAN));
			}
			if (!string.IsNullOrEmpty(p_MA_CHI_SO))
			{
			    lstFilter.Add(new SolrQuery("MA_CHI_SO:" + p_MA_CHI_SO));
			}
			if (!string.IsNullOrEmpty(p_MA_CONG_DAN))
			{
			    lstFilter.Add(new SolrQuery("MA_CONG_DAN:" + p_MA_CONG_DAN));
			}
			if (!string.IsNullOrEmpty(p_MA_HSSK))
			{
			    lstFilter.Add(new SolrQuery("MA_HSSK:" + p_MA_HSSK));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_CAN_LAM_SANG))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_CAN_LAM_SANG:" + p_MA_KQ_CAN_LAM_SANG));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_CAN_LAM_SANG_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_CAN_LAM_SANG_DON_VI:" + p_MA_KQ_CAN_LAM_SANG_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_KQ_KCB))
			{
			    lstFilter.Add(new SolrQuery("MA_KQ_KCB:" + p_MA_KQ_KCB));
			}
			if (!string.IsNullOrEmpty(p_MA_MAY))
			{
			    lstFilter.Add(new SolrQuery("MA_MAY:" + p_MA_MAY));
			}
			if (!string.IsNullOrEmpty(p_MO_TA))
			{
			    lstFilter.Add(new SolrQuery("MO_TA:" + p_MO_TA));
			}
			if (p_NGAY_KQ_START != null && p_NGAY_KQ_END != null)
			{
			    lstFilter.Add(new SolrQuery("NGAY_KQ:[" + Utility.GetJSONZFromUserDateTime(p_NGAY_KQ_START.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_NGAY_KQ_END.Value) + "]"));
			}
			if (!string.IsNullOrEmpty(p_NHOM_DICH_VU))
			{
			    lstFilter.Add(new SolrQuery("NHOM_DICH_VU:" + p_NHOM_DICH_VU));
			}
			if (!string.IsNullOrEmpty(p_TEN_CHI_SO))
			{
			    lstFilter.Add(new SolrQuery("TEN_CHI_SO:" + p_TEN_CHI_SO));
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
            string[] fieldSelect = { "ID","CODE","GIA_TRI","KET_LUAN","MAU_XET_NGHIEM","MA_BENH_NHAN","MA_CHI_SO","MA_CONG_DAN","MA_HSSK","MA_KQ_CAN_LAM_SANG","MA_KQ_CAN_LAM_SANG_DON_VI","MA_KQ_KCB","MA_MAY","MO_TA","NGAY_KQ","NHOM_DICH_VU","TEN_CHI_SO","VERSION_XML","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_KQ_KCB_CAN_LAM_SANG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_KQ_KCB_CAN_LAM_SANG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_KQ_KCB_CAN_LAM_SANG> listAddRange = new List<EHR_KQ_KCB_CAN_LAM_SANG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

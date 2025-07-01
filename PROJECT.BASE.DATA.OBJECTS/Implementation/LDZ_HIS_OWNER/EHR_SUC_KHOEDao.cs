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
    public class EHR_SUC_KHOEDao : OracleBaseImpl<EHR_SUC_KHOE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "EHR_SUC_KHOE";
            PackageName = "PK_EHR_SUC_KHOE";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(EHR_SUC_KHOE_Information info)
        {        
			var paramValue = new EHR_SUC_KHOE
			{
				CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                BI_NGAT_LUC_DE = (info != null && !string.IsNullOrEmpty(info.BI_NGAT_LUC_DE)) ? info.BI_NGAT_LUC_DE : null,
                CAN_NANG_KHI_DE = (info != null && info.CAN_NANG_KHI_DE != null) ? info.CAN_NANG_KHI_DE.Value : (decimal?)null,
                CHIEU_DAI_KHI_DE = (info != null && info.CHIEU_DAI_KHI_DE != null) ? info.CHIEU_DAI_KHI_DE.Value : (decimal?)null,
                DE_MO = (info != null && !string.IsNullOrEmpty(info.DE_MO)) ? info.DE_MO : null,
                DE_THIEU_THANG = (info != null && !string.IsNullOrEmpty(info.DE_THIEU_THANG)) ? info.DE_THIEU_THANG : null,
                DE_THUONG = (info != null && !string.IsNullOrEmpty(info.DE_THUONG)) ? info.DE_THUONG : null,
                DI_TAT_BAM_SINH = (info != null && !string.IsNullOrEmpty(info.DI_TAT_BAM_SINH)) ? info.DI_TAT_BAM_SINH : null,
                HOAT_DONG_THE_LUC_CO_KHONG = (info != null && !string.IsNullOrEmpty(info.HOAT_DONG_THE_LUC_CO_KHONG)) ? info.HOAT_DONG_THE_LUC_CO_KHONG : null,
                HOAT_DONG_THE_LUC_THUONG_XUYEN = (info != null && !string.IsNullOrEmpty(info.HOAT_DONG_THE_LUC_THUONG_XUYEN)) ? info.HOAT_DONG_THE_LUC_THUONG_XUYEN : null,
                HUT_THUOC_CO_KHONG = (info != null && !string.IsNullOrEmpty(info.HUT_THUOC_CO_KHONG)) ? info.HUT_THUOC_CO_KHONG : null,
                HUT_THUOC_DA_BO = (info != null && !string.IsNullOrEmpty(info.HUT_THUOC_DA_BO)) ? info.HUT_THUOC_DA_BO : null,
                HUT_THUOC_THUONG_XUYEN = (info != null && !string.IsNullOrEmpty(info.HUT_THUOC_THUONG_XUYEN)) ? info.HUT_THUOC_THUONG_XUYEN : null,
                LOAI_TOILET = (info != null && !string.IsNullOrEmpty(info.LOAI_TOILET)) ? info.LOAI_TOILET : null,
                MA_BENH_NHAN = (info != null && !string.IsNullOrEmpty(info.MA_BENH_NHAN)) ? info.MA_BENH_NHAN : null,
                MA_CONG_DAN = (info != null && !string.IsNullOrEmpty(info.MA_CONG_DAN)) ? info.MA_CONG_DAN : null,
                MA_HSSK = (info != null && !string.IsNullOrEmpty(info.MA_HSSK)) ? info.MA_HSSK : null,
                MA_SUC_KHOE = (info != null && !string.IsNullOrEmpty(info.MA_SUC_KHOE)) ? info.MA_SUC_KHOE : null,
                MA_SUC_KHOE_DON_VI = (info != null && !string.IsNullOrEmpty(info.MA_SUC_KHOE_DON_VI)) ? info.MA_SUC_KHOE_DON_VI : null,
                MA_TUY_CO_KHONG = (info != null && !string.IsNullOrEmpty(info.MA_TUY_CO_KHONG)) ? info.MA_TUY_CO_KHONG : null,
                MA_TUY_DA_BO = (info != null && !string.IsNullOrEmpty(info.MA_TUY_DA_BO)) ? info.MA_TUY_DA_BO : null,
                MA_TUY_THUONG_XUYEN = (info != null && !string.IsNullOrEmpty(info.MA_TUY_THUONG_XUYEN)) ? info.MA_TUY_THUONG_XUYEN : null,
                NGUY_CO_KHAC = (info != null && !string.IsNullOrEmpty(info.NGUY_CO_KHAC)) ? info.NGUY_CO_KHAC : null,
                RUOU_BIA_CO_KHONG = (info != null && !string.IsNullOrEmpty(info.RUOU_BIA_CO_KHONG)) ? info.RUOU_BIA_CO_KHONG : null,
                RUOU_BIA_DA_BO = (info != null && !string.IsNullOrEmpty(info.RUOU_BIA_DA_BO)) ? info.RUOU_BIA_DA_BO : null,
                RUOU_BIA_SO_LY_COC_MOI_NGAY = (info != null && info.RUOU_BIA_SO_LY_COC_MOI_NGAY != null) ? info.RUOU_BIA_SO_LY_COC_MOI_NGAY.Value : (decimal?)null,
                VAN_DE_KHAC = (info != null && !string.IsNullOrEmpty(info.VAN_DE_KHAC)) ? info.VAN_DE_KHAC : null,
                VERSION_XML = (info != null && !string.IsNullOrEmpty(info.VERSION_XML)) ? info.VERSION_XML : null,
                YEU_TO_NGHE_NGHIEP_MOI_TRUONG = (info != null && !string.IsNullOrEmpty(info.YEU_TO_NGHE_NGHIEP_MOI_TRUONG)) ? info.YEU_TO_NGHE_NGHIEP_MOI_TRUONG : null,
				IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,				
				CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
			};
			var result = this.Add(paramValue);
            return result;
        }
		public List<EHR_SUC_KHOE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BI_NGAT_LUC_DE",(dictParam != null && dictParam.ContainsKey("BI_NGAT_LUC_DE")) ? dictParam["BI_NGAT_LUC_DE"] : null),
                new OracleParameter("p_CAN_NANG_KHI_DE",(dictParam != null && dictParam.ContainsKey("CAN_NANG_KHI_DE")) ? dictParam["CAN_NANG_KHI_DE"] : null),
                new OracleParameter("p_CHIEU_DAI_KHI_DE",(dictParam != null && dictParam.ContainsKey("CHIEU_DAI_KHI_DE")) ? dictParam["CHIEU_DAI_KHI_DE"] : null),
                new OracleParameter("p_DE_MO",(dictParam != null && dictParam.ContainsKey("DE_MO")) ? dictParam["DE_MO"] : null),
                new OracleParameter("p_DE_THIEU_THANG",(dictParam != null && dictParam.ContainsKey("DE_THIEU_THANG")) ? dictParam["DE_THIEU_THANG"] : null),
                new OracleParameter("p_DE_THUONG",(dictParam != null && dictParam.ContainsKey("DE_THUONG")) ? dictParam["DE_THUONG"] : null),
                new OracleParameter("p_DI_TAT_BAM_SINH",(dictParam != null && dictParam.ContainsKey("DI_TAT_BAM_SINH")) ? dictParam["DI_TAT_BAM_SINH"] : null),
                new OracleParameter("p_HOAT_DONG_THE_LUC_CO_KHONG",(dictParam != null && dictParam.ContainsKey("HOAT_DONG_THE_LUC_CO_KHONG")) ? dictParam["HOAT_DONG_THE_LUC_CO_KHONG"] : null),
                new OracleParameter("p_HOAT_DONG_THE_LUC_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("HOAT_DONG_THE_LUC_THUONG_XUYEN")) ? dictParam["HOAT_DONG_THE_LUC_THUONG_XUYEN"] : null),
                new OracleParameter("p_HUT_THUOC_CO_KHONG",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_CO_KHONG")) ? dictParam["HUT_THUOC_CO_KHONG"] : null),
                new OracleParameter("p_HUT_THUOC_DA_BO",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_DA_BO")) ? dictParam["HUT_THUOC_DA_BO"] : null),
                new OracleParameter("p_HUT_THUOC_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_THUONG_XUYEN")) ? dictParam["HUT_THUOC_THUONG_XUYEN"] : null),
                new OracleParameter("p_LOAI_TOILET",(dictParam != null && dictParam.ContainsKey("LOAI_TOILET")) ? dictParam["LOAI_TOILET"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MA_SUC_KHOE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE_DON_VI")) ? dictParam["MA_SUC_KHOE_DON_VI"] : null),
                new OracleParameter("p_MA_TUY_CO_KHONG",(dictParam != null && dictParam.ContainsKey("MA_TUY_CO_KHONG")) ? dictParam["MA_TUY_CO_KHONG"] : null),
                new OracleParameter("p_MA_TUY_DA_BO",(dictParam != null && dictParam.ContainsKey("MA_TUY_DA_BO")) ? dictParam["MA_TUY_DA_BO"] : null),
                new OracleParameter("p_MA_TUY_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("MA_TUY_THUONG_XUYEN")) ? dictParam["MA_TUY_THUONG_XUYEN"] : null),
                new OracleParameter("p_NGUY_CO_KHAC",(dictParam != null && dictParam.ContainsKey("NGUY_CO_KHAC")) ? dictParam["NGUY_CO_KHAC"] : null),
                new OracleParameter("p_RUOU_BIA_CO_KHONG",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_CO_KHONG")) ? dictParam["RUOU_BIA_CO_KHONG"] : null),
                new OracleParameter("p_RUOU_BIA_DA_BO",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_DA_BO")) ? dictParam["RUOU_BIA_DA_BO"] : null),
                new OracleParameter("p_RUOU_BIA_SO_LY_COC_MOI_NGAY",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_SO_LY_COC_MOI_NGAY")) ? dictParam["RUOU_BIA_SO_LY_COC_MOI_NGAY"] : null),
                new OracleParameter("p_VAN_DE_KHAC",(dictParam != null && dictParam.ContainsKey("VAN_DE_KHAC")) ? dictParam["VAN_DE_KHAC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_YEU_TO_NGHE_NGHIEP_MOI_TRUONG",(dictParam != null && dictParam.ContainsKey("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")) ? dictParam["YEU_TO_NGHE_NGHIEP_MOI_TRUONG"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<EHR_SUC_KHOE> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END ,int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_BI_NGAT_LUC_DE",(dictParam != null && dictParam.ContainsKey("BI_NGAT_LUC_DE")) ? dictParam["BI_NGAT_LUC_DE"] : null),
                new OracleParameter("p_CAN_NANG_KHI_DE",(dictParam != null && dictParam.ContainsKey("CAN_NANG_KHI_DE")) ? dictParam["CAN_NANG_KHI_DE"] : null),
                new OracleParameter("p_CHIEU_DAI_KHI_DE",(dictParam != null && dictParam.ContainsKey("CHIEU_DAI_KHI_DE")) ? dictParam["CHIEU_DAI_KHI_DE"] : null),
                new OracleParameter("p_DE_MO",(dictParam != null && dictParam.ContainsKey("DE_MO")) ? dictParam["DE_MO"] : null),
                new OracleParameter("p_DE_THIEU_THANG",(dictParam != null && dictParam.ContainsKey("DE_THIEU_THANG")) ? dictParam["DE_THIEU_THANG"] : null),
                new OracleParameter("p_DE_THUONG",(dictParam != null && dictParam.ContainsKey("DE_THUONG")) ? dictParam["DE_THUONG"] : null),
                new OracleParameter("p_DI_TAT_BAM_SINH",(dictParam != null && dictParam.ContainsKey("DI_TAT_BAM_SINH")) ? dictParam["DI_TAT_BAM_SINH"] : null),
                new OracleParameter("p_HOAT_DONG_THE_LUC_CO_KHONG",(dictParam != null && dictParam.ContainsKey("HOAT_DONG_THE_LUC_CO_KHONG")) ? dictParam["HOAT_DONG_THE_LUC_CO_KHONG"] : null),
                new OracleParameter("p_HOAT_DONG_THE_LUC_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("HOAT_DONG_THE_LUC_THUONG_XUYEN")) ? dictParam["HOAT_DONG_THE_LUC_THUONG_XUYEN"] : null),
                new OracleParameter("p_HUT_THUOC_CO_KHONG",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_CO_KHONG")) ? dictParam["HUT_THUOC_CO_KHONG"] : null),
                new OracleParameter("p_HUT_THUOC_DA_BO",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_DA_BO")) ? dictParam["HUT_THUOC_DA_BO"] : null),
                new OracleParameter("p_HUT_THUOC_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("HUT_THUOC_THUONG_XUYEN")) ? dictParam["HUT_THUOC_THUONG_XUYEN"] : null),
                new OracleParameter("p_LOAI_TOILET",(dictParam != null && dictParam.ContainsKey("LOAI_TOILET")) ? dictParam["LOAI_TOILET"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MA_SUC_KHOE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE_DON_VI")) ? dictParam["MA_SUC_KHOE_DON_VI"] : null),
                new OracleParameter("p_MA_TUY_CO_KHONG",(dictParam != null && dictParam.ContainsKey("MA_TUY_CO_KHONG")) ? dictParam["MA_TUY_CO_KHONG"] : null),
                new OracleParameter("p_MA_TUY_DA_BO",(dictParam != null && dictParam.ContainsKey("MA_TUY_DA_BO")) ? dictParam["MA_TUY_DA_BO"] : null),
                new OracleParameter("p_MA_TUY_THUONG_XUYEN",(dictParam != null && dictParam.ContainsKey("MA_TUY_THUONG_XUYEN")) ? dictParam["MA_TUY_THUONG_XUYEN"] : null),
                new OracleParameter("p_NGUY_CO_KHAC",(dictParam != null && dictParam.ContainsKey("NGUY_CO_KHAC")) ? dictParam["NGUY_CO_KHAC"] : null),
                new OracleParameter("p_RUOU_BIA_CO_KHONG",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_CO_KHONG")) ? dictParam["RUOU_BIA_CO_KHONG"] : null),
                new OracleParameter("p_RUOU_BIA_DA_BO",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_DA_BO")) ? dictParam["RUOU_BIA_DA_BO"] : null),
                new OracleParameter("p_RUOU_BIA_SO_LY_COC_MOI_NGAY",(dictParam != null && dictParam.ContainsKey("RUOU_BIA_SO_LY_COC_MOI_NGAY")) ? dictParam["RUOU_BIA_SO_LY_COC_MOI_NGAY"] : null),
                new OracleParameter("p_VAN_DE_KHAC",(dictParam != null && dictParam.ContainsKey("VAN_DE_KHAC")) ? dictParam["VAN_DE_KHAC"] : null),
                new OracleParameter("p_VERSION_XML",(dictParam != null && dictParam.ContainsKey("VERSION_XML")) ? dictParam["VERSION_XML"] : null),
                new OracleParameter("p_YEU_TO_NGHE_NGHIEP_MOI_TRUONG",(dictParam != null && dictParam.ContainsKey("YEU_TO_NGHE_NGHIEP_MOI_TRUONG")) ? dictParam["YEU_TO_NGHE_NGHIEP_MOI_TRUONG"] : null),    
				new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<EHR_SUC_KHOE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public EHR_SUC_KHOE QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID","CODE","BI_NGAT_LUC_DE","CAN_NANG_KHI_DE","CHIEU_DAI_KHI_DE","DE_MO","DE_THIEU_THANG","DE_THUONG","DI_TAT_BAM_SINH","HOAT_DONG_THE_LUC_CO_KHONG","HOAT_DONG_THE_LUC_THUONG_XUYEN","HUT_THUOC_CO_KHONG","HUT_THUOC_DA_BO","HUT_THUOC_THUONG_XUYEN","LOAI_TOILET","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SUC_KHOE","MA_SUC_KHOE_DON_VI","MA_TUY_CO_KHONG","MA_TUY_DA_BO","MA_TUY_THUONG_XUYEN","NGUY_CO_KHAC","RUOU_BIA_CO_KHONG","RUOU_BIA_DA_BO","RUOU_BIA_SO_LY_COC_MOI_NGAY","VAN_DE_KHAC","VERSION_XML","YEU_TO_NGHE_NGHIEP_MOI_TRUONG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            int totalRecord = 0;
            List<EHR_SUC_KHOE> results = QuerySolrBase<EHR_SUC_KHOE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_SUC_KHOE/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<EHR_SUC_KHOE> QuerySolr_GetListPaging(string keyword,
			string p_CODE,
			string p_BI_NGAT_LUC_DE,			string p_CAN_NANG_KHI_DE,			string p_CHIEU_DAI_KHI_DE,			string p_DE_MO,			string p_DE_THIEU_THANG,			string p_DE_THUONG,			string p_DI_TAT_BAM_SINH,			string p_HOAT_DONG_THE_LUC_CO_KHONG,			string p_HOAT_DONG_THE_LUC_THUONG_XUYEN,			string p_HUT_THUOC_CO_KHONG,			string p_HUT_THUOC_DA_BO,			string p_HUT_THUOC_THUONG_XUYEN,			string p_LOAI_TOILET,			string p_MA_BENH_NHAN,			string p_MA_CONG_DAN,			string p_MA_HSSK,			string p_MA_SUC_KHOE,			string p_MA_SUC_KHOE_DON_VI,			string p_MA_TUY_CO_KHONG,			string p_MA_TUY_DA_BO,			string p_MA_TUY_THUONG_XUYEN,			string p_NGUY_CO_KHAC,			string p_RUOU_BIA_CO_KHONG,			string p_RUOU_BIA_DA_BO,			string p_RUOU_BIA_SO_LY_COC_MOI_NGAY,			string p_VAN_DE_KHAC,			string p_VERSION_XML,			string p_YEU_TO_NGHE_NGHIEP_MOI_TRUONG,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();      
			if (!string.IsNullOrEmpty(p_CODE))
			{
				lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
			}			

			if (!string.IsNullOrEmpty(p_BI_NGAT_LUC_DE))
			{
			    lstFilter.Add(new SolrQuery("BI_NGAT_LUC_DE:" + p_BI_NGAT_LUC_DE));
			}
			if (!string.IsNullOrEmpty(p_CAN_NANG_KHI_DE))
			{
			    lstFilter.Add(new SolrQuery("CAN_NANG_KHI_DE:" + p_CAN_NANG_KHI_DE));
			}
			if (!string.IsNullOrEmpty(p_CHIEU_DAI_KHI_DE))
			{
			    lstFilter.Add(new SolrQuery("CHIEU_DAI_KHI_DE:" + p_CHIEU_DAI_KHI_DE));
			}
			if (!string.IsNullOrEmpty(p_DE_MO))
			{
			    lstFilter.Add(new SolrQuery("DE_MO:" + p_DE_MO));
			}
			if (!string.IsNullOrEmpty(p_DE_THIEU_THANG))
			{
			    lstFilter.Add(new SolrQuery("DE_THIEU_THANG:" + p_DE_THIEU_THANG));
			}
			if (!string.IsNullOrEmpty(p_DE_THUONG))
			{
			    lstFilter.Add(new SolrQuery("DE_THUONG:" + p_DE_THUONG));
			}
			if (!string.IsNullOrEmpty(p_DI_TAT_BAM_SINH))
			{
			    lstFilter.Add(new SolrQuery("DI_TAT_BAM_SINH:" + p_DI_TAT_BAM_SINH));
			}
			if (!string.IsNullOrEmpty(p_HOAT_DONG_THE_LUC_CO_KHONG))
			{
			    lstFilter.Add(new SolrQuery("HOAT_DONG_THE_LUC_CO_KHONG:" + p_HOAT_DONG_THE_LUC_CO_KHONG));
			}
			if (!string.IsNullOrEmpty(p_HOAT_DONG_THE_LUC_THUONG_XUYEN))
			{
			    lstFilter.Add(new SolrQuery("HOAT_DONG_THE_LUC_THUONG_XUYEN:" + p_HOAT_DONG_THE_LUC_THUONG_XUYEN));
			}
			if (!string.IsNullOrEmpty(p_HUT_THUOC_CO_KHONG))
			{
			    lstFilter.Add(new SolrQuery("HUT_THUOC_CO_KHONG:" + p_HUT_THUOC_CO_KHONG));
			}
			if (!string.IsNullOrEmpty(p_HUT_THUOC_DA_BO))
			{
			    lstFilter.Add(new SolrQuery("HUT_THUOC_DA_BO:" + p_HUT_THUOC_DA_BO));
			}
			if (!string.IsNullOrEmpty(p_HUT_THUOC_THUONG_XUYEN))
			{
			    lstFilter.Add(new SolrQuery("HUT_THUOC_THUONG_XUYEN:" + p_HUT_THUOC_THUONG_XUYEN));
			}
			if (!string.IsNullOrEmpty(p_LOAI_TOILET))
			{
			    lstFilter.Add(new SolrQuery("LOAI_TOILET:" + p_LOAI_TOILET));
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
			if (!string.IsNullOrEmpty(p_MA_SUC_KHOE))
			{
			    lstFilter.Add(new SolrQuery("MA_SUC_KHOE:" + p_MA_SUC_KHOE));
			}
			if (!string.IsNullOrEmpty(p_MA_SUC_KHOE_DON_VI))
			{
			    lstFilter.Add(new SolrQuery("MA_SUC_KHOE_DON_VI:" + p_MA_SUC_KHOE_DON_VI));
			}
			if (!string.IsNullOrEmpty(p_MA_TUY_CO_KHONG))
			{
			    lstFilter.Add(new SolrQuery("MA_TUY_CO_KHONG:" + p_MA_TUY_CO_KHONG));
			}
			if (!string.IsNullOrEmpty(p_MA_TUY_DA_BO))
			{
			    lstFilter.Add(new SolrQuery("MA_TUY_DA_BO:" + p_MA_TUY_DA_BO));
			}
			if (!string.IsNullOrEmpty(p_MA_TUY_THUONG_XUYEN))
			{
			    lstFilter.Add(new SolrQuery("MA_TUY_THUONG_XUYEN:" + p_MA_TUY_THUONG_XUYEN));
			}
			if (!string.IsNullOrEmpty(p_NGUY_CO_KHAC))
			{
			    lstFilter.Add(new SolrQuery("NGUY_CO_KHAC:" + p_NGUY_CO_KHAC));
			}
			if (!string.IsNullOrEmpty(p_RUOU_BIA_CO_KHONG))
			{
			    lstFilter.Add(new SolrQuery("RUOU_BIA_CO_KHONG:" + p_RUOU_BIA_CO_KHONG));
			}
			if (!string.IsNullOrEmpty(p_RUOU_BIA_DA_BO))
			{
			    lstFilter.Add(new SolrQuery("RUOU_BIA_DA_BO:" + p_RUOU_BIA_DA_BO));
			}
			if (!string.IsNullOrEmpty(p_RUOU_BIA_SO_LY_COC_MOI_NGAY))
			{
			    lstFilter.Add(new SolrQuery("RUOU_BIA_SO_LY_COC_MOI_NGAY:" + p_RUOU_BIA_SO_LY_COC_MOI_NGAY));
			}
			if (!string.IsNullOrEmpty(p_VAN_DE_KHAC))
			{
			    lstFilter.Add(new SolrQuery("VAN_DE_KHAC:" + p_VAN_DE_KHAC));
			}
			if (!string.IsNullOrEmpty(p_VERSION_XML))
			{
			    lstFilter.Add(new SolrQuery("VERSION_XML:" + p_VERSION_XML));
			}
			if (!string.IsNullOrEmpty(p_YEU_TO_NGHE_NGHIEP_MOI_TRUONG))
			{
			    lstFilter.Add(new SolrQuery("YEU_TO_NGHE_NGHIEP_MOI_TRUONG:" + p_YEU_TO_NGHE_NGHIEP_MOI_TRUONG));
			}
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID","CODE","BI_NGAT_LUC_DE","CAN_NANG_KHI_DE","CHIEU_DAI_KHI_DE","DE_MO","DE_THIEU_THANG","DE_THUONG","DI_TAT_BAM_SINH","HOAT_DONG_THE_LUC_CO_KHONG","HOAT_DONG_THE_LUC_THUONG_XUYEN","HUT_THUOC_CO_KHONG","HUT_THUOC_DA_BO","HUT_THUOC_THUONG_XUYEN","LOAI_TOILET","MA_BENH_NHAN","MA_CONG_DAN","MA_HSSK","MA_SUC_KHOE","MA_SUC_KHOE_DON_VI","MA_TUY_CO_KHONG","MA_TUY_DA_BO","MA_TUY_THUONG_XUYEN","NGUY_CO_KHAC","RUOU_BIA_CO_KHONG","RUOU_BIA_DA_BO","RUOU_BIA_SO_LY_COC_MOI_NGAY","VAN_DE_KHAC","VERSION_XML","YEU_TO_NGHE_NGHIEP_MOI_TRUONG","IS_DELETE","CDATE","LDATE","CUSER","LUSER" };
            var results = QuerySolrBase<EHR_SUC_KHOE>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"EHR_SUC_KHOE/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<EHR_SUC_KHOE> listAddRange = new List<EHR_SUC_KHOE>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion

        


    }
}

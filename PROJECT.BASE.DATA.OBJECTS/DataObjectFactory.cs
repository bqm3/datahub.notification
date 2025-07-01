namespace PROJECT.BASE.DAO
{
    //------------------------------------------------------------------------------------------------------------------------
    //-- Created By: <<DAO_AUTHOR>>
    //-- Date: <<DAO_CREATE_DATE>>
    //-- Todo: 
    //------------------------------------------------------------------------------------------------------------------------ 
    public class DataObjectFactory
    {
        private static APPLICATIONDao _APPLICATIONDao;
        public static APPLICATIONDao GetInstanceAPPLICATION()
        {
            return _APPLICATIONDao ?? (_APPLICATIONDao = new APPLICATIONDao());
        }

        private static DYNAMICDao _DYNAMICDao;
        public static DYNAMICDao GetInstanceDYNAMIC()
        {
            return _DYNAMICDao ?? (_DYNAMICDao = new DYNAMICDao());
        }
        public static DYNAMICDao GetInstanceDYNAMIC(string storage)
        {
            return new DYNAMICDao(storage);
        }
        private static BaseMongoDao _BaseMongoDao;
        public static BaseMongoDao GetInstanceBaseMongo()
        {
            return _BaseMongoDao ?? (_BaseMongoDao = new BaseMongoDao());
        }
        public static BaseMongoDao GetInstanceBaseMongo(string storage)
        {
            return new BaseMongoDao(storage);
        }
        private static CONFIGURATION_FIELDDao _CONFIGURATION_FIELDDao;
        public static CONFIGURATION_FIELDDao GetInstanceCONFIGURATION_FIELD()
        {
            return _CONFIGURATION_FIELDDao ?? (_CONFIGURATION_FIELDDao = new CONFIGURATION_FIELDDao());
        }
        #region NOTIFICATION
        private static NOTIFICATIONDao _NOTIFICATIONDao;
        public static NOTIFICATIONDao GetInstanceNOTIFICATION()
        {
            return _NOTIFICATIONDao ?? (_NOTIFICATIONDao = new NOTIFICATIONDao());
        }
        private static CONFIG_FIRE_BASEDao _CONFIG_FIRE_BASEDao;
        public static CONFIG_FIRE_BASEDao GetInstanceCONFIG_FIRE_BASE()
        {
            return _CONFIG_FIRE_BASEDao ?? (_CONFIG_FIRE_BASEDao = new CONFIG_FIRE_BASEDao());
        }
        private static SEND_FIRE_BASEDao _SEND_FIRE_BASEDao;
        public static SEND_FIRE_BASEDao GetInstanceSEND_FIRE_BASE()
        {
            return _SEND_FIRE_BASEDao ?? (_SEND_FIRE_BASEDao = new SEND_FIRE_BASEDao());
        }
        private static SEND_MAILDao _SEND_MAILDao;
        public static SEND_MAILDao GetInstanceSEND_MAIL()
        {
            return _SEND_MAILDao ?? (_SEND_MAILDao = new SEND_MAILDao());
        }
        private static SEND_SMSDao _SEND_SMSDao;
        public static SEND_SMSDao GetInstanceSEND_SMS()
        {
            return _SEND_SMSDao ?? (_SEND_SMSDao = new SEND_SMSDao());
        }
        #endregion
        #region AMS
        private static AMS_ACTIONSDao _AMS_ACTIONSDao;
        public static AMS_ACTIONSDao GetInstanceAMS_ACTIONS()
        {
            return _AMS_ACTIONSDao ?? (_AMS_ACTIONSDao = new AMS_ACTIONSDao());
        }
        private static AMS_FUNCTIONSDao _AMS_FUNCTIONSDao;
        public static AMS_FUNCTIONSDao GetInstanceAMS_FUNCTIONS()
        {
            return _AMS_FUNCTIONSDao ?? (_AMS_FUNCTIONSDao = new AMS_FUNCTIONSDao());
        }
        #endregion
        #region LOGS
        private static LOG_LOGINDao _LOG_LOGINDao;
        public static LOG_LOGINDao GetInstanceLOG_LOGIN()
        {
            return _LOG_LOGINDao ?? (_LOG_LOGINDao = new LOG_LOGINDao());
        }
        private static LOG_VERSIONDao _LOG_VERSIONDao;
        public static LOG_VERSIONDao GetInstanceLOG_VERSION()
        {
            return _LOG_VERSIONDao ?? (_LOG_VERSIONDao = new LOG_VERSIONDao());
        }
        private static LOG_AUDITDao _LOG_AUDITDao;
        public static LOG_AUDITDao GetInstanceLOG_AUDIT()
        {
            return _LOG_AUDITDao ?? (_LOG_AUDITDao = new LOG_AUDITDao());
        }
        private static LOG_TRANSACTIONDao _LOG_TRANSACTIONDao;
        public static LOG_TRANSACTIONDao GetInstanceLOG_TRANSACTION()
        {
            return _LOG_TRANSACTIONDao ?? (_LOG_TRANSACTIONDao = new LOG_TRANSACTIONDao());
        }
        #endregion
        #region Integration      
        private static BaseObjectDao _BaseObjectDao;      
        public static BaseObjectDao GetInstanceBaseObject(string databaseName)
        {
            return new BaseObjectDao(databaseName);
        }
        public static BaseObjectDao GetInstanceBaseObject(string tableName, string databaseName)
        {
            return new BaseObjectDao(tableName, databaseName);
        }
        public static BaseObjectDao GetInstanceBaseObject(string tableName, string packageName, string databaseName)
        {
            return new BaseObjectDao(tableName, packageName, databaseName);
        }
        private static ITG_MSGIDao _ITG_MSGIDao;
        public static ITG_MSGIDao GetInstanceITG_MSGI()
        {
            return _ITG_MSGIDao ?? (_ITG_MSGIDao = new ITG_MSGIDao());
        }
        private static ITG_MSGODao _ITG_MSGODao;
        public static ITG_MSGODao GetInstanceITG_MSGO()
        {
            return _ITG_MSGODao ?? (_ITG_MSGODao = new ITG_MSGODao());
        }
        private static ITG_CONFIGDao _ITG_CONFIGDao;
        public static ITG_CONFIGDao GetInstanceITG_CONFIG()
        {
            return _ITG_CONFIGDao ?? (_ITG_CONFIGDao = new ITG_CONFIGDao());
        }
        private static ITG_CATEGORYDao _ITG_CATEGORYDao;
        public static ITG_CATEGORYDao GetInstanceITG_CATEGORY()
        {
            return _ITG_CATEGORYDao ?? (_ITG_CATEGORYDao = new ITG_CATEGORYDao());
        }
        private static ITG_MESSAGESDao _ITG_MESSAGESDao;
        public static ITG_MESSAGESDao GetInstanceITG_MESSAGES()
        {
            return _ITG_MESSAGESDao ?? (_ITG_MESSAGESDao = new ITG_MESSAGESDao());
        }

        #endregion
        #region LDZ_HIS_OWNER
        private static EHR_BAC_SIDao _EHR_BAC_SIDao;
        public static EHR_BAC_SIDao GetInstanceEHR_BAC_SI()
        {
            return _EHR_BAC_SIDao ?? (_EHR_BAC_SIDao = new EHR_BAC_SIDao());
        }
        private static EHR_BENH_NHANDao _EHR_BENH_NHANDao;
        public static EHR_BENH_NHANDao GetInstanceEHR_BENH_NHAN()
        {
            return _EHR_BENH_NHANDao ?? (_EHR_BENH_NHANDao = new EHR_BENH_NHANDao());
        }
        private static EHR_BENH_TATDao _EHR_BENH_TATDao;
        public static EHR_BENH_TATDao GetInstanceEHR_BENH_TAT()
        {
            return _EHR_BENH_TATDao ?? (_EHR_BENH_TATDao = new EHR_BENH_TATDao());
        }
        private static EHR_CONG_DANDao _EHR_CONG_DANDao;
        public static EHR_CONG_DANDao GetInstanceEHR_CONG_DAN()
        {
            return _EHR_CONG_DANDao ?? (_EHR_CONG_DANDao = new EHR_CONG_DANDao());
        }
        private static EHR_CO_SO_Y_TEDao _EHR_CO_SO_Y_TEDao;
        public static EHR_CO_SO_Y_TEDao GetInstanceEHR_CO_SO_Y_TE()
        {
            return _EHR_CO_SO_Y_TEDao ?? (_EHR_CO_SO_Y_TEDao = new EHR_CO_SO_Y_TEDao());
        }
        private static EHR_DI_UNGDao _EHR_DI_UNGDao;
        public static EHR_DI_UNGDao GetInstanceEHR_DI_UNG()
        {
            return _EHR_DI_UNGDao ?? (_EHR_DI_UNGDao = new EHR_DI_UNGDao());
        }
        private static EHR_DM_DBHCDao _EHR_DM_DBHCDao;
        public static EHR_DM_DBHCDao GetInstanceEHR_DM_DBHC()
        {
            return _EHR_DM_DBHCDao ?? (_EHR_DM_DBHCDao = new EHR_DM_DBHCDao());
        }
        private static EHR_DM_DUNG_CHUNGDao _EHR_DM_DUNG_CHUNGDao;
        public static EHR_DM_DUNG_CHUNGDao GetInstanceEHR_DM_DUNG_CHUNG()
        {
            return _EHR_DM_DUNG_CHUNGDao ?? (_EHR_DM_DUNG_CHUNGDao = new EHR_DM_DUNG_CHUNGDao());
        }
        private static EHR_DON_THUOCDao _EHR_DON_THUOCDao;
        public static EHR_DON_THUOCDao GetInstanceEHR_DON_THUOC()
        {
            return _EHR_DON_THUOCDao ?? (_EHR_DON_THUOCDao = new EHR_DON_THUOCDao());
        }
        private static EHR_DON_THUOC_CHI_TIETDao _EHR_DON_THUOC_CHI_TIETDao;
        public static EHR_DON_THUOC_CHI_TIETDao GetInstanceEHR_DON_THUOC_CHI_TIET()
        {
            return _EHR_DON_THUOC_CHI_TIETDao ?? (_EHR_DON_THUOC_CHI_TIETDao = new EHR_DON_THUOC_CHI_TIETDao());
        }
        private static EHR_HO_GIA_DINHDao _EHR_HO_GIA_DINHDao;
        public static EHR_HO_GIA_DINHDao GetInstanceEHR_HO_GIA_DINH()
        {
            return _EHR_HO_GIA_DINHDao ?? (_EHR_HO_GIA_DINHDao = new EHR_HO_GIA_DINHDao());
        }
        private static EHR_HSSKDao _EHR_HSSKDao;
        public static EHR_HSSKDao GetInstanceEHR_HSSK()
        {
            return _EHR_HSSKDao ?? (_EHR_HSSKDao = new EHR_HSSKDao());
        }
        private static EHR_KHUYET_TATDao _EHR_KHUYET_TATDao;
        public static EHR_KHUYET_TATDao GetInstanceEHR_KHUYET_TAT()
        {
            return _EHR_KHUYET_TATDao ?? (_EHR_KHUYET_TATDao = new EHR_KHUYET_TATDao());
        }
        private static EHR_KQ_KCBDao _EHR_KQ_KCBDao;
        public static EHR_KQ_KCBDao GetInstanceEHR_KQ_KCB()
        {
            return _EHR_KQ_KCBDao ?? (_EHR_KQ_KCBDao = new EHR_KQ_KCBDao());
        }
        private static EHR_KQ_KCB_CAN_LAM_SANGDao _EHR_KQ_KCB_CAN_LAM_SANGDao;
        public static EHR_KQ_KCB_CAN_LAM_SANGDao GetInstanceEHR_KQ_KCB_CAN_LAM_SANG()
        {
            return _EHR_KQ_KCB_CAN_LAM_SANGDao ?? (_EHR_KQ_KCB_CAN_LAM_SANGDao = new EHR_KQ_KCB_CAN_LAM_SANGDao());
        }
        private static EHR_KQ_KCB_CO_QUANDao _EHR_KQ_KCB_CO_QUANDao;
        public static EHR_KQ_KCB_CO_QUANDao GetInstanceEHR_KQ_KCB_CO_QUAN()
        {
            return _EHR_KQ_KCB_CO_QUANDao ?? (_EHR_KQ_KCB_CO_QUANDao = new EHR_KQ_KCB_CO_QUANDao());
        }
        private static EHR_KQ_KCB_KET_LUANDao _EHR_KQ_KCB_KET_LUANDao;
        public static EHR_KQ_KCB_KET_LUANDao GetInstanceEHR_KQ_KCB_KET_LUAN()
        {
            return _EHR_KQ_KCB_KET_LUANDao ?? (_EHR_KQ_KCB_KET_LUANDao = new EHR_KQ_KCB_KET_LUANDao());
        }
        private static EHR_KQ_KCB_SINH_TON_VA_STHDao _EHR_KQ_KCB_SINH_TON_VA_STHDao;
        public static EHR_KQ_KCB_SINH_TON_VA_STHDao GetInstanceEHR_KQ_KCB_SINH_TON_VA_STH()
        {
            return _EHR_KQ_KCB_SINH_TON_VA_STHDao ?? (_EHR_KQ_KCB_SINH_TON_VA_STHDao = new EHR_KQ_KCB_SINH_TON_VA_STHDao());
        }
        private static EHR_KQ_KCB_THI_LUCDao _EHR_KQ_KCB_THI_LUCDao;
        public static EHR_KQ_KCB_THI_LUCDao GetInstanceEHR_KQ_KCB_THI_LUC()
        {
            return _EHR_KQ_KCB_THI_LUCDao ?? (_EHR_KQ_KCB_THI_LUCDao = new EHR_KQ_KCB_THI_LUCDao());
        }
        private static EHR_QUAN_HE_GIA_DINHDao _EHR_QUAN_HE_GIA_DINHDao;
        public static EHR_QUAN_HE_GIA_DINHDao GetInstanceEHR_QUAN_HE_GIA_DINH()
        {
            return _EHR_QUAN_HE_GIA_DINHDao ?? (_EHR_QUAN_HE_GIA_DINHDao = new EHR_QUAN_HE_GIA_DINHDao());
        }
        private static EHR_SKSS_KHHGDDao _EHR_SKSS_KHHGDDao;
        public static EHR_SKSS_KHHGDDao GetInstanceEHR_SKSS_KHHGD()
        {
            return _EHR_SKSS_KHHGDDao ?? (_EHR_SKSS_KHHGDDao = new EHR_SKSS_KHHGDDao());
        }
        private static EHR_SUC_KHOEDao _EHR_SUC_KHOEDao;
        public static EHR_SUC_KHOEDao GetInstanceEHR_SUC_KHOE()
        {
            return _EHR_SUC_KHOEDao ?? (_EHR_SUC_KHOEDao = new EHR_SUC_KHOEDao());
        }
        private static EHR_THE_BHYTDao _EHR_THE_BHYTDao;
        public static EHR_THE_BHYTDao GetInstanceEHR_THE_BHYT()
        {
            return _EHR_THE_BHYTDao ?? (_EHR_THE_BHYTDao = new EHR_THE_BHYTDao());
        }
        private static EHR_THUOCDao _EHR_THUOCDao;
        public static EHR_THUOCDao GetInstanceEHR_THUOC()
        {
            return _EHR_THUOCDao ?? (_EHR_THUOCDao = new EHR_THUOCDao());
        }
        private static EHR_TIEM_CHUNGDao _EHR_TIEM_CHUNGDao;
        public static EHR_TIEM_CHUNGDao GetInstanceEHR_TIEM_CHUNG()
        {
            return _EHR_TIEM_CHUNGDao ?? (_EHR_TIEM_CHUNGDao = new EHR_TIEM_CHUNGDao());
        }
        #endregion
        #region DTM_HIS_OWNER
        private static F_TONG_QUANDao _F_TONG_QUANDao;
        public static F_TONG_QUANDao GetInstanceF_TONG_QUAN()
        {
            return _F_TONG_QUANDao ?? (_F_TONG_QUANDao = new F_TONG_QUANDao());
        }
        private static F_TINH_TRANG_SUC_KHOEDao _F_TINH_TRANG_SUC_KHOEDao;
        public static F_TINH_TRANG_SUC_KHOEDao GetInstanceF_TINH_TRANG_SUC_KHOE()
        {
            return _F_TINH_TRANG_SUC_KHOEDao ?? (_F_TINH_TRANG_SUC_KHOEDao = new F_TINH_TRANG_SUC_KHOEDao());
        }
        private static F_TIEN_SU_DI_UNGDao _F_TIEN_SU_DI_UNGDao;
        public static F_TIEN_SU_DI_UNGDao GetInstanceF_TIEN_SU_DI_UNG()
        {
            return _F_TIEN_SU_DI_UNGDao ?? (_F_TIEN_SU_DI_UNGDao = new F_TIEN_SU_DI_UNGDao());
        }
        private static F_TIEN_SU_BENH_TATDao _F_TIEN_SU_BENH_TATDao;
        public static F_TIEN_SU_BENH_TATDao GetInstanceF_TIEN_SU_BENH_TAT()
        {
            return _F_TIEN_SU_BENH_TATDao ?? (_F_TIEN_SU_BENH_TATDao = new F_TIEN_SU_BENH_TATDao());
        }
        private static F_TIEM_CHUNGDao _F_TIEM_CHUNGDao;
        public static F_TIEM_CHUNGDao GetInstanceF_TIEM_CHUNG()
        {
            return _F_TIEM_CHUNGDao ?? (_F_TIEM_CHUNGDao = new F_TIEM_CHUNGDao());
        }
        private static F_HANH_CHINHDao _F_HANH_CHINHDao;
        public static F_HANH_CHINHDao GetInstanceF_HANH_CHINH()
        {
            return _F_HANH_CHINHDao ?? (_F_HANH_CHINHDao = new F_HANH_CHINHDao());
        }
        private static D_CONG_DANDao _D_CONG_DANDao;
        public static D_CONG_DANDao GetInstanceD_CONG_DAN()
        {
            return _D_CONG_DANDao ?? (_D_CONG_DANDao = new D_CONG_DANDao());
        }
        private static D_CO_SO_Y_TEDao _D_CO_SO_Y_TEDao;
        public static D_CO_SO_Y_TEDao GetInstanceD_CO_SO_Y_TE()
        {
            return _D_CO_SO_Y_TEDao ?? (_D_CO_SO_Y_TEDao = new D_CO_SO_Y_TEDao());
        }
        #endregion
        #region DHUB_GOVERNANCE      
        private static SYS_DST_TYPEDao _SYS_DST_TYPEDao;
        public static SYS_DST_TYPEDao GetInstanceSYS_DST_TYPE()
        {
            return _SYS_DST_TYPEDao ?? (_SYS_DST_TYPEDao = new SYS_DST_TYPEDao());
        }
        private static SYS_DSTDao _SYS_DSTDao;
        public static SYS_DSTDao GetInstanceSYS_DST()
        {
            return _SYS_DSTDao ?? (_SYS_DSTDao = new SYS_DSTDao());
        }
        private static SYS_DST_DTLDao _SYS_DST_DTLDao;
        public static SYS_DST_DTLDao GetInstanceSYS_DST_DTL()
        {
            return _SYS_DST_DTLDao ?? (_SYS_DST_DTLDao = new SYS_DST_DTLDao());
        }
        private static SYS_DATASETDao _SYS_DATASETDao;
        public static SYS_DATASETDao GetInstanceSYS_DATASET()
        {
            return _SYS_DATASETDao ?? (_SYS_DATASETDao = new SYS_DATASETDao());
        }
        private static SYS_DATASET_DTLDao _SYS_DATASET_DTLDao;
        public static SYS_DATASET_DTLDao GetInstanceSYS_DATASET_DTL()
        {
            return _SYS_DATASET_DTLDao ?? (_SYS_DATASET_DTLDao = new SYS_DATASET_DTLDao());
        }
        private static SYS_DATA_ZONEDao _SYS_DATA_ZONEDao;
        public static SYS_DATA_ZONEDao GetInstanceSYS_DATA_ZONE()
        {
            return _SYS_DATA_ZONEDao ?? (_SYS_DATA_ZONEDao = new SYS_DATA_ZONEDao());
        }
        private static SYS_DATA_ZONE_DTLDao _SYS_DATA_ZONE_DTLDao;
        public static SYS_DATA_ZONE_DTLDao GetInstanceSYS_DATA_ZONE_DTL()
        {
            return _SYS_DATA_ZONE_DTLDao ?? (_SYS_DATA_ZONE_DTLDao = new SYS_DATA_ZONE_DTLDao());
        }
        private static SYS_CONFIG_METHODDao _SYS_CONFIG_METHODDao;
        public static SYS_CONFIG_METHODDao GetInstanceSYS_CONFIG_METHOD()
        {
            return _SYS_CONFIG_METHODDao ?? (_SYS_CONFIG_METHODDao = new SYS_CONFIG_METHODDao());
        }
        private static SYS_CONFIG_WFLOWDao _SYS_CONFIG_WFLOWDao;
        public static SYS_CONFIG_WFLOWDao GetInstanceSYS_CONFIG_WFLOW()
        {
            return _SYS_CONFIG_WFLOWDao ?? (_SYS_CONFIG_WFLOWDao = new SYS_CONFIG_WFLOWDao());
        }
        private static SYS_CONFIG_PIPELINEDao _SYS_CONFIG_PIPELINEDao;
        public static SYS_CONFIG_PIPELINEDao GetInstanceSYS_CONFIG_PIPELINE()
        {
            return _SYS_CONFIG_PIPELINEDao ?? (_SYS_CONFIG_PIPELINEDao = new SYS_CONFIG_PIPELINEDao());
        }
        private static SYS_CONFIG_WFLOW_MAPDao _SYS_CONFIG_WFLOW_MAPDao;
        public static SYS_CONFIG_WFLOW_MAPDao GetInstanceSYS_CONFIG_WFLOW_MAP()
        {
            return _SYS_CONFIG_WFLOW_MAPDao ?? (_SYS_CONFIG_WFLOW_MAPDao = new SYS_CONFIG_WFLOW_MAPDao());
        }
        private static LOG_TYPEDao _LOG_TYPEDao;
        public static LOG_TYPEDao GetInstanceLOG_TYPE()
        {
            return _LOG_TYPEDao ?? (_LOG_TYPEDao = new LOG_TYPEDao());
        }
        private static LOG_ACTIONDao _LOG_ACTIONDao;
        public static LOG_ACTIONDao GetInstanceLOG_ACTION()
        {
            return _LOG_ACTIONDao ?? (_LOG_ACTIONDao = new LOG_ACTIONDao());
        }
        private static LOG_ERRORDao _LOG_ERRORDao;
        public static LOG_ERRORDao GetInstanceLOG_ERROR()
        {
            return _LOG_ERRORDao ?? (_LOG_ERRORDao = new LOG_ERRORDao());
        }
        private static LOG_WARNINGDao _LOG_WARNINGDao;
        public static LOG_WARNINGDao GetInstanceLOG_WARNING()
        {
            return _LOG_WARNINGDao ?? (_LOG_WARNINGDao = new LOG_WARNINGDao());
        }
        private static DATA_INTEGRATED_CONFIGDao _DATA_INTEGRATED_CONFIGDao;
        public static DATA_INTEGRATED_CONFIGDao GetInstanceDATA_INTEGRATED_CONFIG()
        {
            return _DATA_INTEGRATED_CONFIGDao ?? (_DATA_INTEGRATED_CONFIGDao = new DATA_INTEGRATED_CONFIGDao());
        }
        private static ETL_DATA_CONFIGDao _ETL_DATA_CONFIGDao;
        public static ETL_DATA_CONFIGDao GetInstanceETL_DATA_CONFIG()
        {
            return _ETL_DATA_CONFIGDao ?? (_ETL_DATA_CONFIGDao = new ETL_DATA_CONFIGDao());
        }
        private static SYS_CONFIG_WFPIPELINEDao _SYS_CONFIG_WFPIPELINEDao;
        public static SYS_CONFIG_WFPIPELINEDao GetInstanceSYS_CONFIG_WFPIPELINE()
        {
            return _SYS_CONFIG_WFPIPELINEDao ?? (_SYS_CONFIG_WFPIPELINEDao = new SYS_CONFIG_WFPIPELINEDao());
        }
        private static SYS_CONFIG_WFPIPELINE_DTLDao _SYS_CONFIG_WFPIPELINE_DTLDao;
        public static SYS_CONFIG_WFPIPELINE_DTLDao GetInstanceSYS_CONFIG_WFPIPELINE_DTL()
        {
            return _SYS_CONFIG_WFPIPELINE_DTLDao ?? (_SYS_CONFIG_WFPIPELINE_DTLDao = new SYS_CONFIG_WFPIPELINE_DTLDao());
        }        
        private static SYS_SHARE_CONFIGDao _SYS_SHARE_CONFIGDao;
        public static SYS_SHARE_CONFIGDao GetInstanceSYS_SHARE_CONFIG()
        {
            return _SYS_SHARE_CONFIGDao ?? (_SYS_SHARE_CONFIGDao = new SYS_SHARE_CONFIGDao());
        }
        private static SYS_SHARE_CORPDao _SYS_SHARE_CORPDao;
        public static SYS_SHARE_CORPDao GetInstanceSYS_SHARE_CORP()
        {
            return _SYS_SHARE_CORPDao ?? (_SYS_SHARE_CORPDao = new SYS_SHARE_CORPDao());
        }
        private static SYS_SHARE_SERVICESDao _SYS_SHARE_SERVICESDao;
        public static SYS_SHARE_SERVICESDao GetInstanceSYS_SHARE_SERVICES()
        {
            return _SYS_SHARE_SERVICESDao ?? (_SYS_SHARE_SERVICESDao = new SYS_SHARE_SERVICESDao());
        }
        private static SYS_SHARE_AUTHORDao _SYS_SHARE_AUTHORDao;
        public static SYS_SHARE_AUTHORDao GetInstanceSYS_SHARE_AUTHOR()
        {
            return _SYS_SHARE_AUTHORDao ?? (_SYS_SHARE_AUTHORDao = new SYS_SHARE_AUTHORDao());
        }
        private static SYS_SHARE_CORP_TOKENDao _SYS_SHARE_CORP_TOKENDao;
        public static SYS_SHARE_CORP_TOKENDao GetInstanceSYS_SHARE_CORP_TOKEN()
        {
            return _SYS_SHARE_CORP_TOKENDao ?? (_SYS_SHARE_CORP_TOKENDao = new SYS_SHARE_CORP_TOKENDao());
        }
        private static SYS_CONSUMER_CONFIGDao _SYS_CONSUMER_CONFIGDao;
        public static SYS_CONSUMER_CONFIGDao GetInstanceSYS_CONSUMER_CONFIG()
        {
            return _SYS_CONSUMER_CONFIGDao ?? (_SYS_CONSUMER_CONFIGDao = new SYS_CONSUMER_CONFIGDao());
        }
        private static SYS_CONFIG_CAMERADao _SYS_CONFIG_CAMERADao;
        public static SYS_CONFIG_CAMERADao GetInstanceSYS_CONFIG_CAMERA()
        {
            return _SYS_CONFIG_CAMERADao ?? (_SYS_CONFIG_CAMERADao = new SYS_CONFIG_CAMERADao());
        }
        private static CHARTSDao _CHARTSDao;
        public static CHARTSDao GetInstanceCHARTS()
        {
            return _CHARTSDao ?? (_CHARTSDao = new CHARTSDao());
        }
        private static CHART_DATASOURCEDao _CHART_DATASOURCEDao;
        public static CHART_DATASOURCEDao GetInstanceCHART_DATASOURCE()
        {
            return _CHART_DATASOURCEDao ?? (_CHART_DATASOURCEDao = new CHART_DATASOURCEDao());
        }
        private static CHART_PARAMETERSDao _CHART_PARAMETERSDao;
        public static CHART_PARAMETERSDao GetInstanceCHART_PARAMETERS()
        {
            return _CHART_PARAMETERSDao ?? (_CHART_PARAMETERSDao = new CHART_PARAMETERSDao());
        }
        private static SYS_RESOURCESDao _SYS_RESOURCESDao;
        public static SYS_RESOURCESDao GetInstanceSYS_RESOURCES()
        {
            return _SYS_RESOURCESDao ?? (_SYS_RESOURCESDao = new SYS_RESOURCESDao());
        }
        private static SYS_RESOURCE_AUTHORDao _SYS_RESOURCE_AUTHORDao;
        public static SYS_RESOURCE_AUTHORDao GetInstanceSYS_RESOURCE_AUTHOR()
        {
            return _SYS_RESOURCE_AUTHORDao ?? (_SYS_RESOURCE_AUTHORDao = new SYS_RESOURCE_AUTHORDao());
        }
        #endregion
        #region SHARE_OWNER
        private static S_DULIEU_FWDao _S_DULIEU_FWDao;
        public static S_DULIEU_FWDao GetInstanceS_DULIEU_FW()
        {
            return _S_DULIEU_FWDao ?? (_S_DULIEU_FWDao = new S_DULIEU_FWDao());
        }
        #endregion
    }
}

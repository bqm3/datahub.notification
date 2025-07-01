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
    public class F_HANH_CHINHDao : OracleBaseImpl<F_HANH_CHINH>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "F_HANH_CHINH";
            PackageName = "PK_F_HANH_CHINH";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        
		public List<F_HANH_CHINH> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_QUOC_TICH",(dictParam != null && dictParam.ContainsKey("QUOC_TICH")) ? dictParam["QUOC_TICH"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null),
                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_HKTT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("HKTT_XA_PHUONG")) ? dictParam["HKTT_XA_PHUONG"] : null),
                new OracleParameter("p_HKTT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("HKTT_QUAN_HUYEN")) ? dictParam["HKTT_QUAN_HUYEN"] : null),
                new OracleParameter("p_HKTT_TINH_TP",(dictParam != null && dictParam.ContainsKey("HKTT_TINH_TP")) ? dictParam["HKTT_TINH_TP"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null)
                
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<F_HANH_CHINH> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_QUOC_TICH",(dictParam != null && dictParam.ContainsKey("QUOC_TICH")) ? dictParam["QUOC_TICH"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null),
                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_HKTT_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("HKTT_XA_PHUONG")) ? dictParam["HKTT_XA_PHUONG"] : null),
                new OracleParameter("p_HKTT_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("HKTT_QUAN_HUYEN")) ? dictParam["HKTT_QUAN_HUYEN"] : null),
                new OracleParameter("p_HKTT_TINH_TP",(dictParam != null && dictParam.ContainsKey("HKTT_TINH_TP")) ? dictParam["HKTT_TINH_TP"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null)  
				
            };
            List<F_HANH_CHINH> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        

        


    }
}

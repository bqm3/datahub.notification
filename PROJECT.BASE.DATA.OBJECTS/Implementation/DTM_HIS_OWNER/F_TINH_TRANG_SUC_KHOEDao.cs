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
    public class F_TINH_TRANG_SUC_KHOEDao : OracleBaseImpl<F_TINH_TRANG_SUC_KHOE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "F_TINH_TRANG_SUC_KHOE";
            PackageName = "PK_F_TINH_TRANG_SUC_KHOE";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        
		public List<F_TINH_TRANG_SUC_KHOE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MA_SUC_KHOE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE_DON_VI")) ? dictParam["MA_SUC_KHOE_DON_VI"] : null)
                
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<F_TINH_TRANG_SUC_KHOE> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_HSSK",(dictParam != null && dictParam.ContainsKey("MA_HSSK")) ? dictParam["MA_HSSK"] : null),
                new OracleParameter("p_MA_SUC_KHOE",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE")) ? dictParam["MA_SUC_KHOE"] : null),
                new OracleParameter("p_MA_SUC_KHOE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_SUC_KHOE_DON_VI")) ? dictParam["MA_SUC_KHOE_DON_VI"] : null)   
				
            };
            List<F_TINH_TRANG_SUC_KHOE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        

        


    }
}

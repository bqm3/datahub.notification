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
    public class F_TIEM_CHUNGDao : OracleBaseImpl<F_TIEM_CHUNG>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "F_TIEM_CHUNG";
            PackageName = "PK_F_TIEM_CHUNG";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
       
		public List<F_TIEM_CHUNG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
               
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("MA_LOAI_TIEM_CHUNG")) ? dictParam["MA_LOAI_TIEM_CHUNG"] : null)
                
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<F_TIEM_CHUNG> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				
                new OracleParameter("p_MA_BENH_NHAN",(dictParam != null && dictParam.ContainsKey("MA_BENH_NHAN")) ? dictParam["MA_BENH_NHAN"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_LOAI_TIEM_CHUNG",(dictParam != null && dictParam.ContainsKey("MA_LOAI_TIEM_CHUNG")) ? dictParam["MA_LOAI_TIEM_CHUNG"] : null)    
				
            };
            List<F_TIEM_CHUNG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        

        


    }
}

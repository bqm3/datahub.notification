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
    public class F_TONG_QUANDao : OracleBaseImpl<F_TONG_QUAN>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "F_TONG_QUAN";
            PackageName = "PK_F_TONG_QUAN";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
     
		public List<F_TONG_QUAN> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                

                new OracleParameter("p_SO_DIEN_THOAI",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI")) ? dictParam["SO_DIEN_THOAI"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null)
               
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<F_TONG_QUAN> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				
                new OracleParameter("p_SO_DIEN_THOAI",(dictParam != null && dictParam.ContainsKey("SO_DIEN_THOAI")) ? dictParam["SO_DIEN_THOAI"] : null),
                new OracleParameter("p_EMAIL",(dictParam != null && dictParam.ContainsKey("EMAIL")) ? dictParam["EMAIL"] : null),
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_SO_THE_BHYT",(dictParam != null && dictParam.ContainsKey("SO_THE_BHYT")) ? dictParam["SO_THE_BHYT"] : null),
                new OracleParameter("p_HO_TEN",(dictParam != null && dictParam.ContainsKey("HO_TEN")) ? dictParam["HO_TEN"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null)
				
            };
            List<F_TONG_QUAN> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

    

        


    }
}

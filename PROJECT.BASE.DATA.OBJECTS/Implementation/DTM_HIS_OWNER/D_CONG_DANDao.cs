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
    public class D_CONG_DANDao : OracleBaseImpl<D_CONG_DAN>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "D_CONG_DAN";
            PackageName = "PK_D_CONG_DAN";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
      
		public List<D_CONG_DAN> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
               
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_DAN_TOC",(dictParam != null && dictParam.ContainsKey("DAN_TOC")) ? dictParam["DAN_TOC"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null)
                
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<D_CONG_DAN> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				
                new OracleParameter("p_MA_CONG_DAN",(dictParam != null && dictParam.ContainsKey("MA_CONG_DAN")) ? dictParam["MA_CONG_DAN"] : null),
                new OracleParameter("p_MA_HO_GD",(dictParam != null && dictParam.ContainsKey("MA_HO_GD")) ? dictParam["MA_HO_GD"] : null),
                new OracleParameter("p_DAN_TOC",(dictParam != null && dictParam.ContainsKey("DAN_TOC")) ? dictParam["DAN_TOC"] : null),
                new OracleParameter("p_GIOI_TINH",(dictParam != null && dictParam.ContainsKey("GIOI_TINH")) ? dictParam["GIOI_TINH"] : null),
                new OracleParameter("p_CCCD",(dictParam != null && dictParam.ContainsKey("CCCD")) ? dictParam["CCCD"] : null),
                new OracleParameter("p_CMND",(dictParam != null && dictParam.ContainsKey("CMND")) ? dictParam["CMND"] : null)    
				
            };
            List<D_CONG_DAN> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        

        


    }
}

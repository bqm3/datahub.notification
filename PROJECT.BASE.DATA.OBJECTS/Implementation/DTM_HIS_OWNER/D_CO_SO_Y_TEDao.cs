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
    public class D_CO_SO_Y_TEDao : OracleBaseImpl<D_CO_SO_Y_TE>
    {        
		protected override void SetInfoDerivedClass()
        {
            TableName = "D_CO_SO_Y_TE";
            PackageName = "PK_D_CO_SO_Y_TE";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DTM_HIS_OWNER.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        
		public List<D_CO_SO_Y_TE> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                
                new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE_DON_VI")) ? dictParam["MA_CO_SO_Y_TE_DON_VI"] : null),
                new OracleParameter("p_TEN_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("TEN_CO_SO_Y_TE")) ? dictParam["TEN_CO_SO_Y_TE"] : null),
                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("XA_PHUONG")) ? dictParam["XA_PHUONG"] : null),
                new OracleParameter("p_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("QUAN_HUYEN")) ? dictParam["QUAN_HUYEN"] : null),
                new OracleParameter("p_TINH_TP",(dictParam != null && dictParam.ContainsKey("TINH_TP")) ? dictParam["TINH_TP"] : null)
                
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<D_CO_SO_Y_TE> GetListPaging(Dictionary<string, object> dictParam, int pageIndex, int pageSize, ref int totalRecord)
        {            
            OracleParameter[] sqlParam = {
				new OracleParameter("p_MA_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE")) ? dictParam["MA_CO_SO_Y_TE"] : null),
                new OracleParameter("p_MA_CO_SO_Y_TE_DON_VI",(dictParam != null && dictParam.ContainsKey("MA_CO_SO_Y_TE_DON_VI")) ? dictParam["MA_CO_SO_Y_TE_DON_VI"] : null),
                new OracleParameter("p_TEN_CO_SO_Y_TE",(dictParam != null && dictParam.ContainsKey("TEN_CO_SO_Y_TE")) ? dictParam["TEN_CO_SO_Y_TE"] : null),
                new OracleParameter("p_DIA_CHI",(dictParam != null && dictParam.ContainsKey("DIA_CHI")) ? dictParam["DIA_CHI"] : null),
                new OracleParameter("p_XA_PHUONG",(dictParam != null && dictParam.ContainsKey("XA_PHUONG")) ? dictParam["XA_PHUONG"] : null),
                new OracleParameter("p_QUAN_HUYEN",(dictParam != null && dictParam.ContainsKey("QUAN_HUYEN")) ? dictParam["QUAN_HUYEN"] : null),
                new OracleParameter("p_TINH_TP",(dictParam != null && dictParam.ContainsKey("TINH_TP")) ? dictParam["TINH_TP"] : null) 
				
            };
            List<D_CO_SO_Y_TE> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       


        


    }
}

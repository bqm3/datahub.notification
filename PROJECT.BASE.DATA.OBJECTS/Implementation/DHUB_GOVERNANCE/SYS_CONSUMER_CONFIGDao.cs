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
    public class SYS_CONSUMER_CONFIGDao : OracleBaseImpl<SYS_CONSUMER_CONFIG>
    {
        protected override void SetInfoDerivedClass()
        {
            TableName = "SYS_CONSUMER_CONFIG";
            PackageName = "PK_SYS_CONSUMER_CONFIG";
            ConnectionString = Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString;
        }
        #region User defined

        #endregion

        #region Base		
        public long? Insert(SYS_CONSUMER_CONFIG_Request request)
        {
            var info = new SYS_CONSUMER_CONFIG
            {
                CODE = (request != null && !string.IsNullOrEmpty(request.CODE)) ? request.CODE : null,

                NAME = (request != null && !string.IsNullOrEmpty(request.NAME)) ? request.NAME : null,
                PARENT_CODE = (request != null && !string.IsNullOrEmpty(request.PARENT_CODE)) ? request.PARENT_CODE : null,
                PARENT_NAME = (request != null && !string.IsNullOrEmpty(request.PARENT_NAME)) ? request.PARENT_NAME : null,
                DESCRIPTION = (request != null && !string.IsNullOrEmpty(request.DESCRIPTION)) ? request.DESCRIPTION : null,
                SEND_TOPIC = (request != null && !string.IsNullOrEmpty(request.SEND_TOPIC)) ? request.SEND_TOPIC : null,
                IS_FORWARD = (request != null && request.IS_FORWARD != null) ? request.IS_FORWARD.Value : (short?)null,
                STATUS = (request != null && request.STATUS != null) ? request.STATUS.Value : (short?)null,
                IS_DELETE = (request != null && request.REMOVED != null) ? request.REMOVED.Value : (short?)null,
                CUSER = !string.IsNullOrEmpty(request.CREATOR) ? request.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
            };
            var result = this.Add(info);
            return result;
        }
        public bool? Insert(List<SYS_CONSUMER_CONFIG_Request> request)
        {
            var list = new List<SYS_CONSUMER_CONFIG>();
            foreach (var info in request)
            {
                list.Add(new SYS_CONSUMER_CONFIG
                {
                    CODE = (info != null && !string.IsNullOrEmpty(info.CODE)) ? info.CODE : null,

                    NAME = (info != null && !string.IsNullOrEmpty(info.NAME)) ? info.NAME : null,
                    PARENT_CODE = (info != null && !string.IsNullOrEmpty(info.PARENT_CODE)) ? info.PARENT_CODE : null,
                    PARENT_NAME = (info != null && !string.IsNullOrEmpty(info.PARENT_NAME)) ? info.PARENT_NAME : null,
                    DESCRIPTION = (info != null && !string.IsNullOrEmpty(info.DESCRIPTION)) ? info.DESCRIPTION : null,
                    SEND_TOPIC = (info != null && !string.IsNullOrEmpty(info.SEND_TOPIC)) ? info.SEND_TOPIC : null,
                    IS_FORWARD = (info != null && info.IS_FORWARD != null) ? info.IS_FORWARD.Value : (short?)null,
                    STATUS = (info != null && info.STATUS != null) ? info.STATUS.Value : (short?)null,
                    IS_DELETE = (info != null && info.REMOVED != null) ? info.REMOVED.Value : (short?)null,
                    CUSER = !string.IsNullOrEmpty(info.CREATOR) ? info.CREATOR : Config.CONFIGURATION_PRIVATE.Infomation.Service
                });
            }
            var getPropertyNames = this.GetPropertyNames();
            var result = this.InsertList(list, getPropertyNames);
            return result;
        }
        public bool? UpdateList(List<SYS_CONSUMER_CONFIG> list)
        {
            var getPropertyNames = this.GetPropertyNames();
            var result = this.UpdateList(list, "ID", getPropertyNames);
            return result;
        }
        public bool? UpInsertList(List<SYS_CONSUMER_CONFIG> list)
        {
            var getPropertyNames = this.GetPropertyNames();
            var result = this.UpInsertList(list, "ID", getPropertyNames);
            return result;
        }
        public List<SYS_CONSUMER_CONFIG> GetList(Dictionary<string, object> dictParam)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_PARENT_NAME",(dictParam != null && dictParam.ContainsKey("PARENT_NAME")) ? dictParam["PARENT_NAME"] : null),
                new OracleParameter("p_SEND_TOPIC",(dictParam != null && dictParam.ContainsKey("SEND_TOPIC")) ? dictParam["SEND_TOPIC"] : null),
                new OracleParameter("p_IS_FORWARD",(dictParam != null && dictParam.ContainsKey("IS_FORWARD")) ? dictParam["IS_FORWARD"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",(dictParam != null && dictParam.ContainsKey("CDATE_START")) ? dictParam["CDATE_START"] : null),
                new OracleParameter("p_CDATE_END", (dictParam != null && dictParam.ContainsKey("CDATE_END")) ? dictParam["CDATE_END"] : null)
            };
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public List<SYS_CONSUMER_CONFIG> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END, int pageIndex, int pageSize, ref int totalRecord)
        {
            OracleParameter[] sqlParam = {
                new OracleParameter("p_CODE",(dictParam != null && dictParam.ContainsKey("CODE")) ? dictParam["CODE"] : null),

                new OracleParameter("p_NAME",(dictParam != null && dictParam.ContainsKey("NAME")) ? dictParam["NAME"] : null),
                new OracleParameter("p_PARENT_CODE",(dictParam != null && dictParam.ContainsKey("PARENT_CODE")) ? dictParam["PARENT_CODE"] : null),
                new OracleParameter("p_PARENT_NAME",(dictParam != null && dictParam.ContainsKey("PARENT_NAME")) ? dictParam["PARENT_NAME"] : null),
                new OracleParameter("p_SEND_TOPIC",(dictParam != null && dictParam.ContainsKey("SEND_TOPIC")) ? dictParam["SEND_TOPIC"] : null),
                new OracleParameter("p_IS_FORWARD",(dictParam != null && dictParam.ContainsKey("IS_FORWARD")) ? dictParam["IS_FORWARD"] : null),
                new OracleParameter("p_STATUS",(dictParam != null && dictParam.ContainsKey("STATUS")) ? dictParam["STATUS"] : null),
                new OracleParameter("p_IS_DELETE",(dictParam != null && dictParam.ContainsKey("IS_DELETE")) ? dictParam["IS_DELETE"] : null),
                new OracleParameter("p_CDATE_START",p_CDATE_START),
                new OracleParameter("p_CDATE_END", p_CDATE_END)
            };
            List<SYS_CONSUMER_CONFIG> result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }

        #endregion       

        #region Using Solr

        public SYS_CONSUMER_CONFIG QuerySolr_GetInfo(string _ID, string _CODE)
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
            string[] fieldSelect = { "ID", "CODE", "NAME", "PARENT_CODE", "PARENT_NAME", "DESCRIPTION", "SEND_TOPIC", "IS_FORWARD", "STATUS", "IS_DELETE", "CDATE", "LDATE", "CUSER", "LUSER" };
            int totalRecord = 0;
            List<SYS_CONSUMER_CONFIG> results = QuerySolrBase<SYS_CONSUMER_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONSUMER_CONFIG/", string.Empty, lstFilter, fieldSelect, lstOrder, 0, int.Parse(Config.SOLR_PAGE_SIZE), ref totalRecord);
            if (results != null && results.Count > 0)
                return results[0];
            return null;
        }

        public List<SYS_CONSUMER_CONFIG> QuerySolr_GetListPaging(string keyword,
            string p_CODE,
            string p_NAME, string p_PARENT_CODE, string p_PARENT_NAME, string p_SEND_TOPIC, string p_IS_FORWARD, string p_STATUS,
            DateTime? p_CDATE_START_DATE,
            DateTime? p_CDATE_END_DATE,
            int pageIndex, int pageSize, ref int totalRecord)
        {
            List<ISolrQuery> lstFilter = new List<ISolrQuery>();
            if (!string.IsNullOrEmpty(p_CODE))
            {
                lstFilter.Add(new SolrQuery("CODE:" + p_CODE));
            }

            if (!string.IsNullOrEmpty(p_NAME))
            {
                lstFilter.Add(new SolrQuery("NAME:" + p_NAME));
            }
            if (!string.IsNullOrEmpty(p_PARENT_CODE))
            {
                lstFilter.Add(new SolrQuery("PARENT_CODE:" + p_PARENT_CODE));
            }
            if (!string.IsNullOrEmpty(p_PARENT_NAME))
            {
                lstFilter.Add(new SolrQuery("PARENT_NAME:" + p_PARENT_NAME));
            }
            if (!string.IsNullOrEmpty(p_SEND_TOPIC))
            {
                lstFilter.Add(new SolrQuery("SEND_TOPIC:" + p_SEND_TOPIC));
            }
            if (!string.IsNullOrEmpty(p_IS_FORWARD))
            {
                lstFilter.Add(new SolrQuery("IS_FORWARD:" + p_IS_FORWARD));
            }
            if (!string.IsNullOrEmpty(p_STATUS))
            {
                lstFilter.Add(new SolrQuery("STATUS:" + p_STATUS));
            }
            if (p_CDATE_START_DATE != null && p_CDATE_END_DATE != null)
            {
                lstFilter.Add(new SolrQuery("CDATE:[" + Utility.GetJSONZFromUserDateTime(p_CDATE_START_DATE.Value) + " TO " + Utility.GetJSONZFromUserDateTime(p_CDATE_END_DATE.Value) + "]"));
            }
            List<SortOrder> lstOrder = new List<SortOrder>();
            lstOrder.Add(new SortOrder("ID", Order.DESC));
            string[] fieldSelect = { "ID", "CODE", "NAME", "PARENT_CODE", "PARENT_NAME", "DESCRIPTION", "SEND_TOPIC", "IS_FORWARD", "STATUS", "IS_DELETE", "CDATE", "LDATE", "CUSER", "LUSER" };
            var results = QuerySolrBase<SYS_CONSUMER_CONFIG>.QuerySolr_GetList(Config.SOLR_URL_CORE_BASE + $"SYS_CONSUMER_CONFIG/", keyword, lstFilter, fieldSelect, lstOrder, pageIndex - 1, pageSize, ref totalRecord);
            List<SYS_CONSUMER_CONFIG> listAddRange = new List<SYS_CONSUMER_CONFIG>();
            if (results != null)
                listAddRange.AddRange(results);
            return listAddRange;
        }
        #endregion




    }
}

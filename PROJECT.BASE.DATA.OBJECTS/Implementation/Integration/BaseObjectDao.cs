using System;
using System.Collections.Generic;
using PROJECT.BASE.CORE;
using Lib.Setting;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using System.Data;
using Lib.Utility;
using System.Linq;
using PROJECT.BASE.ENTITY;


namespace PROJECT.BASE.DAO
{
    public class BaseObjectDao : OracleBaseImpl<System.Object>
    {
        private Dictionary<string, Dictionary<string, string>> _DictConnectionString = null;
        protected override void SetInfoDerivedClass()
        {

            if (_DictConnectionString == null)
                _DictConnectionString = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(Config.CONFIGURATION_GLOBAL.Databases.ORACLE.ToString());
        }
        public BaseObjectDao(string databaseName)
        {
            DatabaseName = databaseName;
            ConnectionString = _DictConnectionString[databaseName]["ConnectionString"];
        }
        public BaseObjectDao(string tableName, string databaseName)
        {
            TableName = tableName;
            DatabaseName = databaseName;
            ConnectionString = _DictConnectionString[databaseName]["ConnectionString"];
        }
        public BaseObjectDao(string tableName, string packageName, string databaseName)
        {
            TableName = tableName;
            PackageName = packageName;
            DatabaseName = databaseName;
            ConnectionString = _DictConnectionString[databaseName]["ConnectionString"];
        }
        public List<System.Object> GetList(Dictionary<string, object> dictParam)
        {
            int index = 0;
            OracleParameter[] sqlParam = new OracleParameter[dictParam.Count - 1];
            foreach (var pair in dictParam)
            {
                sqlParam[index] = new OracleParameter($"p_{pair.Key}", (dictParam != null && dictParam.ContainsKey(pair.Key)) ? dictParam[pair.Key] : null);
                index++;
            }
            var result = this.ExecuteQuery("SP_GET_LIST", sqlParam);
            return result;
        }
        public dynamic GetListPaging(Dictionary<string, KeyValueInfo> dictParam, string corpCode, string orderBy, int pageIndex, int pageSize, ref int totalRecord,ref string message)
        {

            string sql_where = string.Empty;
            var redisGetData = RedisCacheProvider.GetData<string>($"SHARE_COLUMN.{corpCode}.{DatabaseName}.{TableName}");
            if (redisGetData == null)
            {
                var list = DataObjectFactory.GetInstanceSYS_SHARE_AUTHOR().GetList(new Dictionary<string, object>() {
                    {"CORP_CODE", corpCode},
                    {"SCHEMAS", DatabaseName},
                    {"TABLES", TableName},
                    {"IS_ACTIVE",1 },
                    {"IS_DELETE",0 }
                });
                if (list != null && list.Count > 0)
                {
                    redisGetData = string.Join(",", list.Select(p => p.COLUMNS));
                    if (!string.IsNullOrEmpty(redisGetData))
                        RedisCacheProvider.SetData<string>($"SHARE_COLUMN.{corpCode}.{DatabaseName}.{TableName}", redisGetData);
                }
            }
            if (redisGetData == null)
            {
                message = $"Redis SHARE_COLUMN.{corpCode}.{DatabaseName}.{TableName} : {Constant.MESSAGE_NOT_FOUND}";
                Console.WriteLine(message);
                return null;
            }
            if (dictParam != null && dictParam.Count > 0)
            {
                int index = 0;
                foreach (var pair in dictParam)
                {
                    if (index == 0)
                        sql_where += $"WHERE {GetClauseSQL(pair.Value.Key, pair.Key, pair.Value.Value)}";
                    else
                        sql_where += $"\n AND {GetClauseSQL(pair.Value.Key, pair.Key, pair.Value.Value)}";
                    index++;
                }
            }
            string sql = @"SELECT *
                           FROM (
                               SELECT tab.*, ROWNUM AS STT
                               FROM (
                                   SELECT
                                       " + redisGetData + @"
                                       ,COUNT(*) OVER () AS TOTAL_RECORD
                                   FROM " + TableName + @"
                                   " + sql_where + @"
                                   ORDER BY " + orderBy + @"
                               ) tab
                               WHERE ROWNUM <= (" + pageIndex + @" - 1) * " + pageSize + @" + " + pageSize + @"
                           )
                           WHERE STT > (" + pageSize + @" * (" + pageIndex + @" - 1))";
            if (Valid.IsSqlInjection(sql))
                return null;
            var ds = this.ExecuteQueryToDataSet(sql);
            totalRecord = ds.Tables[0].Rows.Count > 0 ? int.Parse(ds.Tables[0].Rows[0]["TOTAL_RECORD"].ToString()) : 0;
            var dataTable = ds.Tables[0];
            dataTable.Columns.Remove("TOTAL_RECORD");
            var result = Utility.ToDictionary(dataTable);
            return result;
        }
        public List<System.Object> GetListPaging(Dictionary<string, object> dictParam, DateTime? p_CDATE_START,
            DateTime? p_CDATE_END, int pageIndex, int pageSize, ref int totalRecord)
        {
            int index = 0;
            OracleParameter[] sqlParam = new OracleParameter[dictParam.Count];
            foreach (var pair in dictParam)
            {
                sqlParam[index] = new OracleParameter($"p_{pair.Key}", (dictParam != null && dictParam.ContainsKey(pair.Key)) ? dictParam[pair.Key] : null);
                index++;
            }
            var result = this.ExecuteQuery("SP_GET_LIST_PAGING", sqlParam, pageIndex, pageSize, ref totalRecord);
            return result;
        }
        #region User defined
        private string GetClauseSQL(string sqlType, string columnName, object Value)
        {
            if (sqlType == ConstantSQL.SQL_TYPE_1)
            {
                var arr = Value.ToString().Split(',');
                return $"{columnName} >= TO_DATE('{arr[0]}', 'YYYY-MM-DD') AND {columnName} <= TO_DATE('{arr[1]}', 'YYYY-MM-DD')";
            }
            if (sqlType == ConstantSQL.SQL_TYPE_2)
            {
                var arr = Value.ToString().Split(',');
                return $"{columnName} >= TO_DATE('{arr[0]}', 'YYYY-MM-DD HH24:MI:SS') AND {columnName} <= TO_DATE('{arr[1]}', 'YYYY-MM-DD HH24:MI:SS')";
            }
            if (sqlType == ConstantSQL.SQL_TYPE_3)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_4)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_5)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_6)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_7)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_8)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_9)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_10)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_11)
                return $"{columnName} {Value}";
            if (sqlType == ConstantSQL.SQL_TYPE_12)
            {
                bool isNumber = decimal.TryParse(Value.ToString(), out _);
                if (isNumber)
                    return $"{columnName} = {Value}";
                return $"{columnName} = '{Value}'";
            }
            return string.Empty;
        }
        #endregion
        #region Base

        public dynamic GetListTableFromSchema(string owner)
        {
            string sql = $"SELECT TABLE_NAME, NUM_ROWS " +
                $"FROM all_tables " +
                $"WHERE owner = '{owner.ToUpper()}'";
            var ds = this.ExecuteQueryToDataSet(sql);
            var dataTable = ds.Tables[0];
            var result = Utility.ToDictionary(dataTable);
            return result;
        }
        public dynamic GetListColumnFromTable(string owner, string table_name)
        {
            string sql = $"SELECT column_name, data_type, data_length, nullable " +
                $"FROM all_tab_columns " +
                $"WHERE table_name = '{table_name.ToUpper()}'  AND owner = '{owner.ToUpper()}'";

            var ds = this.ExecuteQueryToDataSet(sql);
            var dataTable = ds.Tables[0];
            var result = Utility.ToDictionary(dataTable);
            return result;
        }
        //public string Add(string tableName, string primaryKey, Dictionary<string, object> paramValue, ref string message)
        //{
        //    try
        //    {
        //        string columnName = string.Empty;
        //        string columnValue = string.Empty;
        //        foreach (var keyValuePair in paramValue)
        //        {
        //            columnName += $"\"{keyValuePair.Key}\",";
        //            columnValue += $"@{keyValuePair.Key},";
        //        }
        //        columnName = columnName.Substring(0, columnName.Length - 1);
        //        columnValue = columnValue.Substring(0, columnValue.Length - 1);
        //        string query = $"INSERT INTO \"{schemaName}\".\"{tableName}\" ({columnName}) VALUES ({columnValue})" + $" RETURNING \"{primaryKey}\";";
        //        var result = _IDbConnection.ExecuteScalarAsync<string>(query, paramValue).Result;
        //        message = !string.IsNullOrEmpty(result) ? string.Empty : Constant.MESSAGE_ERROR;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
        //        return null;
        //    }

        //}
        //public string Add(string schema, string tableName, string primaryKey, Dictionary<string, object> paramValue, ref string message)
        //{
        //    try
        //    {
        //        string columnName = string.Empty;
        //        string columnValue = string.Empty;
        //        foreach (var keyValuePair in paramValue)
        //        {
        //            columnName += $"\"{keyValuePair.Key}\",";
        //            columnValue += $"@{keyValuePair.Key},";
        //        }
        //        columnName = columnName.Substring(0, columnName.Length - 1);
        //        columnValue = columnValue.Substring(0, columnValue.Length - 1);
        //        string query = $"INSERT INTO \"{schema}\".\"{tableName}\" ({columnName}) VALUES ({columnValue})" + $" RETURNING \"{primaryKey}\";";
        //        var result = _IDbConnection.ExecuteScalarAsync<string>(query, paramValue).Result;
        //        message = !string.IsNullOrEmpty(result) ? string.Empty : Constant.MESSAGE_ERROR;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
        //        return null;
        //    }

        //}
        //public async Task<DataTable> GetDataBy(string query, Dictionary<string, object> paramValue)
        //{
        //    var result = await _IDbConnection.ExecuteFunctionToDataTableAsync<DataTable>(query, paramValue);
        //    return result;

        //}
        //public async Task<DataTable> GetDataBy(string tableName, string selectColumn, string whereColumn, Dictionary<string, object> paramValue)
        //{
        //    string query = $"SELECT \"{selectColumn}\" FROM \"{schemaName}\".\"{tableName}\"" + $"WHERE \"{whereColumn}\" = @{whereColumn}";
        //    var result = await _IDbConnection.ExecuteFunctionToDataTableAsync<DataTable>(query, paramValue);
        //    return result;

        //}


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using Lib.Setting;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using Lib.Utility;
using ServiceStack;
using System.Data;
using MySqlX.XDevAPI;
using Minio.DataModel;
using Consul;
using PROJECT.BASE.CORE;

namespace PROJECT.BASE.SERVICES
{
    public partial class TableNameInfo
    {
        public string TableName { get; set; }
        public string ColumName { get; set; }
        public string Value { get; set; }
        public string ReferID { get; set; }

        public TableNameInfo()
        {
            TableName = string.Empty;
            Value = string.Empty;
            ColumName = string.Empty;
            ReferID = string.Empty;
        }
    }
    public static class SampleXML
    {
        public static XmlDocument ToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        private static string GetQName(XElement xe)
        {
            string prefix = xe.GetPrefixOfNamespace(xe.Name.Namespace);
            if (xe.Name.Namespace == XNamespace.None || prefix == null)
                return xe.Name.LocalName.ToString();
            else
                return prefix + ":" + xe.Name.LocalName.ToString();
        }

        private static string GetQName(XAttribute xa)
        {
            string prefix =
                xa.Parent.GetPrefixOfNamespace(xa.Name.Namespace);
            if (xa.Name.Namespace == XNamespace.None || prefix == null)
                return xa.Name.ToString();
            else
                return prefix + ":" + xa.Name.LocalName;
        }

        private static string NameWithPredicate(XElement el)
        {
            if (el.Parent != null && el.Parent.Elements(el.Name).Count() != 1)
                return GetQName(el) + "[" +
                    (el.ElementsBeforeSelf(el.Name).Count() + 1) + "]";
            else
                return GetQName(el);
        }

        public static string StrCat<T>(this IEnumerable<T> source,
            string separator)
        {
            return source.Aggregate(new StringBuilder(),
                       (sb, i) => sb
                           .Append(i.ToString())
                           .Append(separator),
                       s => s.ToString());
        }

        public static string GetXPath(this XObject xobj)
        {
            if (xobj.Parent == null)
            {
                XDocument doc = xobj as XDocument;
                if (doc != null)
                    return ".";
                XElement el = xobj as XElement;
                if (el != null)
                    return "/" + NameWithPredicate(el);
                // The XPath data model doesn't include white space text nodes
                // that are children of a document, so this method returns null.
                XText xt = xobj as XText;
                if (xt != null)
                    return null;
                XComment com = xobj as XComment;
                if (com != null)
                    return
                        "/" +
                        (
                            com.Document
                            .Nodes()
                            .OfType<XComment>()
                            .Count() != 1 ?
                            "comment()[" +
                            (com.NodesBeforeSelf()
                            .OfType<XComment>()
                            .Count() + 1) +
                            "]" :
                            "comment()"
                        );
                XProcessingInstruction pi = xobj as XProcessingInstruction;
                if (pi != null)
                    return
                        "/" +
                        (
                            pi.Document.Nodes()
                            .OfType<XProcessingInstruction>()
                            .Count() != 1 ?
                            "processing-instruction()[" +
                            (pi
                            .NodesBeforeSelf()
                            .OfType<XProcessingInstruction>()
                            .Count() + 1) +
                            "]" :
                            "processing-instruction()"
                        );
                return null;
            }
            else
            {
                XElement el = xobj as XElement;
                if (el != null)
                {
                    return
                        "/" +
                        el.Ancestors()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        NameWithPredicate(el);
                }
                XAttribute at = xobj as XAttribute;
                if (at != null)
                    return
                        "/" +
                        at.Parent
                        .AncestorsAndSelf()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        "@" + GetQName(at);
                XComment com = xobj as XComment;
                if (com != null)
                    return
                        "/" +
                        com.Parent
                        .AncestorsAndSelf()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        (
                            com.Parent
                            .Nodes()
                            .OfType<XComment>()
                            .Count() != 1 ?
                            "comment()[" +
                            (com.NodesBeforeSelf()
                            .OfType<XComment>()
                            .Count() + 1) + "]" :
                            "comment()"
                        );
                XCData cd = xobj as XCData;
                if (cd != null)
                    return
                        "/" +
                        cd.Parent
                        .AncestorsAndSelf()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        (
                            cd.Parent
                            .Nodes()
                            .OfType<XText>()
                            .Count() != 1 ?
                            "text()[" +
                            (cd.NodesBeforeSelf()
                            .OfType<XText>()
                            .Count() + 1) + "]" :
                            "text()"
                        );
                XText tx = xobj as XText;
                if (tx != null)
                    return
                        "/" +
                        tx.Parent
                        .AncestorsAndSelf()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        (
                            tx.Parent
                            .Nodes()
                            .OfType<XText>()
                            .Count() != 1 ?
                            "text()[" +
                            (tx.NodesBeforeSelf()
                            .OfType<XText>()
                            .Count() + 1) + "]" :
                            "text()"
                        );
                XProcessingInstruction pi = xobj as XProcessingInstruction;
                if (pi != null)
                    return
                        "/" +
                        pi.Parent
                        .AncestorsAndSelf()
                        .InDocumentOrder()
                        .Select(e => NameWithPredicate(e))
                        .StrCat("/") +
                        (
                            pi.Parent
                            .Nodes()
                            .OfType<XProcessingInstruction>()
                            .Count() != 1 ?
                            "processing-instruction()[" +
                            (pi.NodesBeforeSelf()
                            .OfType<XProcessingInstruction>()
                            .Count() + 1) + "]" :
                            "processing-instruction()"
                        );
                return null;
            }
        }
    }
    internal class ExtractXML
    {
        public static List<TableNameInfo> XMLToTableName(XmlDocument xmlDocument, string pathFileXSD, ref List<string> listTableName)
        {
            if (!string.IsNullOrEmpty(pathFileXSD))
            {
                var resultValid = Valid.ValidateXML(xmlDocument, pathFileXSD);
                if (resultValid != null && resultValid.Count > 0)
                    return null;
            }
            var listTableNameInfo = new List<TableNameInfo>();
            //var listTableName = new List<string>();            
            XDocument xDocument = SampleXML.ToXDocument(xmlDocument);
            foreach (XObject obj in xDocument.DescendantNodes())
            {
                string referID = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(obj.ToString()));
                string xpath = obj.GetXPath();
                if (xpath.IndexOf("text()") != -1)
                {
                    var arrItem = xpath.Split('/');
                    string tableName = string.Empty;
                    for (int i = 1; i <= arrItem.Length - 3; i++)
                        tableName += $"{arrItem[i]}.";
                    tableName = Regex.Replace(tableName.Substring(0, tableName.Length - 1), @"[\[\]0-9]", "");
                    var arrTableName = tableName.Split(".");
                    if (arrTableName.Length > 3)
                    {
                        string nodeName = string.Empty;
                        if (tableName.IndexOf(".Item") != -1 && tableName.IndexOf(".item") != -1 && tableName.IndexOf(".ITEM") != -1)
                        {
                            for (int i = 1; i <= arrTableName.Length - 2; i++)
                                nodeName += $"N{i}.";
                            tableName = $"{arrTableName[0]}.{nodeName}{arrTableName[arrTableName.Length - 2]}.{arrTableName[arrTableName.Length - 1]}";
                        }
                        else
                        {
                            for (int i = 1; i <= arrTableName.Length - 1; i++)
                                nodeName += $"N{i}.";
                            tableName = $"{arrTableName[0]}.{arrTableName[arrTableName.Length - 1]}";
                        }

                    }
                    //string columnName = arrItem[arrItem.Length - 2];
                    //if (!listTableName.Contains(tableName) && tableName.IndexOf("CKyDTu") == -1)
                    //    listTableName.Add(tableName);
                    if (!listTableName.Contains(tableName))
                        listTableName.Add(tableName);
                    string columnName = arrItem[arrItem.Length - 2];
                    //columnName = columnName.Replace("[", "_").Replace("]", "");
                    columnName = Regex.Replace(columnName.Substring(0, columnName.Length - 1), @"[\[\]0-9]", "");
                    listTableNameInfo.Add(new TableNameInfo
                    {
                        TableName = tableName,
                        ColumName = columnName,
                        Value = xpath,
                        ReferID = referID
                    });
                }

            }
            return listTableNameInfo;
        }
        public static void ExtractXMLData(List<TableNameInfo> listTableNameInfo, List<string> listTableName, XmlDocument xmlDocument,
            ref Dictionary<string, List<string>> dictTableColumn, ref Dictionary<string, Dictionary<string, string>> dictTableColumnValue)
        {
            if (listTableNameInfo == null || listTableNameInfo.Count == 0)
                return;
            foreach (string tableName in listTableName)
            {
                var listSelect = listTableNameInfo.Where(x => x.TableName == tableName).ToList();
                if (listSelect != null && listSelect.Count > 0)
                {
                    List<string> listColumn = new List<string>();
                    Dictionary<string, string> listColumnValue = new Dictionary<string, string>();
                    foreach (var info in listSelect)
                    {
                        string getValueColumn = string.Empty;
                        var selectNodes = xmlDocument.SelectNodes(info.Value);
                        Console.WriteLine($"{info.ColumName}-{info.Value} : {selectNodes.Count}");
                        if (!listColumn.Contains(info.ColumName))
                            listColumn.Add(info.ColumName);
                        if (selectNodes != null && selectNodes.Count > 0)
                        {
                            getValueColumn = selectNodes[0].InnerText;
                            Console.WriteLine($"{tableName}-{info.ColumName} : {selectNodes[0].InnerText}");
                        }
                        listColumnValue.Add(info.Value, getValueColumn);

                    }
                    dictTableColumnValue.Add(tableName, listColumnValue);
                    dictTableColumn.Add(tableName, listColumn);
                }

            }
        }
        public static Dictionary<string, List<string>> CreateScriptTable(Dictionary<string, List<string>> dictTableColumn, string schema)
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var itemTableColumn in dictTableColumn)
            {
                List<string> listScript = new List<string>();
                string scriptColumn = string.Empty;
                foreach (var columnName in itemTableColumn.Value)
                {
                    scriptColumn += $"\"{columnName}\" VARCHAR(150)  NULL,";
                }
                string scriptDropTable = $"DROP TABLE IF EXISTS \"{schema}\".\"{itemTableColumn.Key}\";";
                listScript.Add(scriptDropTable);
                string scriptCreateTable = $"CREATE TABLE \"{schema}\".\"{itemTableColumn.Key}\" " +
                  $"(\r\n  \"ID\" VARCHAR(50) NOT NULL," +
                  $"\r\n  \"MSG_ID\" VARCHAR(50) NOT NULL," +
                  $"\r\n  \"TRAN_CODE\" VARCHAR(50) NOT NULL," +
                  $"\r\n  \"TRAN_NAME\" VARCHAR(250) NOT NULL," +
                  $"\r\nSQL_SCRIPT_COLUMN" +
                  $"\r\n  \"IS_DELETE\" INT2 NULL," +
                  $"\r\n  \"CDATE\" TIMESTAMP(6) NOT NULL," +
                  $"\r\n  \"LDATE\" TIMESTAMP(6)," +
                  $"\r\n  \"CUSER\" VARCHAR(50) NOT NULL," +
                  $"\r\n  \"LUSER\" VARCHAR(50) NULL\r\n);";
                scriptCreateTable = scriptCreateTable.Replace("SQL_SCRIPT_COLUMN", scriptColumn);
                listScript.Add(scriptCreateTable);
                string scriptCreatePrimary = $"ALTER TABLE \"{schema}\".\"{itemTableColumn.Key}\" ADD CONSTRAINT \"{itemTableColumn.Key}_pkey\" PRIMARY KEY (\"ID\");";
                listScript.Add(scriptCreatePrimary);
                result.Add(itemTableColumn.Key, listScript);
                //var resultExcuteScriptPrimary = DataObjectFactory.GetInstanceBaseObject(schema, connectionString).ExecuteScalarAsync(scriptCreatePrimary);
            }
            return result.Count > 0 ? result : null;
        }
        public static void ExcuteScriptTable(Dictionary<string, List<string>> dictScript, ref List<string> message)
        {
            //var listCurentTable = GetTableFromSchema(ConstantConfig.GPDB_EXTRACT_SCHEMA);
            //foreach (var itemDictScript in dictScript)
            //{
            //    try
            //    {
            //        if (!listCurentTable.Contains(itemDictScript.Key))
            //        {
            //            foreach (var itemScript in itemDictScript.Value)
            //            {
            //                DataObjectFactory.GetInstanceBaseObject(ConstantConfig.GPDB_EXTRACT_CONNECTTION_STRING).ExecuteScalarAsync(itemScript);
            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        message.Add($"{itemDictScript.Key}:{ex.Message}");
            //    }
            //}
        }
        //public static List<string> GetTableFromSchema(string schemaName)
        //{
        //    string sqlScript = $"SELECT table_name" +
        //       $" FROM information_schema.tables" +
        //       $" WHERE table_type = 'BASE TABLE'" +
        //       $" AND table_schema = @table_schema" +
        //       $" ORDER BY table_name; ";
        //    var dataTable = DataObjectFactory.GetInstanceBaseObject().GetDataBy(sqlScript, new Dictionary<string, object>() {
        //        {"table_schema",schemaName }
        //    }).Result;
        //    if (dataTable == null)
        //        return null;
        //    List<string> result = new List<string>();
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        result.Add(row["table_name"].ToString());
        //    }
        //    //List<string> result = ObjectConverter.ConvertDataTable<string>(dataTable);
        //    return result;
        //}
        public static Dictionary<string, List<Dictionary<string, object>>> CreateDataInsert(Dictionary<string, List<string>> dictTableColumn, Dictionary<string, Dictionary<string, string>> dictTableColumnValue)
        {
            var result = new Dictionary<string, List<Dictionary<string, object>>>();
            foreach (var itemTableColumn in dictTableColumn)
            {
                string schemaName = string.Empty;
                string tableName = itemTableColumn.Key;
                string strColumnName = string.Empty;
                List<string> listScript = new List<string>();
                Dictionary<string, object> paramValue = new Dictionary<string, object>();
                foreach (var columnName in itemTableColumn.Value)
                    paramValue.Add(columnName, null);
                List<Dictionary<string, object>> list_paramValue = new List<Dictionary<string, object>>();
                var checkItem = dictTableColumnValue[itemTableColumn.Key]
                .Where(item => item.Key.Contains("]/", StringComparison.OrdinalIgnoreCase))
                .ToList();
                if (checkItem != null && checkItem.Count > 0)
                {
                    foreach (var columnName in itemTableColumn.Value)
                    {
                        var checkItemValue = dictTableColumnValue[itemTableColumn.Key]
                        .Where(item => item.Key.EndsWith($"/{columnName}/text()", StringComparison.OrdinalIgnoreCase))
                        .ToList();
                        if (checkItemValue != null && checkItemValue.Count > 1)
                        {
                            for (int i = 1; i <= checkItemValue.Count; i++)
                            {
                                if (list_paramValue.Count < checkItemValue.Count)
                                    list_paramValue.Add(paramValue.CreateCopy());
                            }
                            for (int i = 1; i <= checkItemValue.Count; i++)
                            {
                                list_paramValue[i - 1][columnName] = checkItemValue[i - 1].Value;
                            }

                        }
                    }
                }
                else
                {
                    list_paramValue.Add(paramValue.CreateCopy());
                    foreach (var columnName in itemTableColumn.Value)
                    {
                        var filteredItemsColumn = dictTableColumnValue[itemTableColumn.Key]
                                .Where(item => item.Key.Contains($"/{columnName}/text()", StringComparison.OrdinalIgnoreCase))
                                .ToList();
                        if (filteredItemsColumn != null)
                        {
                            foreach (var item in filteredItemsColumn)
                                list_paramValue[0][columnName] = item.Value;
                        }
                    }
                }
                result.Add(tableName, list_paramValue);
            }
            return result;
        }

        public static List<string> ExcuteInsert(Dictionary<string, object> dictAppen, Dictionary<string, List<string>> dictTableColumn, Dictionary<string, Dictionary<string, string>> dictTableColumnValue)
        {
            List<string> list_message = new List<string>();
            var objectData = CreateDataInsert(dictTableColumn, dictTableColumnValue);
            if (objectData != null)
            {
                foreach (var item in objectData)
                {
                    string tableName = item.Key;
                    string message = string.Empty;
                    foreach (var itemValue in item.Value)
                    {
                        itemValue.Add("ID", Guid.NewGuid().ToString());
                        itemValue.Add("MSG_ID", dictAppen["MSG_ID"]);
                        itemValue.Add("TRAN_CODE", dictAppen["TRAN_CODE"]);
                        itemValue.Add("TRAN_NAME", dictAppen["TRAN_NAME"]);
                        itemValue.Add("IS_DELETE", dictAppen["IS_DELETE"]);
                        itemValue.Add("CDATE", DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
                        itemValue.Add("CUSER", dictAppen["CUSER"]);
                        //DataObjectFactory.GetInstanceBaseObject(ConstantConfig.DHUB_EXTRACT_CONNECTTION_STRING).Add(tableName, "ID", itemValue, ref message);
                        //if (message != string.Empty)
                        //    list_message.Add(message);
                    }
                }
            }
            return list_message;
        }
    }
}

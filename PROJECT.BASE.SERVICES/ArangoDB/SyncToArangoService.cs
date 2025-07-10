using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ArangoDBNetStandard;
using Microsoft.Extensions.Logging;
using System.Threading;
using ArangoDBNetStandard.Transport.Http;
using Lib.Setting;
using ArangoDBNetStandard.CollectionApi.Models;
using PROJECT.BASE.DAO;
using Lib.Utility;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using ArangoDBNetStandard.CursorApi.Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PROJECT.BASE.SERVICES
{
    public class ArangoDBConnector
    {
        private static ArangoDBClient arangoClient;
        private static readonly object _lock = new object();
        public static ArangoDBClient GetArangoDBConnector()
        {           
            ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;
            if (arangoClient != null)
                return arangoClient;

            lock (_lock)
            {
                if (arangoClient == null)
                {
                    string _arangoUrl = Config.CONFIGURATION_GLOBAL.ArangoDB.ArangoUrl;
                    string _database = Config.CONFIGURATION_GLOBAL.ArangoDB.Database;
                    string _userName = Config.CONFIGURATION_GLOBAL.ArangoDB.UserName;
                    string _passWord = Config.CONFIGURATION_GLOBAL.ArangoDB.PassWord;

                    var transport = HttpApiTransport.UsingBasicAuth(new Uri(_arangoUrl), _database, _userName, _passWord);
                    arangoClient = new ArangoDBClient(transport);

                    var dbInfo = arangoClient.Database.GetCurrentDatabaseInfoAsync();
                    Console.WriteLine($"ArangoDBClient Connected to: {dbInfo.Result.ToString()}");
                }
            }

            return arangoClient;

        }
        public static ArangoDBClient GetArangoDBConnector(string _database)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;
            if (arangoClient != null)
                return arangoClient;

            lock (_lock)
            {
                if (arangoClient == null)
                {
                    string _arangoUrl = Config.CONFIGURATION_GLOBAL.ArangoDB.ArangoUrl;
                    //string _database = Config.CONFIGURATION_GLOBAL.ArangoDB.Database;
                    string _userName = Config.CONFIGURATION_GLOBAL.ArangoDB.UserName;
                    string _passWord = Config.CONFIGURATION_GLOBAL.ArangoDB.PassWord;

                    var transport = HttpApiTransport.UsingBasicAuth(new Uri(_arangoUrl), _database, _userName, _passWord);
                    arangoClient = new ArangoDBClient(transport);

                    var dbInfo = arangoClient.Database.GetCurrentDatabaseInfoAsync();
                    Console.WriteLine($"ArangoDBClient Connected to: {dbInfo.Result.ToString()}");
                }
            }

            return arangoClient;

        }

    }
    public class SyncToArangoService
    {        
        public static async Task<bool> CreateCollection(string collectionName)
        {
            try
            {
                var existing = await ArangoDBConnector.GetArangoDBConnector().Collection.GetCollectionAsync(collectionName);
                if (existing != null && !string.IsNullOrEmpty(existing.Name))
                    return true;
                else 
                    return false;
            }
            catch (ApiErrorException ex)
            {
                if (ex.ApiError != null && ex.ApiError.ErrorNum == 1203) // 1203: collection not found
                {
                    // Collection chưa tồn tại → tạo mới
                    var createResult = await ArangoDBConnector.GetArangoDBConnector().Collection.PostCollectionAsync(
                        new PostCollectionBody
                        {
                            Name = collectionName
                        });
                    if (!string.IsNullOrEmpty(createResult.Id) && !string.IsNullOrEmpty(createResult.Name))
                        return true;
                    else { return false; }

                }
                return false;
            }
        }
        public static async Task Insert(string tableName, Dictionary<string, object> docDict)
        {
            await ArangoDBConnector.GetArangoDBConnector().Document.PostDocumentAsync($"{tableName}", docDict);
        }
        public static async Task InsertIfNotExists(string tableName, Dictionary<string, object> docDict)
        {
            string uniqueField = "ID";
            var uniqueValue = docDict[uniqueField]?.ToString();

            if (string.IsNullOrEmpty(uniqueValue))
                throw new ArgumentException($"Field {uniqueField} không tồn tại trong document.");
            var query = $@"
                            FOR doc IN {tableName}
                            FILTER doc.{uniqueField} == @value
                            RETURN doc
                        ";
            var cursor = await ArangoDBConnector.GetArangoDBConnector().Cursor.PostCursorAsync<Dictionary<string, object>>(
                    new PostCursorBody
                    {
                        Query = query,
                        BindVars = new Dictionary<string, object>
                        {
                            { "value", uniqueValue }
                        }
                    });

            if (cursor.Result != null && cursor.Count > 0)
                Console.WriteLine("Document đã tồn tại.");

            await Insert(tableName, docDict);
            //Console.WriteLine("Thêm mới document thành công.");

        }

        public static async Task Update(string tableName, Dictionary<string, object> docDict)
        {
            //var docDict = docData.ToObject<Dictionary<string, object>>();
            if (!docDict.ContainsKey("_key") && docDict.ContainsKey("ID"))
                docDict["_key"] = docDict["ID"].ToString();
            await ArangoDBConnector.GetArangoDBConnector().Document.PutDocumentAsync($"{tableName}", docDict["_key"].ToString(), docDict);
        }
        public static async Task UpdateIfNotExists(string tableName, Dictionary<string, object> docDict)
        {
            string uniqueField = "ID";
            var uniqueValue = docDict[uniqueField]?.ToString();
            if (!docDict.ContainsKey("_key") && docDict.ContainsKey(uniqueField))
                docDict["_key"] = uniqueValue;            

            if (string.IsNullOrEmpty(uniqueValue))
                throw new ArgumentException($"Field {uniqueField} không tồn tại trong document.");
            var query = $@"
                            FOR doc IN {tableName}
                            FILTER doc.{uniqueField} == @value
                            RETURN doc
                        ";
            var cursor = await ArangoDBConnector.GetArangoDBConnector().Cursor.PostCursorAsync<Dictionary<string, object>>(
                    new PostCursorBody
                    {
                        Query = query,
                        BindVars = new Dictionary<string, object>
                        {
                            { "value", uniqueValue }
                        }
                    });

            if (cursor.Result != null && cursor.Count > 0)
                await Update(tableName, docDict);
            else
                await Insert(tableName, docDict);
        }
        public static async Task Delete(string key, string tableName)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await ArangoDBConnector.GetArangoDBConnector().Document.DeleteDocumentAsync<object>($"{tableName}", key);
                //Console.WriteLine("Deleted document with key: " + key);
            }
        }
    }

}

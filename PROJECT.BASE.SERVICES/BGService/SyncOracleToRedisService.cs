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

namespace PROJECT.BASE.SERVICES
{
    public class SyncOracleToRedisService : BackgroundService
    {
        private readonly ILogger<SyncOracleToRedisService> _logger;
        private readonly ArangoDBClient _arangoClient;
        private readonly string _arangoUrl = Config.CONFIGURATION_GLOBAL.ArangoDB.ArangoUrl;
        private readonly string _database = Config.CONFIGURATION_GLOBAL.ArangoDB.Database;
        private readonly string _userName = Config.CONFIGURATION_GLOBAL.ArangoDB.UserName;
        private readonly string _passWord = Config.CONFIGURATION_GLOBAL.ArangoDB.PassWord;
        public SyncOracleToRedisService(ILogger<SyncOracleToRedisService> logger)
        {
            _logger = logger;
            if (_arangoClient == null)
            {
                var transport = HttpApiTransport.UsingBasicAuth(new Uri(_arangoUrl), _database, _userName, _passWord);
                _arangoClient = new ArangoDBClient(transport);
                var dbInfo = _arangoClient.Database.GetCurrentDatabaseInfoAsync();
                Console.WriteLine($"ArangoDBClient Connected to: {dbInfo.Result.ToString()}");
            }

        }
        public async Task<bool> CreateCollection(string collectionName)
        {
            try
            {
                var existing = await _arangoClient.Collection.GetCollectionAsync(collectionName);
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
                    var createResult = await _arangoClient.Collection.PostCollectionAsync(
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
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("|------ Sync Oracle -> Redis...");
                    string tableName = "ITG_MESSAGES";
                    //Create Collection                    
                    var existing = CreateCollection(tableName);
                    if (existing.Result)
                    {
                        var oracleData = GetDataAsync(tableName);
                        await _arangoClient.Document.PostDocumentAsync(tableName, oracleData);
                        //foreach (var row in oracleData.Result)
                        //{
                        //    await _arangoClient.Document.PostDocumentAsync(tableName, row);
                        //}

                        _logger.LogInformation("|------ Sync Oracle -> Redis success!");
                    }
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // delay 5 giây
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"|------ Sync Oracle -> Redis {ex.Message} ");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // retry chậm lại
                }
            }
        }

        private async Task<List<Dictionary<string, object>>> GetDataAsync(string tableName)
        {            
            var ds = DataObjectFactory.GetInstanceBaseObject(ConstantSQL.SHARE_OWNER).ExecuteQueryToDataSet($"SELECT * FROM {tableName} WHERE ID >= 0");
            var dataTable = ds.Tables[0];
            var result = Utility.ToDictionary(dataTable);           
            return result;
        }
        
    }

}

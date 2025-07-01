using Lib.Utility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Lib.Consul.Configuration;
using System.Text.RegularExpressions;
using System.Reflection;

namespace PROJECT.BASE.DAO
{
    public class DYNAMICDao
    {
        protected static IMongoClient Client = null;
        protected static IMongoDatabase Database = null;
        protected static string Storage = null;
        protected static int FindMaxTime = 0;
        public DYNAMICDao() { }
        public DYNAMICDao(string storage)
        {
            Storage = storage;
            FindMaxTime = int.Parse(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_FindMaxTime, storage)));
            if (CacheProvider.Exists(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_DatabaseName,storage)), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01)))
                Client = (MongoClient)CacheProvider.Get(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_DatabaseName, storage)), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            else
            {
                Client = new MongoClient(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_ConnectionString,storage)));
                CacheProvider.Add(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_DatabaseName, storage)), Client, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            }
            if (Client != null)
                Database = Client.GetDatabase(BaseSettings.Get(string.Format(ConstantConsul.DINAMIC_DatabaseName, storage)));

        }
        public async Task<List<COLLECTIONInfo>> DYNAMIC_GetListCollectionName()
        {
            try
            {
                List<COLLECTIONInfo> result = new List<COLLECTIONInfo>();
                foreach (BsonDocument collection in Database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
                {
                    var collectionName = Database.GetCollection<BsonDocument>(collection["name"].AsString);
                    result.Add(new COLLECTIONInfo()
                    {
                        NAME = collection["name"].AsString,
                        TYPE = collection["type"].AsString,
                        STORAGE = Storage,
                        TOTAL_RECORD = await collectionName.CountAsync(new BsonDocument())
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public List<string> DYNAMIC_GetListStorage()
        {
            try
            {
                var list_Storage = Regex.Split(BaseSettings.Get(ConstantConsul.Microservice_Internal_List_Storage), ",", RegexOptions.Compiled);
                return list_Storage.ToList();
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public List<string> DYNAMIC_GetListFieldName(string collectionName)
        {
            try
            {
                List<string> listData = new List<string>();
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                var result = collection.Find(new BsonDocument()).Skip(0).Limit(1).ToList();
                if (result != null && result.Count > 0)
                {
                    var dictData = BsonSerializer.Deserialize<Dictionary<string, object>>(result[0]);
                    if (dictData != null)
                    {
                        if (!dictData.ContainsKey("NO"))
                            listData.Add("NO");
                        foreach (var keyValue in dictData)
                            listData.Add(keyValue.Key);
                    }
                }
                else
                    listData.Add("_id");
                return listData;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        //var collection = Database.GetCollection<BsonDocument>(CollectionName);
        //return collection.Find(new BsonDocument()).Count();
        public async Task<long> DYNAMIC_GetTotalRecord(string collectionName)
        {
            try
            {
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                //var totalRecord = collection.Find(new BsonDocument()).Count();
                var totalRecord = await collection.CountAsync(new BsonDocument());
                return totalRecord;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return 0;
            }

        }
        public bool DYNAMIC_Insert(Dictionary<string, string> doc, string collectionName)
        {
            try
            {
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                collection.InsertOne(doc.ToBsonDocument());
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }
        }
        public bool DYNAMIC_InsertMany(List<Dictionary<string, string>> documents, string collectionName)
        {
            try
            {
                List<BsonDocument> docs = new List<BsonDocument>();
                for (int i = 0; i < documents.Count(); i++)
                {
                    var doc = documents[i].ToBsonDocument();
                    docs.Add(doc);
                }
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                collection.InsertMany(docs);
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }
        }
        public bool DYNAMIC_Update(KeyValuePair<string, string> keyValueFilter, Dictionary<string, string> doc, string collectionName)
        {
            try
            {
                FilterDefinition<BsonDocument> filter = Builder.FilterEq(keyValueFilter.Key, keyValueFilter.Value);
                foreach (var keyValue in doc)
                {
                    UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set(keyValue.Key, keyValue.Value);
                    var collection = Database.GetCollection<BsonDocument>(collectionName);
                    collection.UpdateOne(filter, update);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }

        }
        public bool DYNAMIC_Delete(string id, string collectionName)
        {
            try
            {
                var filter = Builder.FilterEq("_id", ObjectId.Parse(id));
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                var result = collection.DeleteOne(filter);
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }
        }
        public async Task<DYNAMICInfo<OrderedDictionary>> DYNAMIC_GetAllWithPagination(string collectionName, List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        {
            try
            {
                //BsonDocument filter = new BsonDocument();
                //if(listFilter != null)
                //{
                //    foreach (var info in listFilter)
                //    {
                //        if (info.DATA_TYPE.ToLower() == "bit")
                //        {
                //            if (info.FIELD_VALUE.ToString() != "-1")
                //                filter.Add(info.FIELD_NAME, new BsonBoolean(bool.Parse(info.FIELD_VALUE.ToString())));

                //        }
                //        if (info.DATA_TYPE.ToLower() == "int")
                //        {
                //            if (info.FIELD_VALUE.ToString() != "-1")
                //                filter.Add(info.FIELD_NAME, (int)info.FIELD_VALUE);

                //        }
                //        else
                //        {
                //            //su dung menh de like
                //            //filter.Add("CODE", new BsonRegularExpression("^.*" + _CODE + ".*$", "i"));
                //            if (info.FIELD_VALUE.ToString() != "-1")
                //                filter.Add(info.FIELD_NAME, info.FIELD_VALUE.ToString());
                //        }
                //    }
                //}
                //
                BsonDocument filter = new BsonDocument();
                if (listFilter != null)
                {
                    foreach (var info in listFilter)
                    {
                        if (info.FIELD_VALUE.ToString() != "-1")
                        {
                            switch (info.DATA_TYPE.ToLower())
                            {
                                case "bit":
                                    filter.Add(info.FIELD_NAME, new BsonBoolean(bool.Parse(info.FIELD_VALUE.ToString())));
                                    break;
                                case "int":
                                    filter.Add(info.FIELD_NAME, int.Parse(info.FIELD_VALUE.ToString()));
                                    break;
                                case "date":
                                    long[] arr_FIELD_VALUE = (long[])info.FIELD_VALUE;
                                    filter.Add("$and", new BsonArray()
                                            .Add(new BsonDocument().Add(info.FIELD_NAME, new BsonDocument().Add("$gt", new BsonInt64(arr_FIELD_VALUE[0]))))
                                            .Add(new BsonDocument().Add(info.FIELD_NAME, new BsonDocument().Add("$lt", new BsonInt64(arr_FIELD_VALUE[1]))))
                                    );
                                    break;
                                default:
                                    filter.Add(info.FIELD_NAME, new BsonRegularExpression(string.Format("^.*{0}.*$", info.FIELD_VALUE.ToString()), "i"));
                                    break;
                            }
                        }
                    }
                }
                var options = new FindOptions
                {
                    MaxTime = TimeSpan.FromMilliseconds(FindMaxTime)
                };
                //List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
                List<OrderedDictionary> returnList = new List<OrderedDictionary>();
                var collection = Database.GetCollection<BsonDocument>(collectionName);
                var totalRecord = await collection.CountAsync(filter);
                using (var cursor = await collection.Find(filter, options).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToCursorAsync())
                {
                    while (await cursor.MoveNextAsync())
                    {
                        int orderNumber = (pageIndex - 1) * pageSize + 1;
                        var batch = cursor.Current;
                        foreach (BsonDocument document in batch)
                        {
                            //OrderedDictionary myDict = new OrderedDictionary();
                            var item = BsonSerializer.Deserialize<OrderedDictionary>(document);
                            if (item.Contains("NO"))
                                item["NO"] = orderNumber++;
                            else
                                item.Insert(0, "NO", orderNumber++);
                            returnList.Add(item);

                            //var item = BsonSerializer.Deserialize<Dictionary<string, object>>(document);
                            //if (item.ContainsKey("NO"))
                            //    item["NO"] = orderNumber++;
                            //else
                            //    item.Add("NO", orderNumber++);
                            //returnList.Add(item);
                        }
                    }
                }
                return new DYNAMICInfo<OrderedDictionary>
                {
                    TOTAL_RECORD = totalRecord,
                    DATA = returnList

                };

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
    }
}

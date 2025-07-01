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
using Newtonsoft.Json;
using Lib.Utility;
using System.Collections;

namespace PROJECT.BASE.DAO
{
    public class BaseMongoDao
    {
        protected readonly IMongoClient Client;
        protected readonly IMongoDatabase Database;
        protected readonly string Storage;
        protected readonly int FindMaxTime = 0;
        protected readonly string ConnectionString;
        protected static Dictionary<string, Dictionary<string, string>> _DictConnectionString;
        public BaseMongoDao() { }
        public BaseMongoDao(string storage)
        {
            if (_DictConnectionString == null)
                _DictConnectionString = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(Config.CONFIGURATION_GLOBAL.Databases.MONGODB.ToString());
            Storage = storage;
            ConnectionString = _DictConnectionString[storage]["ConnectionString"];
            FindMaxTime = int.Parse(_DictConnectionString[storage]["FindMaxTime"]);
            if (Client == null)
                Client = new MongoClient(ConnectionString);
            if (Database == null)
                Database = Client.GetDatabase(storage);

        }
        public async Task<dynamic> GetListCollectionName()
        {
            List<COLLECTIONInfo> result = new List<COLLECTIONInfo>();
            var collectionsCursor = await Database.ListCollectionsAsync();
            var collections = await collectionsCursor.ToListAsync();
            foreach (var collection in collections)
            {
                var collectionName = Database.GetCollection<BsonDocument>(collection["name"].AsString);
                result.Add(new COLLECTIONInfo()
                {
                    NAME = collection["name"].AsString,
                    TYPE = collection["type"].AsString,
                    STORAGE = Storage,
                    TOTAL_RECORD = await collectionName.CountDocumentsAsync(new BsonDocument())
                });
            }
            return result;

        }
        public dynamic GetListStorage()
        {
            var list_Storage = Regex.Split(BaseSettings.Get(ConstantConsul.Microservice_Internal_List_Storage), ",", RegexOptions.Compiled);
            return list_Storage.ToList();
        }
        public async Task<dynamic> GetListFieldName(string collectionName)
        {
            List<string> listData = new List<string>();
            var collection = Database.GetCollection<BsonDocument>(collectionName);            
            var result = await collection.Find(new BsonDocument()).Limit(1).ToListAsync();
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
        public async Task<long?> GetTotalRecord(string collectionName)
        {
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            //var totalRecord = collection.Find(new BsonDocument()).Count();
            var totalRecord = await collection.CountAsync(new BsonDocument());
            return totalRecord;
        }
        public async Task<bool?> Insert(BsonDocument doc, string collectionName)
        {
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            await collection.InsertOneAsync(doc);
            return true;

        }
        public async Task<bool?> InsertMany(List<BsonDocument> documents, string collectionName)
        {
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            await collection.InsertManyAsync(documents);
            return true;
        }
        public async Task<bool?> Update(KeyValuePair<string, string> keyValueFilter, Dictionary<string, string> doc, string collectionName)
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
        public async Task<bool?> Delete(string id, string collectionName)
        {
            var filter = Builder.FilterEq("_id", ObjectId.Parse(id));
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            var result = collection.DeleteOne(filter);
            return true;
        }
        public async Task<dynamic> GetListPaging(string collectionName,List<DATA_FIELD> listFilter, int pageIndex, int pageSize)
        {
            BsonDocument filter = Builder.FilterEq(listFilter);
            List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            var totalRecord = await collection.CountAsync(filter);
            using (var cursor = await collection.Find(filter, new FindOptions { MaxTime = TimeSpan.FromMilliseconds(FindMaxTime) })
                .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
                .Sort(new BsonDocument("CDATE", -1)).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    int orderNumber = (pageIndex - 1) * pageSize + 1;
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        var item = BsonSerializer.Deserialize<Dictionary<string, object>>(document);
                        if (item.ContainsKey("NO"))
                            item["NO"] = orderNumber++;
                        else
                            item.Add("NO", orderNumber++);
                        returnList.Add(item);
                    }
                }
            }
            return new {
                TOTAL_RECORD = totalRecord,
                DATA = returnList
            };
        }
        //public async Task<dynamic> GetListPaging(string collectionName, List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        //{
        //    BsonDocument filter = new BsonDocument();
        //    if (listFilter != null)
        //    {
        //        foreach (var info in listFilter)
        //        {
        //            if (info.FIELD_VALUE.ToString() != "-1")
        //            {
        //                switch (info.DATA_TYPE.ToLower())
        //                {
        //                    case "bit":
        //                        filter.Add(info.FIELD_NAME, new BsonBoolean(bool.Parse(info.FIELD_VALUE.ToString())));
        //                        break;
        //                    case "int":
        //                        filter.Add(info.FIELD_NAME, int.Parse(info.FIELD_VALUE.ToString()));
        //                        break;
        //                    case "date":
        //                        long[] arr_FIELD_VALUE = (long[])info.FIELD_VALUE;
        //                        filter.Add("$and", new BsonArray()
        //                                .Add(new BsonDocument().Add(info.FIELD_NAME, new BsonDocument().Add("$gt", new BsonInt64(arr_FIELD_VALUE[0]))))
        //                                .Add(new BsonDocument().Add(info.FIELD_NAME, new BsonDocument().Add("$lt", new BsonInt64(arr_FIELD_VALUE[1]))))
        //                        );
        //                        break;
        //                    default:
        //                        filter.Add(info.FIELD_NAME, new BsonRegularExpression(string.Format("^.*{0}.*$", info.FIELD_VALUE.ToString()), "i"));
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    var options = new FindOptions {
        //        MaxTime = TimeSpan.FromMilliseconds(FindMaxTime)
        //    };
        //    List<OrderedDictionary> returnList = new List<OrderedDictionary>();
        //    var collection = Database.GetCollection<BsonDocument>(collectionName);
        //    var totalRecord = await collection.CountAsync(filter);
        //    using (var cursor = await collection.Find(filter, options).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToCursorAsync())
        //    {
        //        while (await cursor.MoveNextAsync())
        //        {
        //            int orderNumber = (pageIndex - 1) * pageSize + 1;
        //            var batch = cursor.Current;
        //            foreach (BsonDocument document in batch)
        //            {                        
        //                var item = BsonSerializer.Deserialize<OrderedDictionary>(document);
        //                if (item.Contains("NO"))
        //                    item["NO"] = orderNumber++;
        //                else
        //                    item.Insert(0, "NO", orderNumber++);
        //                returnList.Add(item);
        //            }
        //        }
        //    }
        //    return new
        //    {
        //        TOTAL_RECORD = totalRecord,
        //        DATA = returnList
        //    };
        //    //return new DYNAMICInfo<OrderedDictionary>
        //    //{
        //    //    TOTAL_RECORD = totalRecord,
        //    //    DATA = returnList
        //    //};
        //}
    }
}

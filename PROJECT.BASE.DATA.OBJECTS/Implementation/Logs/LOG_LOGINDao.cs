using Lib.Consul.Configuration;
using Lib.Utility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PROJECT.BASE.DAO
{
    public class LOG_LOGINDao : MongoBaseImpl<LOG_LOGINInfo>
    {
        protected static int FindMaxTime = 0;
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "LOG_LOGIN";
            FindMaxTime = int.Parse(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_FindMaxTime));
            if (CacheProvider.Exists(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_DatabaseName), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01)))
                Client = (MongoClient)CacheProvider.Get(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_DatabaseName), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            else
            {
                Client = new MongoClient(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_ConnectionString));
                CacheProvider.Add(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_DatabaseName), Client, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            }
            if (Client != null)
                Database = Client.GetDatabase(BaseSettings.Get(ConstantConsul.MSB_NHDT_INTERNAL_DatabaseName));

        }

        #region relationship support

        #endregion
        public bool LOG_LOGIN_Update(LOG_LOGINInfo info)
        {            
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            var filter = Builder.FilterEq("CODE", info.CODE);
            var result = this.UpdateOne(filter,
                Builders<BsonDocument>.Update
                      .Set("REALMS", info.REALMS)
                      .Set("CLIENT_ID", info.CLIENT_ID)
                      .Set("USER_NAME", info.USER_NAME)
                      .Set("AUTH_TIME", info.AUTH_TIME)
                      .Set("LUSER", info.LUSER)
                      .Set("LDATE", info.LDATE));
            return result;

        }
        public bool LOG_LOGIN_Delete(string _CODE)
        {
            var filter = Builder.FilterEq("CODE", _CODE);
            var result = this.Delete(filter);
            return result;

        }
        #region Get List              
        public LOG_LOGINInfo LOG_LOGIN_GetInfo(string _CODE)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("CODE", _CODE);
            var result = this.Select<LOG_LOGINInfo>(filter);
            if (result != null && result.Count > 0)
                return result[0];
            return null;

        }
        public LOG_LOGINInfo LOG_LOGIN_GetInfo(string _USER_ID,long _AUTH_TIME)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("USER_ID", _USER_ID);
            filter.Add("AUTH_TIME", _AUTH_TIME);
            var result = this.Select<LOG_LOGINInfo>(filter);
            if (result != null && result.Count > 0)
                return result[0];
            return null;

        }
        public List<LOG_LOGINInfo> LOG_LOGIN_GetList()
        {
            BsonDocument filter = new BsonDocument();
            var result = this.Select<LOG_LOGINInfo>(filter);
            return result;

        }
        //su dung menh de like
        //filter.Add("CODE", new BsonRegularExpression("^.*" + _CODE + ".*$", "i"));
        //public async Task<DYNAMICInfo<Dictionary<string, object>>> LOG_LOGIN_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        //{
        //    BsonDocument filter = new BsonDocument();
        //    foreach (var info in listFilter)
        //    {
        //        if (info.DATA_TYPE.ToLower() == "bit")
        //            filter.Add(info.FIELD_NAME, new BsonBoolean(bool.Parse(info.FIELD_VALUE.ToString())));
        //        if (info.DATA_TYPE.ToLower() == "int")
        //            filter.Add(info.FIELD_NAME, (int)info.FIELD_VALUE);
        //        else
        //            filter.Add(info.FIELD_NAME, info.FIELD_VALUE.ToString());
        //    }
        //    List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
        //    var collection = Database.GetCollection<BsonDocument>(CollectionName);
        //    var totalRecord = await collection.CountAsync(filter);
        //    using (var cursor = await collection.Find(filter, new FindOptions { MaxTime = TimeSpan.FromMilliseconds(FindMaxTime) })
        //        .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
        //        .Sort(new BsonDocument("CDATE", -1)).ToCursorAsync())
        //    {
        //        while (await cursor.MoveNextAsync())
        //        {
        //            int orderNumber = (pageIndex - 1) * pageSize + 1;
        //            var batch = cursor.Current;
        //            foreach (BsonDocument document in batch)
        //            {
        //                var item = BsonSerializer.Deserialize<Dictionary<string, object>>(document);
        //                if (item.ContainsKey("NO"))
        //                    item["NO"] = orderNumber++;
        //                else
        //                    item.Add("NO", orderNumber++);
        //                returnList.Add(item);
        //            }
        //        }
        //    }
        //    return new DYNAMICInfo<Dictionary<string, object>>
        //    {
        //        TOTAL_RECORD = totalRecord,
        //        DATA = returnList

        //    };

        //}
        public async Task<DYNAMICInfo<Dictionary<string, object>>> LOG_LOGIN_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        {
            BsonDocument filter = new BsonDocument();
            foreach (var info in listFilter)
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
            List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
            var collection = Database.GetCollection<BsonDocument>(CollectionName);
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
            return new DYNAMICInfo<Dictionary<string, object>>
            {
                TOTAL_RECORD = totalRecord,
                DATA = returnList

            };
        }
        #endregion

    }
}

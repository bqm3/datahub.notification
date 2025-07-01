using Lib.Utility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Consul.Configuration;
using System.Reflection;

namespace PROJECT.BASE.DAO
{
    public class CONFIGURATION_FIELDDao : MongoBaseImpl<CONFIGURATION_FIELDInfo>
    {
        protected static int FindMaxTime = 0;
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "CONFIGURATION_FIELD";
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

        public bool CONFIGURATION_FIELD_Update(CONFIGURATION_FIELDInfo info)
        {
            try
            {
                info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.LUSER = Config.CONFIGURATION_PRIVATE.Infomation.Service;
                var filter = Builder.FilterEq("CODE", info.CODE);                
                var result = this.UpdateOne(filter,
                    Builders<BsonDocument>.Update
                          .Set("CODE", info.CODE)
                          .Set("STORAGE", info.STORAGE)
                          .Set("COLLECTIONS", info.COLLECTIONS)
                          .Set("FIELD_NAME", info.FIELD_NAME)                          
                          .Set("FIELD_VIEW", info.FIELD_VIEW)
                          .Set("FIELD_SEARCH", info.FIELD_SEARCH)
                          .Set("CUSER", info.CUSER)
                          .Set("CDATE", info.CDATE)
                          .Set("LUSER", info.LUSER)
                          .Set("LDATE", info.LDATE));
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }

        }


        #region Get List
        public CONFIGURATION_FIELDInfo CONFIGURATION_FIELD_GetInfo(string _CODE)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("CODE", _CODE);
                var result = this.Select<CONFIGURATION_FIELDInfo>(filter);
                if (result != null && result.Count > 0)
                    return result[0];
                return null;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public CONFIGURATION_FIELDInfo CONFIGURATION_FIELD_GetInfo(string _STORAGE,string _COLLECTIONS, string _FIELD_NAME)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("STORAGE", _STORAGE);
                filter.Add("COLLECTIONS", _COLLECTIONS);
                filter.Add("FIELD_NAME", _FIELD_NAME);
                var result = this.Select<CONFIGURATION_FIELDInfo>(filter);
                if (result != null && result.Count > 0)
                    return result[0];
                return null;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }

        public List<CONFIGURATION_FIELDInfo> CONFIGURATION_FIELD_GetList(string _STORAGE, string _COLLECTIONS)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("STORAGE", _STORAGE);
                filter.Add("COLLECTIONS", _COLLECTIONS);
                var result = this.Select<CONFIGURATION_FIELDInfo>(filter);
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public List<CONFIGURATION_FIELDInfo> CONFIGURATION_FIELD_GetList()
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                var result = this.Select<CONFIGURATION_FIELDInfo>(filter);
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public async Task<DYNAMICInfo<Dictionary<string, object>>> CONFIGURATION_FIELD_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                foreach (var info in listFilter)
                {
                    if (info.DATA_TYPE.ToLower() == "bit")
                        filter.Add(info.FIELD_NAME, new BsonBoolean(bool.Parse(info.FIELD_VALUE.ToString())));
                    if (info.DATA_TYPE.ToLower() == "int")
                        filter.Add(info.FIELD_NAME, (int)info.FIELD_VALUE);
                    else
                        filter.Add(info.FIELD_NAME, info.FIELD_VALUE.ToString());
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
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public List<CONFIGURATION_FIELDInfo> CONFIGURATION_FIELD_GetAllWithPadding(string _CODE, string _STORAGE, string _COLLECTIONS,string _FIELD_NAME, bool _FIELD_VIEW, bool _FIELD_SEARCH, int pageIndex, int pageSize, ref int totalRecord)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("CODE", ""))
                      .Add(new BsonDocument().Add("CODE", _CODE))
                );
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("STORAGE", ""))
                      .Add(new BsonDocument().Add("STORAGE", _STORAGE))
                );
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("COLLECTIONS", ""))
                      .Add(new BsonDocument().Add("COLLECTIONS", _COLLECTIONS))
                );
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("FIELD_NAME", ""))
                      .Add(new BsonDocument().Add("FIELD_NAME", _FIELD_NAME))
                );
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("FIELD_SEARCH", new BsonInt64(-1L)))
                      .Add(new BsonDocument().Add("FIELD_SEARCH", new BsonBoolean(_FIELD_SEARCH)))
                );
                filter.Add("$or", new BsonArray()
                      .Add(new BsonDocument().Add("FIELD_VIEW", new BsonInt64(-1L)))
                      .Add(new BsonDocument().Add("FIELD_VIEW", new BsonBoolean(_FIELD_VIEW)))
                );
                var result = this.Select<CONFIGURATION_FIELDInfo>(filter, pageIndex, pageSize, ref totalRecord);
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }

        #endregion


    }
}

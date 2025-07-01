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
    public class AMS_FUNCTIONSDao : MongoBaseImpl<AMS_FUNCTIONSInfo>
    {
        protected static int FindMaxTime = 0;        
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "AMS_FUNCTIONS";
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
        public bool AMS_FUNCTIONS_Insert(AMS_FUNCTIONSInfo info)
        {
            try
            {
                info.PARENT_CODE = info.PARENT_CODE == "-1" ? "0" : info.PARENT_CODE;
                info.CDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.CUSER = Config.CONFIGURATION_PRIVATE.Infomation.Service;
                var result = this.Insert(info);
                List<AMS_FUNCTIONSInfo> listData = new List<AMS_FUNCTIONSInfo>();
                listData.Add(info);                
                return result;

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }

        }
        public bool AMS_FUNCTIONS_Update(AMS_FUNCTIONSInfo info)
        {
            try
            {
                info.PARENT_CODE = info.PARENT_CODE == "-1" ? "0" : info.PARENT_CODE;
                info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.LUSER = Config.CONFIGURATION_PRIVATE.Infomation.Service;
                var filter = Builder.FilterEq("CODE", info.CODE);
                var result = this.UpdateOne(filter,
                    Builders<BsonDocument>.Update
                          .Set("CODE", info.CODE)
                          .Set("FUNCTION_NAME", info.FUNCTION_NAME)
                          .Set("FUNCTION_ICON", info.FUNCTION_ICON)
                          .Set("FUNCTION_URL", info.FUNCTION_URL)
                          .Set("PARENT_CODE", info.PARENT_CODE)
                          .Set("DESCRIPTION", info.DESCRIPTION)
                          .Set("INCLUDE_MENU", info.INCLUDE_MENU)
                          .Set("STATUS", info.STATUS)
                          .Set("IS_DELETE", info.IS_DELETE)
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
        public bool AMS_FUNCTIONS_Delete(string _CODE)
        {
            try
            {
                var filter = Builder.FilterEq("CODE", _CODE);
                var result = this.Delete(filter);
                return result;

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }

        }
        #region Get List
        public AMS_FUNCTIONSInfo AMS_FUNCTIONS_GetInfo(string _CODE)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("CODE", _CODE);
                var result = this.Select<AMS_FUNCTIONSInfo>(filter);
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
        public List<AMS_FUNCTIONSInfo> AMS_FUNCTIONS_GetByPARENT_CODE(string _PARENT_CODE)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("PARENT_CODE", _PARENT_CODE);
                var result = this.Select<AMS_FUNCTIONSInfo>(filter);
                return result;

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public List<AMS_FUNCTIONSInfo> AMS_FUNCTIONS_GetBySTATUS(int _STATUS)
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("STATUS", _STATUS);
                var result = this.Select<AMS_FUNCTIONSInfo>(filter);
                return result;

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }        
        public List<AMS_FUNCTIONSInfo> AMS_FUNCTIONS_GetList()
        {
            try
            {
                BsonDocument filter = new BsonDocument();
                var result = this.Select<AMS_FUNCTIONSInfo>(filter);
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public async Task<DYNAMICInfo<Dictionary<string, object>>> AMS_FUNCTIONS_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
        {
            try
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
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }

        #endregion


    }
}

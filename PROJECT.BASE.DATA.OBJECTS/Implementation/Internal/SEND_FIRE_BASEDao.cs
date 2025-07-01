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
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.BASE.DAO
{
    public class SEND_FIRE_BASEDao : MongoBaseImpl<SEND_FIRE_BASEInfo>
    {
        protected static int FindMaxTime = 0;
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "SEND_FIRE_BASE";
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
        public bool SEND_FIRE_BASE_Update(SEND_FIRE_BASEInfo info)
        {
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            var filter = Builder.FilterEq("CODE", info.CODE);
            var result = this.UpdateOne(filter,
                Builders<BsonDocument>.Update
                      .Set("IS_READ", info.IS_READ)
                      .Set("LUSER", info.LUSER)
                      .Set("LDATE", info.LDATE));
            return result;

        }

        #region "Sonct Update all Send Fire Base <used for >"

        public bool SEND_FIRE_BASE_UpdateList(UpdateIsReadModel obj)
        {

            var oLDATE = Utility.ConvertToUnixTime(DateTime.Now);
            var oLUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            var filter = Builder.FilterEq("USER_ID", obj.UserId) & Builder.FilterEq("IS_READ", false);
            var result = this.UpdateMany(filter,
                Builders<BsonDocument>.Update
                      .Set("IS_READ", true)
                      .Set("LUSER", oLUSER)
                      .Set("LDATE", oLDATE));

            return result;

        }

        #endregion


        public bool Delete(string _CODE)
        {        
            var filter = Builder.FilterEq("CODE", _CODE);
            var result = this.Delete(filter);
            return result;

        }
        #region Get List             
        public SEND_FIRE_BASEInfo SEND_FIRE_BASE_GetInfo(string _CODE)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("CODE", _CODE);
            var result = this.Select<SEND_FIRE_BASEInfo>(filter);
            if (result != null && result.Count > 0)
                return result[0];
            return null;

        }

        public List<SEND_FIRE_BASEInfo> SEND_FIRE_BASE_GetList(int _STATUS)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("STATUS", _STATUS);
            var result = this.Select<SEND_FIRE_BASEInfo>(filter);
            return result;

        }
        public List<SEND_FIRE_BASEInfo> SEND_FIRE_BASE_GetList(string UserId)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("USER_ID", UserId);
            var result = this.Select<SEND_FIRE_BASEInfo>(filter).OrderByDescending(c => c.CDATE).ToList();
            return result;
        }

        public List<SEND_FIRE_BASEInfo> SEND_FIRE_BASE_GetListByUserId(string UserId, bool IsRead = false)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("USER_ID", UserId);
            filter.Add("IS_READ", IsRead);
            var result = this.Select<SEND_FIRE_BASEInfo>(filter).OrderByDescending(c => c.CDATE).ToList();
            return result;
        }

        public List<SEND_FIRE_BASEInfo> SEND_FIRE_BASE_GetList()
        {
            BsonDocument filter = new BsonDocument();
            var result = this.Select<SEND_FIRE_BASEInfo>(filter);
            return result;

        }
        //su dung menh de like
        //filter.Add("CODE", new BsonRegularExpression("^.*" + _CODE + ".*$", "i"));
        public async Task<DYNAMICInfo<Dictionary<string, object>>> SEND_FIRE_BASE_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
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

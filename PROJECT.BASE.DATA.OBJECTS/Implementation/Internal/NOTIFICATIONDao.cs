﻿using Lib.Consul.Configuration;
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
    public class NOTIFICATIONDao : MongoBaseImpl<NOTIFICATIONInfo>
    {
        protected static int FindMaxTime = 0;
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "NOTIFICATION";
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
        public bool NOTIFICATION_Update(NOTIFICATIONInfo info)
        {
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            var filter = Builder.FilterEq("CODE", info.CODE);
            var result = this.UpdateOne(filter,
                Builders<BsonDocument>.Update
                      .Set("FROM", info.FROM)
                      .Set("TO_CHANNEL", info.TO_CHANNEL)
                      .Set("MESSAGE", info.MESSAGE)
                      .Set("TIMESTAMP", info.TIMESTAMP)
                      .Set("STATUS", info.STATUS)
                      .Set("CUSER", info.CUSER)
                      .Set("CDATE", info.CDATE)
                      .Set("LUSER", info.LUSER)
                      .Set("LDATE", info.LDATE));
            return result;

        }
        public bool NOTIFICATION_Delete(string _CODE)
        {
            var filter = Builder.FilterEq("CODE", _CODE);
            var result = this.Delete(filter);
            return result;

        }
        #region Get List       
        public Dictionary<string,string> NOTIFICATION_GetListChannel()
        {
            var jsonNode = Config.CONFIGURATION_PRIVATE.AppSetting.NotificationChannel;
            var result = BsonSerializer.Deserialize<Dictionary<string, string>>(jsonNode.ToString());
            return result;
        }
        public NOTIFICATIONInfo NOTIFICATION_GetInfo(string _CODE)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("CODE", _CODE);
            var result = this.Select<NOTIFICATIONInfo>(filter);
            if (result != null && result.Count > 0)
                return result[0];
            return null;

        }
        public List<NOTIFICATIONInfo> NOTIFICATION_GetList(int _STATUS)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("STATUS", _STATUS);
            var result = this.Select<NOTIFICATIONInfo>(filter);
            return result;

        }
        public List<NOTIFICATIONInfo> NOTIFICATION_GetList()
        {
            BsonDocument filter = new BsonDocument();
            var result = this.Select<NOTIFICATIONInfo>(filter);
            return result;

        }
        //su dung menh de like
        //filter.Add("CODE", new BsonRegularExpression("^.*" + _CODE + ".*$", "i"));
        public async Task<DYNAMICInfo<Dictionary<string, object>>> NOTIFICATION_GetAllWithPadding(List<DATA_FIELDInfo> listFilter, int pageIndex, int pageSize)
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

        public dynamic NOTIFICATION_SendGateway(MessageInfo info)
        {
            string GatewayUrl = Config.CONFIGURATION_PRIVATE.AppSetting.Notification.GatewayUrl;
            string Realms = Config.CONFIGURATION_PRIVATE.AppSetting.Notification.Realms;
            string UserID = Config.CONFIGURATION_PRIVATE.AppSetting.Notification.UserID;
            var dictData = HttpClientBase.GET_TO_INFO(string.Format("{0}/Identity/GetToken/{1}/{2}", GatewayUrl, Realms, UserID), string.Empty,string.Empty);
            var access_token = dictData.ContainsKey("ReturnValue") ? dictData["ReturnValue"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(access_token))
            {
                access_token = access_token.ToString().Replace("Bearer ", string.Empty);
                var result = HttpClientBase.POST(info, string.Format("{0}/Notification", GatewayUrl), access_token,string.Empty);
                return result;
            }
            return default(dynamic);
        }

    }
}

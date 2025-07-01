using Lib.Consul.Configuration;
using Lib.Setting;
using Lib.Utility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PROJECT.BASE.DAO
{
    public class WIDGET_CHARTDao : MongoBaseImpl<WIDGET_CHART>
    {
        protected static int FindMaxTime = 0;
        protected override void SetInfoDerivedClass()
        {
            CollectionName = "WIDGET_CHART";            
			FindMaxTime = int.Parse(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_FindMaxTime));
            if (CacheProvider.Exists(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_DatabaseName), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01)))
                Client = (MongoClient)CacheProvider.Get(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_DatabaseName), Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            else
            {
                Client = new MongoClient(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_ConnectionString));
                CacheProvider.Add(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_DatabaseName), Client, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.INSTANCE_DATA_01));
            }
            if (Client != null)
                Database = Client.GetDatabase(BaseSettings.Get(ConstantConsul.MSB_NHDT_REPORT_DatabaseName));
        }

        #region relationship support

        #endregion           
        public bool Update(WIDGET_CHART info)
        {
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = info.LUSER == null ? Config.CONFIGURATION_PRIVATE.Infomation.Service : info.LUSER;
            var filter = Builder.FilterEq("CODE", info.CODE);
            var result = this.UpdateOne(filter,
                Builders<BsonDocument>.Update

                      .Set("SYSTEM_CODE", info.SYSTEM_CODE)
                      .Set("SERVICE_CODE", info.SERVICE_CODE)
                      .Set("SERVICE_NAME", info.SERVICE_NAME)
                      .Set("SERVICE_TYPE", info.SERVICE_TYPE)
                      .Set("ICON_CLASS", info.ICON_CLASS)
                      .Set("COLOR_CLASS", info.COLOR_CLASS)
                      .Set("REPORT_TYPE", info.REPORT_TYPE)
                      .Set("VALUES", info.VALUES)
                      .Set("DATE", info.DATE)                      
                      .Set("LUSER", info.LUSER)
                      .Set("LDATE", info.LDATE));
            return result;
        }
        public bool Delete(string CODE)
        {
            var filter = Builder.FilterEq("CODE", CODE);
            var result = this.UpdateOne(filter,
                Builders<BsonDocument>.Update
                      .Set("ISDELETE", true));
            return result;

        }
        #region Get List              
        public WIDGET_CHART GetInfo(string CODE)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("CODE", CODE);
            var result = this.Select<WIDGET_CHART>(filter);
            if (result != null && result.Count > 0)
                return result[0];
            return null;

        }
        public List<WIDGET_CHART> GetList()
        {
            BsonDocument filter = new BsonDocument();
            var result = this.Select<WIDGET_CHART>(filter);
            return result;

        }
        public List<WIDGET_CHART> GetList(string _SYSTEM_CODE, string _SERVICE_CODE, string _SERVICE_TYPE, string _REPORT_TYPE, string _DATE_START, string _DATE_END)
        {
            List<DATA_FIELD> listFilter = new List<DATA_FIELD>();
            if (!string.IsNullOrEmpty(_SYSTEM_CODE))
                listFilter.Add(new DATA_FIELD { FIELD_NAME = "SYSTEM_CODE", FIELD_VALUE = _SYSTEM_CODE, DATA_TYPE = "$string" });
            if (!string.IsNullOrEmpty(_SERVICE_CODE))
                listFilter.Add(new DATA_FIELD { FIELD_NAME = "SERVICE_CODE", FIELD_VALUE = _SERVICE_CODE, DATA_TYPE = "$string" });
            if (!string.IsNullOrEmpty(_SERVICE_TYPE))
                listFilter.Add(new DATA_FIELD { FIELD_NAME = "SERVICE_TYPE", FIELD_VALUE = _SERVICE_TYPE, DATA_TYPE = "$string" });
            if (!string.IsNullOrEmpty(_REPORT_TYPE))
                listFilter.Add(new DATA_FIELD { FIELD_NAME = "REPORT_TYPE", FIELD_VALUE = _REPORT_TYPE, DATA_TYPE = "$string" });
            if (!string.IsNullOrEmpty(_DATE_START) && !string.IsNullOrEmpty(_DATE_END))
            {
                long[] arrFIELD_VALUE = { long.Parse(_DATE_START), long.Parse(_DATE_END) };
                listFilter.Add(new DATA_FIELD { FIELD_NAME = "DATE", FIELD_VALUE = arrFIELD_VALUE, DATA_TYPE = "$gte-long-range" });
            }
            BsonDocument filter = Builder.FilterEq(listFilter);
            var result = this.Select<WIDGET_CHART>(filter);
            return result;

        }
        public async Task<DYNAMICInfo<Dictionary<string, object>>> GetAllWithPadding(List<DATA_FIELD> listFilter, int pageIndex, int pageSize)
        {
            BsonDocument filter = Builder.FilterEq(listFilter); 
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

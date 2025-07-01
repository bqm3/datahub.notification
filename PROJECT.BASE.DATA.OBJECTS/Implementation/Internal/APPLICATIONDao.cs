using Lib.Utility;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Reflection;

namespace PROJECT.BASE.DAO
{
    public class APPLICATIONDao
    {       
        public APPLICATIONDao() { }       
        public List<APPLICATIONInfo> APPLICATION_GetList()
        {
            try
            {
                List<APPLICATIONInfo> result = new List<APPLICATIONInfo>();                
                var dictionary = RedisCacheProvider.GetData<Dictionary<string, APPLICATIONInfo>>(Config.CONFIGURATION_PRIVATE.AppSetting.RedisConfig.GlobalKeyRedisApplication);
                if (dictionary == null)
                    return null;
                foreach(var item in dictionary)
                    result.Add(item.Value);                
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public APPLICATIONInfo APPLICATION_GetInfo(string appName)
        {
            try
            {                
                var dictionary = RedisCacheProvider.GetData<Dictionary<string, APPLICATIONInfo>>(Config.CONFIGURATION_PRIVATE.AppSetting.RedisConfig.GlobalKeyRedisApplication);
                if (dictionary.ContainsKey(appName))
                    return dictionary[appName];                
                return null;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public bool APPLICATION_SetStatus(SERVICE_STATUSInfo info)
        {
            try
            {
                string AppSetting_ApiShell = Config.CONFIGURATION_PRIVATE.AppSetting.ApiShell;
                string AppSetting_DirectoryBashShell = Config.CONFIGURATION_PRIVATE.AppSetting.DirectoryBashShell;
                string shellCommand = string.Format(AppSetting_DirectoryBashShell + "/status-service.sh {0} {1}", info.SERVICE_NAME, info.STATUS);
                LogEventError.LogEvent("shellCommand : " + shellCommand);
                var httpContent = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>() {
                                            { "BashScript", shellCommand}
                                        }), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var httpResponse = client.PostAsync(AppSetting_ApiShell, httpContent).Result;
                if (httpResponse.Content == null)
                    LogEventError.LogEvent("Content Null");
                else
                {
                    var jsonResponse = httpResponse.Content.ReadAsStringAsync();
                    LogEventError.LogEvent("POST : " + jsonResponse.Result.ToString());                    
                }
                LogEventError.LogEvent("Excute : " + shellCommand);
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return false;
            }

        }        
        public List<CONFIGURATIONInfo> CONFIGURATION_GetList(string appName)
        {
            try
            {
                List<CONFIGURATIONInfo> result = new List<CONFIGURATIONInfo>();
                var dictionary = RedisCacheProvider.GetData<Dictionary<string, Dictionary<string, CONFIGURATIONInfo>>>(Config.CONFIGURATION_PRIVATE.AppSetting.RedisConfig.GlobalKeyRedisConfiguretionSetting);
                if (dictionary == null)
                    return null;
                if (dictionary.ContainsKey(appName))
                {
                    foreach (var item in dictionary[appName])
                    {
                        result.Add(item.Value);
                    }                   
                }                            
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
        public CONFIGURATIONInfo CONFIGURATION_GetInfo(string appName,string configName)
        {
            try
            {                
                var dictionary = RedisCacheProvider.GetData<Dictionary<string, Dictionary<string, CONFIGURATIONInfo>>>(
                    Config.CONFIGURATION_PRIVATE.AppSetting.RedisConfig.GlobalKeyRedisConfiguretionSetting);
                if (dictionary == null)
                    return null;
                if (dictionary.ContainsKey(appName))
                {
                    CONFIGURATIONInfo info = dictionary[appName][configName];
                    if (info != null)
                        return info;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }

    }
}

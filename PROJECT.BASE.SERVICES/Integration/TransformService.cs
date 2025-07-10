using Lib.Setting;
using Lib.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace PROJECT.BASE.SERVICES
{
    public class TransformService
    {
        public TransformService() { }
        /// <summary>
        /// STEP_5 - Transform
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<bool> TransformTrans(IntegrationRequest request,string creator,string databaseName)
        {
            try
            {
                string tran_code = request.Header.Tran_Code;
                var xmlDocumet = StringConvert.StringToXml(StringConvert.DecodeStringFromBase64(request.Body.Content));
                if (xmlDocumet == null)
                {
                    IntegrationService.ITG_MSGO_Activitie(request,short.Parse(Constant.RETURN_CODE_WARNING),
                        $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[xmlDocumet == null]", creator);
                    return false;
                }
                var dictObject = (Dictionary<string, dynamic>)CacheProvider.Get(tran_code, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_TRANSFORM));
                if (dictObject == null)
                {                    
                    IntegrationService.ITG_MSGO_Activitie(request,short.Parse(Constant.RETURN_CODE_WARNING),
                        $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[dictObject == null]", creator);
                    return false;
                }
                foreach (var keyValueObject in dictObject)
                {
                    string tableName = keyValueObject.Key.ToString();
                    string primaryKey = string.Empty;
                    Dictionary<string, dynamic> dictTableName = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(keyValueObject.Value.ToString());
                    if (!dictTableName.ContainsKey("ItemData"))
                    {
                        Dictionary<string, object> paramValue = IntegrationService.GetParamValue(null, creator, xmlDocumet, dictTableName, ref primaryKey);
                        string message = string.Empty;                       
                        var result = DataObjectFactory.GetInstanceBaseObject(tableName, $"PK_{tableName}", databaseName).AddDictObject(paramValue);
                        message = !string.IsNullOrEmpty(message) ?
                            $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[{tableName}]-{message}" :
                            $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[{tableName}]";
                        IntegrationService.ITG_MSGO_Activitie(request,
                            result != null ? short.Parse(Constant.RETURN_CODE_SUCCESS) : short.Parse(Constant.RETURN_CODE_WARNING),
                            $"{message}",
                            creator);
                    }
                    else
                    {
                        object result =null;
                        string message = string.Empty;
                        dictTableName = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(keyValueObject.Value.ItemData.Item.ToString());
                        string xpathItem = keyValueObject.Value.ItemData.XpathItem.ToString();
                        XmlNodeList xmlNodeList = xmlDocumet.SelectNodes(xpathItem);
                        for (int i = 1; i <= xmlNodeList.Count; i++)
                        {
                            Dictionary<string, object> paramValue = IntegrationService.GetParamValue(i, creator, xmlDocumet, dictTableName, ref primaryKey);                           
                            result = DataObjectFactory.GetInstanceBaseObject(tableName, $"PK_{tableName}", databaseName).AddDictObject(paramValue);
                            if (result == null)
                                break;
                        }
                        message = !string.IsNullOrEmpty(message) ?
                           $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[{tableName}]-{message}" :
                           $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[{tableName}]";
                        IntegrationService.ITG_MSGO_Activitie(request,
                            result == null ? short.Parse(Constant.RETURN_CODE_WARNING) : short.Parse(Constant.RETURN_CODE_SUCCESS),
                            $"{message}",
                            creator);

                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                var message = $"{Constant.STEP_5}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_5]}:[{ex.Message}]";
                IntegrationService.ITG_MSGO_Activitie(request,
                    short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return false;
            }


        }
        
    }
}

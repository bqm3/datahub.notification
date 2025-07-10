using Lib.Setting;
using Lib.Utility;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace PROJECT.BASE.SERVICES
{
    public class TransExtractService
    {
        public TransExtractService() { }
        /// <summary>
        /// STEP_4 - Extract
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<bool> ExtractTrans(IntegrationRequest request, string creator)
        {
            try
            {
                string pathFileXSD = $"{Environment.CurrentDirectory}/configuration/xsd/{request.Header.Tran_Code}.xsd";
                var xmlDocument = StringConvert.StringToXml(StringConvert.DecodeStringFromBase64(request.Body.Content));
                if (xmlDocument == null)
                {
                    var message = $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[xmlDocument == null]";
                    IntegrationService.ITG_MSGO_Activitie(request,
                        short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                    return false;
                }
                List<string> listTableName = new List<string>();
                var listTableNameInfo = ExtractXML.XMLToTableName(xmlDocument, pathFileXSD, ref listTableName);
                if (listTableName.Count > 0)
                {
                    Dictionary<string, List<string>> dictTableColumn = new Dictionary<string, List<string>>();
                    Dictionary<string, Dictionary<string, string>> dictTableColumnValue = new Dictionary<string, Dictionary<string, string>>();
                    ExtractXML.ExtractXMLData(listTableNameInfo, listTableName, xmlDocument, ref dictTableColumn, ref dictTableColumnValue);
                    var createScriptTable = ExtractXML.CreateScriptTable(dictTableColumn, ConstantConfig.DHUB_EXTRACT_SCHEMA);
                    if (createScriptTable != null)
                    {
                        List<string> message = new List<string>();
                        ExtractXML.ExcuteScriptTable(createScriptTable, ref message);
                        if (message.Count > 0)
                        {                            
                            IntegrationService.ITG_MSGO_Activitie(request,
                                short.Parse(Constant.RETURN_CODE_ERROR),
                                $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{string.Join(",", message.ToArray())}]"
                                , creator);
                            return false;
                        }
                        Dictionary<string, object> dictAppen = new Dictionary<string, object>();
                        dictAppen.Add("ID", Guid.NewGuid().ToString());
                        dictAppen.Add("MSG_ID", request.Header.Msg_Id);
                        dictAppen.Add("TRAN_CODE", request.Header.Tran_Code);
                        dictAppen.Add("TRAN_NAME", request.Header.Tran_Name);
                        dictAppen.Add("IS_DELETE", 0);
                        dictAppen.Add("CDATE", DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
                        dictAppen.Add("CUSER", creator);
                        var excuteInsert = ExtractXML.ExcuteInsert(dictAppen, dictTableColumn, dictTableColumnValue);
                        if (excuteInsert != null && excuteInsert.Count > 0)
                        {
                            IntegrationService.ITG_MSGO_Activitie(request,
                                short.Parse(Constant.RETURN_CODE_ERROR),
                                $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{string.Join(",", excuteInsert.ToArray())}]"
                                , creator);
                            return false;
                        }
                    }

                }
                IntegrationService.ITG_MSGO_Activitie(request,
                    short.Parse(Constant.RETURN_CODE_SUCCESS),
                    $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}",
                    creator);
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                var message = $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{ex.Message}]";
                IntegrationService.ITG_MSGO_Activitie(request,
                    short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return false;
            }


        }

        public static async Task<bool> ExtractXmlDocument(XmlDocument xmlDocument,string tran_Code, string creator)
        {
            try
            {
                //string pathFileXSD = $"{Environment.CurrentDirectory}/configuration/xsd/{request.Header.Tran_Code}.xsd";
                //var xmlDocument = StringConvert.StringToXml(StringConvert.DecodeStringFromBase64(request.Body.Content));
                //if (xmlDocument == null)
                //{
                //    var message = $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[xmlDocument == null]";
                //    IntegrationService.ITG_MSGO_Activitie(request,
                //        short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                //    return false;
                //}
                List<string> listTableName = new List<string>();
                var listTableNameInfo = ExtractXML.XMLToTableName(xmlDocument, string.Empty, ref listTableName);
                if (listTableName.Count > 0)
                {
                    Dictionary<string, List<string>> dictTableColumn = new Dictionary<string, List<string>>();
                    Dictionary<string, Dictionary<string, string>> dictTableColumnValue = new Dictionary<string, Dictionary<string, string>>();
                    ExtractXML.ExtractXMLData(listTableNameInfo, listTableName, xmlDocument, ref dictTableColumn, ref dictTableColumnValue);
                    var createScriptTable = ExtractXML.CreateScriptTable(dictTableColumn, ConstantConfig.DHUB_EXTRACT_SCHEMA);
                    if (createScriptTable != null)
                    {
                        List<string> message = new List<string>();
                        ExtractXML.ExcuteScriptTable(createScriptTable, ref message);
                        if (message.Count > 0)
                        {
                            //IntegrationService.ITG_MSGO_Activitie(request,
                            //    short.Parse(Constant.RETURN_CODE_ERROR),
                            //    $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{string.Join(",", message.ToArray())}]"
                            //    , creator);
                            return false;
                        }
                        Dictionary<string, object> dictAppen = new Dictionary<string, object>();
                        dictAppen.Add("ID", Guid.NewGuid().ToString());
                        //dictAppen.Add("MSG_ID", request.Header.Msg_Id);
                        dictAppen.Add("TRAN_CODE", tran_Code);
                        dictAppen.Add("TRAN_NAME", string.Empty);
                        dictAppen.Add("IS_DELETE", 0);
                        dictAppen.Add("CDATE", DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
                        dictAppen.Add("CUSER", creator);
                        var excuteInsert = ExtractXML.ExcuteInsert(dictAppen, dictTableColumn, dictTableColumnValue);
                        if (excuteInsert != null && excuteInsert.Count > 0)
                        {
                            //IntegrationService.ITG_MSGO_Activitie(request,
                            //    short.Parse(Constant.RETURN_CODE_ERROR),
                            //    $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{string.Join(",", excuteInsert.ToArray())}]"
                            //    , creator);
                            return false;
                        }
                    }

                }
                //IntegrationService.ITG_MSGO_Activitie(request,
                //    short.Parse(Constant.RETURN_CODE_SUCCESS),
                //    $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}",
                //    creator);
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                //var message = $"{Constant.STEP_4}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_4]}:[{ex.Message}]";
                //IntegrationService.ITG_MSGO_Activitie(request,
                //    short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return false;
            }


        }
    }

}

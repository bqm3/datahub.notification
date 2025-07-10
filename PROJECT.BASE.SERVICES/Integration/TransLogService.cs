using Lib.Setting;
using Lib.Utility;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using ServiceStack;
using SolrNet.Utils;
using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PROJECT.BASE.SERVICES
{
    public class TransLogService
    {
        public TransLogService() { }
        public static async Task<bool> LogEventRequest(IntegrationRequest request)
        {
            string fileName = $"{request.Header.Request_Id}{".log"}";
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/logs"))
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/logs");
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/logs/request"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/logs/request");
            }
            string pathFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/logs/request", fileName);
            try
            {
                using (StreamWriter writer = new StreamWriter(pathFile, true))
                {
                    writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                }
                return true;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public static async Task<long?> LogInsert(LogRequest request)
        {          
            try
            {
                long result = 0;
                switch (request.LogType.ToUpper())
                {
                    case "LOG_ACTION":                                         
                        result = DataObjectFactory.GetInstanceLOG_ACTION().Add(new LOG_ACTION
                        {
                            CODE = (request != null && request.Information.ContainsKey("CODE")) ? (string)request.Information["CODE"] : null,
                            LOG_TYPE_CODE = (request != null && request.Information.ContainsKey("LOG_TYPE_CODE")) ? (string)request.Information["LOG_TYPE_CODE"] : null,
                            MESSAGE_LOG = (request != null && request.Information.ContainsKey("MESSAGE_LOG")) ? (string)request.Information["MESSAGE_LOG"] : null,
                            MESSAGE_DETAIL = (request != null && request.Information.ContainsKey("MESSAGE_DETAIL")) ? (string)request.Information["MESSAGE_DETAIL"] : null,
                            WORKING_SESSION = (request != null && request.Information.ContainsKey("WORKING_SESSION")) ? DateTime.SpecifyKind(DateTime.ParseExact((string)request.Information["WORKING_SESSION"], "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                            START_DATE = (request != null && request.Information.ContainsKey("START_DATE")) ? DateTime.SpecifyKind(DateTime.ParseExact((string)request.Information["START_DATE"], "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                            END_DATE = (request != null && request.Information.ContainsKey("END_DATE")) ? DateTime.SpecifyKind(DateTime.ParseExact((string)request.Information["END_DATE"], "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc) : (DateTime?)null,
                            CONFIG_PIPELINE_CODE = (request != null && request.Information.ContainsKey("LOG_TYPE_CODE")) ? (string)request.Information["CONFIG_PIPELINE_CODE"] : null,
                            RESULT_VALUE = (request != null && request.Information.ContainsKey("RESULT_VALUE")) ? (string)request.Information["RESULT_VALUE"] : null,
                            RESULT_ETL_RDS = (request != null && request.Information.ContainsKey("RESULT_ETL_RDS")) ? (long)request.Information["RESULT_ETL_RDS"] : (long?)null,
                            RESULT_ETL_RIS = (request != null && request.Information.ContainsKey("RESULT_ETL_RIS")) ? (long)request.Information["RESULT_ETL_RIS"] : (long?)null,
                            IS_ACTIVE = (request != null && request.Information.ContainsKey("IS_ACTIVE")) ? (short)request.Information["IS_ACTIVE"] : (short?)null,
                            STATUS = (request != null && request.Information.ContainsKey("STATUS")) ? (short)request.Information["STATUS"] : (short?)null,
                            IS_DELETE = (request != null && request.Information.ContainsKey("IS_DELETE")) ? (short)request.Information["IS_DELETE"] : (short?)null,
                            CUSER = (request != null && request.Information.ContainsKey("CUSER")) ? (string)request.Information["CUSER"] : Config.CONFIGURATION_PRIVATE.Infomation.Service
                        }).Value;
                        break;
                    case "LOG_ERROR":                        
                        result = DataObjectFactory.GetInstanceLOG_ERROR().Add(new LOG_ERROR
                        {
                            CODE = (request != null && request.Information.ContainsKey("CODE")) ? (string)request.Information["CODE"] : null,
                            LOG_TYPE_CODE = (request != null && request.Information.ContainsKey("LOG_TYPE_CODE")) ? (string)request.Information["LOG_TYPE_CODE"] : null,
                            LOG_ACTION_ID = (request != null && request.Information.ContainsKey("LOG_ACTION_ID")) ? (long)request.Information["LOG_ACTION_ID"] : (long?)null,
                            MESSAGE_LOG = (request != null && request.Information.ContainsKey("MESSAGE_LOG")) ? (string)request.Information["MESSAGE_LOG"] : null,
                            MESSAGE_DETAIL = (request != null && request.Information.ContainsKey("MESSAGE_DETAIL")) ? (string)request.Information["MESSAGE_DETAIL"] : null,
                            IS_DELETE = (request != null && request.Information.ContainsKey("IS_DELETE")) ? (short)request.Information["IS_DELETE"] : (short?)null,
                            CUSER = (request != null && request.Information.ContainsKey("CUSER")) ? (string)request.Information["CUSER"] : Config.CONFIGURATION_PRIVATE.Infomation.Service
                        }).Value;
                        break;
                    case "LOG_WARNING":
                        result = DataObjectFactory.GetInstanceLOG_WARNING().Add(new LOG_WARNING
                        {
                            CODE = (request != null && request.Information.ContainsKey("CODE")) ? (string)request.Information["CODE"] : null,
                            LOG_TYPE_CODE = (request != null && request.Information.ContainsKey("LOG_TYPE_CODE")) ? (string)request.Information["LOG_TYPE_CODE"] : null,
                            LOG_ACTION_ID = (request != null && request.Information.ContainsKey("LOG_ACTION_ID")) ? (long)request.Information["LOG_ACTION_ID"] : (long?)null,
                            MESSAGE_LOG = (request != null && request.Information.ContainsKey("MESSAGE_LOG")) ? (string)request.Information["MESSAGE_LOG"] : null,
                            MESSAGE_DETAIL = (request != null && request.Information.ContainsKey("MESSAGE_DETAIL")) ? (string)request.Information["MESSAGE_DETAIL"] : null,
                            IS_DELETE = (request != null && request.Information.ContainsKey("IS_DELETE")) ? (short)request.Information["IS_DELETE"] : (short?)null,
                            CUSER = (request != null && request.Information.ContainsKey("CUSER")) ? (string)request.Information["CUSER"] : Config.CONFIGURATION_PRIVATE.Infomation.Service
                        }).Value;
                        break;
                    default:
                        // code block
                        break;
                }
               
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

    }
}

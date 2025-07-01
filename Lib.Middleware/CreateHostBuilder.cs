using Lib.Setting;
using Lib.Utility;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Middleware
{
    public class CreateHostBuilder
    {
        public static Dictionary<string,bool> ConfigureAppConfiguration()
        {
            try
            {
                Dictionary<string,bool> dictData = new Dictionary<string,bool>();
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"/configuration/routes"))
                {
                    string templateDataFileRoutes = string.Empty;
                    if (File.Exists(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.routes.template.json"))
                        templateDataFileRoutes = File.ReadAllText(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.routes.template.json");
                    string templateDataFileHealthCheck = string.Empty;
                    if (File.Exists(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.health.check.template.json"))
                        templateDataFileHealthCheck = File.ReadAllText(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.health.check.template.json");
                    if (!string.IsNullOrEmpty(templateDataFileRoutes))
                    {
                        //List<MicroServiceInfo> listMicroservice = JsonConvert.DeserializeObject<List<MicroServiceInfo>>(Config.CONFIGURATION_APP_SETTING["AppSetting"]["MicroServiceName"].ToString());
                        List<MicroServiceInfo> listMicroservice = Config.CONFIGURATION_GLOBAL.AppSetting.MicroServiceName.ToObject<List<MicroServiceInfo>>();
                        if (listMicroservice.Count > 0)
                        {
                            string dataTemplateHealthCheck = string.Empty;
                            if (File.Exists(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.data.health.check.template.json"))
                                dataTemplateHealthCheck = File.ReadAllText(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.data.health.check.template.json");
                            string dataTemplateRoutes = string.Empty;
                            if (File.Exists(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.data.template.json"))
                                dataTemplateRoutes = File.ReadAllText(Directory.GetCurrentDirectory() + @"/configuration/routes/template/configuration.data.template.json");
                            if (!string.IsNullOrEmpty(dataTemplateRoutes))
                            {
                                string dataHealthCheck = string.Empty;
                                string dataRoutes = string.Empty;
                                foreach (var item in listMicroservice)
                                {
                                    string serviceName = item.ServiceName;
                                    string apiName = serviceName.Replace("microservice_", string.Empty);
                                    Regex trimmer = new Regex(@"\s\s+");
                                    string tempRoutes = dataTemplateRoutes;
                                    tempRoutes = tempRoutes.Replace("{MicroServiceName}", serviceName);
                                    tempRoutes = tempRoutes.Replace("{ApiName}", apiName);
                                    tempRoutes = tempRoutes.Replace("{Host}", item.Host);
                                    tempRoutes = tempRoutes.Replace("{Port}", item.Port);
                                    dataRoutes += tempRoutes + ",";

                                    string tempHealthCheck = dataTemplateHealthCheck;
                                    tempHealthCheck = tempHealthCheck.Replace("{Description}", item.Description);
                                    tempHealthCheck = tempHealthCheck.Replace("{ApiName}", apiName);
                                    tempHealthCheck = tempHealthCheck.Replace("{Host}", "localhost");
                                    tempHealthCheck = tempHealthCheck.Replace("{Port}", item.Port);
                                    dataHealthCheck += tempHealthCheck + ",";
                                }
                                if (!string.IsNullOrEmpty(dataRoutes))
                                {
                                    //templateDataFileRoutes = templateDataFileRoutes.Replace("#DataRoutes#", DataRoutes.Substring(0, DataRoutes.Length - 1));
                                    templateDataFileRoutes = templateDataFileRoutes.Replace("\"#DataRoutes#\"", dataRoutes.Substring(0, dataRoutes.Length - 1));
                                    File.WriteAllText(Directory.GetCurrentDirectory() + @"/configuration/configuration.routes.json", templateDataFileRoutes);
                                    dictData.Add("route", true);
                                }
                                if (!string.IsNullOrEmpty(dataHealthCheck))
                                {
                                    //templateDataFileHealthCheck = templateDataFileHealthCheck.Replace("#DataHealthCheck#", DataHealthCheck.Substring(0, DataHealthCheck.Length - 1));
                                    templateDataFileHealthCheck = templateDataFileHealthCheck.Replace("\"#DataHealthCheck#\"", dataHealthCheck.Substring(0, dataHealthCheck.Length - 1));
                                    File.WriteAllText(Directory.GetCurrentDirectory() + @"/configuration/configuration.health.check.json", templateDataFileHealthCheck);
                                    dictData.Add("health", true);
                                }
                            }

                        }
                    }
                }                
                return dictData;
            }
            catch (Exception ex)
            {                
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }

        }
    }
}

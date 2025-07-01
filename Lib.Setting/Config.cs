using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
//using Lib.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Lib.Setting
{
    public class ProviderOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public int? FindMaxTime { get; set; }
        public string TypeName { get; set; }
    }
    public sealed class Config
    {
        private static dynamic _CONFIGURATION_GLOBAL;
        public static dynamic CONFIGURATION_GLOBAL
        {
            get
            {
                if (_CONFIGURATION_GLOBAL == null)
                {
                    var pathFile = Path.Combine(Directory.GetCurrentDirectory() + @"/configuration/global", "global.setting.json");
                    string dataFile = File.ReadAllText(pathFile);
                    _CONFIGURATION_GLOBAL = JsonConvert.DeserializeObject<dynamic>(dataFile);                    
                }
                return _CONFIGURATION_GLOBAL;
            }
        }
        private static dynamic _CONFIGURATION_PRIVATE;
        public static dynamic CONFIGURATION_PRIVATE
        {
            get
            {
                if (_CONFIGURATION_PRIVATE == null)
                {
                    var pathFile = Path.Combine(Directory.GetCurrentDirectory() + @"/configuration/private", "private.setting.json");
                    string dataFile = File.ReadAllText(pathFile);
                    _CONFIGURATION_PRIVATE = JsonConvert.DeserializeObject<dynamic>(dataFile);
                }
                return _CONFIGURATION_PRIVATE;
            }
        }
        #region Database ConnectionString
        private static Dictionary<string, List<ProviderOptions>> _DictConnectionString;
        public static Dictionary<string, List<ProviderOptions>> DictConnectionString
        {
            get
            {
                if (_DictConnectionString == null || _DictConnectionString.Count == 0)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine("configuration", "configuration.appsettings.json"));
                    var root = builder.Build();
                    _DictConnectionString = root.GetSection("ConnectionString").Get<Dictionary<string, List<ProviderOptions>>>();

                }
                return _DictConnectionString;
            }
        }

        public static IConfiguration NEO4J_CONFIGURATION { get; set; }
        #endregion       
        private static string _INTERNET_IP;
        public static string INTERNET_IP
        {
            get
            {
                if (string.IsNullOrEmpty(_INTERNET_IP))
                {
                    _INTERNET_IP = new WebClient().DownloadString("https://api.ipify.org");
                }
                return _INTERNET_IP;
            }
        }      
        private static string _LOG_PATH;
        public static string LOG_PATH
        {
            get
            {
                if (string.IsNullOrEmpty(_LOG_PATH))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Logs"))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Logs");
                    }
                    _LOG_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Logs";

                }
                return _LOG_PATH;
            }
        }

        private static string _ASSEMBLY_NAME;
        public static string ASSEMBLY_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_ASSEMBLY_NAME))
                {
                    _ASSEMBLY_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.ASSEMBLY_NAME;
                }
                return _ASSEMBLY_NAME;
            }
        }

        private static string _NAMESPACE_NAME;
        public static string NAMESPACE_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_NAMESPACE_NAME))
                {
                    _NAMESPACE_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.NAMESPACE_NAME;
                }
                return _NAMESPACE_NAME;
            }
        }

        private static string _PAGE_SIZE;
        public static string PAGE_SIZE
        {
            get
            {
                if (string.IsNullOrEmpty(_PAGE_SIZE))
                {
                    _PAGE_SIZE = Config.CONFIGURATION_GLOBAL.AppSetting.PAGE_SIZE;
                }
                return _PAGE_SIZE;
            }
        }

        #region Product info
        private static string _SOFTWARE_NAME;
        public static string SOFTWARE_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_SOFTWARE_NAME))
                {
                    _SOFTWARE_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.SOFTWARE_NAME;
                }
                return _SOFTWARE_NAME;
            }
        }

        private static string _VERSION;
        public static string VERSION
        {
            get
            {
                if (string.IsNullOrEmpty(_VERSION))
                {
                    _VERSION = Config.CONFIGURATION_GLOBAL.AppSetting.VERSION;
                }
                return _VERSION;
            }
        }

        private static string _SOFTWARE_CODE;
        public static string SOFTWARE_CODE
        {
            get
            {
                if (string.IsNullOrEmpty(_SOFTWARE_CODE))
                {
                    _SOFTWARE_CODE = Config.CONFIGURATION_GLOBAL.AppSetting.SOFTWARE_CODE;
                }
                return _SOFTWARE_CODE;
            }
        }

        private static string _LICENSE_KEY;
        public static string LICENSE_KEY
        {
            get
            {
                if (string.IsNullOrEmpty(_LICENSE_KEY))
                {
                    _LICENSE_KEY = Config.CONFIGURATION_GLOBAL.AppSetting.LICENSE_KEY;
                }
                return _LICENSE_KEY;
            }
        }

        #endregion

        #region config - infomation

        private static string _SUPPLIER_COMPANY_NAME;
        public static string SUPPLIER_COMPANY_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_SUPPLIER_COMPANY_NAME))
                {
                    _SUPPLIER_COMPANY_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.SUPPLIER_COMPANY_NAME;
                }
                return _SUPPLIER_COMPANY_NAME;
            }
        }

        private static string _SUPPLIER_PHONE_SUPPORT;
        public static string SUPPLIER_PHONE_SUPPORT
        {
            get
            {
                if (string.IsNullOrEmpty(_SUPPLIER_PHONE_SUPPORT))
                {
                    _SUPPLIER_PHONE_SUPPORT = Config.CONFIGURATION_GLOBAL.AppSetting.SUPPLIER_PHONE_SUPPORT;
                }
                return _SUPPLIER_PHONE_SUPPORT;
            }
        }

        private static string _SUPPLIER_WEBSITE_SUPPORT;
        public static string SUPPLIER_WEBSITE_SUPPORT
        {
            get
            {
                if (string.IsNullOrEmpty(_SUPPLIER_WEBSITE_SUPPORT))
                {
                    _SUPPLIER_WEBSITE_SUPPORT = Config.CONFIGURATION_GLOBAL.AppSetting.SUPPLIER_WEBSITE_SUPPORT;
                }
                return _SUPPLIER_WEBSITE_SUPPORT;
            }
        }

        #endregion

        #region Solr
        private static string _SOLR_URL_CORE_BASE;
        public static string SOLR_URL_CORE_BASE
        {
            get
            {
                if (string.IsNullOrEmpty(_SOLR_URL_CORE_BASE))
                {
                    _SOLR_URL_CORE_BASE = Config.CONFIGURATION_GLOBAL.AppSetting.SOLR_URL_CORE_BASE;
                }
                return _SOLR_URL_CORE_BASE;
            }
        }

        private static string _SOLR_URL_REPORT;
        public static string SOLR_URL_REPORT
        {
            get
            {
                if (string.IsNullOrEmpty(_SOLR_URL_REPORT))
                {
                    _SOLR_URL_REPORT = Config.CONFIGURATION_GLOBAL.AppSetting.SOLR_URL_REPORT;
                }
                return _SOLR_URL_REPORT;
            }
        }

        private static string _SOLR_URL_SEARCH;
        public static string SOLR_URL_SEARCH
        {
            get
            {
                if (string.IsNullOrEmpty(_SOLR_URL_SEARCH))
                {
                    _SOLR_URL_SEARCH = Config.CONFIGURATION_GLOBAL.AppSetting.SOLR_URL_SEARCH;
                }
                return _SOLR_URL_SEARCH;
            }
        }

        private static string _SOLR_PAGE_SIZE;
        public static string SOLR_PAGE_SIZE
        {
            get
            {
                if (string.IsNullOrEmpty(_SOLR_PAGE_SIZE))
                {
                    _SOLR_PAGE_SIZE = Config.CONFIGURATION_GLOBAL.AppSetting.SOLR_PAGE_SIZE;
                }
                return _SOLR_PAGE_SIZE;
            }
        }
        #endregion

        #region FTP CONFIG

        private static string _FTP_USING;
        public static string FTP_USING
        {
            get
            {
                if (string.IsNullOrEmpty(_FTP_USING))
                {
                    _FTP_USING = Config.CONFIGURATION_GLOBAL.AppSetting.FTP_USING;
                }
                return _FTP_USING;
            }
        }

        private static string _FTP_SERVER_URI;
        public static string FTP_SERVER_URI
        {
            get
            {
                if (string.IsNullOrEmpty(_FTP_SERVER_URI))
                {
                    _FTP_SERVER_URI = Config.CONFIGURATION_GLOBAL.AppSetting.FTP_SERVER_URI;
                }
                return _FTP_SERVER_URI;
            }
        }

        private static string _FTP_USER;
        public static string FTP_USER
        {
            get
            {
                if (string.IsNullOrEmpty(_FTP_USER))
                {
                    _FTP_USER = Config.CONFIGURATION_GLOBAL.AppSetting.FTP_USER;
                }
                return _FTP_USER;
            }
        }

        private static string _FTP_PASSWORD;
        public static string FTP_PASSWORD
        {
            get
            {
                if (string.IsNullOrEmpty(_FTP_PASSWORD))
                {
                    _FTP_PASSWORD = Config.CONFIGURATION_GLOBAL.AppSetting.FTP_PASSWORD;
                }
                return _FTP_PASSWORD;
            }
        }

        private static string _FTP_DOWNLOAD_FILE;
        public static string FTP_DOWNLOAD_FILE
        {
            get
            {
                if (string.IsNullOrEmpty(_FTP_DOWNLOAD_FILE))
                {
                    _FTP_DOWNLOAD_FILE = Config.CONFIGURATION_GLOBAL.AppSetting.FTP_DOWNLOAD_FILE;
                }
                return _FTP_DOWNLOAD_FILE;
            }
        }

        #endregion

        #region RabbitMQ

        private static string _MQ_PORT;
        public static string MQ_PORT
        {
            get
            {
                if (string.IsNullOrEmpty(_MQ_PORT))
                {
                    _MQ_PORT = Config.CONFIGURATION_GLOBAL.AppSetting.MQ_PORT;
                }
                return _MQ_PORT;
            }
        }

        private static string _MQ_NAME;
        public static string MQ_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_MQ_NAME))
                {
                    _MQ_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.MQ_NAME;
                }
                return _MQ_NAME;
            }
        }

        private static Dictionary<int, string> _DICT_MQ_NAME;
        public static Dictionary<int, string> DICT_MQ_NAME
        {
            get
            {
                if (_DICT_MQ_NAME == null || _DICT_MQ_NAME.Count == 0)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                    var root = builder.Build();
                    string mqName = root.GetConnectionString(Constant.MQ_NAME_LIST);
                    if (!string.IsNullOrEmpty(mqName))
                    {

                        var array = mqName.Split(',');

                        for (int i = 0; i < array.Length; i++)
                        {
                            dict.Add(i, array[i]);
                        }

                    }

                    _DICT_MQ_NAME = dict;

                }
                return _DICT_MQ_NAME;
            }
        }

        private static string _MQ_USER;
        public static string MQ_USER
        {
            get
            {
                if (string.IsNullOrEmpty(_MQ_USER))
                {
                    _MQ_USER = Config.CONFIGURATION_GLOBAL.AppSetting.MQ_USER;
                }
                return _MQ_USER;
            }
        }



        private static string _MQ_PASSWORD;
        public static string MQ_PASSWORD
        {
            get
            {
                if (string.IsNullOrEmpty(_MQ_PASSWORD))
                {
                    _MQ_PASSWORD = Config.CONFIGURATION_GLOBAL.AppSetting.MQ_PASSWORD;
                }
                return _MQ_PASSWORD;
            }
        }

        private static string _MQ_HOST;
        public static string MQ_HOST
        {
            get
            {
                if (string.IsNullOrEmpty(_MQ_HOST))
                {
                    _MQ_HOST = Config.CONFIGURATION_GLOBAL.AppSetting.MQ_HOST;
                }
                return _MQ_HOST;
            }
        }

        #endregion       

        #region CA
        private static string _CA_TOKENT_SERIAL;
        public static string CA_TOKENT_SERIAL
        {
            get
            {
                if (string.IsNullOrEmpty(_CA_TOKENT_SERIAL))
                {
                    _CA_TOKENT_SERIAL = Config.CONFIGURATION_GLOBAL.AppSetting.CA_TOKENT_SERIAL;
                }
                return _CA_TOKENT_SERIAL;
            }
        }
        private static string _CA_FILE_NAME;
        public static string CA_FILE_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_CA_FILE_NAME))
                {
                    _CA_FILE_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.CA_FILE_NAME;
                }
                return _CA_FILE_NAME;
            }
        }

        private static string _CA_PASSWORD;
        public static string CA_PASSWORD
        {
            get
            {
                if (string.IsNullOrEmpty(_CA_PASSWORD))
                {
                    _CA_PASSWORD = Config.CONFIGURATION_GLOBAL.AppSetting.CA_PASSWORD;
                }
                return _CA_PASSWORD;
            }
        }

        #endregion

        #region MICRO SERVICE
        private static string _MICRO_SERVICE_IP;
        public static string MICRO_SERVICE_IP
        {
            get
            {
                if (string.IsNullOrEmpty(_MICRO_SERVICE_IP))
                {
                    _MICRO_SERVICE_IP = Config.CONFIGURATION_GLOBAL.AppSetting.MICRO_SERVICE_IP;
                }
                return _MICRO_SERVICE_IP;
            }
        }
        private static string _MICRO_SERVICE_PORT;
        public static string MICRO_SERVICE_PORT
        {
            get
            {
                if (string.IsNullOrEmpty(_MICRO_SERVICE_PORT))
                {
                    _MICRO_SERVICE_PORT = Config.CONFIGURATION_GLOBAL.AppSetting.MICRO_SERVICE_PORT;
                }
                return _MICRO_SERVICE_PORT;
            }
        }

        private static string _MICRO_SERVICE_NAME;
        public static string MICRO_SERVICE_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_MICRO_SERVICE_NAME))
                {
                    _MICRO_SERVICE_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.MICRO_SERVICE_NAME;
                }
                return _MICRO_SERVICE_NAME;
            }
        }

        private static string _MICRO_SERVICE_DISPLAY_NAME;
        public static string MICRO_SERVICE_DISPLAY_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(_MICRO_SERVICE_DISPLAY_NAME))
                {
                    _MICRO_SERVICE_DISPLAY_NAME = Config.CONFIGURATION_GLOBAL.AppSetting.MICRO_SERVICE_DISPLAY_NAME;
                }
                return _MICRO_SERVICE_DISPLAY_NAME;
            }
        }
        #endregion
        #region CONFIG SSO

        private static string _SSO_HOST;
        public static string SSO_HOST
        {
            get
            {
                if (string.IsNullOrEmpty(_SSO_HOST))
                {
                    _SSO_HOST = Config.CONFIGURATION_GLOBAL.AppSetting.SSO_HOST;
                }
                return _SSO_HOST;
            }
        }

        #endregion
        #region TRANSACTION
        private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> _TRANSACTION_CONFIG;
        public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> TRANSACTION_CONFIG
        {
            get
            {
                if (_TRANSACTION_CONFIG == null)
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory() + @"/configuration", "transaction.config.json");
                    string jsonData = File.ReadAllText(file);
                    _TRANSACTION_CONFIG = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(jsonData); ;
                }
                return _TRANSACTION_CONFIG;
            }
        }
        private static string _TRANSACTION_REQUEST;
        public static string TRANSACTION_REQUEST
        {
            get
            {
                if (_TRANSACTION_REQUEST == null)
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory() + @"/configuration/Templates", "Request.xml");
                    _TRANSACTION_REQUEST = File.ReadAllText(file);
                }
                return _TRANSACTION_REQUEST;
            }
        }
        private static string _TRANSACTION_RESPONSE;
        public static string TRANSACTION_RESPONSE
        {
            get
            {
                if (_TRANSACTION_RESPONSE == null)
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory() + @"/configuration/Templates", "Response.xml");
                    _TRANSACTION_RESPONSE = File.ReadAllText(file);
                }
                return _TRANSACTION_RESPONSE;
            }
        }
        #endregion
    }
}

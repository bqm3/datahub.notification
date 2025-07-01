using System;
using System.Collections.Generic;

namespace Lib.Setting
{
    public sealed class ConstantConfig
    {
        public static bool KAFKA_START = false;
       
        public static string KAFKA_TOPIC
        {
            get { return $"TOPIC-ITG-{Environment.MachineName}"; }
        }
        #region ORACLE
        public static string LDZ_SLG_SCHEMA
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_SLG.SchemaName; }
        }
        public static string LDZ_SLG_CONNECTTION_STRING
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.LDZ_SLG.ConnectionString; }
        }
        public static string DHUB_TRANS_SCHEMA
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_TRANS.SchemaName; }
        }
        public static string DHUB_TRANS_CONNECTTION_STRING
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_TRANS.ConnectionString; }
        }
        public static string DHUB_GOVERNANCE_SCHEMA
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.SchemaName; }
        }
        public static string DHUB_GOVERNANCE_CONNECTTION_STRING
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_GOVERNANCE.ConnectionString; }
        }
        public static string DHUB_EXTRACT_SCHEMA
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_EXTRACT.SchemaName; }
        }
        public static string DHUB_EXTRACT_CONNECTTION_STRING
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.ORACLE.DHUB_EXTRACT.ConnectionString; }
        }

        public static string DHUB_EXT_SOCIAL
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.MONGODB.DHUB_EXT_SOCIAL.DatabaseName; }
        }
        public static string DHUB_DATA_LOGS
        {
            get { return Config.CONFIGURATION_GLOBAL.Databases.MONGODB.DHUB_DATA_LOGS.DatabaseName; }
        }
        #endregion

        #region GPDB
        //public static string GPDB_LANDING_SCHEMA
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.landing.schemaName; }
        //}        
        //public static string GPDB_LANDING_CONNECTTION_STRING
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.landing.connectionString; }
        //}

        //public static string GPDB_MDM_SCHEMA
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.mdm.schemaName; }
        //}        
        //public static string GPDB_MDM_CONNECTTION_STRING
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.mdm.connectionString; }
        //}

        //public static string GPDB_KTH_SCHEMA
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.kth.schemaName; }
        //}
        //public static string GPDB_KTH_CONNECTTION_STRING
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.kth.connectionString; }
        //}

        //public static string GPDB_EXTRACT_SCHEMA
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.extract.schemaName; }
        //}        
        //public static string GPDB_EXTRACT_CONNECTTION_STRING
        //{
        //    get { return Config.CONFIGURATION_GLOBAL.Databases.GPDB.extract.connectionString; }
        //}
        #endregion
        public static string[] ATTRIBUTES_USER_PROFILE
        {
            get
            {
                return new string[]
                  {
                      "Code",
                      "ReferralCode",
                      "HierarchyPath",
                      "DateOfBirth",
                      "Address",
                      "Address2",
                      "Phone",
                      "StateProvince",
                      "City",
                      "ZipCode",
                      "Sex",
                      "Country",
                      "NationalIdNumber",
                      "NationalIdImage",
                      "NationalIdImage2",
                      "NationalIdSelfieImage"
                  };
            }
        }

        public static Dictionary<string, string> DICT_APPLICATION_CODE
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "role","https://role.safin.top"},
                    { "dashboard","https://dashboard.safin.top"},
                    { "log_management","https://logs.safin.top"},
                    { "notification_management","https://notification.safin.top"},
                    { "customer_management","https://customer.safin.top"},
                    { "product_management","https://product.safin.top"},
                    { "external_management","M_EXTERNAL_DEV"},
                    { "internal_management","M_INTERNAL_DEV"},
                    { "onepay_client","ONEPAY_PAYMENT"},
                    { "microservice_client","MICROSERVICE_CLIENT"}
                };
            }
        }        
        public static Dictionary<string, object> DICT_INSTANCE_NAME
        {
            get
            {
                return new Dictionary<string, object>();
            }
        }        
        public static Dictionary<string, int[]> DICT_INSTANCE_NAME_PROCESS
        {
            get
            {
                return new Dictionary<string, int[]>();
            }
        }
        public static Dictionary<string, string> DICT_STEP_TRANSACTION
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"STEP_1","Tiếp nhận giao dịch và chờ xử lý" },
                    {"STEP_2","Kiểm tra tính hợp lệ giao dịch" },
                    {"STEP_3","Xử lý giao dịch - Thụ lý" },
                    {"STEP_4","Xử lý giao dịch - Extract" },
                    {"STEP_5","Xử lý giao dịch - Transform" },
                    {"STEP_6","Xử lý giao dịch - Transform.Yellow" }
                };
            }
        }

        public static Dictionary<string, string> DICT_RETURN_CODE_TRANSACTION
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"100","Tiếp nhận giao dịch" },
                    {"101","Giao dịch đang xử lý" },
                    {"200","Giao dịch thành công" },
                    {"203","Giao dịch bị lỗi" }
                };
            }
        }

    }
    public class Constant
    {                
        public const int ActiveStatus = 1;
        public const int DeactiveStatus = 0;       
        /// <summary>
        /// Tiếp nhận giao dịch và chờ xử lý
        /// </summary>
        public const string STEP_1 = "STEP_1";
        /// <summary>
        /// Kiểm tra tính hợp lệ giao dịch
        /// </summary>
        public const string STEP_2 = "STEP_2";
        /// <summary>
        /// Xử lý giao dịch - Thụ lý
        /// </summary>
        public const string STEP_3 = "STEP_3";
        /// <summary>
        /// Xử lý giao dịch - Extract
        /// </summary>
        public const string STEP_4 = "STEP_4";
        /// <summary>
        /// Xử lý giao dịch - Transform
        /// </summary>
        public const string STEP_5 = "STEP_5";
        /// <summary>
        /// Xử lý giao dịch - Transform.Yellow
        /// </summary>
        public const string STEP_6 = "STEP_6";
        //SYNC DATA
        public const string SYNC_ARANGODB = "SYNC_ARANGODB";
        public const string SYNC_MONGODB = "SYNC_MONGODB";
        public const string SYNC_MINIO = "SYNC_MINIO";

        //NOTIFY_TYPE
        public const string NOTIFY_TYPE_PAY_SUCCESS = "PAY_SUCCESS";
        public const string NOTIFY_TYPE_PAY_FAIL= "PAY_FAIL";
        public const string NOTIFY_TYPE_TRA_SUCCESS = "TRA_SUCCESS";
        public const string NOTIFY_TYPE_TRA_FAIL = "TRA_FAIL";
        public const string NOTIFY_TYPE_PAY_SUCCESS_UNDER_LIMIT = "PAY_SUCCESS_UNDER_LIMIT";
        public const string NOTIFY_TYPE_PAY_SUCCESS_OVER_LIMIT = "PAY_SUCCESS_OVER_LIMIT";
        public const string NOTIFY_TYPE_DOI_MA_PIN = "DOI_MA_PIN";
        //MSG_TYPE
        public const string MSG_TYPE_PAY0003 = "PAY0003";
        public const string MSG_TYPE_PAY0001 = "PAY0001";
        public const string MSG_TYPE_TRA0003 = "TRA0003";
        public const string MSG_TYPE_TRA0001 = "TRA0001";
        public const string MSG_TYPE_TRA0002_SF_NOTIFY = "TRA0002_SF_NOTIFY";
        public const string MSG_TYPE_PAY0002 = "PAY0002";
        public const string MSG_TYPE_PAY0002_NOTIFY = "PAY0002_NOTIFY";
        public const string MSG_TYPE_TRA0002 = "TRA0002";
        public const string MSG_TYPE_TRA0002_NOTIFY = "TRA0002_NOTIFY";
        //PayMethod
        public const string PAY_METHOD_SAFIN = "SAFIN";
        public const string PAY_METHOD_ONEPAY = "ONEPAY";
        public const string PAY_METHOD_APPOTAPAY = "APPOTAPAY";
        public const string PAY_METHOD_KSS = "KSS";

        public const string RECEIVER_SAFIN = "SAFIN";
        public const string RECEIVER_KSS = "KSS";
        public const string RECEIVER_APPOTAPAY = "APPOTAPAY";
        public const string RECEIVER_ONEPAY = "ONEPAY";
        public const string SENDER_SAFIN = "SAFIN";
        public const string SENDER_KSS = "KSS";
        public const string SENDER_APPOTAPAY = "APPOTAPAY";
        public const string SENDER_ONEPAY = "ONEPAY";
        //TYPE_NAME
        public const string TYPE_NAME_SAFIN_PAYMENT = "SAFIN_PAYMENT";
        public const string TYPE_NAME_KSS_PAYMENT = "KSS_PAYMENT";
        public const string TYPE_NAME_APPOTAPAY_PAYMENT = "APPOTAPAY_PAYMENT";
        public const string TYPE_NAME_ONEPAY_PAYMENT = "ONEPAY_PAYMENT";
        public const string TYPE_NAME_ONEPAY_PAYMENT_NOTIFY = "ONEPAY_PAYMENT_NOTIFY";
        public const string TYPE_NAME_SAFIN_PAYMENT_NOTIFY = "SAFIN_PAYMENT_NOTIFY";
        public const string TYPE_NAME_SAFIN_TRANSFER = "SAFIN_TRANSFER";
        public const string TYPE_NAME_SAFIN_TRANSFER_NOTIFY = "SAFIN_TRANSFER_NOTIFY";
        public const string TYPE_NAME_KSS_TRANSFER = "KSS_TRANSFER";
        public const string TYPE_NAME_APPOTAPAY_TRANSFER = "APPOTAPAY_TRANSFER";
        public const string TYPE_NAME_ONEPAY_TRANSFER = "ONEPAY_TRANSFER";
        public const string TYPE_NAME_ONEPAY_TRANSFER_NOTIFY = "ONEPAY_TRANSFER_NOTIFY";
        //WarningType-mail
        public const string WARNING_TYPE_PAYMENT = "PAYMENT";
        public const string WARNING_TYPE_TRANSFER = "TRANSFER";
        public const string WARNING_TYPE_APPOTAPAY_PAYMENT = "APPOTAPAY_PAYMENT";
        public const string WARNING_TYPE_APPOTAPAY_TRANSFER = "APPOTAPAY_TRANSFER";
        public const string WARNING_TYPE_BUILD_CODE = "BUILD_CODE";
        public const string WARNING_TYPE_DEFAULT = "DEFAULT";
        public const string WARNING_TYPE_ONEPAY_PAYMENT_NOTIFY = "ONEPAY_PAYMENT_NOTIFY";
        public const string WARNING_TYPE_ONEPAY_TRANSFER = "ONEPAY_TRANSFER";
        public const string WARNING_TYPE_ONEPAY_TRANSFER_NOTIFY = "ONEPAY_TRANSFER_NOTIFY";
        public const string WARNING_TYPE_SAFIN_PAYMENT_NOTIFY = "SAFIN_PAYMENT_NOTIFY";
        public const string WARNING_TYPE_SAFIN_TRANSFER = "SAFIN_TRANSFER";
        public const string WARNING_TYPE_SAFIN_TRANSFER_NOTIFY = "SAFIN_TRANSFER_NOTIFY";
        public const string WARNING_TYPE_SIGNIN_UP = "SIGNIN_UP";
        public const string WARNING_TYPE_SUPORT_CUSTOMER = "SUPORT_CUSTOMER";
        public const string WARNING_TYPE_TRANSACTION = "TRANSACTION";
        public const string WARNING_TYPE_TRANSACTION_PROCESS = "TRANSACTION_PROCESS";
        public const string WARNING_TYPE_KSS_PAYMENT = "KSS_PAYMENT";
        public const string WARNING_TYPE_KSS_TRANSFER = "KSS_TRANSFER";

        //INSTANCE - CACHE
        public static string INSTANCE_FILE_IMPORT = "INSTANCE_DATA_01";
        public static string INSTANCE_DATA_SEND_MAIL = "INSTANCE_DATA_02";
        public static string INSTANCE_DATA_NOTIFICATIONS = "INSTANCE_NOTIFICATIONS";             
        //ConnectString
        //public static string MONGO_CONNECTION_STRING = "mongodb://identity_user:identity_user@10.20.0.210:27017/identity?safe=true";
        public const string MONGO_CONNECTION_STRING = "mongodb://{0}:{1}@{2}:{3}/{4}?safe=true";
        public const string POSTGRESQL_CONNECTION_STRING = "Server={0};Port={1};User Id={2};Password={3};Database={4};";
        public const string MYSQL_CONNECTION_STRING = "Server={0};Port={1};User Id={2};Password={3};Database={4};";
        public const string SQLSERVER_CONNECTION_STRING = "Data Source = {0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=true;User ID={2};Password={3};";
        public const string SQLLITE_CONNECTION_STRING = "Data Source={0}.sqlite;Version={1};";
        public const string Authorization = "Authorization";
        public const string APP_SETTING = "AppSetting";
        public const string LANGUAGE = "LANGUAGE";
        public const string ACCESS_TOKEN = "ACCESS_TOKEN";
        //Setting SSO
        public const string SSO_HOST = "SSO_HOST";

        public const string SESSION_USER_LOGIN = "SESSION_USER_LOGIN";
        public const string SESSION_SERVICE_API = "SESSION_SERVICE_API";

        //Database NEO4J

        public const string NEO4J_DRIVER_URL = "NEO4J_DRIVER_URL";
        public const string NEO4J_CLIENT_URL = "NEO4J_CLIENT_URL";
        public const string NEO4J_USER = "NEO4J_USER";
        public const string NEO4J_PASSWORD = "NEO4J_PASSWORD";
        //Host setting
        public const string HTTP_URL = "HTTP_URL";
        public const string HTTPS_URL = "HTTPS_URL";

        //Lucene
        public const string IS_USE_LUCENE = "IS_USE_LUCENE";
        public const string IS_UPDATE_LUCENE = "IS_UPDATE_LUCENE";
        public const string PATH_INDEX_LUCENE_MSSQL = "PATH_INDEX_LUCENE_MSSQL";
        public const string PATH_INDEX_LUCENE_ORACLE = "PATH_INDEX_LUCENE_ORACLE";
        public const string LUCENE_PAGE_SIZE = "LUCENE_PAGE_SIZE";
        public const string MAX_FIELD_LENGTH = "MAX_FIELD_LENGTH";
        public const string FILE_NAME_LOCK_LUCENE = "FILE_NAME_LOCK_LUCENE";//"write.lock";
        //PREFIX
        public const string PREFIX = "PREFIX";
        //PREFIX
        public const string API_KEY = "API_KEY";
        //CMCSOFT_MAIL
        public const string CMCSOFT_MAIL = "CMCSOFT_MAIL";
        //MICRO SERVICE
        public const string MICRO_SERVICE_IP = "MICRO_SERVICE_IP";
        public const string MICRO_SERVICE_PORT = "MICRO_SERVICE_PORT";
        public const string MICRO_SERVICE_NAME = "MICRO_SERVICE_NAME";
        public const string MICRO_SERVICE_DISPLAY_NAME = "MICRO_SERVICE_DISPLAY_NAME";
        public const string MICRO_SERVICE_MAXLENGTH_MESSAGESIZE = "MICRO_SERVICE_MAXLENGTH_MESSAGESIZE";
        //USING_ENVIROMENT
        public const string USING_ENVIROMENT = "USING_ENVIROMENT";
        //Solr
        public const string SOLR_URL_CORE_BASE = "SOLR_URL_CORE_BASE";
        public const string SOLR_URL_REPORT = "SOLR_URL_REPORT";
        public const string SOLR_URL_SEARCH = "SOLR_URL_SEARCH";
        public const string SOLR_URL_SHARE = "SOLR_URL_SHARE";
        public const string SOLR_URL_DMDC = "SOLR_URL_DMDC";
        //Thong tin CA
        public const string CA_FILE_NAME = "CA_FILE_NAME";
        public const string CA_PASSWORD = "CA_PASSWORD";
        public const string CA_TOKENT_SERIAL = "CA_TOKENT_SERIAL";
        //SOLR_PAGE_SIZE
        public const string SOLR_PAGE_SIZE = "SOLR_PAGE_SIZE";
        //LogPath
        public const string LOG_PATH = "LOG_PATH";
        public const string PATH_DATA_PORTAL = "PATH_DATA_PORTAL";
        public const string PATH_DATA_SERVICE = "PATH_DATA_SERVICE";
        public const string PATH_DATA_SAVE_FILE = "PATH_DATA_SAVE_FILE";

        public const string SERVICE_NAME = "SERVICE_NAME";
        public const string KEY_AUTHORIZATION = "KEY_AUTHORIZATION";
        public const string LIST_HOST_REFERER = "LIST_HOST_REFERER";
        //Product info
        public const string VERSION = "VERSION";
        public const string SOFTWARE_NAME = "SOFTWARE_NAME";
        public const string ASSEMBLY_NAME = "ASSEMBLY_NAME";
        public const string NAMESPACE_NAME = "NAMESPACE_NAME";
        public const string SOFTWARE_CODE = "SOFTWARE_CODE";
        public const string LICENSE_KEY = "LICENSE_KEY";
        public const string LICENSE_SERVICE = "LICENSE_SERVICE";
        public const string PAGE_SIZE = "PAGE_SIZE";
        //CONFIG-INFOMATION        
        public const string SUPPLIER_COMPANY_NAME = "SUPPLIER_COMPANY_NAME";
        public const string SUPPLIER_PHONE_SUPPORT = "SUPPLIER_PHONE_SUPPORT";
        public const string SUPPLIER_WEBSITE_SUPPORT = "SUPPLIER_WEBSITE_SUPPORT";
        public const string SUPPLIER_WEBSITE_SEARCH = "SUPPLIER_WEBSITE_SEARCH";

        public const string SIGN_TYPE_FILE = "SIGN_TYPE_FILE";
        //File upload
        public const string FILE_UPLOAD_EXTENSIVE = "*.xls|*.xlsx|*.xlt";
        //ENCRYPT_KEY
        public const string ENCRYPT_KEY = "120619841721484752016";
        //RabbitMQ
        //public const string MQ_NAME_HTKK = "MQ_NAME_HTKK";
        public const string MQ_NAME_LIST = "MQ_NAME_LIST";
        public const string MQ_NAME = "MQ_NAME";
        public const string MQ_PORT = "MQ_PORT";
        public const string MQ_USER = "MQ_USER";
        public const string MQ_PASSWORD = "MQ_PASSWORD";
        public const string MQ_HOST = "MQ_HOST";
        // FTP CONFIG
        public const string FTP_USING = "FTP_USING";
        public const string FTP_SERVER_URI = "FTP_SERVER_URI";
        public const string FTP_USER = "FTP_USER";
        public const string FTP_PASSWORD = "FTP_PASSWORD";
        public const string FTP_DOWNLOAD_FILE = "FTP_DOWNLOAD_FILE";
        //Return code
        /// <summary>
        /// Trạng thái Tiếp nhận giao dịch
        /// </summary>
        public const string RETURN_CODE_RECEIVE = "100";
        /// <summary>
        /// Trạng thái Giao dịch đang xử lý
        /// </summary>
        public const string RETURN_CODE_PROCESSING = "101";
        /// <summary>
        /// Trạng thái Giao dịch bị từ chối
        /// </summary>
        public const string RETURN_CODE_REJECT = "102";
        /// <summary>
        /// Trạng thái giao dịch thành công
        /// </summary>
        public const string RETURN_CODE_SUCCESS = "200";
        /// <summary>
        /// Trạng thái giao dịch thất bại
        /// </summary>
        public const string RETURN_CODE_FAILED = "201";
        /// <summary>
        /// Trạng thái giao dịch thành công chờ notify
        /// </summary>
        public const string RETURN_CODE_NOTIFY = "202";        
        /// <summary>
        /// Trạng thái giao dịch lỗi
        /// </summary>
        public const string RETURN_CODE_ERROR = "203";

        public const string RETURN_CODE_BAD_REQUEST = "400";
        public const string MESSAGE_BAD_BAD_REQUEST = "Yêu cầu không hợp lệ";
        /// <summary>
        /// Trạng thái cảnh báo giao dịch
        /// </summary>
        public const string RETURN_CODE_WARNING = "406";
        public const string MESSAGE_WARNING_ADD = "Cảnh báo dữ liệu chưa được thêm mới";
        public const string MESSAGE_WARNING_UPDATE = "Cảnh báo dữ liệu chưa được cập nhật";
        public const string MESSAGE_WARNING_DELETE = "Cảnh báo dữ liệu chưa được xóa";
        /// <summary>
        /// Trạng thái giao dịch không tồn tại
        /// </summary>
        public const string RETURN_CODE_FORBIDDEN = "403";        
        public const string MESSAGE_FORBIDDEN = "Bị từ chối truy cập,không đủ quyền hạn";

        public const string RETURN_CODE_NOT_FOUND = "404";        
        public const string MESSAGE_NOT_FOUND = "Không tìm thấy thông tin.";
        /// <summary>
        /// Trạng thái không có quyền truy cập
        /// </summary>
        public const string RETURN_CODE_UNAUTHORIZED = "401";
        public const string MESSAGE_UNAUTHORIZED = "Không được xác thực";
        
        public const string RETURN_CODE_CONFLICT = "409";
        public const string MESSAGE_CONFLICT = "Lỗi liên quan đến trùng dữ liệu.";

        public const string RETURN_CODE_INTERNAL_SERVER_ERROR = "500";
        public const string MESSAGE_INTERNAL_SERVER_ERROR = "Có lỗi không xác định hoặc không xử lý được ở phía server.";

        public const string SENDER_CODE = "SOCIAL";
        public const string SENDER_NAME = "Nền tảng SOCIAL";

        public const string RECEIVER_CODE = "DHUB";
        public const string RECEIVER_NAME = "Nền tảng DataHub";

        public const string MESSAGE_ERROR = "Thực hiện không thành công.";        
        public const string MESSAGE_NOT_DEFINE = "Thông tin chưa được định nghĩa.";
        
        public const string MESSAGE_ERROR_ADD = "Thêm mới dữ liệu không thành công.";
        public const string MESSAGE_ERROR_DELETE = "Xóa dữ liệu không thành công.";
        public const string MESSAGE_ERROR_UPDATE = "Cập nhật dữ liệu không thành công.";
        public const string MESSAGE_ERROR_EXIST = "Nội dung đã tồn tại trong hệ thống.";
        public const string MESSAGE_ERROR_FORMAT = "Nội dung không đúng định dạng.";

        public const string MESSAGE_SUCCESS = "Thực hiện thành công.";
        public const string MESSAGE_SUCCESS_NOT = "Thực hiện không thành công.";
        public const string MESSAGE_SUCCESS_EXTRACT = "Extract dữ liệu thành công";
        public const string MESSAGE_SUCCESS_EXTRACT_NOT = "Extract dữ liệu không thành công";

        public const string MESSAGE_SUCCESS_TRANSFORM = "Transform dữ liệu thành công";
        public const string MESSAGE_SUCCESS_TRANSFORM_NOT = "Transform dữ liệu không thành công";

        public const string MESSAGE_SUCCESS_ADD = "Thêm mới dữ liệu thành công.";
        public const string MESSAGE_SUCCESS_ADD_NOT = "Thêm mới dữ liệu không thành công.";

        public const string MESSAGE_SUCCESS_UPDATE = "Cập nhật dữ liệu thành công.";
        public const string MESSAGE_SUCCESS_UPDATE_NOT = "Cập nhật dữ liệu không thành công.";

        public const string MESSAGE_SUCCESS_DELETE = "Xóa dữ liệu thành công.";
        public const string MESSAGE_SUCCESS_DELETE_NOT = "Xóa dữ liệu không thành công.";        
        public const string MESSAGE_NOT_VALIDATE = "Nội dung message không đúng.";
        public const string MESSAGE_DATE_VALIDATE = "Ngày không đúng định dạng yyyy-MM-dd.";
        public const string MESSAGE_DATE_FULL_VALIDATE = "Ngày không đúng định dạng yyyy-MM-dd HH:mm:ss.";
        public const string MESSAGE_DATE_REPORT_VALIDATE = "Ngày không đúng định dạng yyyy-MM-dd hoặc yyyy-MM-dd HH:mm:ss.";
        public const string MESSAGE_DATE_COMPARE_VALIDATE = "Ngày bắt đầu không phải nhỏ hơn hoặc bằng ngày kết thúc.";
        public const string MESSAGE_NOT_VALIDATE_FIRE_BASE = "Người dung chưa được đăng ký nhận thông báo.";

        public const string MESSAGE_SUCCESS_AUT = "Xác thực thành công.";
        public const string MESSAGE_SUCCESS_AUT_NOT = "Xác thực không thành công.";
        public const string MESSAGE_SUCCESS_AUT_TOKEN = "Token hợp lệ.";
        public const string MESSAGE_SUCCESS_AUT_TOKEN_NOT = "Token không hợp lệ.";
        public const string MESSAGE_SUCCESS_CREATE_TOKEN = "Tạo mới Token thành công.";
        public const string MESSAGE_NOT_CREATE_TOKEN = "Tạo mới Token không thành công.";

        public const string MESSAGE_SERVICE_ACTIVE = "Dịch vụ đã được kích hoạt.";
        public const string MESSAGE_SERVICE_ACTIVE_NOT = "Dịch vụ chưa được kích hoạt.";
        public const string MESSAGE_SERVICE_DUPLICATE = "Dịch vụ đã được đăng ký.";
        public const string MESSAGE_SERVICE_EXPIRED = "Dịch vụ hết hạn sử dụng.";
        
        public const string MESSAGE_AUT_ERROR_INVALID = "Yêu cầu không hợp lệ.Vui lòng kiểm tra lại.";
        public const string MESSAGE_AUT_ERROR_GET_TOKEN = "Thực hiện tạo token không thành công.";
        public const string MESSAGE_AUT_ERROR = "Tài khoản không hợp lệ (Không tồn tại,Chưa kích hoạt,hết hạn sử dụng...)";
        public const string MESSAGE_AUT_ERROR_TOKEN = "Token không hợp lệ (Không tồn tại,Chưa kích hoạt,hết hạn sử dụng...)";
        public const string MESSSAGE_NOT_PERMISSION = "Không có quyền truy cập.";
        public const string MESSAGE_SERVER_OVERLOAD = "Server đang quá tải, bạn vui lòng thực hiện lại thao tác sau ít phút.";
        public const string MESSAGE_DATA_INVALID = "Dữ liệu chưa hợp lệ, bạn vui lòng kiểm tra lại.";

        public const string MESSAGE_VALIDATE_EMAIL = "Địa chỉ email không hợp lệ.";
        public const string MESSAGE_VALIDATE_PHONE = "Số điện thoại không hợp lệ.";
        public const string MESSAGE_VALIDATE_EMAIL_PHONE = "Thông tin khách hàng phải có số điện thoại hoặc email.";
        public const string MESSAGE_VALIDATE_CONTENT = "Nội dung không hợp lệ.";
        public const string MESSAGE_VALIDATE_PASSWORD = "2 Mật khẩu không giống nhau hợp lệ.";
        public const string MESSAGE_VALIDATE_ACCOUNT = "Tài khoản không hợp lệ.";
        public const string MESSAGE_VALIDATE_SESSION = "Tài khoản chưa đăng nhập.";
        public const string MESSAGE_VALIDATE_AUDIT = "2 đối tượng không hợp lệ.";
        public const string MESSAGE_VALIDATE_TRANS_AMOUNT = "Số tiền giao dịch phải lớn hơn 0.";
        public const string MESSAGE_VALIDATE_eKYC = "Khách hàng chưa xác thực eKYC.";

        public const string MESSAGE_VALIDATE_BANK_STK = "Số tài khoản không hợp lệ.";
        public const string MESSAGE_VALIDATE_BANK_STK_NAME = "Tên tài khoản không hợp lệ.";
        public const string MESSAGE_VALIDATE_BANK_NAME = "Mã ngân hàng không hợp lệ.";

        public const string MESSAGE_VALIDATE_FILLTER_LOGS = "Nội dung tra cứu chỉ trong quý hiện tại! ";
        public const string MESSAGE_NOT_MESSAGE_TYPE = "Loại giao dịch không hợp lệ.";
        public const string MESSAGE_NOT_APPLICATION = "Mã ứng dụng không hợp lệ.";
        public const string MESSAGE_NOT_RECEIVER = "Mã tiếp nhận không hợp lệ.";
        public const string MESSAGE_NOT_STATUS = "Mã trạng thái không hợp lệ.";
        public const string MESSAGE_NOT_SENDER_CODE = "Mã người gửi không hợp lệ.";

        public const string MESSAGE_TRANSACTION_DUPLICATE = "Mã giao dịch đã tồn tại.";
        public const string MESSAGE_TRANSACTION_SUCCESS = "Tiếp nhận giao dịch thành công.";
        public const string MESSAGE_TRANSACTION_ERROR = "Tiếp nhận giao dịch không thành công.";

        public const string MESSAGE_TRANSACTION_PROCESS_SUCCESS = "Tiếp nhận giao dịch chờ xử lý thành công.";
        public const string MESSAGE_TRANSACTION_PROCESS_ERROR = "Tiếp nhận giao dịch chờ xử lý không thành công.";

        public const string MESSAGE_TRANSACTION_PAYMENT_SUCCESS = "Xử lý giao dịch thành công.";
        public const string MESSAGE_TRANSACTION_PAYMENT_ERROR = "Xử lý giao dịch không thành công.";
        public const string MESSAGE_TRANSACTION_TRANSFER_SUCCESS = "Xử lý giao dịch thành công.";
        public const string MESSAGE_TRANSACTION_TRANSFER_ERROR = "Xử lý giao dịch không thành công.";
        public const string MESSAGE_TRANSACTION_CUSTOMER_ERROR = "Không tồn tại mã khách hàng.";
        public const string MESSAGE_TRANSACTION_CUSTOMER_NAME_ERROR = "Không tồn tại tên khách hàng.";
        public const string MESSAGE_TRANSACTION_CONTRACT_ERROR = "Không tồn tại mã hợp đồng.";
        public const string MESSAGE_TRANSACTION_CONTRACT_NAME_ERROR = "Không tồn tại tên hợp đồng.";
        public const string MESSAGE_TRANSACTION_TRANS_CODE_ERROR = "Không tồn tại mã giao dịch.";
        public const string MESSAGE_TRANSACTION_PAYMENT_WAIT_NOTIFY = "Xử lý giao dịch thành công.Chờ phản hồi từ kênh thanh toán";
        public const string MESSAGE_TRANSACTION_PAYMENT_SUCCESS_NOTIFY = "Xử lý giao dịch {0} thành công";
        public const string MESSAGE_TRANSACTION_PAYMENT_ERROR_NOTIFY = "Xử lý giao dịch {0} không thành công";
        public const string MESSAGE_TRANSACTION_CONFIG_NOTIFY = "Số điện thoại chưa được đăng ký notify.";

        public const string MESSAGE_TRANSACTION_CONTRACT_INTEREST_ERROR = "Mã hợp đồng chưa có lãi suất áp dụng để tính lãi hàng ngày";
        public const string MESSAGE_TRANSACTION_CONTRACT_INTEREST = "Lãi suất áp dụng để tính lãi hàng ngày phải lớn hơn 0.";
        public const string MESSAGE_TRANSACTION_CONTRACT_LIMITS_ERROR = "Mã hợp đồng chưa thiết lập hạn mức";
        public const string MESSAGE_TRANSACTION_CONTRACT_LIMITS = "Hạn mức thiết lập phải lớn hơn 0.";

        public const string MESSAGE_EVALUATE_EXPRESSIONS_ERROR = "Biểu thức không hợp lệ.";
        public const string MESSAGE_EVALUATE_ELEMENT_2_ERROR = "Số lượng phần tử phải > 1.";
        public const string MESSAGE_EVALUATE_ELEMENT_ERROR = "Phần tử không tồn tại trong biểu thức.";

        public const string MESSAGE_ERROR_CALL_API = "Thực hiện không thành công.";
        public const string MESSAGE_SUBJECT_EMAIL_PROCESS = "Thông báo xử lý giao dịch mã hợp đồng:{0}";
        public const string MESSAGE_SUBJECT_EMAIL_RECEIVER = "Thông báo tiếp nhận giao dịch mã hợp đồng:{0}";
        //Nội dung chuyển tiền
        public const string MESSAGE_TRANSER_CONTENT = "Xử lý mã hợp đồng:{0}";

        public const string MESSAGE_SERVICE_RUNNING = "Dịch vụ đang hoạt động.";
        public const string MESSAGE_SERVICE_STOP = "Dịch vụ đang bị đóng.";
        public const string MESSAGE_UPLOAD_STORAGE_ERROR = "Upload file thất bại.";
        public const string MESSAGE_UPLOAD_STORAGE = "Upload file thành công.";


    }
}

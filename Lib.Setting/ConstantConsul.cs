using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Lib.Setting
{
    public class ConstantConsul
    {
        //----------------------------PublicSetting--------------------------------------------------------------------------------------
        #region UrlConfig
        public const string UrlConfig_GatewayUrl = "PublicSetting:UrlConfig:GatewayUrl";
        //public const string UrlConfig_GatewaySecret = "PublicSetting:UrlConfig:GatewaySecret";
        public const string UrlConfig_OAuth2_Url = "PublicSetting:UrlConfig:OAuth2_Url";
        //public const string UrlConfig_OAuth2_Secret = "PublicSetting:UrlConfig:OAuth2_Secret";
        #endregion
        #region ConnectionDB  
        //ConnectionDB:PRF_TRANSACTION        
        public const string TTDLQG_TRANS_ConnectionString = "PublicSetting:ConnectionDB:TTDLQG_TRANS:ConnectionString";
        public const string PRF_TRANSACTION_ConnectionString = "PublicSetting:ConnectionDB:PRF_TRANSACTION:ConnectionString";
        public const string PRF_TRANSACTION_TypeName = "PublicSetting:ConnectionDB:PRF_TRANSACTION:TypeName";      
        //ConnectionDB:DINAMIC
        public const string DINAMIC_ConnectionString = "PublicSetting:ConnectionDB:{0}:ConnectionString";
        public const string DINAMIC_DatabaseName = "PublicSetting:ConnectionDB:{0}:DatabaseName";
        public const string DINAMIC_FindMaxTime = "PublicSetting:ConnectionDB:{0}:FindMaxTime";
        public const string DINAMIC_TypeName = "PublicSetting:ConnectionDB:{0}:TypeName";
        //ConnectionDB:MSB_NHDT_INTERNAL
        public const string MSB_NHDT_INTERNAL_ConnectionString = "PublicSetting:ConnectionDB:MSB_NHDT_INTERNAL:ConnectionString";
        public const string MSB_NHDT_INTERNAL_DatabaseName = "PublicSetting:ConnectionDB:MSB_NHDT_INTERNAL:DatabaseName";
        public const string MSB_NHDT_INTERNAL_FindMaxTime = "PublicSetting:ConnectionDB:MSB_NHDT_INTERNAL:FindMaxTime";
        public const string MSB_NHDT_INTERNAL_TypeName = "PublicSetting:ConnectionDB:MSB_NHDT_INTERNAL:TypeName";
        //ConnectionDB:MSB_NHDT_REPORT
        public const string MSB_NHDT_REPORT_ConnectionString = "PublicSetting:ConnectionDB:MSB_NHDT_REPORT:ConnectionString";
        public const string MSB_NHDT_REPORT_DatabaseName = "PublicSetting:ConnectionDB:MSB_NHDT_REPORT:DatabaseName";
        public const string MSB_NHDT_REPORT_FindMaxTime = "PublicSetting:ConnectionDB:MSB_NHDT_REPORT:FindMaxTime";
        public const string MSB_NHDT_REPORT_TypeName = "PublicSetting:ConnectionDB:MSB_NHDT_REPORT:TypeName";
        #endregion         
        #region S3_FileStorage
        public const string S3_FileStorage_RestEndPoint = "PublicSetting:S3_FileStorage:RestEndPoint";
        public const string S3_FileStorage_AccessKeyID = "PublicSetting:S3_FileStorage:AccessKeyID";
        public const string S3_FileStorage_RecretAccessKey = "PublicSetting:S3_FileStorage:RecretAccessKey";
        #endregion
        #region S3_Minio
        public const string Minio_EndPoint = "PublicSetting:Minio:EndPoint";
        public const string Minio_AccessKey = "PublicSetting:Minio:AccessKey";
        public const string Minio_SecretKey = "PublicSetting:Minio:SecretKey";
        public const string Minio_Secure = "PublicSetting:Minio:Secure";
        #endregion
        #region RabbitMQ_Config
        public const string RabbitMQ_HostName = "PublicSetting:RabbitMQ:HostName";
        public const string RabbitMQ_UserName = "PublicSetting:RabbitMQ:UserName";
        public const string RabbitMQ_Password = "PublicSetting:RabbitMQ:Password";
        public const string RabbitMQ_Port = "PublicSetting:RabbitMQ:Port";
        #endregion
        #region Redis
        public const string Redis_HostName = "PublicSetting:Redis:HostName";
        public const string Redis_Port = "PublicSetting:Redis:Port";
        #endregion
        #region Kafka
        public const string Kafka_BootstrapServers = "PublicSetting:Kafka:BootstrapServers";
        public const string Kafka_AutoOffsetRest = "PublicSetting:Kafka:AutoOffsetRest";
        public const string Kafka_EnableAutoOffsetStore = "PublicSetting:Kafka:EnableAutoOffsetStore";
        #endregion
        #region Notification
        public const string Notification_UrlConnect = "PublicSetting:Notification:UrlConnect";
        public const string Notification_GatewayUrl = "PublicSetting:Notification:GatewayUrl";
        public const string Notification_Realms = "PublicSetting:Notification:Realms";
        public const string Notification_Channel_NOTIFICATION_INTERNAL = "PublicSetting:Notification:Channel:NOTIFICATION_INTERNAL";
        public const string Notification_Channel_ETL_TRANSACTION = "PublicSetting:Notification:Channel:ETL_TRANSACTION";
        #endregion
        #region EmailSupport  
        public const string EmailSupport_Host = "PublicSetting:EmailSupport:Host";
        public const string EmailSupport_Port = "PublicSetting:EmailSupport:Port";
        public const string EmailSupport_UserName = "PublicSetting:EmailSupport:UserName";
        public const string EmailSupport_Password = "PublicSetting:EmailSupport:Password";
        public const string EmailSupport_DisplayName = "PublicSetting:EmailSupport:DisplayName:{TYPE_MAIL}";
        #endregion
        #region AccessTokenSso        
        public const string AccessTokenSso_UserName = "PublicSetting:AccessTokenSso:UserName";
        public const string AccessTokenSso_Password = "PublicSetting:AccessTokenSso:Password";
        public const string AccessTokenSso_Realms = "PublicSetting:AccessTokenSso:Realms";
        public const string AccessTokenSso_ClientID = "PublicSetting:AccessTokenSso:ClientID";
        public const string AccessTokenSso_AppCode = "PublicSetting:AccessTokenSso:AppCode";
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------

        //---------------------------- PrivateSetting ---------------------------------------------------------------------------------------
        #region microservice.integration       
        public const string Microservice_Integration_Service_Name = "PrivateSetting:microservice.integration:Service_Name";
        #endregion   
        #region microservice.sample       
        public const string Microservice_Sample_Service_Name = "PrivateSetting:microservice.sample:Service_Name";
        #endregion        
        #region Microservice.BashShell       
        public const string Microservice_BashShell_Service_Name = "PrivateSetting:Microservice.BashShell:Service_Name";
        #endregion        
        #region Microservice.BGWorker       
        public const string Microservice_BGWorker_Service_Name = "PrivateSetting:Microservice.BGWorker:Service_Name";
        public const string Microservice_BGWorker_Interval_SorlIndex = "PrivateSetting:Microservice.BGWorker:Interval:SorlIndex";
        public const string Microservice_BGWorker_Interval_TokenApi = "PrivateSetting:Microservice.BGWorker:Interval:TokenApi";
        public const string Microservice_BGWorker_Interval_CheckLogin = "PrivateSetting:Microservice.BGWorker:Interval:CheckLogin";
        #endregion        
        #region Microservice.Digital.Signature        
        public const string Microservice_Digital_Signature_Service_Name = "PrivateSetting:Microservice.Digital.Signature:Service_Name";
        #endregion      
        #region Microservice.ElasticSearch       
        public const string Microservice_ElasticSearch_Service_Name = "PrivateSetting:Microservice.ElasticSearch:Service_Name";
        #endregion
        #region Microservice.Finance       
        public const string Microservice_Finance_Service_Name = "PrivateSetting:Microservice.Finance:Service_Name";
        #endregion
        #region microservice.identity       
        public const string Microservice_Identity_Service_Name = "PrivateSetting:microservice.identity:Service_Name";
        public const string Microservice_Identity_Endpoint_AuthUri = "PrivateSetting:microservice.identity:Endpoint:AuthUri";
        public const string Microservice_Identity_Endpoint_AccessTokenUri = "PrivateSetting:microservice.identity:Endpoint:AccessTokenUri";
        public const string Microservice_Identity_Endpoint_UserInfoUri = "PrivateSetting:microservice.identity:Endpoint:UserInfoUri";
        public const string Microservice_Identity_UserName = "PrivateSetting:microservice.identity:UserName";
        public const string Microservice_Identity_PassWord = "PrivateSetting:microservice.identity:PassWord";
        public const string Microservice_Identity_ClientId = "PrivateSetting:microservice.identity:ClientId";
        public const string Microservice_Identity_Realms = "PrivateSetting:microservice.identity:Realms";
        public const string Microservice_Identity_ClientSecret = "PrivateSetting:microservice.identity:ClientSecret";
        
        #endregion
        #region microservice.internalss       
        public const string Microservice_Internal_Service_Name = "PrivateSetting:microservice.internalss:Service_Name";
        public const string Microservice_Internal_List_Storage = "PrivateSetting:microservice.internalss:List_Storage";
        #endregion
        #region Microservice.Logs        
        public const string Microservice_Logs_Service_Name = "PrivateSetting:Microservice.Logs:Service_Name";
        public const string Microservice_Logs_Channel_Registration = "PrivateSetting:Microservice.Logs:Channel_Registration";
        public const string Microservice_Logs_Channel_LOG_LOG_IN = "PrivateSetting:Microservice.Logs:Channel_Pub.Sub:LOG_LOG_IN";
        public const string Microservice_Logs_Channel_LOG_VERSION = "PrivateSetting:Microservice.Logs:Channel_Pub.Sub:LOG_VERSION";
        public const string Microservice_Logs_Channel_LOG_TRANSACTION = "PrivateSetting:Microservice.Logs:Channel_Pub.Sub:LOG_TRANSACTION";
        public const string Microservice_Logs_Channel_LOG_AUDIT = "PrivateSetting:Microservice.Logs:Channel_Pub.Sub:LOG_AUDIT";
        public const string Microservice_Logs_Channel_LOG_UPLOAD = "PrivateSetting:Microservice.Logs:Channel_Pub.Sub:LOG_UPLOAD";
        #endregion
        #region Microservice.Notification        
        public const string Microservice_Notification_Service_Name = "PrivateSetting:Microservice.Notification:Service_Name";
        public const string Microservice_Notification_Channel_Registration = "PrivateSetting:Microservice.Notification:Channel_Registration";
        public const string Microservice_Notification_Channel_NOTIFICATION_INTERNAL = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:NOTIFICATION_INTERNAL";
        public const string Microservice_Notification_Channel_ETL_TRANSACTION = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:ETL_TRANSACTION";
        public const string Microservice_Notification_Channel_SEND_EMAIL = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:SEND_EMAIL";
        public const string Microservice_Notification_Channel_SEND_SMS = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:SEND_SMS";
        public const string Microservice_Notification_Channel_SEND_FIRE_BASE = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:SEND_FIRE_BASE";
        public const string Microservice_Notification_Channel_Send_SLACK = "PrivateSetting:Microservice.Notification:Channel_Pub.Sub:SEND_SLACK";
        public const string Microservice_Notification_Send_Mail_Build = "PrivateSetting:Microservice.Notification:Send_Mail_Build";
        public const string Microservice_Notification_KEY = "PrivateSetting:Microservice.Notification:KEY";
        public const string Microservice_Notification_ID_SENDER = "PrivateSetting:Microservice.Notification:ID_SENDER";
        #endregion
        #region Microservice.Report       
        public const string Microservice_Report_Service_Name = "PrivateSetting:Microservice.Report:Service_Name";
        #endregion
        #region Microservice.ElasticSearch       
        public const string Microservice_Search_Service_Name = "PrivateSetting:Microservice.Search:Service_Name";
        #endregion
        #region Microservice.Transactions        
        public const string Microservice_Transactions_Endpoint_Url_SendMail = "PrivateSetting:Microservice.Transactions:Endpoint:Url_SendMail";
        public const string Microservice_Transactions_Endpoint_Url_GetCustomerInfo = "PrivateSetting:Microservice.Transactions:Endpoint:Url_GetCustomerInfo";
        public const string Microservice_Transactions_Endpoint_Url_GetContractInfo = "PrivateSetting:Microservice.Transactions:Endpoint:Url_GetContractInfo";
        /// <summary>
        /// Có lãi suất áp dụng để tính lãi hàng ngày
        /// </summary>
        public const string Microservice_Transactions_Endpoint_Url_ProProdPoliPriApply = "PrivateSetting:Microservice.Transactions:Endpoint:Url_ProProdPoliPriApply";
        /// <summary>
        /// Hạn mức giao dịch nạp/rút tiền là bao nhiêu
        /// </summary>
        public const string Microservice_Transactions_Endpoint_Url_ProProdLimitApply = "PrivateSetting:Microservice.Transactions:Endpoint:Url_ProProdLimitApply";

        public const string Microservice_Transactions_Endpoint_Url_AppOTaPay_Payment = "PrivateSetting:Microservice.Transactions:Endpoint:Url_AppOTaPay_Payment";
        public const string Microservice_Transactions_Endpoint_Url_AppOTaPay_Transfer = "PrivateSetting:Microservice.Transactions:Endpoint:Url_AppOTaPay_Transfer";
        public const string Microservice_Transactions_Endpoint_Url_OnePay_Transfer = "PrivateSetting:Microservice.Transactions:Endpoint:Url_OnePay_Transfer";
        public const string Microservice_Transactions_Endpoint_Url_GetConfigFireBase = "PrivateSetting:Microservice.Transactions:Endpoint:Url_GetConfigFireBase";
        public const string Microservice_Transactions_Endpoint_Url_NotifyFireBase = "PrivateSetting:Microservice.Transactions:Endpoint:Url_NotifyFireBase";        

        public const string Microservice_Transactions_Service_Name = "PrivateSetting:Microservice.Transactions:Service_Name";
        public const string Microservice_Transactions_Mail_CC_Transaction = "PrivateSetting:Microservice.Transactions:Mail_CC_Transaction";

        public const string Microservice_Transactions_Channel_Registration = "PrivateSetting:Microservice.Transactions:Channel_Registration";
        public const string Microservice_Transactions_Config_ChannelTransfer_ONEPAY_MAX_VALUE = "PrivateSetting:Microservice.Transactions:Config_Channel.Transfer:ONEPAY_MAX_VALUE";
        public const string Microservice_Transactions_Config_ChannelTransfer_APPOTAPAY_MAX_VALUE = "PrivateSetting:Microservice.Transactions:Config_Channel.Transfer:APPOTAPAY_MAX_VALUE";
        public const string Microservice_Transactions_Config_ChannelTransfer_KSS_MAX_VALUE = "PrivateSetting:Microservice.Transactions:Config_Channel.Transfer:KSS_MAX_VALUE";

        public const string Microservice_Transactions_Channel_PAY0001 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:PAY0001";
        public const string Microservice_Transactions_Channel_TRA0001 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0001";
        public const string Microservice_Transactions_Channel_PAY0002 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:PAY0002";
        public const string Microservice_Transactions_Channel_TRA0002 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0002";
        public const string Microservice_Transactions_Channel_TRA0002_SF = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0002_SF";
        public const string Microservice_Transactions_Channel_PAY0002_NOTIFY = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:PAY0002_NOTIFY";
        public const string Microservice_Transactions_Channel_PAY0002_SF_NOTIFY = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:PAY0002_SF_NOTIFY";
        public const string Microservice_Transactions_Channel_TRA0002_NOTIFY = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0002_NOTIFY";
        public const string Microservice_Transactions_Channel_TRA0002_SF_NOTIFY = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0002_SF_NOTIFY";
        public const string Microservice_Transactions_Channel_PAY0003 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:PAY0003";
        public const string Microservice_Transactions_Channel_TRA0003 = "PrivateSetting:Microservice.Transactions:Channel_Pub.Sub:TRA0003";
        #endregion
        #region Microservice.Upload       
        public const string Microservice_Upload_Service_Name = "PrivateSetting:Microservice.Upload:Service_Name";
        #endregion
        #region microservice.eventbus       
        public const string Microservice_ServiceBus_Service_Name = "PrivateSetting:microservice.eventbus:Service_Name";        
        public const string Microservice_ServiceBus_GroupId = "PrivateSetting:microservice.eventbus:GroupId";        
        #endregion
        #region Microservice.Kafka       
        public const string Microservice_Kafka_Service_Name = "PrivateSetting:Microservice.Kafka:Service_Name";        
        public const string Microservice_Kafka_GroupId = "PrivateSetting:Microservice.Kafka:GroupId";        
        #endregion


        //---------------------------- ----------------- ---------------------------------------------------------------------------------------
    }
}

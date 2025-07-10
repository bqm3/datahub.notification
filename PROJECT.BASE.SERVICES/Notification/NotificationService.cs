using FirebaseAdmin.Messaging;
using Lib.Consul.Configuration;
using Lib.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PROJECT.BASE.SERVICES
{
    public class NotificationService : INotificationService
    {
        private ISmsService _smsService;
        private ISlackService _slackService;
        private ILogger<NotificationService> _logger;
        public NotificationService(
            ISmsService smsService,
            ISlackService slackService,
            ILogger<NotificationService> logger)
        {
            _smsService = smsService;
            _slackService = slackService;
            _logger = logger;
        }
        public void SendNotification(string urlConnect, MessegeQueue data)
        {
            var jsonMessage = JsonConvert.SerializeObject(data.Message);
            var result = SignalRClient.InvokeSendMessage(urlConnect, JsonConvert.DeserializeObject<MessageInfo>(jsonMessage));
            if (result)
            {
                var info = DataObjectFactory.GetInstanceNOTIFICATION().NOTIFICATION_GetInfo(data.Id);
                if (info != null)
                {
                    info.STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS);
                    info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                    info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                    DataObjectFactory.GetInstanceNOTIFICATION().NOTIFICATION_Update(info);
                }
            }
        }
        public void SendSlackMessage(MessegeQueue data)
        {
            var jsonMessage = JsonConvert.SerializeObject(data.Message);
            var sendSms = JsonConvert.DeserializeObject<SendSmsRequest>(jsonMessage);
            var slackRequest = new SendSlackMessageRequest() { Text = "ToPhone:" + sendSms.Phone + ". Message:" + sendSms.Content };
            var result = _slackService.SendMessageAsync(slackRequest).ConfigureAwait(false).GetAwaiter().GetResult();


            var info = DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_GetInfo(data.Id);

            if (info == null) return;

            if (result == null)
            {
                info.STATUS = short.Parse(Constant.RETURN_CODE_ERROR);
                info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_Update(info);
                return;
            }

            info.STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS);
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_Update(info);
        }
        public void SendSMS(MessegeQueue data)
        {
            var jsonMessage = JsonConvert.SerializeObject(data.Message);

            var sendSms = JsonConvert.DeserializeObject<SendSmsRequest>(jsonMessage);

            var result = _smsService.SendSmsAsync(sendSms).ConfigureAwait(false).GetAwaiter().GetResult();

            var info = DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_GetInfo(data.Id);

            if (info == null) return;

            if (result == null)
            {
                info.STATUS = short.Parse(Constant.RETURN_CODE_ERROR);
                info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_Update(info);
                return;
            }


            //100 Request đã được nhận và xử lý thànhcông.
            //104 Brandname không tồn tại hoặc đã bịhủy
            //118 Loại tin nhắn không hợp lệ
            //119 Brandname quảng cáo phải gửi ít nhất20 số điện thoại
            //131 Tin nhắn brandname quảng cáo độ dàitối đa 422 kí tự
            //132 Không có quyền gửi tin nhắn đầu số cốđịnh 8755
            //99 Lỗi không xác định
            //
            //177 Brandname không có hướng ( 
            // - Viettel -The Network Viettel have not registry.
            // - VinaPhone - The Network VinaPhone have not registry.
            // - Mobifone - The Network Mobifone have not registry.
            // - Gtel - The Network Gtel have not registry.
            // - Vietnammobile - The Network Vietnammoile have not registry.
            //)
            //
            //159 RequestId quá 120 ký tự
            //145 Sai template mạng xã hội
            //146 Sai template Brandname CSKH
            //101 Sai ApiKey hoặc SecretKey
            //103 Tài khoản không đủ tiền
            if(result.CodeResult != "100")
            {
                info.STATUS = short.Parse(Constant.RETURN_CODE_ERROR);
                info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                info.VERIFY_CODE = JsonConvert.SerializeObject(result);
                DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_Update(info);
                return;
            }


            info.STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS);
            info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
            info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
            DataObjectFactory.GetInstanceSEND_SMS().SEND_SMS_Update(info);

        }

        public void SendEmail(MessegeQueue data)
        {
            var jsonMessage = JsonConvert.SerializeObject(data.Message);
            var sendMail = JsonConvert.DeserializeObject<SendMail>(jsonMessage);
            string mailTo = sendMail.ToEmail;
            string mailCC = sendMail.CCEmail;
            string mailBCC = string.Empty;
            string subject = sendMail.Subject;
            string htmlBody = string.Empty;
            string pathAttachment = string.Empty;
            if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/configuration/templates/{sendMail.WarningType}.html"))
            {
                htmlBody = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/configuration/templates/{sendMail.WarningType}.html");
                foreach (var item in sendMail.Content)
                    htmlBody = htmlBody.Replace("{" + item.Key.ToUpper() + "}", item.Value);
            }
            if (sendMail.Attachment != null && sendMail.Attachment.Count > 0)
            {
                foreach (var fileInfo in sendMail.Attachment)
                    pathAttachment += Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/configuration/data_files/{DateTime.Now.ToString("yyyy-MM-dd")}/{fileInfo.FileName};";
            }

            string eMailFrom = BaseSettings.Get(ConstantConsul.EmailSupport_UserName);
            string ePassword = BaseSettings.Get(ConstantConsul.EmailSupport_Password);
            string eDisplayname = BaseSettings.Get(ConstantConsul.EmailSupport_DisplayName.Replace("{TYPE_MAIL}", sendMail.WarningType));
            string smtpHost = BaseSettings.Get(ConstantConsul.EmailSupport_Host);
            string smtpPort = BaseSettings.Get(ConstantConsul.EmailSupport_Port);
            var result = NotificationHelper.SendEmail365(mailTo, mailCC, mailBCC, subject, htmlBody, pathAttachment, eMailFrom, ePassword, eDisplayname, smtpHost, int.Parse(smtpPort));
            if (result)
            {
                var info = DataObjectFactory.GetInstanceSEND_MAIL().SEND_MAIL_GetInfo(data.Id);
                if (info != null)
                {
                    info.STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS);
                    info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                    info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                    DataObjectFactory.GetInstanceSEND_MAIL().SEND_MAIL_Update(info);
                }

            }
        }
        public async void SendFireBase(MessegeQueue data)
        {
            var sendFireBase = JsonConvert.DeserializeObject<FirebaseModel>(data.Message.ToString());            
            
            var msg = new MulticastMessage()
            {
                Tokens = sendFireBase.ListTokens,
                Data = new Dictionary<string, string>()
                {
                    {"title", sendFireBase.Title},
                    {"body", sendFireBase.Message},
                    {"notiType", sendFireBase.NotiType },
                    {"transCode", sendFireBase.TransCode }
                }
            };
            
            //_logger.LogInformation($"DATA: {JsonConvert.SerializeObject(message)}");
            var firebaseMessage = FirebaseMessaging.DefaultInstance;
            var response = await firebaseMessage.SendMulticastAsync(msg);            
            if (response.FailureCount == 0 && response.SuccessCount > 0)
            {
                var info = DataObjectFactory.GetInstanceSEND_FIRE_BASE().SEND_FIRE_BASE_GetInfo(data.Id);
                if (info != null)
                {
                    info.STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS);
                    info.LDATE = Utility.ConvertToUnixTime(DateTime.Now);
                    info.LUSER = BaseSettings.Get(ConstantConsul.Microservice_Notification_Service_Name);
                    DataObjectFactory.GetInstanceSEND_FIRE_BASE().SEND_FIRE_BASE_Update(info);
                }
            }
        }
        public string LogFileReplace(string pathFile)
        {
            string data = File.ReadAllText(pathFile);
            List<string> CorsPolicyWithOrigins = new List<string>(); ;
            var asArray = Config.CONFIGURATION_PRIVATE.AppSetting.PrivateLog.AsArray;
            if (asArray.Count > 0)
            {
                string dataFile = data;
                foreach (var item in asArray)
                    dataFile = dataFile.Replace(item.Value.ToString().Replace("\"", string.Empty), "#######################");
                return dataFile;
            }
            return string.Empty;
        }
        public async void WriteLogFileReplace(string pathFile)
        {
            string data = File.ReadAllText(pathFile);
            List<string> CorsPolicyWithOrigins = new List<string>(); ;
            var asArray = Config.CONFIGURATION_PRIVATE.AppSetting.PrivateLog.AsArray;
            if (asArray.Count > 0)
            {
                string dataFile = data;
                foreach (var item in asArray)
                    dataFile = dataFile.Replace(item.Value.ToString().Replace("\"", string.Empty), "#######################");
                await File.WriteAllTextAsync(pathFile, dataFile);
            }
        }
    }
}

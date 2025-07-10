using Lib.Consul.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PROJECT.BASE.SERVICES
{
    public static class SendSmsResultCode
    {

        public const int SystemError = 1000;
        public const int FailToSendMessage = 999;
        public const int VerifyCodeIsEmpty = 998;
        public const int MessageIsEmpty = 997;
        public const int SmsContentTypeIsNotSupported = 996;
        public const int PhoneNumberIsEmpty = 995;
        public const int InvalidParameters = 994;
        public const int Success = 1;
    }
    public static class SendSmsResultMessage
    {
        public const string FailToSendMessage = "Fail to send message";
        public const string VerifyCodeIsEmpty = "Parameters is empty";
        public const string MessageIsEmpty = "Message is Empty";
        public const string SmsContentTypeIsNotSupported = "SmsContentType {0} is not supported";
        public const string SystemError = "Exception is occured! {0}";
        public const string PhoneNumberIsEmpty = "Phone number is empty";
        public const string Success = "Success";
    }

    public static class SmsContent
    {
        public static string GetSmsContentTypeName(int smsContentType)
        {
            var smsContentTypes = BaseSettings.GetDictionary("SmsContentType");
            foreach (var item in smsContentTypes)
            {
                if (Convert.ToInt32(item.Value) == smsContentType)
                {
                    return item.Key;
                }
            }
            return string.Empty;
        }
        
        public static SendSmsRequest Fill(this SendSmsRequest request, int smsContentType)
        {
            var contentTypeName = BaseSettings.Get(GetSmsContentTypeName(smsContentType));
            var contentTypeOTPs = BaseSettings.Get("ContentTypeOTPs");
            
            if (contentTypeOTPs.Contains(contentTypeName))
            {
            }
            
            return request;
        }
        
        public static string GetSmsMessage(int smsContentType, Dictionary<string, string> _parameters, out string errorMessage)
        {
            var smsContentTypeName = GetSmsContentTypeName(smsContentType);
            if(string.IsNullOrEmpty(smsContentTypeName))
            {
                errorMessage = $"SmsContentType {smsContentType} is not support!";
                return string.Empty;
            }    
            string messageTemplate = BaseSettings.Get($"SmsTemplate:{smsContentTypeName}");

            if (string.IsNullOrEmpty(messageTemplate))
            {
                errorMessage = $"SmsContentType {smsContentType} is not support!";
                return string.Empty;
            }

            string pattern = "{{(.*?)}}";
            var listMatches = Regex.Matches(messageTemplate, pattern);
            var listParams = new List<string>();

            foreach(Match match in listMatches)
            {
                foreach(Capture capture in match.Captures)
                {
                    listParams.Add(capture.Value);
                }    
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>(_parameters, StringComparer.OrdinalIgnoreCase);

            foreach (var parameter in listParams)
            {
                var paramName = parameter.Replace("{{", "").Replace("}}", "").Split('_')[0];
                var paramMaxLength = Convert.ToInt32(parameter.Replace("{{", "").Replace("}}", "").Split('_')[1]);

                if(!parameters.ContainsKey(paramName))
                {
                    errorMessage = $"parameter {paramName} is not found";
                    return string.Empty;
                }
                
                if(parameters[paramName].Length > paramMaxLength)
                {
                    errorMessage = $"parameter {paramName} has max length {paramMaxLength} characters";
                    return string.Empty;
                }

                messageTemplate = messageTemplate.Replace(parameter, parameters[paramName]);
            }

            errorMessage = string.Empty;
            return messageTemplate;
        }
    }
}
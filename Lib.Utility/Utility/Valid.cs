using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Lib.Utility
{
    public static class Valid
    {
        public static bool IsValidXml(string data,ref string message)
        {
            if (string.IsNullOrEmpty(data)) return false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        private static List<string> outMessage;
        public static List<string> ValidateXML(XmlDocument xmlDocument, string pathXSD)
        {
            outMessage = new List<string>();
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", pathXSD);
            XmlReader xmlReader = new XmlNodeReader(xmlDocument);
            XDocument doc = XDocument.Load(xmlReader);
            //XmlReader rd = XmlReader.Create(pathXML);
            //XDocument doc = XDocument.Load(rd);
            doc.Validate(schema, ValidationEventHandler);
            return outMessage;
        }
        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error)
                {
                    outMessage.Add(e.Message);
                }

            }
        }
        public static bool IsValidJson(string strInput, ref string message)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    message = jex.Message;
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    message = ex.Message;
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidEmail(this string strInput)
        {
            return Regex.IsMatch(strInput, @"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+\.)+[a-z]{2,5}$");
        }
        public static bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            string sMailPattern = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }
        public static bool IsNumericType(object obj)
        {
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsValidDate(string value, string dateFormats)
        {
            DateTime result;
            bool validDate = DateTime.TryParseExact(value, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result);
            return validDate;
        }
        // Hàm kiểm tra đầu vào từ người dùng xem có thể là SQL Injection hay không
        public static bool IsSqlInjection(string input)
        {
            // Kiểm tra các chuỗi phổ biến dùng trong SQL Injection
            string[] sqlInjectionPatterns = new string[]
            {
            "' OR 1=1 --", // Cách viết phổ biến của SQL Injection
            "' OR 'a'='a", // Một cách khác của SQL Injection
            "' UNION SELECT", // Câu lệnh UNION có thể dùng để tấn công
            "'; DROP TABLE", // Thực hiện câu lệnh DROP TABLE
            "--", // Comment trong SQL
            "/*", "*/" // Comment multi-line trong SQL
            };

            foreach (var pattern in sqlInjectionPatterns)
            {
                if (input.Contains(pattern))
                {
                    Console.WriteLine("Có thể là SQL Injection: " + pattern);
                    return true;
                }
            }

            return false;
        }
    }
}

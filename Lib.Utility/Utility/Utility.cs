//using Oracle.DataAccess.Client;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Lib.Utility
{
    public static class Utility
    {
        public static string GenerateIPV6()
        {
            try
            {
                byte[] bytes = new byte[16];
                new Random().NextBytes(bytes);
                IPAddress ipv6Address = new IPAddress(bytes);
                string addressString = ipv6Address.ToString();
                return addressString;
            }
            catch (Exception ex)
            {
                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return string.Empty;
            }
        }

        //public static ExpandoObject Merge<TLeft, TRight>(this TLeft left, TRight right)
        //{
        //    var expando = new ExpandoObject();
        //    IDictionary<string, object> dict = expando;
        //    foreach (var p in typeof(TLeft).GetProperties())
        //        dict[p.Name] = p.GetValue(left);
        //    foreach (var p in typeof(TRight).GetProperties())
        //        dict[p.Name] = p.GetValue(right);
        //    return expando;
        //}
        public static Dictionary<K, V> MergeLeft<K, V>(this Dictionary<K, V> me, params IDictionary<K, V>[] others)
        {
            var newMap = new Dictionary<K, V>(me, me.Comparer);
            foreach (IDictionary<K, V> src in
                (new List<IDictionary<K, V>> { me }).Concat(others))
            {
                // ^-- echk. Not quite there type-system.
                foreach (KeyValuePair<K, V> p in src)
                {
                    if (string.IsNullOrEmpty(me[p.Key].ToString()))
                        newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }        
        public static Dictionary<string, string> GetParams(string str)
        {
            int questionMarkIndex = str.IndexOf('?');
            if (questionMarkIndex != -1)
            {
                string paramsString = str.Substring(questionMarkIndex + 1);
                string[] keyValues = paramsString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> result = new Dictionary<string, string>();

                foreach (string keyValue in keyValues)
                {
                    string[] splitKeyValue = keyValue.Split(new char[] { '=' });
                    if (splitKeyValue.Length == 2 && !string.IsNullOrWhiteSpace(splitKeyValue[0]))
                    {
                        string unencodedKey = Uri.UnescapeDataString(splitKeyValue[0]);
                        string unencodedValue = Uri.UnescapeDataString(splitKeyValue[1]);
                        result[unencodedKey] = unencodedValue;
                    }
                }

                return result;
            }
            else return new Dictionary<string, string>();
        }
        public static string GetNodeValue(XmlDocument xmlDocument, string nodeKey)
        {
            string result = string.Empty;
            string namespaceURI = xmlDocument.DocumentElement.NamespaceURI;
            if (!string.IsNullOrWhiteSpace(namespaceURI))
            {
                XmlNamespaceManager nsmgrSign = new XmlNamespaceManager(xmlDocument.NameTable);
                nsmgrSign.AddNamespace("NamespaceCode", namespaceURI);
                if (xmlDocument.SelectNodes(string.Format("//NamespaceCode:{0}", nodeKey), nsmgrSign).Count > 0)
                {
                    result = xmlDocument.SelectNodes(string.Format("//NamespaceCode:{0}", nodeKey), nsmgrSign)[0].InnerText;
                }
            }
            return result;
        }
        public static byte[] ObjectToByte<T>(this T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T ByteToObject<T>(this byte[] byteArray)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(byteArray));
        }
        //public static byte[] ArraySerialize<T>(this T m)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        new BinaryFormatter().Serialize(ms, m);
        //        return ms.ToArray();
        //    }
        //}
        //public static T ArrayDeserialize<T>(this byte[] byteArray)
        //{
        //    using (var ms = new MemoryStream(byteArray))
        //    {
        //        return (T)new BinaryFormatter().Deserialize(ms);
        //    }
        //}
        
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
        public static string GetRandomString(string Prefix)
        {
            Thread.Sleep(1);
            string result = Prefix + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return result;
        }
        private static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
        public static string GetRandomString(string format, string pattern)
        {
            Random rn = new Random();
            string charsToUse = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa123456789";
            MatchEvaluator RandomChar = delegate (Match m)
            {
                return charsToUse[rn.Next(charsToUse.Length)].ToString();
            };
            return Regex.Replace(format, pattern, RandomChar);
            //return Regex.Replace("XXXX:XXXX:XXXX:XXXX", "X", RandomChar);
            //Console.WriteLine(Regex.Replace("XXXX-XXXX-XXXX-XXXX-XXXX", "X", RandomChar));// Lv2U-jHsa-TUep-NqKa-jlBx
            //Console.WriteLine(Regex.Replace("XXXX", "X", RandomChar)); // 8cPD
        }
        public static string GetRandomStringIPV6(string format, string pattern)
        {
            Random rn = new Random();
            string charsToUse = "123456789abcdef";
            MatchEvaluator RandomChar = delegate (Match m)
            {
                return charsToUse[rn.Next(charsToUse.Length)].ToString();
            };
            return Regex.Replace(format, pattern, RandomChar);
            //return Regex.Replace("XXXX:XXXX:XXXX:XXXX", "X", RandomChar);
            //Console.WriteLine(Regex.Replace("XXXX-XXXX-XXXX-XXXX-XXXX", "X", RandomChar));// Lv2U-jHsa-TUep-NqKa-jlBx
            //Console.WriteLine(Regex.Replace("XXXX", "X", RandomChar)); // 8cPD
        }
        public static byte[] ConvertStringToBytes(string str)
        {
            try
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            catch
            {
                return null;
            }
        }
        public static string ConvertByteToString(byte[] bytes)
        {
            try
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
            catch (Exception ex)
            {
                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return string.Empty;
            }
        }
        public static string LoadFile(string pathFile)
        {
            try
            {
                string value = "";
                using (StreamReader streamReader = new StreamReader(pathFile))
                {
                    value = streamReader.ReadToEnd();
                }
                return value;
            }
            catch (Exception ex)
            {
                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return string.Empty;
            }
        }
        private static string GetValues(Int32 total, Int32 siZe)
        {
            try
            {
                Int32 totalRemainder = total % siZe;
                Int32 totalGroup = (total - totalRemainder) / siZe;
                return totalGroup.ToString() + "#" + (totalRemainder).ToString();
            }
            catch (Exception ex)
            {

                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return "";
            }
        }
        public static List<int> GetListPage(int totalRecord, int pageSize)
        {
            try
            {
                List<int> listPage = new List<int>();
                if (totalRecord > 0)
                {
                    string strSplip = GetValues(totalRecord, pageSize);
                    int totalGroup = 0;
                    int totalRemainder = 0;
                    int temp = 0;
                    string[] listItem = strSplip.Split('#');
                    if (listItem.Length > 0)
                    {
                        totalGroup = int.Parse(listItem[0].ToString());
                        totalRemainder = int.Parse(listItem[1].ToString());
                    }

                    if (totalGroup > 1)
                    {
                        for (int i = 1; i <= totalGroup; i++)
                        {
                            temp = i;
                            listPage.Add(i);
                        }
                        if (totalRemainder > 0)
                            listPage.Add(temp + 1);

                    }
                    else
                    {
                        if (totalGroup == 1)
                        {
                            listPage.Add(1);
                            if (totalRemainder > 0)
                                listPage.Add(2);
                        }
                        else
                        {
                            if (totalRemainder > 0)
                                listPage.Add(1);
                        }
                    }
                }
                return listPage;
            }
            catch (Exception ex)
            {
                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }
        }
        public static void SendEmail(string mailTo, string subject, string htmlBody, string pathAttack, string eMailFrom, string ePassword, string eDisplayname, string eHost, int ePort, bool eEnableSsl, bool eUseDefaultCredentials)
        {
            new Thread(() =>
            {
                try
                {

                    MailMessage message1 = new MailMessage();
                    message1.From = new MailAddress(eMailFrom, eDisplayname, Encoding.UTF8);
                    message1.To.Add(new MailAddress(mailTo));
                    message1.Body = htmlBody;
                    message1.IsBodyHtml = true;
                    message1.Subject = subject;
                    message1.Priority = MailPriority.High;
                    if (!string.IsNullOrEmpty(pathAttack))
                    {
                        Attachment att = new Attachment(pathAttack);
                        message1.Attachments.Add(att);
                    }
                    message1.SubjectEncoding = Encoding.UTF8;
                    message1.BodyEncoding = Encoding.UTF8;
                    var smtp = new System.Net.Mail.SmtpClient
                    {
                        Host = eHost,
                        Port = ePort,
                        EnableSsl = eEnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = eUseDefaultCredentials,
                        Credentials = new NetworkCredential(eMailFrom, ePassword)
                    };
                    smtp.Send(message1);
                }
                catch (Exception ex)
                {
                    // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                }
            }).Start();
        }

        #region Date
        public static DateTime GetCurentDateInternet()
        {
            try
            {
                DateTime dateTime = new DateTime(1900, 1, 1);
                string todaysDates = string.Empty;
                string[] servers = new string[] {
                                            "http://www.microsoft.com",
                                            "https://www.google.com/"
                                            };
                Random rnd = new Random();
                foreach (string server in servers.OrderBy(s => rnd.NextDouble()).Take(5))
                {
                    try
                    {
                        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create(server);
                        var response = myHttpWebRequest.GetResponse();
                        todaysDates = response.Headers["date"];
                    }
                    catch
                    {
                        todaysDates = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(todaysDates))
                    {
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(todaysDates))
                {
                    dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);

                }
                return dateTime;
            }
            catch (Exception ex)
            {
                // LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return DateTime.Now;
            }


        }
        // <summary>
        /// Lấy ra ngày đầu tiên trong tháng có chứa
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtDate">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng được truyền vào
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth">Thứ tự của tháng trong năm</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng có chứa
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtInput">Ngày nhập vào</param>
        /// <returns>Ngày cuối cùng trong tháng</returns>
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng được truyền vào
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// Lấy ra quý của năm
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetQuarterOfYear(int month)
        {
            if (month == 1 || month == 2 || month == 3)
                return 1;
            if (month == 4 || month == 5 || month == 6)
                return 2;
            if (month == 7 || month == 8 || month == 9)
                return 3;
            if (month == 10 || month == 11 || month == 12)
                return 4;
            return -1;
        }

        /// <summary>
        /// Lấy ra ngày bắt đầu và ngày kết thúc của quý trong năm
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime[] GetDateQuarterOfYear(DateTime date)
        {
            if (date.Month == 1 || date.Month == 2 || date.Month == 3)
            {
                DateTime[] result = { new DateTime(date.Year, 1, 1, 1, 0, 0), new DateTime(date.Year, 3, 31, 23, 59, 59) };
                return result;
            }
            if (date.Month == 4 || date.Month == 5 || date.Month == 6)
            {
                DateTime[] result = { new DateTime(date.Year, 4, 1, 1, 0, 0), new DateTime(date.Year, 6, 30, 23, 59, 59) };
                return result;
            }
            if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                DateTime[] result = { new DateTime(date.Year, 7, 1, 1, 0, 0), new DateTime(date.Year, 9, 30, 23, 59, 59) };
                return result;
            }
            if (date.Month == 10 || date.Month == 11 || date.Month == 12)
            {
                DateTime[] result = { new DateTime(date.Year, 10, 1, 1, 0, 0), new DateTime(date.Year, 12, 31, 23, 59, 59) };
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert UserDateTime({9/7/2018 8:37:20 AM}) to JSON datetime(1536309440373) format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetJSONFromUserDateTime(DateTime dateTime)
        {
            string jsonDateTime = string.Empty;
            if (dateTime != null)
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                jsonDateTime = JsonConvert.SerializeObject(dateTime, microsoftDateFormatSettings);
                jsonDateTime = jsonDateTime.Replace("\"\\/Date(", "").Replace(")\\/\"", "");
            }
            return jsonDateTime;
        }

        /// <summary>
        /// Convert JSON datetime(1536309440373) to user datetime({9/7/2018 8:37:20 AM})
        /// </summary>
        /// <param name="jsonDateTime"></param>
        /// <returns></returns>
        public static dynamic GetUserDateTimeFromJSON(string jsonDateTime)
        {
            dynamic userDateTime = null;
            if (!string.IsNullOrEmpty(jsonDateTime))
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                userDateTime = JsonConvert.DeserializeObject("\"\\/Date(" + jsonDateTime + ")\\/\"", microsoftDateFormatSettings);
            }
            return userDateTime;
        }

        /// <summary>
        /// Convert UserDateTime({9/7/2018 8:37:20 AM}) to datetime 2017-09-08T16:08:19.290Z format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetJSONZFromUserDateTime(DateTime dateTime)
        {
            //string jsonDateTime = dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            string jsonDateTime = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            return jsonDateTime;
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }

        #endregion

        public static string AddCommas(string value)
        {
            string amount = value.Replace("\\,", "").Replace(" ", "");
            string phannguyen = amount.Split('.')[0];
            string phanthapphan = amount.IndexOf(".") > 0 ? amount.Split('.')[1] : "";
            char[] charArray = phannguyen.ToCharArray();
            Array.Reverse(charArray);
            amount = new string(charArray);
            string output = "";
            for (var i = 0; i <= amount.Length - 1; i++)
            {
                output = amount[i] + output;
                if ((i + 1) % 3 == 0 && (amount.Length - 1) != i) output = ',' + output;
            }
            return !string.IsNullOrEmpty(phanthapphan) ? (output + "." + phanthapphan) : output;
        }

        #region Doc so thanh chu

        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }

        private static string Donvi(string so)
        {
            string Kdonvi = "";

            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }

        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";

            }


            return Ktach;

        }

        public static string ChuyenSoThanhChu(decimal gNum)
        {
            if (gNum == 0)
                return "Không đồng";

            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            decimal Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";

            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";

            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai

            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";

            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng.";

            return lso_chu.ToString().Trim();

        }

        #endregion

        #region Conver datatype
        public static T CastObject<T>(object input)
        {
            return (T)input;
        }
        public static T ConvertObject<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
        public static string ConvertDataTypeSQLToSolr(string dataTypeSQL)
        {
            string dataTypeOracle = "";
            switch (dataTypeSQL.ToLower())
            {
                case "char": dataTypeOracle = "string"; break;
                case "nchar": dataTypeOracle = "string"; break;
                case "varchar": dataTypeOracle = "string"; break;
                case "nvarchar": dataTypeOracle = "string"; break;
                case "text": dataTypeOracle = "string"; break;
                case "ntext": dataTypeOracle = "string"; break;

                case "smallint": dataTypeOracle = "sint"; break;
                case "tinyint": dataTypeOracle = "sint"; break;
                case "int": dataTypeOracle = "sint"; break;
                case "bigint": dataTypeOracle = "long"; break;

                case "date": dataTypeOracle = "date"; break;
                case "datetime": dataTypeOracle = "date"; break;
                case "smalldatetime": dataTypeOracle = "date"; break;
                case "decimal": dataTypeOracle = "sdouble"; break;
                case "float": dataTypeOracle = "sdouble"; break;
                case "money": dataTypeOracle = "sdouble"; break;
                case "smallmoney": dataTypeOracle = "sdouble"; break;
                case "numeric": dataTypeOracle = "sdouble"; break;

                //case "binary": dataTypeOracle = "Byte[]"; break;
                //case "varbinary": dataTypeOracle = "Byte[]"; break;
                case "bit": dataTypeOracle = "boolean"; break;
                //case "image": dataTypeOracle = "Byte[]"; break;

                //case "real": dataTypeOracle = "Single"; break;
                case "uniqueidentifier": dataTypeOracle = "string"; break;
                case "xml": dataTypeOracle = "string"; break;

                default: dataTypeOracle = "string"; break;
            }
            return dataTypeOracle;
        }
        public static string ConvertDataTypeSQLToOracle(string dataTypeSQL)
        {
            string dataTypeOracle = "";
            switch (dataTypeSQL.ToLower())
            {
                case "char": dataTypeOracle = "CHAR"; break;
                case "nchar": dataTypeOracle = "CHAR"; break;
                case "varchar": dataTypeOracle = "VARCHAR2"; break;
                case "nvarchar": dataTypeOracle = "NVARCHAR2"; break;
                case "text": dataTypeOracle = "VARCHAR2"; break;
                case "ntext": dataTypeOracle = "NVARCHAR2"; break;

                case "smallint": dataTypeOracle = "INTEGER"; break;
                case "tinyint": dataTypeOracle = "INTEGER"; break;
                case "int": dataTypeOracle = "INTEGER"; break;
                case "bigint": dataTypeOracle = "INTEGER"; break;

                case "date": dataTypeOracle = "DATE"; break;
                case "datetime": dataTypeOracle = "DATE"; break;
                case "smalldatetime": dataTypeOracle = "DATE"; break;
                case "decimal": dataTypeOracle = "INTEGER"; break;
                case "float": dataTypeOracle = "INTEGER"; break;
                case "money": dataTypeOracle = "INTEGER"; break;
                case "smallmoney": dataTypeOracle = "INTEGER"; break;
                case "numeric": dataTypeOracle = "INTEGER"; break;

                //case "binary": dataTypeOracle = "Byte[]"; break;
                //case "varbinary": dataTypeOracle = "Byte[]"; break;
                case "bit": dataTypeOracle = "CHAR"; break;
                //case "image": dataTypeOracle = "Byte[]"; break;

                //case "real": dataTypeOracle = "Single"; break;
                case "uniqueidentifier": dataTypeOracle = "VARCHAR2"; break;
                case "xml": dataTypeOracle = "XMLTYPE"; break;

                default: dataTypeOracle = "VARCHAR2"; break;
            }
            return dataTypeOracle;
        }
        public static string ConvertDataTypeSQLToOracle(string dataTypeSQL, int length)
        {
            string dataTypeOracle = "";
            switch (dataTypeSQL.ToLower())
            {
                case "char": dataTypeOracle = "CHAR(" + length + ")"; break;
                case "nchar": dataTypeOracle = "CHAR(" + length + ")"; break;
                case "varchar": dataTypeOracle = "VARCHAR2(" + length + ")"; break;
                case "nvarchar": dataTypeOracle = "NVARCHAR2(" + length + ")"; break;
                case "text": dataTypeOracle = "VARCHAR2(" + length + ")"; break;
                case "ntext": dataTypeOracle = "NVARCHAR2(" + length + ")"; break;

                case "smallint": dataTypeOracle = "INTEGER"; break;
                case "tinyint": dataTypeOracle = "INTEGER"; break;
                case "int": dataTypeOracle = "INTEGER"; break;
                case "bigint": dataTypeOracle = "INTEGER"; break;

                case "date": dataTypeOracle = "DATE"; break;
                case "datetime": dataTypeOracle = "DATE"; break;
                case "smalldatetime": dataTypeOracle = "DATE"; break;
                case "decimal": dataTypeOracle = "INTEGER"; break;
                case "float": dataTypeOracle = "INTEGER"; break;
                case "money": dataTypeOracle = "INTEGER"; break;
                case "smallmoney": dataTypeOracle = "INTEGER"; break;
                case "numeric": dataTypeOracle = "INTEGER"; break;

                //case "binary": dataTypeOracle = "Byte[]"; break;
                //case "varbinary": dataTypeOracle = "Byte[]"; break;
                case "bit": dataTypeOracle = "CHAR(1)"; break;
                //case "image": dataTypeOracle = "Byte[]"; break;

                //case "real": dataTypeOracle = "Single"; break;
                case "uniqueidentifier": dataTypeOracle = "VARCHAR2(50)"; break;
                case "xml": dataTypeOracle = "XMLTYPE"; break;

                default: dataTypeOracle = "VARCHAR2(500)"; break;
            }
            return dataTypeOracle;
        }
        public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in dt.Rows.Cast<DataRow>())
            {
                T obj = new T();
                foreach (var typeProperty in typeProperties)
                {
                    object value = null;
                    if (row.Table.Columns.Contains(typeProperty.PropertyInfo.Name))
                    {
                        value = row[typeProperty.PropertyInfo.Name];
                    }

                    if (typeProperty.Type.Name == "Boolean")
                    {
                        if (value.ToString() == "1")
                        {
                            object safeValue = value == null || DBNull.Value.Equals(value) ? null : Convert.ChangeType(true, typeProperty.Type);
                            typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                        }
                        else
                        {
                            object safeValue = value == null || DBNull.Value.Equals(value) ? null : Convert.ChangeType(false, typeProperty.Type);
                            typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                        }
                    }
                    else
                    {
                        object safeValue = value == null || DBNull.Value.Equals(value)
                        ? null
                        : Convert.ChangeType(value, typeProperty.Type);

                        typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                    }

                }
                list.Add(obj);
            }
            return list;
        }
        public static List<Dictionary<string, object>> ToDictionary(DataTable dt)
        {
            var columns = dt.Columns.Cast<DataColumn>();
            var Temp = dt.AsEnumerable().Select(dataRow => columns.Select(column =>
                                 new { Column = column.ColumnName, Value = dataRow[column] })
                             .ToDictionary(data => data.Column, data => data.Value)).ToList();
            return Temp.ToList();
        }
        #endregion

        #region Excel
        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = string.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

        public static int ColumnLetterToColumnIndex(string columnLetter)
        {
            columnLetter = columnLetter.ToUpper();
            int sum = 0;

            for (int i = 0; i < columnLetter.Length; i++)
            {
                sum *= 26;
                sum += (columnLetter[i] - 'A' + 1);
            }
            return sum;
        }
        #endregion      

        public static double Evaluate(string expressions)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expressions);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (double)(loDataTable.Rows[0]["Eval"]);
        }
        public static string Base64ToString(string base64String)
        {
            // Convert Base64 to string
            byte[] bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }
        public static string ToBase64String(string original)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(original);            
            return Convert.ToBase64String(bytes);
        }
        
        public static string StringToCsv(string CsvString)
        {

            // Convert string to CSV
            StringBuilder csvBuilder = new StringBuilder();

            foreach (string line in CsvString.Split('\n'))
            {
                string[] values = line.Split(',');
                csvBuilder.AppendLine(string.Join(",", values));
            }

            return csvBuilder.ToString();
        }
        public static string CsvToString(string CsvString)
        {
            // Convert CSV to string
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in CsvString.Split('\n'))
            {
                string[] values = line.Split(',');
                stringBuilder.AppendLine(string.Join(",", values));
            }

            return stringBuilder.ToString();
        }
        public static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream"; // default nếu không xác định được
            }
            return contentType;
        }
    }
}

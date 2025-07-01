//using Oracle.DataAccess.Client;
using Amazon.Runtime.Internal.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lib.Utility
{
    public static class StringConvert
    {       
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
        public static string EncodeStringToBase64(string stringToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
        }
        public static string DecodeStringFromBase64(string stringToDecode)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecode));
        }
        public static string StringToCSV(string CsvString)
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
        public static string CSVToString(string CsvString)
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
        public static XmlDocument StringToXml(string XmlString)
        {
            try {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(XmlString);
                return xmlDocument;
            } catch {
                return null;
            }
            

        }
        public static string XmlToString(string XmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(XmlString);
            return xmlDocument.OuterXml;
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lib.Utility
{
    public static class ObjectConverter
    {       
        public static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return dictionary;
        }
        public static List<Dictionary<string, object>> ObjectToListDictionary(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
            return dictionary;
        }
        public static string ObjectToXml<T>(T value)
        {
            if (value == null)
                return string.Empty;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }
        public static T XmlToObject<T>(string value)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(new StringReader(value));
        }

        public static string ObjectToJson<T>(T value)
        {
            if (value == null)
                return string.Empty;
            return JsonConvert.SerializeObject(value);
        }
        public static T JsonToObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string XmlToJson(XmlDocument doc)
        {
            if (doc == null)
                return string.Empty;
            return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
        }
        public static XmlDocument JsonToXML(string json)
        {
            XmlDocument doc = new XmlDocument();

            using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                XElement xml = XElement.Load(reader);
                doc.LoadXml(xml.ToString());
            }

            return doc;
        }
        public static string DictionaryToXml(Dictionary<string, object> dict)
        {
            var jsonString = JsonConvert.SerializeObject(dict);
            string jsonData = "{ 'Message' : " + jsonString + "}";
            XmlDocument xmlDocument = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonData);
            return xmlDocument.OuterXml;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

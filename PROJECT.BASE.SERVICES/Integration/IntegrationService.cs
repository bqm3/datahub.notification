using Lib.Setting;
using Lib.Utility;
using Minio.DataModel.Args;
using Minio;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PROJECT.BASE.SERVICES
{
    public class IntegrationService
    {
        public IntegrationService() { }
        public static void CachingData()
        {
            var resultITG_CATEGORY = DataObjectFactory.GetInstanceITG_CATEGORY().GetList(null);
            if (resultITG_CATEGORY != null && resultITG_CATEGORY.Count > 0)
            {
                foreach (var info in resultITG_CATEGORY)
                {
                    if (CacheProvider.Exists(info.CODE, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.ITG_CATEGORY)))
                        CacheProvider.Update(info.CODE, info.NAME, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.ITG_CATEGORY));
                    else
                        CacheProvider.Add(info.CODE, info.NAME, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.ITG_CATEGORY));
                }
            }
            if (Directory.Exists($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/configuration/transform"))
            {
                string[] arrConfigTransform = Directory.GetFiles($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/configuration/transform", "*.json", SearchOption.AllDirectories);
                foreach (string pathFile in arrConfigTransform)
                {
                    var dataFile = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(pathFile));
                    string keyCode = Path.GetFileNameWithoutExtension(pathFile);
                    if (CacheProvider.Exists(keyCode, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_TRANSFORM)))
                        CacheProvider.Update(keyCode, dataFile, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_TRANSFORM));
                    else
                        CacheProvider.Add(keyCode, dataFile, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_TRANSFORM));
                }
            }
            if (Directory.Exists($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/configuration/extract"))
            {
                string[] arrConfigTransform = Directory.GetFiles($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/configuration/extract", "*.json", SearchOption.AllDirectories);
                foreach (string pathFile in arrConfigTransform)
                {
                    var dataFile = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(pathFile));
                    string keyCode = Path.GetFileNameWithoutExtension(pathFile);
                    if (CacheProvider.Exists(keyCode, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_EXTRACT)))
                        CacheProvider.Update(keyCode, dataFile, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_EXTRACT));
                    else
                        CacheProvider.Add(keyCode, dataFile, Enum.GetName(typeof(INSTANCE_CACHE), INSTANCE_CACHE.CONFIG_EXTRACT));
                }
            }

        }
        public static long? ITG_MSGO_Activitie(IntegrationRequest request, short status, string message, string creator)
        {
            var resultMSGO = DataObjectFactory.GetInstanceITG_MSGO().Insert(new ITG_MSGO_Information()
            {
                CODE = Guid.NewGuid().ToString(),
                REF_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Request_Id)) ? request.Header.Request_Id : null,
                MSG_REFID = (request != null && !string.IsNullOrEmpty(request.Header.Msg_Id)) ? request.Header.Msg_Id : null,
                MSG_CONTENT = message,
                TRAN_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Tran_Code)) ? request.Header.Tran_Code : null,
                TRAN_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Tran_Name)) ? request.Header.Tran_Name : null,
                SENDER_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Sender_Code)) ? request.Header.Sender_Code : null,
                SENDER_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Sender_Name)) ? request.Header.Sender_Name : null,
                RECEIVER_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Receiver_Code)) ? request.Header.Receiver_Code : null,
                RECEIVER_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Receiver_Name)) ? request.Header.Receiver_Name : null,
                STATUS = status,
                REMOVED = 0,
                CREATOR = creator

            });
            return resultMSGO;

        }
        /// <summary>
        /// Lưu trữ message Storage
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static async Task<string> UploadStorage(IntegrationRequest request, string creator)
        {
            #region get link upload
            string resultUrlUpload = string.Empty;
            string bucketName1 = Config.CONFIGURATION_GLOBAL.Storage.BucketName;
            string bucketName = $"{Config.CONFIGURATION_GLOBAL.Storage.BucketName}-{request.Header.Tran_Code.ToLower()}";
            string urlGetUpload = Config.CONFIGURATION_GLOBAL.Storage.UrlGetUpload;
            string accessKey = Config.CONFIGURATION_GLOBAL.Storage.AccessKey;
            string secretKey = Config.CONFIGURATION_GLOBAL.Storage.SecretKey;
            string extension = request.Header.Data_Type.IndexOf('/') != -1 ? request.Header.Data_Type.Split('/')[0] : request.Header.Data_Type;
            //string objectName = $"{request.Header.Msg_Id}.{request.Header.Data_Type}";
            string objectName = $"{request.Header.Msg_Id}.{extension}";
            // Initialize the MinIO client
            var minioClient = new MinioClient()
                .WithEndpoint(urlGetUpload)
                .WithCredentials(accessKey, secretKey)
                .Build();
            // Kiểm tra và sửa tên bucket
            bucketName = FixBucketName(bucketName);
            // Kiểm tra bucket có tồn tại không, nếu không thì tạo bucket
            bool found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!found)
            {
                try
                {
                    Console.WriteLine($"Bucket '{bucketName}' không tồn tại. Đang tạo mới...");
                    await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }
                catch (Exception ex) { 
                    Console.WriteLine(ex); 
                }
            }
            // Upload the file
            Console.WriteLine("Uploading file...");
            byte[] byteArray = Convert.FromBase64String(request.Body.Content);
            // Tạo một MemoryStream từ mảng byte
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                long fileSize = memoryStream.Length;

                var putObjectArgs = new PutObjectArgs()
                                        .WithBucket(bucketName)
                                        .WithObject(objectName)
                                        .WithStreamData(memoryStream)
                                        .WithObjectSize(fileSize)
                                        .WithContentType("application/octet-stream");

                Console.WriteLine("Đang tải tệp lên MinIO...");
                await minioClient.PutObjectAsync(putObjectArgs);
                Console.WriteLine($"File uploaded successfully as '{objectName}' to bucket '{bucketName}'.");
                return $"{bucketName}/{objectName}";
            }

            #endregion

        }
        /// <summary>
        /// Lưu trữ message Storage
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static async Task<string> UploadStreamStorage(byte[] byteArray, string creator)
        {
            #region get link upload
            string resultUrlUpload = string.Empty;
            //string bucketName1 = Config.CONFIGURATION_GLOBAL.Storage.BucketName;
            string bucketName = !string.IsNullOrEmpty(creator) ? creator : $"{Config.CONFIGURATION_GLOBAL.Storage.BucketName}";
            string urlGetUpload = Config.CONFIGURATION_GLOBAL.Storage.UrlGetUpload;
            string accessKey = Config.CONFIGURATION_GLOBAL.Storage.AccessKey;
            string secretKey = Config.CONFIGURATION_GLOBAL.Storage.SecretKey;
            string hashID = Encrypt.ComputeMd5Hash(byteArray);
            var result = DataObjectFactory.GetInstanceITG_MSGI().GetList(new Dictionary<string, object>()
            {
                { "MSG_ID",hashID}
            });
            if (result == null || result.Count == 0)
                return null;
            var dictFileInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(result[0].MSG_CONTENT);
            //string extension = dictFileInfo["Extension"].ToString();            
            string objectName = $"{dictFileInfo["fileName"].ToString()}.{dictFileInfo["extension"].ToString()}";
            // Initialize the MinIO client
            var minioClient = new MinioClient()
                .WithEndpoint(urlGetUpload)
                .WithCredentials(accessKey, secretKey)
                .Build();
            // Kiểm tra và sửa tên bucket
            bucketName = FixBucketName(bucketName);
            // Kiểm tra bucket có tồn tại không, nếu không thì tạo bucket
            bool found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!found)
            {
                try
                {
                    Console.WriteLine($"Bucket '{bucketName}' không tồn tại. Đang tạo mới...");
                    await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            // Upload the file
            Console.WriteLine("Uploading file...");            
            // Tạo một MemoryStream từ mảng byte
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                long fileSize = memoryStream.Length;

                var putObjectArgs = new PutObjectArgs()
                                        .WithBucket(bucketName)
                                        .WithObject(objectName)
                                        .WithStreamData(memoryStream)
                                        .WithObjectSize(fileSize)
                                        .WithContentType("application/octet-stream");

                Console.WriteLine("Đang tải tệp lên MinIO...");
                await minioClient.PutObjectAsync(putObjectArgs);
                Console.WriteLine($"File uploaded successfully as '{objectName}' to bucket '{bucketName}'.");
                var resultMSGO = DataObjectFactory.GetInstanceITG_MSGO().Insert(new ITG_MSGO_Information()
                {
                    CODE = Guid.NewGuid().ToString(),
                    REF_CODE = result[0].CODE,
                    MSG_REFID = hashID,                    
                    MSG_CONTENT = $"Streaming: {result[0].MSG_CONTENT} {Constant.MESSAGE_UPLOAD_STORAGE}",                    
                    TRAN_CODE = result[0].TRAN_CODE,
                    TRAN_NAME = result[0].TRAN_NAME,
                    SENDER_CODE = result[0].SENDER_CODE,
                    SENDER_NAME = result[0].SENDER_NAME,
                    RECEIVER_CODE = result[0].RECEIVER_CODE,
                    RECEIVER_NAME = result[0].RECEIVER_NAME,
                    STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS),
                    REMOVED = 0,
                    CREATOR = creator

                });
                return $"{bucketName}/{objectName}";
            }

            #endregion

        }
        public static string FixBucketName(string bucketName)
        {
            // Chuyển về chữ thường
            bucketName = bucketName.ToLower();

            // Loại bỏ ký tự không hợp lệ
            bucketName = Regex.Replace(bucketName, "[^a-z0-9-]", "-");

            // Đảm bảo không bắt đầu hoặc kết thúc bằng dấu gạch ngang
            bucketName = bucketName.Trim('-');

            // Đảm bảo độ dài hợp lệ (tối thiểu 3, tối đa 63)
            if (bucketName.Length < 3)
            {
                bucketName = bucketName.PadRight(3, 'a');
            }
            if (bucketName.Length > 63)
            {
                bucketName = bucketName.Substring(0, 63);
            }

            return bucketName;
        }
        public static string GetValueColumn(XmlDocument xmlDocumet, string xpath, string dataType, object refer)
        {

            var valueColumn = string.Empty;
            if (string.IsNullOrEmpty(xpath) && dataType == "varchar")
            {
                valueColumn = Guid.NewGuid().ToString();
                return valueColumn;
            }
            if (string.IsNullOrEmpty(xpath))
                return valueColumn;
            if (refer == null)
            {
                XmlNodeList nodeList = xmlDocumet.SelectNodes(xpath);
                valueColumn = nodeList[0].InnerText;
                return valueColumn;
            }
            else
            {
                XmlNodeList nodeList = xmlDocumet.SelectNodes(xpath);
                string valueSelectNodes = nodeList[0].InnerText;
                Dictionary<string, string> dictRefer = JsonConvert.DeserializeObject<Dictionary<string, string>>(refer.ToString());
                if (dictRefer != null)
                {
                    //string referTableName = dictRefer["From"];
                    //string referSelect = dictRefer["Select"];
                    //string referWhere = dictRefer["Where"];
                    //var referParamValue = new Dictionary<string, object>();
                    //referParamValue.Add(referWhere, valueSelectNodes);
                    //var getReferData = DataObjectFactory.GetInstanceBaseObject().GetDataBy(referTableName, referSelect, referWhere, referParamValue).Result;
                    //if (getReferData != null && getReferData.Rows.Count > 0)
                    //{
                    //    valueColumn = getReferData.Rows[0][referSelect].ToString();
                    //    return valueColumn;
                    //}
                }
            }
            return valueColumn;
        }
        public static Dictionary<string, object> GetParamValue(int? rowIndex, string creator, XmlDocument xmlDocumet, Dictionary<string, dynamic> dictTableName, ref string primaryKey)
        {
            Dictionary<string, object> paramValue = new Dictionary<string, object>();
            foreach (var keyValueTableName in dictTableName)
            {
                string columnName = keyValueTableName.Key;
                string dataType = keyValueTableName.Value.DataType;
                string xpath = keyValueTableName.Value.Value;
                string primaryKeyValue = keyValueTableName.Value.PrimaryKey;
                if (!string.IsNullOrEmpty(primaryKeyValue) && primaryKeyValue == "1")
                    primaryKey = columnName;
                object refer = keyValueTableName.Value.Refer;
                if (rowIndex != null && !string.IsNullOrEmpty(xpath))
                {
                    xpath = string.Format(xpath, rowIndex);
                }
                string valueColumn = GetValueColumn(xmlDocumet, xpath, dataType, refer);
                switch (dataType)
                {
                    case "date":
                        if (string.IsNullOrEmpty(valueColumn))
                            paramValue.Add(columnName, (DateTime?)null);
                        else
                        {
                            if (Valid.IsValidDate(valueColumn, "yyyy-MM-dd"))
                                valueColumn = $"{valueColumn} 00:00:00";
                            object valueObject = DateTime.SpecifyKind(DateTime.ParseExact(valueColumn, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc);
                            paramValue.Add(columnName, valueObject);

                        }
                        break;
                    case "int":
                        if (string.IsNullOrEmpty(valueColumn))
                            paramValue.Add(columnName, (int?)null);
                        else
                            paramValue.Add(columnName, int.Parse(valueColumn));
                        break;
                    case "double":
                        if (string.IsNullOrEmpty(valueColumn))
                            paramValue.Add(columnName, (double?)null);
                        else
                            paramValue.Add(columnName, double.Parse(valueColumn));
                        break;
                    case "date-bigint":
                        //if (string.IsNullOrEmpty(valueColumn))
                        //    paramValue.Add(columnName, (DateTime?)null);
                        //else
                        //{
                        //    if (Valid.IsValidDate(valueColumn, "yyyy-MM-dd"))
                        //        valueColumn = $"{valueColumn} 00:00:00";
                        //    DateTime valueObject = DateTime.SpecifyKind(DateTime.ParseExact(valueColumn, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture), DateTimeKind.Utc);
                        //    paramValue.Add(columnName, Utility.ConvertToUnixTime(valueObject));

                        //}
                        if (string.IsNullOrEmpty(valueColumn))
                            paramValue.Add(columnName, (long?)null);
                        else
                        {
                            valueColumn = valueColumn.Replace("-", "");
                            if (valueColumn.Length == 4)
                                valueColumn = $"{valueColumn}0000";
                            else if (valueColumn.Length == 6)
                                valueColumn = $"{valueColumn}00";
                            paramValue.Add(columnName, long.Parse(valueColumn));
                        }
                        break;
                    case "bool":
                        if (string.IsNullOrEmpty(valueColumn))
                            paramValue.Add(columnName, (bool?)null);
                        else
                            paramValue.Add(columnName, bool.Parse(valueColumn));
                        break;
                    default:
                        paramValue.Add(columnName, valueColumn);
                        break;
                }

            }
            paramValue.Add("CODE", Guid.NewGuid().ToString());
            paramValue.Add("IS_DELETE", 0);            
            paramValue.Add("CUSER", creator);            
            paramValue.Add("CDATE", DateTime.Now);
          
            return paramValue;
        }


    }
}

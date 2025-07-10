using ArangoDBNetStandard.Transport.Http;
using ArangoDBNetStandard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lib.Setting;
using System.Collections;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace PROJECT.BASE.SERVICES
{
    public class MinIOService
    {        
        private static IMinioClient _minioClient;
        private static string _endPoint;
        private static readonly object _lock = new object();
        public static IMinioClient GetMinioClient()
        {            
            if (_minioClient != null)
                return _minioClient;

            lock (_lock)
            {
                if (_minioClient == null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                    _endPoint = Config.CONFIGURATION_GLOBAL.Storage.Endpoint;
                    string accessKey = Config.CONFIGURATION_GLOBAL.Storage.AccessKey;
                    string secretKey = Config.CONFIGURATION_GLOBAL.Storage.SecretKey;
                    string isSSL = Config.CONFIGURATION_GLOBAL.Storage.UsingSSL;                   
                    _minioClient = new MinioClient()
                        .WithEndpoint(_endPoint)
                        .WithCredentials(accessKey, secretKey)
                        .WithSSL(bool.Parse(isSSL))
                        .Build();
                }
            }
            return _minioClient;

        }
        public static string FixBucketName(string bucketName)
        {          
            bucketName = bucketName.ToLower();
            bucketName = Regex.Replace(bucketName, "[^a-z0-9-]", "-");
            bucketName = bucketName.Trim('-');
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
        public static async Task<MinioModel> UploadFileAsync(byte[] fileBytes, string bucketName,string fileName, bool isPublicBucket = false)
        {
            if (fileBytes == null)
                return null;
            var minioClient = GetMinioClient();
            bucketName = FixBucketName(bucketName);
            var isExists = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

            if (!isExists)
            {
                await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                if (isPublicBucket)
                {
                    string publicReadPolicy = @"
                                                {
                                                    ""Version"": ""2012-10-17"",
                                                    ""Statement"": [
                                                        {
                                                            ""Effect"": ""Allow"",
                                                            ""Principal"": ""*"",
                                                            ""Action"": ""s3:GetObject"",
                                                            ""Resource"": ""arn:aws:s3:::" + bucketName + @"/*""
                                                        }
                                                    ]
                                                }";

                    var policyArgs = new SetPolicyArgs()
                        .WithPolicy(publicReadPolicy)
                        .WithBucket(bucketName);
                    await _minioClient.SetPolicyAsync(policyArgs);
                }
            }
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                StreamReader reader = new StreamReader(memoryStream);
                memoryStream.Position = 0;

                var objectName = new string(fileName.Where(c => !char.IsWhiteSpace(c)).ToArray());
                string contentType = Lib.Utility.Utility.GetContentType(fileName);
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(memoryStream)
                    .WithObjectSize(memoryStream.Length)
                    .WithContentType(contentType);

                await minioClient.PutObjectAsync(putObjectArgs);

                var statObject =
                    await _minioClient.StatObjectAsync(
                        new StatObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject(objectName));

                if (string.IsNullOrEmpty(statObject.ObjectName))
                    return null;
                return new MinioModel()
                {
                    FolderName = bucketName,
                    OriginalFileName = fileName,
                    FileName = statObject.ObjectName,
                    FileSize = statObject.Size,
                    ContentType = statObject.ContentType,
                    PublicUrl = isPublicBucket ? $"{_endPoint}/{bucketName}/{statObject.ObjectName}" : string.Empty
                };
            }
        }

        public static async Task<Stream> DownloadFileAsync(string bucketName, string objectName)
        {
            var minioClient = GetMinioClient();
            var memoryStream = new MemoryStream();
            var arg = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);
            var statObject = await minioClient.StatObjectAsync(arg);
            if (!string.IsNullOrEmpty(statObject.ObjectName))
            {
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream((stream) => { stream.CopyTo(memoryStream); });

                await minioClient.GetObjectAsync(getObjectArgs);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static async Task<string> GetShareLinkAsync(string bucketName, string objectName, int expireTime)
        {
            var minioClient = GetMinioClient();
            var args = new PresignedGetObjectArgs()
                                  .WithBucket(bucketName)
                                  .WithObject(objectName)
                                  .WithExpiry(expireTime * 60);

            return await minioClient.PresignedGetObjectAsync(args);
        }
        public static async Task<List<MinioFileInfo>> ListFilesAsync(string bucketName)
        {
            var minioClient = GetMinioClient();            
            var result = new List<MinioFileInfo>();
            var listArgs = new ListObjectsArgs()
                                .WithBucket(FixBucketName(bucketName))
                                .WithRecursive(true);
            var tcs = new TaskCompletionSource<bool>();
            minioClient.ListObjectsAsync(listArgs).Subscribe(
                item => { 
                    result.Add(new MinioFileInfo() { 
                        Code = Lib.Utility.Utility.ToBase64String($"{bucketName}#{item.Key}"),
                        FileName = item.Key,
                        FileSize = (long)item.Size,
                        LastModified = item.LastModifiedDateTime
                    }); 
                },
                ex => { tcs.TrySetException(ex);},
                () => { tcs.TrySetResult(true); }
            );
            await tcs.Task;
            return result;
        }
        public static async Task<Dictionary<string,object>> StreamVideo(string bucketName, string objectName, string rangeHeader)
        {
            var minioClient = GetMinioClient();
            ObjectStat stat = await minioClient.StatObjectAsync(new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName));

            long fileLength = stat.Size;
            long start = 0;
            long end = fileLength - 1;

            if (!string.IsNullOrEmpty(rangeHeader) && rangeHeader.StartsWith("bytes="))
            {
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                start = long.Parse(range[0]);
                if (range.Length > 1 && !string.IsNullOrEmpty(range[1]))
                    end = long.Parse(range[1]);
            }

            long length = end - start + 1;

            var stream = new MemoryStream();

            await minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithOffsetAndLength(start, length)
                .WithCallbackStream(s => s.CopyTo(stream)));

            stream.Position = 0;

            string contentRange = $"bytes {start}-{end}/{fileLength}";
            var result = new Dictionary<string, object>();
            result.Add("contentRange", contentRange);
            result.Add("length", length);
            result.Add("stream", stream);
            return result;
        }
    }
}

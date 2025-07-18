using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace microservice.mess.Schedules
{
    public class FormatDataMailStep : IMessageStep
    {
        private readonly MailRepository _mailRepository;
        private readonly ILogger<FormatDataMailStep> _logger;

        public FormatDataMailStep(MailRepository mailRepository, ILogger<FormatDataMailStep> logger)
        {
            _mailRepository = mailRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            context.Items.TryGetValue("rawData", out var rawDataObj);
            var rawData = rawDataObj as List<BsonDocument>;
            if (rawData == null)
                throw new InvalidOperationException("rawData must be a List<BsonDocument>");

            var now = DateTime.UtcNow.AddHours(7);
            var firstDoc = rawData.FirstOrDefault();

            var fileName = context.Schedule.Shared?.QueryData?.File ?? "default.docx";
            var configPath = Path.Combine("templates/configs", Path.ChangeExtension(fileName, ".json"));
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Template config file not found", configPath);

            var configJson = await File.ReadAllTextAsync(configPath);
            var templateConfig = JsonConvert.DeserializeObject<TemplateConfig>(configJson)!;

            // Lấy template mail từ tên schedule
            var mailTemplate = await _mailRepository.GetByNameAsync(context.Schedule.Name);
            if (mailTemplate == null)
                throw new InvalidOperationException("Không tìm thấy template email theo tên schedule");

            var placeholderMap = new Dictionary<string, string>();
            foreach (var placeholder in mailTemplate.Placeholders)
            {
                var key = placeholder.Replace("{{", "").Replace("}}", "");
                var value = GetValueFromRaw(firstDoc, key, now);
                placeholderMap[placeholder] = value;
            }

            context.Items["mailData"] = new
            {
                Subject = mailTemplate.Subject,
                BodyHtml = ReplacePlaceholders(mailTemplate.BodyHtml, placeholderMap),
                LogoUrl = mailTemplate.LogoUrl,
                Receivers = mailTemplate.Receivers,
                SkipReceiverError = mailTemplate.SkipReceiverError
            };
        }

        private string ReplacePlaceholders(string template, IDictionary<string, string> placeholderMap)
        {
            foreach (var pair in placeholderMap)
            {
                template = template.Replace(pair.Key, pair.Value ?? "");
            }
            return template;
        }

        private string GetValueFromRaw(BsonDocument doc, string key, DateTime now)
        {
            switch (key.ToLowerInvariant())
            {
                case "date":
                    return now.ToString("dd/MM/yyyy");
                case "name":
                case "voucher_code":
                case "expiry_date":
                case "promotion_url":
                    return doc.GetValue(key, BsonValue.Create("(N/A)")).ToString();
                default:
                    return doc.TryGetValue(key, out var val) ? val.ToString() : "(N/A)";
            }
        }




        // public async Task<IDictionary<string, string>> BuildSignetPlaceholderMapAsync(ScheduleContext context, List<BsonDocument> rawData)
        // {
        //     var template = await _signetRepository.GetTemplateByNameAsync(context.Schedule.Name);
        //     if (template?.Placeholders?.Any() != true)
        //         return new Dictionary<string, string>();

        //     var result = new Dictionary<string, string>();
        //     var firstDoc = rawData.FirstOrDefault();
        //     var now = DateTime.UtcNow.AddHours(7);

        //     var pdfData = context.Items.TryGetValue("signetDataPdf", out var pdfObj) ? pdfObj : null;
        //     var mergeFields = pdfData?.GetType().GetProperty("mergeFields")?.GetValue(pdfData) as Dictionary<string, string>;

        //     var queryConfig = context.Schedule.Shared?.QueryData;

        //     foreach (var p in template.Placeholders)
        //     {
        //         var key = p.Replace("{{", "").Replace("}}", "");

        //         switch (key.ToLowerInvariant())
        //         {
        //             case "date":
        //                 result[key] = now.ToString("dd/MM/yyyy");
        //                 break;
        //             case "current":
        //                 result[key] = rawData.Count.ToString();
        //                 break;
        //             case "file_view":
        //                 result[key] = "Dữ liệu mặc định";
        //                 break;
        //             case "content":
        //                 result[key] = queryConfig?.Collection ?? "(Không rõ nguồn)";
        //                 break;
        //             case "log_error":
        //                 result[key] = "0";
        //                 break;
        //             case "log_number":
        //                 result[key] = "0";
        //                 break;
        //             case "service":
        //                 result[key] = "Service notify";
        //                 break;
        //             default:
        //                 try
        //                 {
        //                     result[key] = GetFieldFromDoc(firstDoc, key);
        //                 }
        //                 catch
        //                 {
        //                     result[key] = "(N/A)";
        //                 }
        //                 break;
        //         }
        //     }

        //     return result;
        // }



        // private string GetFieldFromDoc(BsonDocument doc, string key)
        // {
        //     if (doc.TryGetValue(key, out var val))
        //         return val.ToString();

        //     if (doc.Contains("message") && doc["message"].AsBsonDocument.Contains("properties"))
        //     {
        //         var props = doc["message"]["properties"].AsBsonDocument;
        //         if (props.TryGetValue(key, out var nestedVal))
        //             return nestedVal.AsBsonArray.FirstOrDefault()?.AsString ?? "";
        //     }

        //     return "(N/A)";
        // }

        // private List<Dictionary<string, object>> AggregateChart(List<BsonDocument> docs)
        // {
        //     // Xác định tất cả các "source" có thật
        //     var allSources = docs
        //         .Where(d => d.TryGetValue("source", out var _))
        //         .Select(d => d["source"].AsString)
        //         .Distinct()
        //         .ToList();

        //     return docs
        //         .GroupBy(d => d.GetValue("chuyen_de", BsonValue.Create("Khác")).AsString)
        //         .Select(g =>
        //         {
        //             var dict = new Dictionary<string, object>
        //             {
        //                 ["Chủ đề"] = g.Key
        //             };

        //             foreach (var source in allSources)
        //             {
        //                 dict[source] = g.Count(x => x.TryGetValue("source", out var v) && v == source);
        //             }

        //             return dict;
        //         }).ToList();
        // }

        // private List<object> GroupArticles(List<BsonDocument> docs)
        // {
        //     return docs
        //         .GroupBy(d => d.GetValue("chuyen_de", BsonValue.Create("Khác")).AsString)
        //         .Select(g => new
        //         {
        //             category = g.Key.ToUpperInvariant(),
        //             data = g.Select(d => new
        //             {
        //                 title = d.GetValue("title", BsonValue.Create("(Không tiêu đề)")).AsString,
        //                 content = d.GetValue("content", "").AsString,
        //                 author = d.GetValue("author", "").AsString,
        //                 createdAt = d.TryGetValue("timestamp", out var tsVal) && tsVal.IsValidDateTime
        //                     ? tsVal.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")
        //                     : "",
        //                 articleUrl = d.GetValue("articleUrl", "").AsString,
        //                 image = d.GetValue("image", "").AsString
        //             }).ToList()
        //         }).ToList<object>();
        // }


    }
}

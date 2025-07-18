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
    public class FormatDataSignetStep : IMessageStep
    {
        private readonly SignetRepository _signetRepository;
        private readonly ILogger<FormatDataSignetStep> _logger;

        public FormatDataSignetStep(SignetRepository signetRepository, ILogger<FormatDataSignetStep> logger)
        {
            _signetRepository = signetRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            context.Items.TryGetValue("rawData", out var rawDataObj);
            var rawData = rawDataObj as List<BsonDocument>;
            if (rawData == null)
                throw new InvalidOperationException("rawData must be a List<BsonDocument>");

            var now = DateTime.UtcNow.AddHours(7);
            var fileName = context.Schedule.Shared?.QueryData?.File ?? "default.docx";
            var configPath = Path.Combine("templates/configs", Path.ChangeExtension(fileName, ".json"));

            if (!File.Exists(configPath))
                throw new FileNotFoundException("Template config file not found", configPath);

            var configJson = await File.ReadAllTextAsync(configPath);
            var templateConfig = JsonConvert.DeserializeObject<TemplateConfig>(configJson)!;

            // MergeFields từ JSON + bổ sung
            var mergeFields = new Dictionary<string, string>(templateConfig.MergeFields ?? new())
            {
                ["NGAY"] = now.AddDays(-1).ToString("dd/MM/yyyy"),
                ["BAN_TIN_NGAY"] = $"Bản tin ngày {now:dd/MM/yyyy}"
            };

            var includeDataJson = context.Schedule.Data?.DataJson == true;

            // Nếu OutFile có cấu hình → thay thế {{date}} (nếu có)
            var outFileTemplate = context.Schedule.Data?.OutFile ?? $"Bao_cao_chi_tiet_{now:dd_MM_yyyy}.pdf";
            var fixedOutputFile = outFileTemplate.Replace("{{date}}", now.ToString("dd_MM_yyyy"));

            var result = new
            {
                data = AggregateChart(rawData),
                templateName = fileName,
                outputFileName = fixedOutputFile,
                mergeFields,
                dataJson = includeDataJson ? GroupArticles(rawData) : null
            };

            // Dữ liệu Signet (file_view, content, ...)
            var placeholderMap = await BuildSignetPlaceholderMapAsync(context, rawData);
            context.Items["signetData"] = placeholderMap;
            context.Items["signetDataPdf"] = result;
        }


        public async Task<IDictionary<string, string>> BuildSignetPlaceholderMapAsync(ScheduleContext context, List<BsonDocument> rawData)
        {
            var template = await _signetRepository.GetTemplateByNameAsync(context.Schedule.Name);
            if (template?.Placeholders?.Any() != true)
                return new Dictionary<string, string>();

            var result = new Dictionary<string, string>();
            var firstDoc = rawData.FirstOrDefault();
            var now = DateTime.UtcNow.AddHours(7);

            var pdfData = context.Items.TryGetValue("signetDataPdf", out var pdfObj) ? pdfObj : null;
            var mergeFields = pdfData?.GetType().GetProperty("mergeFields")?.GetValue(pdfData) as Dictionary<string, string>;

            var queryConfig = context.Schedule.Shared?.QueryData;

            foreach (var p in template.Placeholders)
            {
                var key = p.Replace("{{", "").Replace("}}", "");

                switch (key.ToLowerInvariant())
                {
                    case "date":
                        result[key] = now.ToString("dd/MM/yyyy");
                        break;
                    case "current":
                        result[key] = rawData.Count.ToString();
                        break;
                    case "file_view":
                        result[key] = "Dữ liệu mặc định";
                        break;
                    case "content":
                        result[key] = queryConfig?.Collection ?? "(Không rõ nguồn)";
                        break;
                    case "log_error":
                        result[key] = "0";
                        break;
                    case "log_number":
                        result[key] = "0";
                        break;
                    case "service":
                        result[key] = "Service notify";
                        break;
                    default:
                        try
                        {
                            result[key] = GetFieldFromDoc(firstDoc, key);
                        }
                        catch
                        {
                            result[key] = "(N/A)";
                        }
                        break;
                }
            }

            return result;
        }



        private string GetFieldFromDoc(BsonDocument doc, string key)
        {
            if (doc.TryGetValue(key, out var val))
                return val.ToString();

            if (doc.Contains("message") && doc["message"].AsBsonDocument.Contains("properties"))
            {
                var props = doc["message"]["properties"].AsBsonDocument;
                if (props.TryGetValue(key, out var nestedVal))
                    return nestedVal.AsBsonArray.FirstOrDefault()?.AsString ?? "";
            }

            return "(N/A)";
        }

        private List<Dictionary<string, object>> AggregateChart(List<BsonDocument> docs)
        {
            // Xác định tất cả các "source" có thật
            var allSources = docs
                .Where(d => d.TryGetValue("source", out var _))
                .Select(d => d["source"].AsString)
                .Distinct()
                .ToList();

            return docs
                .GroupBy(d => d.GetValue("chuyen_de", BsonValue.Create("Khác")).AsString)
                .Select(g =>
                {
                    var dict = new Dictionary<string, object>
                    {
                        ["Chủ đề"] = g.Key
                    };

                    foreach (var source in allSources)
                    {
                        dict[source] = g.Count(x => x.TryGetValue("source", out var v) && v == source);
                    }

                    return dict;
                }).ToList();
        }

        private List<object> GroupArticles(List<BsonDocument> docs)
        {
            return docs
                .GroupBy(d => d.GetValue("chuyen_de", BsonValue.Create("Khác")).AsString)
                .Select(g => new
                {
                    category = g.Key.ToUpperInvariant(),
                    data = g.Select(d => new
                    {
                        title = d.GetValue("title", BsonValue.Create("(Không tiêu đề)")).AsString,
                        content = d.GetValue("content", "").AsString,
                        author = d.GetValue("author", "").AsString,
                        createdAt = d.TryGetValue("timestamp", out var tsVal) && tsVal.IsValidDateTime
                            ? tsVal.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")
                            : "",
                        articleUrl = d.GetValue("articleUrl", "").AsString,
                        image = d.GetValue("image", "").AsString
                    }).ToList()
                }).ToList<object>();
        }


    }
}

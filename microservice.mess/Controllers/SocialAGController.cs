using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using microservice.mess.Services;
using microservice.mess.Repositories;
using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Documents;

namespace microservice.mess.Controllers
{
    public class GenerateReportRequest
    {
        public string SchemaKey { get; set; } = "";
        public string TableName { get; set; } = "";
        public string Date { get; set; } = "";
    }
    [ApiController]
    [Route("api/[controller]")]
    public class SocialAGController : ControllerBase
    {
        private readonly ILogger<SocialAGController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SocialAGService _socialAGService;
        private readonly SgiPdfChart _sgiPdfChart;

        public SocialAGController(ILogger<SocialAGController> logger, SgiPdfChart sgiPdfChart, IHttpClientFactory httpClientFactory, SocialAGService socialAGService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _socialAGService = socialAGService;
            _sgiPdfChart = sgiPdfChart;
        }
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] GenerateReportRequest request)
        {
            var configPath = Path.Combine("templates/configs", "social_ag.json");
            if (!System.IO.File.Exists(configPath))
                return NotFound("Không tìm thấy file cấu hình.");

            string schemaKey = string.IsNullOrWhiteSpace(request.SchemaKey) ? "SHARE_OWNER" : request.SchemaKey;
            string tableName = string.IsNullOrWhiteSpace(request.TableName) ? "SOCIAL_AGI_TTXH" : request.TableName;
            string dateStr = string.IsNullOrWhiteSpace(request.Date)
                                            ? DateTime.Now.ToString("yyyyMMdd")
                                            : request.Date;

            DateTime dateParsed = DateTime.ParseExact(dateStr, "yyyyMMdd", null);
            string sendDate = dateStr;
            string beforeDate = dateParsed.AddDays(-1).ToString("yyyyMMdd");
            string nowDate = DateTime.Now.ToString("yyyyMMdd");

            var configJson = await System.IO.File.ReadAllTextAsync(configPath);
            var fullConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(configJson)!;

            // Lấy phần DataJson dùng chung
            var dataJsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                fullConfig["DataJson"]?.ToString() ?? "{}"
            );

            // Lấy cấu hình template theo tableName
            if (!dataJsonDict.TryGetValue(tableName, out var displayCategory))
                return BadRequest("Không tìm thấy chuyên mục từ tableName trong DataJson.");

            // Lấy cấu hình theo chuyên mục (không phải tableName)
            if (!fullConfig.TryGetValue(displayCategory, out var rawTemplateConfigObj))
                return BadRequest($"Không tìm thấy cấu hình template cho chuyên mục '{displayCategory}'.");

            var templateConfig = JsonConvert.DeserializeObject<TemplateConfig>(rawTemplateConfigObj.ToString() ?? "{}");

            if (templateConfig == null)
                return BadRequest("Không thể đọc cấu hình template.");


            // Lấy dữ liệu từ Oracle
            var (chartData, rawArticles) = await _socialAGService.GetOracleDataStructuredAsync(schemaKey, tableName, dateStr);
            // _logger.LogInformation("ChartData: {Chart}", System.Text.Json.JsonSerializer.Serialize(chartData, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            // _logger.LogInformation("RawArticles: {Articles}", System.Text.Json.JsonSerializer.Serialize(rawArticles, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

            // Lấy tên chuyên mục từ config["DataJson"]
            string chuyenMuc = displayCategory;


            // Tạo danh sách bài viết theo chuyên mục
            var firstGroup = rawArticles.Cast<dynamic>().FirstOrDefault();
            var articleList = new List<ArticleItem>();

            if (firstGroup?.data is IEnumerable<object> dataItems)
            {
                foreach (var d in dataItems)
                {
                    var article = (dynamic)d;
                    articleList.Add(new ArticleItem
                    {
                        Title = string.IsNullOrWhiteSpace((string)article.title)
                            ? Truncate((string)article.content, 120)
                            : (string)article.title,
                        Content = article.content,
                        Author = article.author,
                        CreatedAt = article.createdAt,
                        ArticleUrl = article.articleUrl,
                        Image = article.image
                    });
                }
            }

            var dataJsonList = new List<DataJsonCategory>
            {
                new DataJsonCategory
                {
                    Category = chuyenMuc,
                    Data = articleList
                }
            };


            // Thay thế {{before}} trong tên file PDF
            var outputFileName = (templateConfig.File ?? "output_{{table}}_{{date}}.pdf")
                        .Replace("{{table}}", tableName)
                        .Replace("{{date}}", dateStr)
                        .Replace("{{send_date}}", sendDate)
                        .Replace("{{before}}", beforeDate)
                        .Replace("{{now}}", nowDate);

            // Replace placeholders in MergeFields
            var mergeFields = new Dictionary<string, string>();
            foreach (var kv in templateConfig.MergeFields)
            {
                mergeFields[kv.Key] = kv.Value
                    .Replace("{{send_date}}", FormatDate(sendDate))
                    .Replace("{{before}}", FormatDate(beforeDate))
                    .Replace("{{now}}", FormatDate(nowDate))
                    .Replace("{{date}}", FormatDate(dateStr));
            }

            // Tạo request sinh PDF
            var chartRequest = new ChartJsonRequest
            {
                Data = chartData,
                MergeFields = mergeFields,
                DataJson = dataJsonList,
                TemplateName = templateConfig.TemplateExcel,
                OutputFileName = outputFileName
            };

            string wordPath = Path.Combine("Exports", Path.ChangeExtension(outputFileName, ".docx"));
            string pdfPath = Path.Combine("Exports", outputFileName);

            var url = await _sgiPdfChart.GenerateFromJson(chartRequest, templateConfig.TemplateExcel, wordPath, pdfPath);
            return Ok(new { message = url });
        }

        private static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return "(Không tiêu đề)";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        string FormatDate(string rawDate)
        {
            return DateTime.TryParseExact(rawDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dt)
                ? dt.ToString("dd/MM/yyyy")
                : rawDate;
        }

    }
}
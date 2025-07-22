using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.IO;

namespace microservice.mess.Services
{
    public class OracleSchemaConfig
    {
        public string ConnectionString { get; set; } = "";
        public string SchemaName { get; set; } = "";
    }

    public class SocialAGService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SocialAGService> _logger;

        public SocialAGService(IConfiguration configuration, ILogger<SocialAGService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<(List<Dictionary<string, object>> data, List<object> dataJson)> GetOracleDataStructuredAsync(string schemaKey, string tableName, string? date = null)
        {
            var rawRows = await GetDataAsync(schemaKey, tableName, date);
            var data = await AggregateChartAsync(rawRows, tableName); // dùng phiên bản async
            var dataJson = await GroupArticlesAsync(rawRows, tableName);
            return (data, dataJson);
        }


        private async Task<List<Dictionary<string, object>>> GetDataAsync(string schemaKey, string tableName, string? date = null)
        {
            var configSection = _configuration.GetSection($"Databases:ORACLE:{schemaKey}");
            if (!configSection.Exists())
                throw new Exception($"Không tìm thấy schema [{schemaKey}] trong cấu hình.");

            var config = configSection.Get<OracleSchemaConfig>()!;
            var connStr = config.ConnectionString;
            var schemaName = config.SchemaName;

            var result = new List<Dictionary<string, object>>();

            using var conn = new OracleConnection(connStr);
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();

            // 1. Truy vấn chính với CTE tách riêng CAST
            var sql = $@"
WITH image_casted AS (
    SELECT 
        ARTICLE_ID, ARTICLE_URL, TITLE, CONTENT, AUTHOR_ID, AUTHOR_NAME,
        CREATED_AT, LOCATION, CAST(IMAGE AS VARCHAR2(2000)) AS IMAGE, CHANNEL_NAME
    FROM {schemaName}.{tableName}
    {(string.IsNullOrWhiteSpace(date) ? "" : "WHERE TRUNC(CREATED_AT) = TO_DATE(:inputDate, 'YYYYMMDD')")}
),
ranked_data AS (
    SELECT 
        ARTICLE_ID, ARTICLE_URL, TITLE, CONTENT, AUTHOR_ID, AUTHOR_NAME,
        CREATED_AT, LOCATION, IMAGE, CHANNEL_NAME,
        ROW_NUMBER() OVER (
            PARTITION BY ARTICLE_ID
            ORDER BY 
                CASE WHEN TITLE IS NOT NULL AND TITLE != '' THEN 1 ELSE 2 END,
                CASE WHEN IMAGE IS NOT NULL AND IMAGE != '' THEN 1 ELSE 2 END,
                CREATED_AT DESC
        ) AS rn
    FROM image_casted
)
SELECT *
FROM ranked_data
WHERE rn = 1";


            cmd.CommandText = sql;
            if (!string.IsNullOrWhiteSpace(date))
            {
                cmd.Parameters.Add(new OracleParameter("inputDate", date));
            }

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i);
                    var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                    row[colName] = value!;
                }
                result.Add(row);
            }

            // 2. Fallback nếu không có dữ liệu
            if (result.Count == 0 && !string.IsNullOrWhiteSpace(date))
            {
                var fallbackCmd = conn.CreateCommand();

                var fallbackSql = $@"
WITH image_casted AS (
    SELECT 
        ARTICLE_ID, ARTICLE_URL, TITLE, CONTENT, AUTHOR_ID, AUTHOR_NAME,
        CREATED_AT, LOCATION, CAST(IMAGE AS VARCHAR2(2000)) AS IMAGE, CHANNEL_NAME
    FROM {schemaName}.{tableName}
),
ranked_data AS (
    SELECT 
        ARTICLE_ID, ARTICLE_URL, TITLE, CONTENT, AUTHOR_ID, AUTHOR_NAME,
        CREATED_AT, LOCATION, IMAGE, CHANNEL_NAME,
        ROW_NUMBER() OVER (
            PARTITION BY ARTICLE_ID
            ORDER BY CREATED_AT DESC
        ) AS rn
    FROM image_casted
)
SELECT *
FROM ranked_data
WHERE rn = 1";

                fallbackCmd.CommandText = fallbackSql;

                using var fallbackReader = await fallbackCmd.ExecuteReaderAsync();
                while (await fallbackReader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < fallbackReader.FieldCount; i++)
                    {
                        var colName = fallbackReader.GetName(i);
                        var value = await fallbackReader.IsDBNullAsync(i) ? null : fallbackReader.GetValue(i);
                        row[colName] = value!;
                    }
                    result.Add(row);
                }
            }

            return result;
        }

        private async Task<List<Dictionary<string, object>>> AggregateChartAsync(List<Dictionary<string, object>> rows, string tableName)
        {
            var allSources = rows
                .Where(d => d.ContainsKey("CHANNEL_NAME"))
                .Select(d => d["CHANNEL_NAME"]?.ToString())
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .ToList();

            // Đọc tên chuyên đề từ JSON
            var configPath = Path.Combine("templates/configs", "social_ag.json");
            var json = await File.ReadAllTextAsync(configPath);
            var config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            string chuyenDeName = tableName;
            if (config != null && config.TryGetValue("DataJson", out var dataJson) && dataJson.TryGetProperty(tableName, out var nameProp))
            {
                chuyenDeName = nameProp.GetString() ?? tableName;
            }

            return rows
                .GroupBy(d =>
                    d.ContainsKey("chuyen_de") && !string.IsNullOrWhiteSpace(d["chuyen_de"]?.ToString())
                        ? d["chuyen_de"]!.ToString()
                        : chuyenDeName)
                .Select(g =>
                {
                    var dict = new Dictionary<string, object>
                    {
                        ["Chủ đề"] = g.Key
                    };

                    foreach (var source in allSources)
                    {
                        dict[source] = g.Count(x =>
                            x.TryGetValue("CHANNEL_NAME", out var v) &&
                            v?.ToString() == source
                        );
                    }

                    return dict;
                })
                .ToList();
        }


        private async Task<List<object>> GroupArticlesAsync(List<Dictionary<string, object>> rows, string tableName)
        {
            var configPath = Path.Combine("templates/configs", "social_ag.json");
            var json = await File.ReadAllTextAsync(configPath);
            var config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            string chuyenDeName = tableName;
            if (config != null && config.TryGetValue("DataJson", out var dataJson) && dataJson.TryGetProperty(tableName, out var nameProp))
            {
                chuyenDeName = nameProp.GetString() ?? tableName;
            }

            return rows
                .GroupBy(d =>
                    d.ContainsKey("chuyen_de") && !string.IsNullOrWhiteSpace(d["chuyen_de"]?.ToString())
                        ? d["chuyen_de"]!.ToString()
                        : chuyenDeName)
                .Select(g => new
                {
                    category = g.Key.ToUpperInvariant(),
                    data = g.Select(d => new
                    {
                        title = d.TryGetValue("TITLE", out var titleVal) && !string.IsNullOrWhiteSpace(titleVal?.ToString())
                        ? Truncate(titleVal.ToString()!, 100)
                        : "(Không tiêu đề)",
                        content = Truncate(
                        d.TryGetValue("CONTENT", out var contentVal) ? contentVal?.ToString() ?? "" : "",
                        400),
                        author = d.TryGetValue("AUTHOR_NAME", out var authVal) ? authVal?.ToString() ?? "" : "",
                        createdAt = d.TryGetValue("CREATED_AT", out var tsVal) && DateTime.TryParse(tsVal?.ToString(), out var dt)
                            ? dt.ToString("yyyy-MM-dd HH:mm:ss")
                            : "",
                        articleUrl = d.TryGetValue("ARTICLE_URL", out var linkVal) ? linkVal?.ToString() ?? "" : "",
                        image = d.TryGetValue("IMAGE", out var imgVal)
                            ? LogAndConvertImage(imgVal)
                            : ""
                    }).ToList()
                })
                .ToList<object>();
        }

        private static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return "(Không tiêu đề)";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        private static string LogAndConvertImage(object? imageObj)
        {
            if (imageObj == null)
            {
                Console.WriteLine("NULL");
                return "";
            }

            Console.WriteLine(imageObj is byte[]? $"(byte[]) Length: {(imageObj as byte[]).Length}"
                : imageObj.ToString());

            return ConvertImageToString(imageObj);
        }

        private static string ConvertImageToString(object? imageObj)
        {
            if (imageObj == null)
                return "";

            // Nếu là byte[]
            if (imageObj is byte[] bytes && bytes.Length > 0)
            {
                // Nếu dữ liệu nhỏ hơn 1KB, có thể là base64 string chứa URL chứ không phải ảnh thật
                if (bytes.Length < 1024)
                {
                    var decoded = Encoding.UTF8.GetString(bytes);
                    if (decoded.StartsWith("http"))
                        return decoded; // Trả lại URL gốc
                }

                // Nếu không phải URL, assume là ảnh thật
                var base64 = Convert.ToBase64String(bytes);
                return $"data:image/png;base64,{base64}";
            }

            var str = imageObj.ToString();
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.StartsWith("http") || str.StartsWith("data:image"))
                    return str;

                // Thử decode base64 → có thể là URL
                try
                {
                    var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(str));
                    if (decoded.StartsWith("http"))
                        return decoded;
                }
                catch
                {
                    // Không phải base64 hợp lệ
                }
            }

            return str ?? "";
        }


    }
}

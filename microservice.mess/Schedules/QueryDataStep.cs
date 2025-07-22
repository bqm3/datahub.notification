using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace microservice.mess.Schedules
{
    public class QueryDataStep : IMessageStep
    {
        private readonly IMongoClientFactory _mongoClientFactory;
        private readonly ILogger<QueryDataStep> _logger;
        private readonly IConfiguration _config;

        public QueryDataStep(IConfiguration config, IMongoClientFactory mongoClientFactory, ILogger<QueryDataStep> logger)
        {
            _mongoClientFactory = mongoClientFactory;
            _logger = logger;
            _config = config;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            try
            {
                _logger.LogInformation("🔍 [QueryDataStep] Start query for schedule '{name}' ",
                    context.Schedule.Name, context.Schedule.Channel);

                var queryConfig = context.Schedule.Data;  // vì đã là kiểu DataConfig


                switch (queryConfig.DataSourceType?.ToLowerInvariant())
                {
                    case "mongo":
                        await QueryFromMongo(queryConfig, context);
                        break;
                    case "oracle":
                        await QueryFromOracle(queryConfig, context);
                        break;
                    default:
                        throw new NotSupportedException($" Query type '{queryConfig.DataSourceType}' is not supported");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🔥 [QueryDataStep] Failed to query data for schedule '{name}'", context.Schedule.Name);
                throw;
            }
        }

        // Mongo Query
        private async Task QueryFromMongo(DataConfig queryConfig, ScheduleContext context)
        {
            var db = _mongoClientFactory.GetDatabase(queryConfig.DataSource);
            var collection = db.GetCollection<BsonDocument>(queryConfig.DataSourceKey);

            var (filter, limit) = BuildMongoQuery(queryConfig.DataQuery);

            var query = collection
                .Find(filter)
                .Sort(Builders<BsonDocument>.Sort.Descending("timestamp"));

            if (limit.HasValue)
                query = query.Limit(limit.Value);

            var data = await query.ToListAsync();

            _logger.LogInformation("✅ [QueryDataStep] Mongo query success: {count} records", data?.Count ?? 0);
            context.Items["rawData"] = data;
        }

        // Oracle Query
        private async Task QueryFromOracle(DataConfig queryConfig, ScheduleContext context)
        {
            var result = new List<BsonDocument>();

            try
            {
                var connStr = _config[$"Databases:ORACLE:{queryConfig.DataSource}:ConnectionString"];
                if (string.IsNullOrWhiteSpace(connStr))
                    throw new Exception($"Không tìm thấy connection string cho ORACLE.{queryConfig.DataSource}");

                using var conn = new OracleConnection(connStr);
                await conn.OpenAsync();

                // Xử lý điều kiện truy vấn
                var query = queryConfig.DataQuery?.ToLowerInvariant();
                string whereClause = "";
                int? limit = null;

                if (query == "one-day-before")
                {
                    var now = DateTime.UtcNow.Date;
                    var dayStart = now.AddDays(-1).ToString("yyyy-MM-dd");
                    var dayEnd = now.ToString("yyyy-MM-dd");
                    whereClause = $"WHERE CREATED_AT >= TO_DATE('{dayStart}', 'YYYY-MM-DD') AND CREATED_AT < TO_DATE('{dayEnd}', 'YYYY-MM-DD')";
                }
                else if (query == "one-day-now")
                {
                    var now = DateTime.UtcNow.Date;
                    var dayNow = now.ToString("yyyy-MM-dd");
                    whereClause = $"WHERE CREATED_AT = TO_DATE('{dayNow}', 'YYYY-MM-DD')";
                }
                else if (int.TryParse(query, out int topN))
                {
                    limit = topN;
                }

                var sql = $"SELECT * FROM {queryConfig.DataSourceKey}";
                if (!string.IsNullOrWhiteSpace(whereClause))
                    sql += " " + whereClause;
                if (limit.HasValue)
                    sql = $"SELECT * FROM ({sql}) WHERE ROWNUM <= {limit.Value}";

                using var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var doc = new BsonDocument
                    {
                        ["chuyen_de"] = queryConfig.DataSourceKey ?? "Khác",
                        ["title"] = reader["TITLE"]?.ToString() ?? "",
                        ["content"] = reader["CONTENT"]?.ToString() ?? "",
                        ["source"] = reader["CHANNEL_NAME"]?.ToString() ?? "",
                        ["author"] = reader["AUTHOR_NAME"]?.ToString() ?? "",
                        ["articleUrl"] = reader["ARTICLE_URL"]?.ToString() ?? "",
                        ["timestamp"] = reader["CREATED_AT"] is DateTime dt ? dt : BsonNull.Value
                    };
                    if (reader["IMAGE"] is byte[] imgBytes)
                    {
                        var mime = GetImageMime(imgBytes);
                        doc["image"] = $"data:image/{mime};base64,{Convert.ToBase64String(imgBytes)}";
                    }
                    else
                    {
                        doc["image"] = "";
                    }

                    result.Add(doc);
                }

                _logger.LogInformation("✅ [QueryDataStep] Oracle query success: {count} records", result.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🚫 [QueryDataStep] Oracle connection/query failed for source '{source}'", queryConfig.DataSource);
            }

            // Dù lỗi hay thành công, vẫn đưa vào context (danh sách rỗng nếu lỗi)
            context.Items["rawData"] = result;
        }


        // Mongo filter builder
        private (FilterDefinition<BsonDocument> filter, int? limit) BuildMongoQuery(string queryValue)
        {
            var now = DateTime.UtcNow;
            switch (queryValue?.ToLowerInvariant())
            {
                case "top-10":
                    var filter = Builders<BsonDocument>.Filter.Empty; // lấy tất cả
                    int? limit = 10;
                    return (filter, limit);

                default:
                    return (Builders<BsonDocument>.Filter.Empty, null);
            }
        }

        private string GetImageMime(byte[] bytes)
        {
            if (bytes.Length >= 4)
            {
                // JPEG
                if (bytes[0] == 0xFF && bytes[1] == 0xD8)
                    return "jpeg";

                // PNG
                if (bytes[0] == 0x89 && bytes[1] == 0x50)
                    return "png";

                // GIF
                if (bytes[0] == 0x47 && bytes[1] == 0x49)
                    return "gif";

                // BMP
                if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                    return "bmp";
            }

            return "jpeg"; // fallback
        }

    }
}

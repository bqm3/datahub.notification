using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Schedules
{
    public class MongoQueryExecutor
    {
        private readonly IMongoDatabase _db;
        private readonly ILogger<MongoQueryExecutor> _logger;

        public MongoQueryExecutor(IOptions<MongoSettings> mongoOptions, IMongoClient mongoClient, ILogger<MongoQueryExecutor> logger)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            _db = mongoClient.GetDatabase(settings.ZaloDatabase);
        }

        public async Task<object?> ExecuteAsync(string queryJson, DataConfig data, ScheduleContext context)
        {
            if (string.IsNullOrWhiteSpace(queryJson)) return null;
            _logger.LogInformation("→ Data input for binding: {data} {queryJson}", JsonConvert.SerializeObject(data), queryJson);

            var template = Newtonsoft.Json.JsonConvert.DeserializeObject<MongoQueryTemplate>(queryJson);

            if (template == null || string.IsNullOrWhiteSpace(template.Collection))
                throw new ArgumentException("Mongo query template không hợp lệ.");

            var collection = _db.GetCollection<BsonDocument>(template.Collection);

            var rawFilterJson = System.Text.Json.JsonSerializer.Serialize(template.Filter);
            var boundFilterJson = BindPlaceholders(rawFilterJson, data, context);
            _logger?.LogInformation("→ Mongo filter sau bind: {filter}", boundFilterJson);

            var filterDoc = BsonSerializer.Deserialize<BsonDocument>(boundFilterJson);
            var doc = await collection.Find(filterDoc).FirstOrDefaultAsync();

            if (doc == null || template.Field == null)
                return null;

            return doc.TryGetValue(template.Field, out var val) ? val.ToString() : null;
        }

        private string BindPlaceholders(string json, DataConfig data, ScheduleContext context)
        {
            if (data == null) return json;

            var map = new Dictionary<string, string>();

            foreach (var prop in data.GetType().GetProperties())
            {
                var rawVal = prop.GetValue(data);
                var strVal = rawVal switch
                {
                    null => "",
                    string s => s,
                    IEnumerable<string> list => string.Join(",", list),
                    DateTime dt => dt.ToString("yyyy-MM-dd HH:mm:ss"),
                    _ => rawVal?.ToString() ?? ""
                };

                map[prop.Name] = strVal;
            }

            // if (!map.ContainsKey("FromEmail"))
            // {
            //     var fallbackEmail = context.Schedule.To?.FirstOrDefault() ?? context.Schedule.From ?? "";
            //     map["FromEmail"] = fallbackEmail;
            // }

            foreach (var kv in map)
            {
                json = json.Replace($"{{{{{kv.Key}}}}}", kv.Value);
            }

            if (json.Contains("{{"))
            {
                var regex = System.Text.RegularExpressions.Regex.Matches(json, "{{(.*?)}}");
                var missing = string.Join(", ", regex.Select(m => m.Groups[1].Value));
                _logger?.LogWarning("⚠️ Placeholder chưa được thay thế: {placeholders}", missing);
            }

            return json;
        }

        private class MongoQueryTemplate
        {
            public string Collection { get; set; } = string.Empty;
            public object Filter { get; set; } = new();
            public string? Field { get; set; }
        }
    }
}

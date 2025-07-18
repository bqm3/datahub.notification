using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models.Message;
using microservice.mess.Kafka;
using microservice.mess.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;

namespace microservice.mess.Services
{
    public class NotificationService
    {
        private readonly KafkaProducerService _kafkaProducer;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;
        private readonly SlackService _slackService;
        private readonly LogMessageRepository _logMessageRepository;

        public NotificationService(
            KafkaProducerService kafkaProducer,
            IHubContext<NotificationHub> hubContext,
            ILogger<NotificationService> logger,
            LogMessageRepository logMessageRepository,
            SlackService slackService)
        {
            _kafkaProducer = kafkaProducer;
            _hubContext = hubContext;
            _logger = logger;
            _slackService = slackService;
            _logMessageRepository = logMessageRepository;
        }

        // public async Task<Dictionary<string, string>> GetDynamicDataAsync(
        //     ScheduledAllModel item,
        //     IServiceProvider serviceProvider)
        // {
        //     var type = item.DataSourceType?.ToLower();
        //     var key = item.DataSourceKey;

        //     if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(key))
        //         return item.Data ?? new();

        //     try
        //     {
        //         return type switch
        //         {
        //             "mongo" => await GetLogErrorDataAsync(key, serviceProvider),
        //             _ => item.Data ?? new()
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Lỗi lấy dữ liệu động từ {type} với key {key}", type, key);
        //         return item.Data ?? new();
        //     }
        // }


        // // public async Task<Dictionary<string, object>> GetDataFromApiAsync(string url)
        // // {
        // //     using var httpClient = new HttpClient();
        // //     var res = await httpClient.GetStringAsync(url);
        // //     return JsonConvert.DeserializeObject<Dictionary<string, object>>(res) ?? new();
        // // }

        // public async Task<Dictionary<string, string>> GetDataFromMongoAsync(string collectionName, IServiceProvider serviceProvider)
        // {
        //     var mongo = serviceProvider.GetRequiredService<IMongoDatabase>();
        //     var collection = mongo.GetCollection<BsonDocument>(collectionName);

        //     var document = await collection.Find(Builders<BsonDocument>.Filter.Empty)
        //                                    .SortByDescending(x => x["timestamp"])
        //                                    .FirstOrDefaultAsync();

        //     // Convert từ BsonDocument sang Dictionary<string, string>
        //     return document?.ToDictionary()
        //                    .ToDictionary(kv => kv.Key, kv => kv.Value?.ToString() ?? "") ?? new();
        // }

        // public async Task<Dictionary<string, string>> GetLogErrorDataAsync(string collectionName, IServiceProvider serviceProvider)
        // {
        //      var mongo = serviceProvider.GetRequiredService<IMongoDatabase>();
        //     var collection = mongo.GetCollection<BsonDocument>(collectionName);

        //     var document = await collection.Find(Builders<BsonDocument>.Filter.Empty)
        //                                    .SortByDescending(x => x["timestamp"])
        //                                    .FirstOrDefaultAsync();

        //     if (document == null) return new();

        //     var data = new Dictionary<string, string>();

        //     // 1. service
        //     data["service"] = document.GetValue("services", BsonNull.Value).ToString();

        //     // 2. log_error từ properties.content_s_srcha[0]
        //     if (document.TryGetValue("properties", out var props) && props.IsBsonDocument)
        //     {
        //         var propsDoc = props.AsBsonDocument;

        //         if (propsDoc.TryGetValue("content_s_srcha", out var contentArr) && contentArr.IsBsonArray && contentArr.AsBsonArray.Count > 0)
        //         {
        //             data["log_error"] = contentArr[0].ToString();
        //         }
        //         else
        //         {
        //             data["log_error"] = "[Không có nội dung lỗi]";
        //         }

        //         // 3. log_number từ numOfComment hoặc numOfView
        //         if (propsDoc.TryGetValue("numOfComment_l_srcha", out var countArr) && countArr.IsBsonArray && countArr.AsBsonArray.Count > 0)
        //         {
        //             data["log_number"] = countArr[0].ToString();
        //         }
        //         else if (propsDoc.TryGetValue("numOfView_l_srcha", out var viewArr) && viewArr.IsBsonArray && viewArr.AsBsonArray.Count > 0)
        //         {
        //             data["log_number"] = viewArr[0].ToString();
        //         }
        //         else
        //         {
        //             data["log_number"] = "0";
        //         }
        //     }
        //     else
        //     {
        //         data["log_error"] = "[Không có dữ liệu]";
        //         data["log_number"] = "0";
        //     }

        //     return data;
        // }



        // public async Task<Dictionary<string, object>> GetDataFromRedisAsync(string key, IServiceProvider serviceProvider)
        // {
        //     var redis = serviceProvider.GetRequiredService<IConnectionMultiplexer>();
        //     var db = redis.GetDatabase();
        //     var json = await db.StringGetAsync(key);

        //     return string.IsNullOrWhiteSpace(json)
        //         ? new()
        //         : JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? new();
        // }

        // public async Task<Dictionary<string, object>> GetDataFromKafkaAsync(string topic, IServiceProvider serviceProvider)
        // {
        //     // var consumer = serviceProvider.GetRequiredService<IKafkaConsumer>(); // bạn cần tự define interface/service này
        //     // var latestMessage = await consumer.ConsumeLatestMessageAsync(topic); // giả định có hàm này
        //     return new();
        //     // return string.IsNullOrWhiteSpace(latestMessage)
        //     //     ? new()
        //     //     : JsonConvert.DeserializeObject<Dictionary<string, object>>(latestMessage) ?? new();
        // }



    }
}

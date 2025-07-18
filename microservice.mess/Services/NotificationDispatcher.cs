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
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly KafkaProducerService _kafkaProducer;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationDispatcher> _logger;
        private readonly SlackService _slackService;
        private readonly LogMessageRepository _logMessageRepository;

        public NotificationDispatcher(
            KafkaProducerService kafkaProducer,
            IHubContext<NotificationHub> hubContext,
            ILogger<NotificationDispatcher> logger,
            LogMessageRepository logMessageRepository,
            SlackService slackService)
        {
            _kafkaProducer = kafkaProducer;
            _hubContext = hubContext;
            _logger = logger;
            _slackService = slackService;
            _logMessageRepository = logMessageRepository;
        }

        public async Task<MessageHeaders?> DispatchMessageAsync(MessageRequest request, IClientProxy? signalRClient = null)
        {
            if (request?.Headers == null || request.Body == null || request.Body.Count == 0)
            {
                _logger.LogWarning("Yêu cầu gửi message không hợp lệ.");
                return null;
            }

            string action = request.Headers.Action;
            string messageType = request.Headers.MessageType?.ToLower() ?? "";
            var vnNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh"));
            string createdAt = string.IsNullOrWhiteSpace(request.Headers.CreatedAt)
                ? vnNow.ToString("HH:mm:ss dd/MM/yyyy")
                : request.Headers.CreatedAt;

            request.Headers.CreatedAt = createdAt;
            string topic = $"topic-{messageType}";

            foreach (var item in request.Body)
            {
                var filteredItem = new MessageBodyItem();

                switch (messageType)
                {
                    case "mail":
                        if (item.Email == null) continue;
                        filteredItem.Email = item.Email;
                        break;

                    case "zalo":
                        if (item.Zalo == null) continue;
                        filteredItem.Zalo = item.Zalo;
                        break;

                    case "signet":
                        if (item.Signet == null) continue;
                        filteredItem.Signet = item.Signet;
                        break;

                    // case "signalr":
                    //     if (item.SignalR != null)
                    //     {
                    //         var signalRData = new Dictionary<string, string>
                    //         {
                    //             ["Action"] = action,
                    //             ["Message"] = item.SignalR.Message ?? "",
                    //             ["CreatedAt"] = createdAt
                    //         };
                    //         await _hubContext.Clients.All.SendAsync("DispatchMessage", signalRData);
                    //         _logger.LogInformation("Dispatched to SignalR: {msg}", item.SignalR.Message);
                    //     }
                    //     continue;

                    case "slack":
                        if (!string.IsNullOrEmpty(item.Slack?.Message))
                        {
                            await _slackService.SendMessageAsync(item.Slack.Message);
                            _logger.LogInformation("Sent to Slack: {msg}", item.Slack.Message);
                        }
                        continue;

                    case "push":
                    case "sms":
                        _logger.LogWarning("MessageType '{type}' chưa được hỗ trợ xử lý.", messageType);
                        continue;

                    default:
                        _logger.LogWarning("Không hỗ trợ messageType: {type}", messageType);
                        continue;
                }

                var message = new MessageRequest
                {
                    Headers = new MessageHeaders
                    {
                        Action = action,
                        MessageType = messageType,
                        CreatedAt = createdAt
                    },
                    Body = new List<MessageBodyItem> { filteredItem }
                };

                var serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(message);

                try
                {
                    await _kafkaProducer.SendMessageAsync(topic, null, serializedRequest);
                    _logger.LogInformation("Đã gửi message Kafka {topic}", topic);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi gửi message type: {type}", messageType);

                    var errorLog = new MessageErrorLog
                    {
                        Error = ex.ToString(),
                        RawPayload = serializedRequest,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _logMessageRepository.InsertErrorAsync(errorLog);
                }
            }

            if (signalRClient != null)
            {
                await signalRClient.SendAsync("MessageSent", $"Đã gửi `{action}` với {request.Body.Count} item(s)");
            }

            return request.Headers;
        }

    }
}

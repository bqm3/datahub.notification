using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models.Message;
using microservice.mess.Kafka;
using microservice.mess.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

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
            string messageType = request.Headers.MessageType.ToLower();
            string createdAt = request.Headers.CreatedAt;
            string topic = $"topic-{messageType}";

            foreach (var item in request.Body)
            {
                var message = new MessageRequest
                {
                    Headers = new MessageHeaders
                    {
                        Action = action,
                        MessageType = messageType,
                        CreatedAt = createdAt
                    },
                    Body = new List<MessageBodyItem> { item }
                };

                string serializedRequest = JsonSerializer.Serialize(message);
                try
                {
                    switch (messageType)
                    {
                        case "mail":
                            await _kafkaProducer.SendMessageAsync(topic, null, serializedRequest);
                            break;
                        case "zalo":
                            await _kafkaProducer.SendMessageAsync(topic, null, serializedRequest);
                            break;

                        case "signalr":
                            var signalRPayload = item.SignalR?.Message ?? "";
                            var data = new Dictionary<string, string>
                            {
                                ["Action"] = action,
                                ["Message"] = signalRPayload,
                                ["CreatedAt"] = createdAt
                            };
                            await _hubContext.Clients.All.SendAsync("DispatchMessage", data);
                            _logger.LogInformation("Dispatched to SignalR: {msg}", signalRPayload);
                            break;

                        case "slack":
                            var slackPayload = item.Slack?.Message ?? "No message";
                            await _slackService.SendMessageAsync(slackPayload);
                            _logger.LogInformation("Sent to Slack: {msg}", slackPayload);
                            break;

                        case "push":
                        case "sms":
                        case "signet":
                            _logger.LogWarning("Chưa hỗ trợ messageType: {type}", messageType);
                            break;

                        default:
                            _logger.LogWarning("Không hỗ trợ messageType: {type}", messageType);
                            break;
                    }

                    // // Ghi log gửi thành công
                    // var log = new MessageLog
                    // {
                    //     Action = action,
                    //     Channel = messageType,
                    //     CreatedAt = DateTime.UtcNow,
                    //     Payload = serializedRequest
                    // };
                    // await _logMessageRepository.InsertAsync(log);
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

            // Notify qua SignalR nếu có
            if (signalRClient != null)
            {
                await signalRClient.SendAsync("MessageSent", $"Đã gửi `{action}` với {request.Body.Count} item(s)");
            }

            // Trả về headers đã gửi để NotifyController biết mà log
            return request.Headers;
        }

    }
}

using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using microservice.mess.Services;
using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Repositories;
using microservice.mess.Configurations;

namespace microservice.mess.Kafka
{
    public class NotifyKafkaConsumer
    {
        private readonly MailService _mailService;
        private readonly SignetService _signetService;
        private readonly ZaloService _zaloService;
        private readonly SlackService _slackService;
        private readonly TelegramService _telegramService;
        private readonly ZaloRepository _zaloRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LogMessageRepository _logMessageRepository;
        private readonly KafkaProducerService _kafkaProducer;
        private readonly SmtpSettings _smtpSettings;
        private readonly ZaloSettings _zaloSettings;
        private readonly ILogger<NotifyKafkaConsumer> _logger;

        private readonly string _bootstrapServers = "host.docker.internal:9092";
        private readonly string[] _topics = new[] { "topic-mail" };
//, "topic-zalo", "topic-signet", "topic-tele", "topic-slack"
        public NotifyKafkaConsumer(
            MailService mailService,
            SignetService signetService,
            ZaloService zaloService,
            SlackService slackService,
            TelegramService telegramService,
            ZaloRepository zaloRepository,
            IHttpClientFactory httpClientFactory,
            IOptions<SmtpSettings> smtpOptions,
            IOptions<ZaloSettings> zaloOptions,
            ILogger<NotifyKafkaConsumer> logger,
            LogMessageRepository logMessageRepository,
            KafkaProducerService kafkaProducer)
        {
            _mailService = mailService;
            _signetService = signetService;
            _zaloService = zaloService;
            _slackService = slackService;
            _telegramService = telegramService;
            _zaloRepository = zaloRepository;
            _httpClientFactory = httpClientFactory;
            _smtpSettings = smtpOptions.Value;
            _zaloSettings = zaloOptions.Value;
            _logger = logger;
            _logMessageRepository = logMessageRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "notify-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topics);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(cancellationToken);
                    string payload = result.Message.Value;
                    string topic = result.Topic;

                    var message = JsonConvert.DeserializeObject<MessageRequest>(payload);
                    if (message == null || message.Headers == null || message.Body == null)
                    {
                        _logger.LogWarning("Invalid message format.");
                        continue;
                    }

                    // Route message based on topic
                    switch (topic)
                    {
                        case "topic-mail":
                            await HandleMailAsync(message);
                            break;
                        case "topic-zalo":
                            await HandleZaloAsync(message);
                            break;
                        case "topic-signet":
                            await HandleSignetAsync(message);
                            break;
                        case "topic-slack":
                            await HandleSlackAsync(message);
                            break;
                        case "topic-tele":
                            await HandleTeleAsync(message);
                            break;

                        default:
                            _logger.LogWarning("No handler for topic {topic}", topic);
                            break;
                    }

                    consumer.Commit(result);
                }
                catch (KafkaException ex)
                {
                    _logger.LogError(ex, "Kafka commit failed.");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consumer cancelled.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled error in NotifyKafkaConsumer.");
                }
            }

            consumer.Close();
        }

        private async Task HandleMailAsync(MessageRequest message)
        {
            try
            {
                if (message.Headers.Action?.ToLower() == "send-mail-name")
                {
                    foreach (var item in message.Body)
                    {
                        if (item.Email != null)
                        {
                            await _mailService.SendEmailAsync(item.Email);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("Unsupported mail action: {action}", message.Headers.Action);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process mail message.");
                // await LogErrorAndRetry("topic-mail-error", message, ex);
            }
        }

        private async Task HandleSignetAsync(MessageRequest jsonMessage)
        {
            try
            {

                var action = jsonMessage.Headers?.Action?.ToLower() ?? "";
                var bodyItem = jsonMessage.Body?.FirstOrDefault();
                var signet = bodyItem?.Signet;

                switch (action)
                {
                    case "send-signet-message-log":
                        var request = new SendTemplateMessageRequest
                        {
                            TemplateName = signet.TemplateName,
                            Data = signet.Data
                        };
                        await _signetService.SendTemplateMessageAsync(request);
                        break;
                    case "send-signet-message-schedule":
                        var requestSchedule = new SendTemplateMessageRequest
                        {
                            TemplateName = signet.TemplateName,
                            Data = signet.Data
                        };
                        await _signetService.SendTemplateMessageAsync(requestSchedule);
                        break;

                    default:
                        _logger.LogWarning("Unsupported signet action: {action}", action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.");
            }
        }

        private async Task HandleZaloAsync(MessageRequest message)
        {
            try
            {
                var action = message.Headers?.Action?.ToLower() ?? "";
                var bodyItem = message.Body?.FirstOrDefault();
                var zalo = bodyItem?.Zalo;

                if (zalo == null)
                {
                    _logger.LogWarning("Zalo payload missing.");
                    return;
                }

                switch (action)
                {
                    // case "create-promotion":
                    //     await _zaloRepository.InsertOrUpdateByTagAsync(zalo.PromotionRequest);
                    //     break;

                    case "send-promotion":

                        if (string.IsNullOrEmpty(zalo.AccessToken) || string.IsNullOrEmpty(zalo.UserId))
                        {
                            _logger.LogWarning("Missing accessToken or userId.");
                            return;
                        }

                        await _zaloService.SendPromotionToUser(zalo.UserId, zalo.AccessToken, zalo.Tag);
                        break;

                    default:
                        _logger.LogWarning("Unsupported zalo action: {action}", action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process zalo message.");
                // await LogErrorAndRetry("topic-zalo-error", message, ex);
            }
        }

        private async Task HandleTeleAsync(MessageRequest message)
        {
            try
            {
                var action = message.Headers?.Action?.ToLower() ?? "";
                var bodyItem = message.Body?.FirstOrDefault();
                var tele = bodyItem?.Tele;

                if (tele == null)
                {
                    _logger.LogWarning("tele payload missing.");
                    return;
                }

                switch (action)
                {
                    case "send-tele-mess":
                        await _telegramService.SendMessageAsync(tele.Message);
                        break;

                  

                    default:
                        _logger.LogWarning("Unsupported tele action: {action}", action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process tele message.");
            }
        }

        private async Task HandleSlackAsync(MessageRequest message)
        {
            try
            {
                var action = message.Headers?.Action?.ToLower() ?? "";
                var bodyItem = message.Body?.FirstOrDefault();
                var slack = bodyItem?.Slack;

                if (slack == null)
                {
                    _logger.LogWarning("slack payload missing.");
                    return;
                }

                switch (action)
                {
                    case "send-slack-mess":
                        await _slackService.SendMessageAsync(slack.Message);
                        break;

                    default:
                        _logger.LogWarning("Unsupported slack action: {action}", action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process slack message.");
            }
        }

        // private async Task LogErrorAndRetry(string errorTopic, MessageRequest message, Exception ex)
        // {
        //     await _logMessageRepository.InsertErrorAsync(new MessageErrorLog
        //     {
        //         Error = ex.ToString(),
        //         RawPayload = JsonConvert.SerializeObject(message),
        //         CreatedAt = DateTime.UtcNow
        //     });

        //     await _kafkaProducer.SendMessageAsync(errorTopic, null, JsonConvert.SerializeObject(message));
        // }
    }
}

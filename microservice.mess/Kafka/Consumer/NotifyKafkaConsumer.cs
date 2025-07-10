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
using microservice.mess.Repositories;
using microservice.mess.Configurations;

namespace microservice.mess.Kafka
{
    public class NotifyKafkaConsumer
    {
        private readonly MailService _mailService;
        private readonly SignetService _signetService;
        private readonly ZaloService _zaloService;
        private readonly ZaloRepository _zaloRepository;
        // private readonly SignetRepository _signetRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<NotifyKafkaConsumer> _logger;
        private readonly LogMessageRepository _logMessageRepository;
        private readonly KafkaProducerService _kafkaProducer;
        private readonly SmtpSettings _smtpSettings;
        private readonly ZaloSettings _zaloSettings;

        private readonly string _bootstrapServers = "host.docker.internal:9092";
        private readonly string[] _topics = new[] { "topic-mail", "topic-zalo", "topic-signet" };

        public NotifyKafkaConsumer(
            MailService mailService,
            SignetService signetService,
            ZaloService zaloService,
            ZaloRepository zaloRepository,
            // SignetRepository signetRepository,
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
            _zaloRepository = zaloRepository;
            // _signetRepository = signetRepository;
            _httpClientFactory = httpClientFactory;
            _smtpSettings = smtpOptions.Value;
            _zaloSettings = zaloOptions.Value;
            _logger = logger;
            _logMessageRepository = logMessageRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("👂 NotifyKafkaConsumer loop is running");

            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "notify-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topics);

            _logger.LogInformation("Unified Kafka Consumer started. Listening to: {topics}", string.Join(", ", _topics));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(cancellationToken);
                    string payload = result.Message.Value;
                    string topic = result.Topic;

                    _logger.LogInformation("Received message from topic {topic}: {message}", topic, payload);

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
                if (message.Headers.Action?.ToLower() == "send-mail-tag")
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
                await LogErrorAndRetry("topic-mail-error", message, ex);
            }
        }

        private async Task HandleSignetAsync(MessageRequest jsonMessage)
        {
            try
            {

                var action = jsonMessage.Headers?.Action?.ToLower() ?? "";
                var bodyItem = jsonMessage.Body?.FirstOrDefault();
                var signet = bodyItem?.Signet;

                if (signet == null)
                {
                    _logger.LogWarning("Signet payload missing.");
                    return;
                }
                
                _logger.LogInformation("HandleSignetAsync - action: {action}, template: {template}", action, signet?.TemplateName);

                // var envelope = JsonConvert.DeserializeObject<MessageRequest>(jsonMessage);
                // if (envelope == null || envelope.Headers == null || envelope.Body == null)
                // {
                //     return;
                // }

                // var action = action.Action;
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

                    default:
                        _logger.LogWarning("Unsupported signet action: {action}", action);
                        break;
                }

                // if (action?.ToLower() == "send-signet-message-log")
                // {
                //     foreach (var item in envelope.Body)
                //     {
                //         if (item.Signet != null)
                //         {
                //             var request = new SendTemplateMessageRequest
                //             {
                //                 TemplateName = item.Signet.TemplateName,
                //                 Data = item.Signet.Data
                //             };

                //             await _signetService.SendTemplateMessageAsync(request);
                //         }

                //     }
                // }
                // else
                // {
                //     _logger.LogWarning("Unsupported action: {action}", action);
                // }
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
                    case "create-promotion":
                        await _zaloRepository.InsertOrUpdateByTagAsync(zalo.PromotionRequest);
                        break;

                    case "send-promotion":
                        if (string.IsNullOrEmpty(zalo.AccessToken) || string.IsNullOrEmpty(zalo.UserId))
                        {
                            _logger.LogWarning("Missing accessToken or userId.");
                            return;
                        }

                        var promotion = await _zaloRepository.GetPromotionByTagAsync(zalo.PromotionRequest.Tag);
                        if (promotion == null)
                        {
                            _logger.LogWarning("No promotion found for tag '{tag}'", zalo.PromotionRequest.Tag);
                            return;
                        }

                        // await _zaloService.HandleSendPromotionAsync(promotion, zalo.AccessToken);
                        break;

                    default:
                        _logger.LogWarning("Unsupported zalo action: {action}", action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process zalo message.");
                await LogErrorAndRetry("topic-zalo-error", message, ex);
            }
        }

        private async Task LogErrorAndRetry(string errorTopic, MessageRequest message, Exception ex)
        {
            await _logMessageRepository.InsertErrorAsync(new MessageErrorLog
            {
                Error = ex.ToString(),
                RawPayload = JsonConvert.SerializeObject(message),
                CreatedAt = DateTime.UtcNow
            });

            await _kafkaProducer.SendMessageAsync(errorTopic, null, JsonConvert.SerializeObject(message));
        }
    }
}

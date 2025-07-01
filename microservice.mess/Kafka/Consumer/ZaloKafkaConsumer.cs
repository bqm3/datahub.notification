using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Lib.Setting;
using Lib.Utility;

using microservice.mess.Services;
using microservice.mess.Models;
using microservice.mess.Repositories;
using microservice.mess.Configurations;


namespace microservice.mess.Kafka
{
    public class ZaloKafkaConsumer
    {
        private readonly ZaloService _zaloService;
        private readonly ZaloEventRepository _zaloEventRepository;
        private readonly ZaloPromotionRepository _zaloPromotionRepository;
        private readonly GroupMemberRepository _groupMemberRepository;
        private readonly TokenRepository _tokenRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ZaloSettings _zaloSettings;
        private readonly ILogger<ZaloKafkaConsumer> _logger;

        private readonly string _bootstrapServers = "host.docker.internal:9092";
        private readonly string _topic = "topic-zalo";

        public ZaloKafkaConsumer(
            ZaloService zaloService,
            ZaloEventRepository zaloEventRepository,
            ZaloPromotionRepository zaloPromotionRepository,
            GroupMemberRepository groupMemberRepository,
            TokenRepository tokenRepository,
            IHttpClientFactory httpClientFactory,
            IOptions<ZaloSettings> zaloOptions,
            ILogger<ZaloKafkaConsumer> logger)
        {
            _zaloService = zaloService;
            _zaloEventRepository = zaloEventRepository;
            _zaloPromotionRepository = zaloPromotionRepository;
            _groupMemberRepository = groupMemberRepository;
            _tokenRepository = tokenRepository;
            _httpClientFactory = httpClientFactory;
            _zaloSettings = zaloOptions.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "zalo-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            _logger.LogInformation("Zalo Kafka Consumer started...");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(cancellationToken);
                    var message = JsonConvert.DeserializeObject<NotificationKafkaEnvelope>(result.Message.Value);

                    if (message == null || string.IsNullOrEmpty(message.Action))
                    {
                        _logger.LogWarning("Received empty or invalid message.");
                        continue;
                    }

                    await ProcessMessageAsync(JsonConvert.SerializeObject(message));

                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consumer cancelled by CancellationToken.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing Kafka message.");
                }
            }

            consumer.Close();
        }

        private async Task ProcessMessageAsync(string jsonMessage)
        {
            try
            {
                var envelope = JsonConvert.DeserializeObject<NotificationKafkaEnvelope>(jsonMessage);
                if (envelope == null || string.IsNullOrWhiteSpace(envelope.Action))
                {
                    _logger.LogWarning("Envelope or Action is null.");
                    return;
                }

                switch (envelope.Action.ToLower())
                {
                    case "create-promotion":
                        var createRequest = JsonConvert.DeserializeObject<ZaloPromotionRequest>(envelope.Payload);
                        if (createRequest == null)
                        {
                            _logger.LogWarning("Invalid create-promotion payload.");
                            return;
                        }

                        var existing = await _zaloPromotionRepository.GetPromotionByIdAsync(createRequest.Tag);
                        if (existing != null)
                        {
                            _logger.LogWarning("Promotion with tag '{tag}' already exists.", createRequest.Tag);
                            return;
                        }

                        await _zaloPromotionRepository.InsertOneAsync(createRequest);
                        _logger.LogInformation("Created promotion with tag '{tag}'", createRequest.Tag);
                        break;


                    case "send-promotion":
                        var sendPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(envelope.Payload);
                        if (!sendPayload.TryGetValue("tag", out var tag) || !sendPayload.TryGetValue("accessToken", out var token))
                        {
                            _logger.LogWarning("Missing tag or accessToken in send-promotion.");
                            return;
                        }

                        var promotion = await _zaloPromotionRepository.GetPromotionByIdAsync(tag);
                        if (promotion == null)
                        {
                            _logger.LogWarning("No promotion found for tag '{tag}'", tag);
                            return;
                        }

                        // await _zaloService.HandleSendPromotionAsync(promotion, token);
                        break;

                    case "callback":
                        var callbackRequest = JsonConvert.DeserializeObject<ZaloCallbackRequest>(envelope.Payload);
                        if (callbackRequest == null)
                        {
                            _logger.LogWarning("Invalid callback payload.");
                            return;
                        }
                        await _zaloService.ProcessCallback(callbackRequest);
                        break;

                    default:
                        _logger.LogWarning("Unsupported action: {action}", envelope.Action);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.");
            }
        }

        
    }
}
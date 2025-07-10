// using Confluent.Kafka;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using Newtonsoft.Json;
// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using System.Linq;
// using Lib.Setting;
// using Lib.Utility;

// using microservice.mess.Services;
// using microservice.mess.Models;
// using microservice.mess.Models.Message;
// using microservice.mess.Repositories;
// using microservice.mess.Configurations;


// namespace microservice.mess.Kafka
// {
//     public class ZaloKafkaConsumer
//     {
//         private readonly ZaloService _zaloService;
//         private readonly ZaloRepository _zaloRepository;
//         private readonly IHttpClientFactory _httpClientFactory;
//         private readonly LogMessageRepository _logMessageRepository;
//         private readonly KafkaProducerService _kafkaProducer;
//         private readonly ZaloSettings _zaloSettings;
//         private readonly ILogger<ZaloKafkaConsumer> _logger;

//         private readonly string _bootstrapServers = "host.docker.internal:9092";
//         private readonly string _topic = "topic-zalo";

//         public ZaloKafkaConsumer(
//             ZaloService zaloService,
//             ZaloRepository zaloRepository,
//             IHttpClientFactory httpClientFactory,
//             IOptions<ZaloSettings> zaloOptions,
//             ILogger<ZaloKafkaConsumer> logger,
//             LogMessageRepository logMessageRepository,
//             KafkaProducerService kafkaProducer)
//         {
//             _zaloService = zaloService;
//             _zaloRepository = zaloRepository;
//             _httpClientFactory = httpClientFactory;
//             _zaloSettings = zaloOptions.Value;
//             _logger = logger;
//             _logMessageRepository = logMessageRepository;
//             _kafkaProducer = kafkaProducer;
//         }

//         public async Task StartAsync(CancellationToken cancellationToken)
//         {
//             var config = new ConsumerConfig
//             {
//                 BootstrapServers = _bootstrapServers,
//                 GroupId = "zalo-consumer-group",
//                 AutoOffsetReset = AutoOffsetReset.Earliest
//             };

//             using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
//             consumer.Subscribe(_topic);

//             _logger.LogInformation("Zalo Kafka Consumer started...");

//             while (!cancellationToken.IsCancellationRequested)
//             {
//                 try
//                 {
//                     var result = consumer.Consume(cancellationToken);
//                     // var message = JsonConvert.DeserializeObject<NotificationKafkaEnvelope>(result.Message.Value);
//                     // _logger.LogWarning("Raw Kafka message: {message}", result.Message.Value);


//                     // if (message == null || string.IsNullOrEmpty(message.Action))
//                     // {
//                     //     _logger.LogWarning("Received empty or invalid message.");
//                     //     continue;
//                     // }

//                     // await ProcessMessageAsync(JsonConvert.SerializeObject(message));
//                     var message = JsonConvert.DeserializeObject<MessageRequest>(result.Message.Value);

//                     if (message == null || string.IsNullOrEmpty(message.Headers.Action))
//                     {
//                         _logger.LogWarning("Received empty or invalid message.");
//                         continue;
//                     }

//                     await ProcessMessageAsync(message);


//                 }
//                 catch (OperationCanceledException)
//                 {
//                     _logger.LogInformation("Consumer cancelled by CancellationToken.");
//                     break;
//                 }
//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "Error processing Kafka message.");
//                 }
//             }

//             consumer.Close();
//         }

//         private async Task ProcessMessageAsync(MessageRequest message)
//         {
//             try
//             {
//                 var action = message.Headers?.Action?.ToLower() ?? "";
//                 var bodyItem = message.Body?.FirstOrDefault();

//                 if (bodyItem == null)
//                 {
//                     _logger.LogWarning("Message body is empty.");
//                     return;
//                 }

//                 var zalo = bodyItem.Zalo;
//                 if (zalo == null)
//                 {
//                     _logger.LogWarning("Zalo payload is missing.");
//                     return;
//                 }

//                 switch (action)
//                 {
//                     case "create-promotion":
//                         if (zalo.PromotionRequest == null)
//                         {
//                             _logger.LogWarning("Missing PromotionRequest.");
//                             return;
//                         }

//                         // var existing = await _zaloPromotionRepository.GetPromotionByTagAsync(zalo.PromotionRequest.Tag);
//                         // if (existing != null)
//                         // {
//                         //     string errorMsg = $"Promotion with tag '{zalo.PromotionRequest.Tag}' already exists.";
//                         //     _logger.LogWarning(errorMsg);

//                         //     await _logMessageRepository.InsertErrorAsync(new MessageErrorLog
//                         //     {
//                         //         Error = errorMsg,
//                         //         RawPayload = JsonConvert.SerializeObject(zalo),
//                         //         CreatedAt = DateTime.UtcNow
//                         //     });

//                         //     await _kafkaProducer.SendMessageAsync("topic-zalo-error", null, JsonConvert.SerializeObject(zalo));

//                         //     return;
//                         // }


//                         await _zaloRepository.InsertOrUpdateByTagAsync(zalo.PromotionRequest);
//                         break;

//                     case "send-promotion":
//                         if (string.IsNullOrEmpty(zalo.AccessToken) || string.IsNullOrEmpty(zalo.UserId))
//                         {
//                             _logger.LogWarning("Missing accessToken or userId.");
//                             return;
//                         }

//                         if (zalo.SendMessagePayload == null)
//                         {
//                             _logger.LogWarning("Missing SendMessagePayload.");
//                             return;
//                         }

//                         var promotion = await _zaloRepository.GetPromotionByTagAsync(zalo.PromotionRequest.Tag);
//                         if (promotion == null)
//                         {
//                             _logger.LogWarning("No promotion found for tag '{tag}'", zalo.PromotionRequest.Tag);
//                             return;
//                         }

//                         // await _zaloService.HandleSendPromotionAsync(promotion, zalo.AccessToken);
//                         break;

//                     default:
//                         _logger.LogWarning("Unsupported action: {action}", action);
//                         break;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error processing message.");
//             }
//         }


//         // private async Task ProcessMessageAsync(string jsonMessage)
//         // {
//         //     try
//         //     {
//         //         var envelope = JsonConvert.DeserializeObject<NotificationKafkaEnvelope>(jsonMessage);
//         //         if (envelope == null || string.IsNullOrWhiteSpace(envelope.Action))
//         //         {
//         //             _logger.LogWarning("Envelope or Action is null.");
//         //             return;
//         //         }

//         //         switch (envelope.Action.ToLower())
//         //         {
//         //             case "create-promotion":
//         //                 var createRequest = JsonConvert.DeserializeObject<ZaloPromotionRequest>(envelope.Payload);
//         //                 if (createRequest == null)
//         //                 {
//         //                     _logger.LogWarning("Invalid create-promotion payload.");
//         //                     return;
//         //                 }

//         //                 var existing = await _zaloPromotionRepository.GetPromotionByTagAsync(createRequest.Tag);
//         //                 if (existing != null)
//         //                 {
//         //                     _logger.LogWarning("Promotion with tag '{tag}' already exists.", createRequest.Tag);
//         //                     return;
//         //                 }

//         //                 await _zaloPromotionRepository.InsertOrUpdateByTagAsync(createRequest);
//         //                 _logger.LogInformation("Created promotion with tag '{tag}'", createRequest.Tag);
//         //                 break;

//         //             case "send-promotion":
//         //                 var sendPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(envelope.Payload);
//         //                 if (!sendPayload.TryGetValue("tag", out var tag) || !sendPayload.TryGetValue("accessToken", out var token))
//         //                 {
//         //                     _logger.LogWarning("Missing tag or accessToken in send-promotion.");
//         //                     return;
//         //                 }

//         //                 var promotion = await _zaloPromotionRepository.GetPromotionByTagAsync(tag);
//         //                 if (promotion == null)
//         //                 {
//         //                     _logger.LogWarning("No promotion found for tag '{tag}'", tag);
//         //                     return;
//         //                 }

//         //                 // await _zaloService.HandleSendPromotionAsync(promotion, token);
//         //                 break;

//         //             case "callback":
//         //                 var callbackRequest = JsonConvert.DeserializeObject<ZaloCallbackRequest>(envelope.Payload);
//         //                 if (callbackRequest == null)
//         //                 {
//         //                     _logger.LogWarning("Invalid callback payload.");
//         //                     return;
//         //                 }
//         //                 await _zaloService.ProcessCallback(callbackRequest);
//         //                 break;

//         //             default:
//         //                 _logger.LogWarning("Unsupported action: {action}", envelope.Action);
//         //                 break;
//         //         }
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         _logger.LogError(ex, "Error processing message.");
//         //     }
//         // }


//     }
// }
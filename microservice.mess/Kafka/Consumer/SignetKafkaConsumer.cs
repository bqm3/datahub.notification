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
//     public class SignetKafkaConsumer
//     {
//         private readonly SignetService _signetService;
//         private readonly IHttpClientFactory _httpClientFactory;
//         private readonly ILogger<SignetKafkaConsumer> _logger;
//         private readonly LogMessageRepository _logMessageRepository;
//         private readonly KafkaProducerService _kafkaProducer;
//         private readonly string _bootstrapServers = "host.docker.internal:9092";
//         private readonly string _topic = "topic-signet";

//         public SignetKafkaConsumer(
//             SignetService signetService,
//             IHttpClientFactory httpClientFactory,
//             ILogger<SignetKafkaConsumer> logger,
//             LogMessageRepository logMessageRepository,
//             KafkaProducerService kafkaProducer)
//         {
//             _signetService = signetService;
//             _httpClientFactory = httpClientFactory;
//             _logger = logger;
//             _logMessageRepository = logMessageRepository;
//             _kafkaProducer = kafkaProducer;
//         }

//         public async Task StartAsync(CancellationToken cancellationToken)
//         {
//             var config = new ConsumerConfig
//             {
//                 BootstrapServers = _bootstrapServers,
//                 GroupId = "signet-consumer-group",
//                 AutoOffsetReset = AutoOffsetReset.Earliest,
//                 EnableAutoCommit = true
//             };

//             using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
//             consumer.Subscribe(_topic);

//             while (!cancellationToken.IsCancellationRequested)
//             {
//                 var result = consumer.Consume(cancellationToken);
//                 string payload = result?.Message?.Value;
//                 try
//                 {

//                     var message = JsonConvert.DeserializeObject<MessageRequest>(payload);

//                     if (message == null || message.Headers == null || message.Body == null)
//                     {
//                         _logger.LogWarning("Envelope/Headers/Body is null.");
//                         return;
//                     }

//                     await ProcessMessageAsync(payload);
//                     consumer.Commit(result);
//                 }
//                 catch (KafkaException ex)
//                 {
//                     _logger.LogError(ex, "Kafka Commit failed for offset {0}", result.Offset);
//                 }
//                 catch (OperationCanceledException)
//                 {
//                     _logger.LogInformation("Consumer cancelled by CancellationToken.");
//                     break;
//                 }
//                 catch (Exception ex)
//                 {

//                     await _logMessageRepository.InsertErrorAsync(new MessageErrorLog
//                     {
//                         Error = ex.ToString(),
//                         RawPayload = payload,
//                         CreatedAt = DateTime.UtcNow
//                     });

//                     await _kafkaProducer.SendMessageAsync("topic-signet-error", null, payload);
//                 }

//             }

//             consumer.Close();
//         }

//         private async Task ProcessMessageAsync(string jsonMessage)
//         {
//             try
//             {
//                 var envelope = JsonConvert.DeserializeObject<MessageRequest>(jsonMessage);
//                 if (envelope == null || envelope.Headers == null || envelope.Body == null)
//                 {
//                     return;
//                 }

//                 var action = envelope.Headers.Action;

//                 // if (action?.ToLower() == "send-signet-message-log")
//                 // {
//                 //     foreach (var item in envelope.Body)
//                 //     {
//                 //         if (item.Signet != null)
//                 //         {
//                 //             var request = new SendTemplateMessageRequest
//                 //             {
//                 //                 TemplateName = item.Signet.TemplateName,
//                 //                 Data = item.Signet.Data
//                 //             };

//                 //             await _signetService.SendTemplateMessageAsync(request);
//                 //         }

//                 //     }
//                 // }
//                 // else
//                 // {
//                 //     _logger.LogWarning("Unsupported action: {action}", action);
//                 // }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error processing message.");
//             }
//         }
//     }
// }
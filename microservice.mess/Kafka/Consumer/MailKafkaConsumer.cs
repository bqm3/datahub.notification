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
//     public class MailKafkaConsumer
//     {
//         private readonly MailService _mailService;
//         private readonly IHttpClientFactory _httpClientFactory;
//         private readonly SmtpSettings _smtpSettings;
//         private readonly ILogger<MailKafkaConsumer> _logger;
//         private readonly LogMessageRepository _logMessageRepository;
//         private readonly KafkaProducerService _kafkaProducer;
//         private readonly string _bootstrapServers = "host.docker.internal:9092";
//         private readonly string _topic = "topic-mail";

//         public MailKafkaConsumer(
//             MailService mailService,
//             IHttpClientFactory httpClientFactory,
//             IOptions<SmtpSettings> stmpOptions,
//             ILogger<MailKafkaConsumer> logger,
//             LogMessageRepository logMessageRepository,
//             KafkaProducerService kafkaProducer)
//         {
//             _mailService = mailService;
//             _httpClientFactory = httpClientFactory;
//             _smtpSettings = stmpOptions.Value;
//             _logger = logger;
//             _logMessageRepository = logMessageRepository;
//             _kafkaProducer = kafkaProducer;
//         }

//         public async Task StartAsync(CancellationToken cancellationToken)
//         {
//             var config = new ConsumerConfig
//             {
//                 BootstrapServers = _bootstrapServers,
//                 GroupId = "notify-consumer-group",
//                 AutoOffsetReset = AutoOffsetReset.Earliest,
//                 EnableAutoCommit = false
//             };

//             using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
//             consumer.Subscribe(_topic);

//             while (!cancellationToken.IsCancellationRequested)
//             {
//                 var result = consumer.Consume(cancellationToken);
//                 string payload = result?.Message?.Value;
//                 try
//                 {

//                     _logger.LogInformation("Received Kafka message: {message}", payload);
//                     var message = JsonConvert.DeserializeObject<MessageRequest>(payload);

//                     if (message == null || message.Headers == null || message.Body == null)
//                     {
//                         _logger.LogWarning("Envelope/Headers/Body is null.");
//                         return;
//                     }

//                     await ProcessMessageAsync(payload);
//                     consumer.Commit(result);
//                     _logger.LogInformation("Commit Kafka offset thành công cho partition {0}, offset {1}", result.Partition, result.Offset);
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
//                     _logger.LogWarning(ex, "Xử lý message mail thất bại");

//                     await _logMessageRepository.InsertErrorAsync(new MessageErrorLog
//                     {
//                         Error = ex.ToString(),
//                         RawPayload = payload,
//                         CreatedAt = DateTime.UtcNow
//                     });

//                     await _kafkaProducer.SendMessageAsync("topic-mail-error", null, payload);
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
// _logger.LogInformation( "Xử lý message mail send-mail-tag");
//                 if (action?.ToLower() == "send-mail-tag")
//                 {
//                     foreach (var item in envelope.Body)
//                     {
//                         if (item.Email != null)
//                         {
//                             await _mailService.SendEmailAsync(item.Email);

//                         }
//                     }
//                 }
//                 else
//                 {
//                     _logger.LogWarning("Unsupported action: {action}", action);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error processing message.");
//             }
//         }
//     }
// }
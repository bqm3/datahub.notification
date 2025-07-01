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
    public class MailKafkaConsumer
    {
        private readonly MailService _mailService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<MailKafkaConsumer> _logger;

        private readonly string _bootstrapServers = "host.docker.internal:9092";
        private readonly string _topic = "topic-mail";

        private readonly SignalRService _signalRService;

        public MailKafkaConsumer(
            MailService mailService,
            SignalRService signalRService,
            IHttpClientFactory httpClientFactory,
            IOptions<SmtpSettings> stmpOptions,
            ILogger<MailKafkaConsumer> logger)
        {
            _mailService = mailService;
            _signalRService = signalRService;
            _httpClientFactory = httpClientFactory;
            _smtpSettings = stmpOptions.Value;
            _logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "mail-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            _logger.LogInformation("Mail Kafka Consumer started...");

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

                    await ProcessMessageAsync(result.Message.Value);

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
                    case "send-mail":
                        var createRequest = JsonConvert.DeserializeObject<MailPayload>(envelope.Payload);
                        if (createRequest == null)
                        {
                            _logger.LogWarning("Invalid send-mail payload.");
                            return;
                        }

                        await _mailService.SendEmailAsync(createRequest.To, createRequest.Subject, createRequest.Body);
                        await _signalRService.SendToAllAsync(JsonConvert.SerializeObject(new
                        {
                            channel = "mail",
                            status = "success",
                            to = createRequest.To,
                            subject = createRequest.Subject
                        }));

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
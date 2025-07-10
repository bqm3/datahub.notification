using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using microservice.mess.Kafka;
using microservice.mess.Services;
using microservice.mess.Repositories;
using microservice.mess.Configurations;
using System.Net.Http;

namespace microservice.mess.Kafka.Consumer
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

        public ConsumeScopedServiceHostedService(IServiceProvider serviceProvider, ILogger<ConsumeScopedServiceHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run all consumers in background
            Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;

                // // SIGNET consumer
                // try
                // {
                //     var signetConsumer = new SignetKafkaConsumer(
                //         services.GetRequiredService<SignetService>(),
                //         services.GetRequiredService<IHttpClientFactory>(),
                //         services.GetRequiredService<ILogger<SignetKafkaConsumer>>(),
                //         services.GetRequiredService<LogMessageRepository>(),
                //         services.GetRequiredService<KafkaProducerService>()
                //     );

                //     _ = signetConsumer.StartAsync(stoppingToken);
                //     _logger.LogInformation("Started SignetKafkaConsumer");
                // }
                // catch (Exception ex)
                // {
                //     _logger.LogError(ex, "SignetKafkaConsumer failed to start.");
                // }


                // // MAIL consumer
                // try
                // {
                //     var mailConsumer = new MailKafkaConsumer(
                //         services.GetRequiredService<MailService>(),
                //         services.GetRequiredService<IHttpClientFactory>(),
                //         services.GetRequiredService<IOptions<SmtpSettings>>(),
                //         services.GetRequiredService<ILogger<MailKafkaConsumer>>(),
                //         services.GetRequiredService<LogMessageRepository>(),
                //         services.GetRequiredService<KafkaProducerService>()
                //     );

                //     _ = mailConsumer.StartAsync(stoppingToken);
                //     _logger.LogInformation("Started MailKafkaConsumer");
                // }
                // catch (Exception ex)
                // {
                //     _logger.LogError(ex, "MailKafkaConsumer failed to start.");
                // }

                // // ZALO consumer
                // try
                // {
                //     var zaloConsumer = new ZaloKafkaConsumer(
                //         services.GetRequiredService<ZaloService>(),
                //         services.GetRequiredService<ZaloRepository>(),
                //         services.GetRequiredService<IHttpClientFactory>(),
                //         services.GetRequiredService<IOptions<ZaloSettings>>(),
                //         services.GetRequiredService<ILogger<ZaloKafkaConsumer>>(),
                //         services.GetRequiredService<LogMessageRepository>(),
                //         services.GetRequiredService<KafkaProducerService>()
                //     );

                //     _ = zaloConsumer.StartAsync(stoppingToken);
                //     _logger.LogInformation("Started ZaloKafkaConsumer");
                // }
                // catch (Exception ex)
                // {
                //     _logger.LogError(ex, "ZaloKafkaConsumer failed to start.");
                // }

                // NOTIFY consumer
                try
                {
                    var notifyConsumer = new NotifyKafkaConsumer(
                        services.GetRequiredService<MailService>(),
                        services.GetRequiredService<SignetService>(),
                        services.GetRequiredService<ZaloService>(),
                        services.GetRequiredService<ZaloRepository>(),
                        services.GetRequiredService<IHttpClientFactory>(),
                        services.GetRequiredService<IOptions<SmtpSettings>>(),
                        services.GetRequiredService<IOptions<ZaloSettings>>(),
                        services.GetRequiredService<ILogger<NotifyKafkaConsumer>>(),
                        services.GetRequiredService<LogMessageRepository>(),
                        services.GetRequiredService<KafkaProducerService>()
                    );

                    _ = notifyConsumer.StartAsync(stoppingToken);
                    _logger.LogInformation("Started NotifyKafkaConsumer");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "NotifyKafkaConsumer failed to start.");
                }

                // Add more consumers here if needed
            }, stoppingToken);

            return Task.CompletedTask;
        }
    }
}

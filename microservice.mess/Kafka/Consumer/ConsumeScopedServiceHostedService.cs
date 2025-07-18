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

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using microservice.mess.Kafka;
using microservice.mess.Repositories;
using microservice.mess.Services;
using microservice.mess.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace microservice.mess.Kafka
{
    public class MailKafkaConsumerHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MailKafkaConsumerHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();

                var mailService = scope.ServiceProvider.GetRequiredService<MailService>();
                var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                var smtpOptions = scope.ServiceProvider.GetRequiredService<IOptions<SmtpSettings>>();
                var signalRService = scope.ServiceProvider.GetRequiredService<SignalRService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<MailKafkaConsumer>>();

                var consumer = new MailKafkaConsumer(
                    mailService,
                    signalRService,
                    httpClientFactory,
                    smtpOptions,
                    logger
                );


                await consumer.StartAsync(stoppingToken);
            }, stoppingToken);

            return Task.CompletedTask;
        }

    }
}

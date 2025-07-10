// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using System.Threading;
// using System.Threading.Tasks;
// using microservice.mess.Kafka;
// using microservice.mess.Repositories;
// using microservice.mess.Services;
// using microservice.mess.Configurations;
// using Microsoft.Extensions.Options;
// using System.Net.Http;

// namespace microservice.mess.Kafka
// {
//     public class ZaloKafkaConsumerHostedService : BackgroundService
//     {
//         private readonly IServiceProvider _serviceProvider;

//         public ZaloKafkaConsumerHostedService(IServiceProvider serviceProvider)
//         {
//             _serviceProvider = serviceProvider;
//         }

//         protected override Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             Task.Run(async () =>
//             {
//                 using var scope = _serviceProvider.CreateScope();

//                 var zaloService = scope.ServiceProvider.GetRequiredService<ZaloService>();
//                 var zaloRepository = scope.ServiceProvider.GetRequiredService<ZaloRepository>();
//                 var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
//                 var zaloOptions = scope.ServiceProvider.GetRequiredService<IOptions<ZaloSettings>>();
//                 var logger = scope.ServiceProvider.GetRequiredService<ILogger<ZaloKafkaConsumer>>();
//                 var logMessageRepo = scope.ServiceProvider.GetRequiredService<LogMessageRepository>();
//                 var kafkaProducer = scope.ServiceProvider.GetRequiredService<KafkaProducerService>();

//                 var consumer = new ZaloKafkaConsumer(
//                     zaloService,
//                     zaloRepository,
//                     httpClientFactory,
//                     zaloOptions,
//                     logger,
//                     logMessageRepo,
//                     kafkaProducer
//                 );

//                 await consumer.StartAsync(stoppingToken);
//             }, stoppingToken);

//             return Task.CompletedTask;
//         }

//     }
// }

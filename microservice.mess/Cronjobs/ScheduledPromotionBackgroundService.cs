using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace microservice.mess.Cronjobs
{
    public class ScheduledPromotionBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ScheduledPromotionBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // using (var scope = _scopeFactory.CreateScope())
                // {
                //     var sender = scope.ServiceProvider.GetRequiredService<ScheduledPromotionSender>();
                //     await sender.RunAsync(); // logic gửi tin nhắn
                // }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}

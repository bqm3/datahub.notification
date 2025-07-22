using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Repositories;
using microservice.mess.Interfaces;
using microservice.mess.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace microservice.mess.Schedules
{
    public class AllSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AllSchedulerService> _logger;

        public AllSchedulerService(IServiceScopeFactory scopeFactory, ILogger<AllSchedulerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var repo = scope.ServiceProvider.GetRequiredService<ScheduledAllRepository>();
                var mailService = scope.ServiceProvider.GetRequiredService<MailService>();
                var zaloService = scope.ServiceProvider.GetRequiredService<ZaloService>();
                var signetService = scope.ServiceProvider.GetRequiredService<SignetService>();
                // var notifyService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

                var dueSchedules = await repo.GetDueSchedulesAsync(now);
                _logger.LogInformation("{now} Có {count} lịch tồn tại", now, dueSchedules.Count);

                foreach (var item in dueSchedules)
                {
                    if (!ShouldSendEmail(item, now)) continue;

                    await SendScheduledMessageAsync(item, now, mailService, zaloService, signetService, repo);
                }

                await Task.Delay(TimeSpan.FromSeconds(300), stoppingToken);
            }
        }

        private async Task SendScheduledMessageAsync(
            ScheduledAllModel item,
            DateTime now,
            MailService mailService,
            ZaloService zaloService,
            SignetService signetService,
            ScheduledAllRepository repo
            // NotificationService notifyService
            )
        {
            using var scope = _scopeFactory.CreateScope();

            if (string.IsNullOrWhiteSpace(item.Channel))
            {
                _logger.LogWarning("Channel không hợp lệ: {channel}", item.Channel);
                return;
            }

            if (item.ChannelStatus)
                return;

            try
            {
                var context = new ScheduleContext
{
    Schedule = item,
    Channel = item.Channel,
    Services = scope.ServiceProvider
};
                // 1. Gọi bước resolve mergeFields trước
                var resolver = scope.ServiceProvider.GetRequiredService<ResolveScheduleFieldStep>();
                await resolver.ExecuteAsync(context);

                // 2. Gọi FormatDataMailStep — sẽ tạo mailData từ mergeFields
                var formatter = scope.ServiceProvider.GetRequiredService<FormatDataMailStep>();
                await formatter.ExecuteAsync(context);

                // 2. Gọi bước gửi tương ứng
                IMessageStep resolveStep = item.Channel switch
{
    "email" => scope.ServiceProvider.GetRequiredService<SendToMailStep>(),
    // "zalo" => scope.ServiceProvider.GetRequiredService<SendToZaloStep>(),
    // "signet" => scope.ServiceProvider.GetRequiredService<SendToSignetStep>(),
    _ => throw new NotSupportedException($"Channel {item.Channel} is not supported.")
};

                await resolveStep.ExecuteAsync(context);


                item.ChannelStatus = true;

                if (item.Recurrence == "DAILY" &&
                    item.LastSentAt.HasValue &&
                    item.LastSentAt.Value.Date != now.Date)
                {
                    item.ChannelStatus = true;
                }

                item.LastSentAt = now;

                if (item.Recurrence == "ONCE")
                {
                    item.IsSent = true;
                }

                await repo.UpdateAsync(item.Id, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gửi thất bại cho {channel} - Id: {id}", item.Channel, item.Id);
            }
        }

        private static bool IsSameTime(TimeSpan now, TimeSpan target, int marginMinutes = 2)
        {
            return Math.Abs((now - target).TotalMinutes) <= marginMinutes;
        }

        private bool WasAlreadySentToday(ScheduledAllModel email, DateTime now)
        {
            if (!email.LastSentAt.HasValue) return false;

            var last = email.LastSentAt.Value;
            return last.Date == now.Date &&
                   email.TimesOfDay?.Any(t => IsSameTime(last.TimeOfDay, t)) == true;
        }

        private bool ShouldSendEmail(ScheduledAllModel email, DateTime now)
        {
            return email.Recurrence switch
            {
                "ONCE" => !email.IsSent && now >= email.ScheduledTime,

                "DAILY" => email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                           !WasAlreadySentToday(email, now),

                "WEEKLY" or "CUSTOM" =>
                    email.DaysOfWeek?.Contains(now.DayOfWeek.ToString()) == true &&
                    email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                    !WasAlreadySentToday(email, now),

                _ => false
            };
        }

    }
}
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Repositories;
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
                var notifyService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
                _logger.LogInformation("Đang kiểm tra lịch gửi lúc {now}", now);

                var dueSchedules = await repo.GetDueSchedulesAsync(now);
                _logger.LogInformation("Có {count} lịch đến hạn gửi", dueSchedules.Count);


                foreach (var item in dueSchedules)
                {
                    if (!ShouldSendEmail(item, now)) continue;

                    await SendScheduledMessageAsync(
                        item, now, mailService, zaloService, signetService, repo, notifyService);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task SendScheduledMessageAsync(
            ScheduledAllModel item,
            DateTime now,
            MailService mailService,
            ZaloService zaloService,
            SignetService signetService,
            ScheduledAllRepository repo,
            NotificationService notifyService
        )
        {
            using var scope = _scopeFactory.CreateScope();

            var stepRunner = scope.ServiceProvider.GetRequiredService<StepRunnerService>();
            foreach (var channel in item.Channels)
            {
                if (item.ChannelStatus.TryGetValue(channel.ToString(), out bool isSent) && isSent)
                    continue;

                try
                {
                    bool sent = false;
                    if (!item.Steps.TryGetValue(channel.ToString().ToLower(), out var steps) || steps.Count == 0)
                    {
                        _logger.LogWarning("Không có step cho channel {channel}", channel);
                        continue;
                    }

                    var context = new ScheduleContext
                    {
                        Schedule = item,
                        Channel = channel,
                        Services = _scopeFactory.CreateScope().ServiceProvider
                    };

                    await stepRunner.RunStepsAsync(steps, context);
                    sent = true;

                    if (sent)
                    {
                        item.ChannelStatus[channel.ToString()] = true;

                        if (item.Recurrence == RecurrenceType.Daily &&
                            item.LastSentAt.HasValue &&
                            item.LastSentAt.Value.Date != now.Date)
                        {
                            foreach (var ch in item.Channels)
                                item.ChannelStatus[ch.ToString()] = false;

                            item.ChannelStatus[channel.ToString()] = true;
                        }

                        item.LastSentAt = now;

                        if (item.Recurrence == RecurrenceType.Once &&
                            item.Channels.All(c => item.ChannelStatus.TryGetValue(c.ToString(), out bool status) && status))
                        {
                            item.IsSent = true;
                        }

                        await repo.UpdateAsync(item.Id, item);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Gửi thất bại cho {channel} - Id: {id}", channel, item.Id);
                }
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
                RecurrenceType.Once => !email.IsSent && now >= email.ScheduledTime,
                RecurrenceType.Daily => email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                                        !WasAlreadySentToday(email, now),
                RecurrenceType.Weekly or RecurrenceType.Custom =>
                    email.DaysOfWeek?.Contains(now.DayOfWeek) == true &&
                    email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                    !WasAlreadySentToday(email, now),
                _ => false
            };
        }
    }
}

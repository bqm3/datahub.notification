using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace microservice.mess.Services
{
    public class MailSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MailSchedulerService> _logger;

        public MailSchedulerService(IServiceScopeFactory scopeFactory, ILogger<MailSchedulerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<ScheduledEmailRepository>();
                var mailService = scope.ServiceProvider.GetRequiredService<MailService>();

                var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh"); // "Asia/Ho_Chi_Minh"
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

                var dueEmails = await repo.GetDueEmailsAsync(now);

                foreach (var email in dueEmails)
                {
                    try
                    {
                        if (!ShouldSendEmail(email, now)) continue;

                        await mailService.SendEmailAsync(new SendMailByTagRequest
                        {
                            Tag = email.Tag,
                            To = email.To,
                            Data = email.Data,
                            Subject = email.Subject,
                            From = email.From
                        });

                        if (email.Recurrence == RecurrenceType.Once)
                        {
                            email.IsSent = true;
                        }

                        email.LastSentAt = now;
                        await repo.UpdateAsync(email.Id, email);

                        _logger.LogInformation("Scheduled email sent: {id}", email.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send scheduled email: {id}", email.Id);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private static bool IsSameTime(TimeSpan now, TimeSpan target, int marginMinutes = 1)
        {
            return Math.Abs((now - target).TotalMinutes) <= marginMinutes;
        }

        private bool WasAlreadySentToday(ScheduledEmailModel email, DateTime now)
        {
            if (!email.LastSentAt.HasValue) return false;

            var last = email.LastSentAt.Value;

            return last.Date == now.Date &&
                   email.TimesOfDay?.Any(t => IsSameTime(last.TimeOfDay, t)) == true;
        }

        private bool ShouldSendEmail(ScheduledEmailModel email, DateTime now)
        {
            switch (email.Recurrence)
            {
                case RecurrenceType.Once:
                    return !email.IsSent && now >= email.ScheduledTime;

                case RecurrenceType.Daily:
                    return email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                           !WasAlreadySentToday(email, now);

                case RecurrenceType.Weekly:
                case RecurrenceType.Custom:
                    return email.DaysOfWeek?.Contains(now.DayOfWeek) == true &&
                           email.TimesOfDay?.Any(t => IsSameTime(now.TimeOfDay, t)) == true &&
                           !WasAlreadySentToday(email, now);
            }

            return false;
        }


    }
}

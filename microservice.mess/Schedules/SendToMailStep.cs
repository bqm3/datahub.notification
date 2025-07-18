using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Services;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace microservice.mess.Schedules
{
    public class SendToMailStep : IMessageStep
    {
        private readonly MailService _mailService;
        private readonly ILogger<SendToMailStep> _logger;

        public SendToMailStep(MailService mailService, ILogger<SendToMailStep> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            try
            {
                if (!context.Items.TryGetValue("mailData", out var mailDataObj))
                    throw new InvalidOperationException("Missing or invalid 'mailData' in context.");

                var mailData = (dynamic)mailDataObj;

                var request = new SendMailByNameRequest
                {
                    Name = context.Schedule.Name,
                    Subject = context.Schedule.Subject,
                    From = context.Schedule.From,
                    To = ((IEnumerable<string>)mailData.Receivers)?.ToList() ?? new List<string>(),
                    Data = ExtractPlaceholders(mailData.BodyHtml),
                };

                await _mailService.SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendToMailStep.ExecuteAsync");
                throw;
            }
        }


        private Dictionary<string, string> ExtractPlaceholders(string html)
        {
            var result = new Dictionary<string, string>();
            var matches = System.Text.RegularExpressions.Regex.Matches(html, "{{(.*?)}}");
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var key = match.Groups[1].Value;
                if (!result.ContainsKey(key))
                    result[key] = ""; // giá trị thực tế đã được thay thế trong BodyHtml, nên dummy tại đây
            }
            return result;
        }
    }
}

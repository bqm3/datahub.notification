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
                if (!context.Items.TryGetValue("mergeFields", out var mergeFieldsObj))
                    throw new InvalidOperationException("Missing 'mergeFields' in context.");

                if (!context.Items.TryGetValue("mailData", out var mailDataObj))
                    throw new InvalidOperationException("Missing or invalid 'mailData' in context.");

                var rawMergeFields = (Dictionary<string, object>)mergeFieldsObj;
                var mergeFields = rawMergeFields.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.ToString() ?? string.Empty
                );

                var mailData = (MailPayload)mailDataObj;

                // Áp dụng các giá trị mergeFields vào nội dung BodyHtml
                string body = ReplacePlaceholders(mailData.BodyHtml, mergeFields);

                var request = new SendMailByNameRequest
                {
                    Name = context.Schedule.Name,
                    Subject = mailData.Subject,
                    From = mailData.From ,
                    To = mailData.To ?? new List<string>(),
                    Data = mergeFields,
                    BodyHtml = body,
                    Attachments = mailData.Attachments
                };

                await _mailService.SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendToMailStep.ExecuteAsync");
                throw;
            }
        }

        private string ReplacePlaceholders(string html, Dictionary<string, string> fields)
        {
            if (string.IsNullOrEmpty(html) || fields == null)
                return html;

            foreach (var field in fields)
            {
                html = html.Replace($"{{{{{field.Key}}}}}", field.Value ?? string.Empty);
            }

            return html;
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

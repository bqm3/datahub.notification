using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace microservice.mess.Schedules
{
    public class FormatDataMailStep : IMessageStep
    {
        private readonly MailRepository _mailRepository;
        private readonly ILogger<FormatDataMailStep> _logger;

        public FormatDataMailStep(MailRepository mailRepository, ILogger<FormatDataMailStep> logger)
        {
            _mailRepository = mailRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            if (!context.Items.TryGetValue("mergeFields", out var mergeFieldsObj) || mergeFieldsObj is not Dictionary<string, object> mergeFields)
                throw new InvalidOperationException("Missing or invalid 'mergeFields' in context.");

            var now = DateTime.UtcNow.AddHours(7);

            var fileName = context.Schedule.Data?.File ?? "default.docx";
            var configPath = Path.Combine("templates/configs", Path.ChangeExtension(fileName, ".json"));
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Template config file not found", configPath);

            var configJson = await File.ReadAllTextAsync(configPath);
            var templateConfig = JsonConvert.DeserializeObject<TemplateConfig>(configJson)!;

            // Lấy template mail từ tên schedule
            var mailTemplate = await _mailRepository.GetByNameAsync(context.Schedule.Name);
            if (mailTemplate == null)
                throw new InvalidOperationException("Không tìm thấy template email theo tên schedule");

            // Build map từ placeholder gốc {{key}} => giá trị từ mergeFields
            var placeholderMap = new Dictionary<string, string>();
            foreach (var placeholder in mailTemplate.Placeholders)
            {
                var key = placeholder.Replace("{{", "").Replace("}}", "");
                var value = mergeFields.TryGetValue(key, out var val) ? val?.ToString() ?? "" : "(N/A)";
                placeholderMap[placeholder] = value;
            }

            var bodyHtml = ReplacePlaceholders(mailTemplate.BodyHtml, placeholderMap);
            var pdfPath = "Exports/Bao_cao_chi_tiet_17_07_2025.pdf"; // đường dẫn file PDF
            var fileBytes = await File.ReadAllBytesAsync(pdfPath);
            // Ghi đè hoặc tạo mới MailPayload
            if (context.Items.TryGetValue("mailData", out var existing) && existing is MailPayload existingPayload)
            {
                existingPayload.BodyHtml = ReplacePlaceholders(existingPayload.BodyHtml, placeholderMap);
                context.Items["mailData"] = existingPayload;
            }
            else
            {
                context.Items["mailData"] = new MailPayload
                {
                    Subject = mailTemplate.Subject,
                    BodyHtml = bodyHtml,
                    To = mailTemplate.Receivers,
                    // From = context.Schedule.From,
                    Attachments = new List<EmailAttachment>
                    {
                        new EmailAttachment
                        {
                            FileName = "BaoCaoThongKe.pdf",
                            Content = fileBytes,
                            ContentType = "application/pdf"
                        }
                    }
                };
            }

            _logger.LogInformation("Đã format mailData với các placeholders: {placeholders}", JsonConvert.SerializeObject(placeholderMap, Formatting.Indented));
        }


        private string ReplacePlaceholders(string template, IDictionary<string, string> placeholderMap)
        {
            foreach (var pair in placeholderMap)
            {
                template = template.Replace(pair.Key, pair.Value ?? "");
            }
            return template;
        }
    }
}
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using microservice.mess.Configurations;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Services
{
    public class MailService
    {
        private readonly SmtpSettings _smtp;
        private readonly ILogger<MailService> _logger;
        private readonly MailRepository _mailRepository;

        public MailService(IOptions<SmtpSettings> smtpOptions, ILogger<MailService> logger, MailRepository mailRepository)
        {
            _smtp = smtpOptions.Value;
            _logger = logger;
            _mailRepository = mailRepository;
        }

        public async Task SendEmailAsync(SendMailByNameRequest request)
        {
            try
            {
                var template = await _mailRepository.GetByNameAsync(request.Name);
                if (template == null)
                    throw new Exception($"Template with Name '{request.Name}' not found.");

                string renderedBody = RenderTemplate(template.BodyHtml, request.Data);
                string subject = request.Subject ?? template.Subject;

                var sender = !string.IsNullOrEmpty(request.From)
                    ? await _mailRepository.GetByEmailAsync(request.From)
                    : null;

                if (sender == null)
                    throw new Exception($"Sender account '{request.From}' not found.");

                using var client = new SmtpClient(sender.SmtpClient)
                {
                    Port = sender.Port,
                    Credentials = new NetworkCredential(sender.FromEmail, sender.Password),
                    EnableSsl = true
                };

                foreach (var toEmail in request.To)
                {
                    if (!IsValidEmail(toEmail))
                    {
                        _logger.LogWarning("Email không hợp lệ: {toEmail}", toEmail);
                        continue;
                    }

                    try
                    {
                        var message = new MailMessage
                        {
                            From = new MailAddress(sender.FromEmail, sender.DisplayName),
                            Subject = subject,
                            Body = renderedBody,
                            IsBodyHtml = true
                        };
                        if (request.Attachments != null)
                        {
                            foreach (var file in request.Attachments)
                            {
                                var stream = new MemoryStream(file.Content);
                                var attachment = new Attachment(stream, file.FileName, file.ContentType);
                                message.Attachments.Add(attachment);
                            }
                        }


                        message.To.Add(toEmail); // không cần lại tạo MailAddress
                        await client.SendMailAsync(message);

                        _logger.LogInformation("Sent email to {to} with subject: {subject}", toEmail, subject);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Lỗi khi gửi email đến {toEmail}", toEmail);
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Failed to send email to {ex}", ex);
            }
        }

        public async Task CreateTemplateAsync(AllMessageTemplate template)
        {
            try
            {
                if (template == null)
                {
                    throw new ArgumentNullException(nameof(template));
                }

                // Save the template to the database (not implemented in this example)
                await _mailRepository.CreateAsync(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Failed to create email template: {name}", template.Name);
            }
        }

        private string RenderTemplate(string template, Dictionary<string, string> data)
        {
            foreach (var kv in data)
            {
                template = template.Replace($"{{{{{kv.Key}}}}}", kv.Value);
            }
            return template;
        }


        private static bool IsValidEmail(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }
    }
}

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using microservice.mess.Configurations;

namespace microservice.mess.Services
{
    public class MailService
    {
        private readonly SmtpSettings _smtp;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<SmtpSettings> smtpOptions, ILogger<MailService> logger)
        {
            _smtp = smtpOptions.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtp.StmpClient)
            {
                Port = _smtp.Port,
                Credentials = new NetworkCredential(_smtp.From, _smtp.Password),
                EnableSsl = true
            };

            var mail = new MailMessage(_smtp.From, to, subject, body);
            await client.SendMailAsync(mail);
        }
    }
}

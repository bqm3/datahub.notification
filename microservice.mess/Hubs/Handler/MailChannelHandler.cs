using microservice.mess.Services;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace microservice.mess.Hubs.Handler
{
    
     public class MailChannelHandler : INotificationChannelHandler
    {
        private readonly MailService _emailService;

        public string ChannelName => "mail";

        public MailChannelHandler(MailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(string action, string payload, HubCallerContext context, IClientProxy caller)
        {
            try
            {
                var mailPayload = JsonSerializer.Deserialize<MailPayload>(payload);
                if (mailPayload != null)
                {
                    await _emailService.SendEmailAsync(mailPayload.To, mailPayload.Subject, mailPayload.Body);
                    await caller.SendAsync("MessageSent", $"Email đã gửi đến {mailPayload.To}");
                }
            }
            catch (Exception ex)
            {
                await caller.SendAsync("MessageError", $"Lỗi gửi mail: {ex.Message}");
            }
        }
    }
}
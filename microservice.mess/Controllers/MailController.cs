using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using microservice.mess.Services;
using microservice.mess.Hubs;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly MailService _mail;

        public MailController(IHubContext<NotificationHub> hubContext, MailService mail)
        {
            _hubContext = hubContext;
            _mail = mail;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(
            [FromQuery] string message,
            [FromQuery] string subject,
            [FromQuery] string email)
        {

            // Gửi mail
            await _mail.SendEmailAsync(email, subject, message);

            return Ok(new { success = true, message });
        }

    }
}
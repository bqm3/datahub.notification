using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using microservice.mess.Services;
using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Hubs;
using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly MailRepository _repo;
        private readonly ScheduledEmailRepository _scheduledEmailRepo;
        private readonly MailService _mail;

        public MailController(IHubContext<NotificationHub> hubContext, MailService mail, MailRepository repo, ScheduledEmailRepository scheduledEmailRepo)
        {
            _hubContext = hubContext;
            _mail = mail;
            _repo = repo;
            _scheduledEmailRepo = scheduledEmailRepo;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(
           [FromBody] SendMailByNameRequest request)
        {
            if (request == null || request.To == null || request.To.Count == 0)
            {
                return BadRequest("Invalid request");
            }
            // Gửi mail
            await _mail.SendEmailAsync(request);

            return Ok(new { success = true, message = "Email sent successfully" });
        }

        [HttpPost("sender-account")]
        public async Task<IActionResult> CreateSenderAccount([FromBody] UserAccountModel model)
        {
            await _repo.CreateAccountAsync(model);
            return Ok(new { success = true, message = "Sender account created" });
        }


        [HttpPost("send-by-name")]
        public async Task<IActionResult> SendEmailByName([FromBody] SendMailByNameRequest request)
        {
            await _mail.SendEmailAsync(request);
            return Ok(new { success = true, message = "Email sent successfully" });
        }


        [HttpPost("template")]
        public async Task<IActionResult> CreateTemplate([FromBody] AllMessageTemplate model)
        {
            await _repo.CreateAsync(model);
            return Ok(new { success = true, message = "Created successfully", model.Id });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllTemplates()
        {
            var list = await _repo.GetAllAsync();
            return Ok(new { success = true, data = list });
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetTemplateByName(string name)
        {
            var template = await _repo.GetByNameAsync(name);
            if (template == null) return NotFound();
            return Ok(new { success = true, data = template });
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleEmail([FromBody] ScheduledEmailModel model)
        {
            await _scheduledEmailRepo.AddAsync(model);
            return Ok(new { message = "Create email scheduled", scheduledFor = model.ScheduledTime });
        }

        [HttpPut("schedule/{id}")]
        public async Task<IActionResult> ScheduleEmail(string id, [FromBody] ScheduledEmailModel model)
        {
            await _scheduledEmailRepo.UpdateAsync(id, model);
            return Ok(new { message = "Update email scheduled", scheduledFor = model.ScheduledTime });
        }
        
        [HttpGet("schedule/list")]
        public async Task<IActionResult> ListScheduleEmail([FromBody] ScheduledEmailModel model)
        {
            var scheduledEmails = await _scheduledEmailRepo.GetAllScheduledEmailsAsync();
            return Ok(new { success = true, data = scheduledEmails });
        }

    }
}
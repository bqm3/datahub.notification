using Microsoft.AspNetCore.Mvc;
using microservice.mess.Models;
using microservice.mess.Services;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly SignalRService _signalRService;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(SignalRService signalRService, ILogger<NotificationsController> logger)
        {
            _signalRService = signalRService;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("Message cannot be empty.");

            string channel = "signalR";

            // if (!string.IsNullOrEmpty(request.UserId))
            //     await _signalRService.SendToUserAsync(request.UserId, request.Message);
            // else
            await _signalRService.SendToAllAsync(request.Message);

            // Ghi log vào MongoDB
            var log = new NotificationLog
            {
                UserId = request.UserId,
                Message = request.Message,
                Channel = channel
            };
            // await _logRepository.InsertLogAsync(log); 

            return Ok(new { success = true, request });
        }

    }
}

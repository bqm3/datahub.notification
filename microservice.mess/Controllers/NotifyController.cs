using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using microservice.mess.Models.Message;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;

        public NotifyController(ILogger<NotifyController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] MessageRequest request)
        {
            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://c740-101-99-6-230.ngrok-free.app/notificationHub")
                    .WithAutomaticReconnect()
                    .Build();

                await connection.StartAsync();

                await connection.InvokeAsync("DispatchMessage", request);

                await connection.StopAsync();

                return Ok(new { message = "Sent to Hub successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to SignalR Hub");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
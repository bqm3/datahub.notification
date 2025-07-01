using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageRelayController : ControllerBase
    {
        private readonly ILogger<MessageRelayController> _logger;

        public MessageRelayController(ILogger<MessageRelayController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send-to-hub")]
        public async Task<IActionResult> SendToHub([FromBody] MessageRequest request)
        {
            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7225/notificationHub")  // URL của Hub
                    .Build();

                await connection.StartAsync();

                await connection.InvokeAsync("DispatchMessage", request);

                await connection.StopAsync();

                return Ok("Message đã được gửi tới SignalR Hub");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi tới Hub");
                return StatusCode(500, "Gửi tới Hub thất bại");
            }
        }
    }
}

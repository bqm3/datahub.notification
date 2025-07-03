// Controllers/NotifyController.cs
using Microsoft.AspNetCore.Mvc;
using microservice.mess.Models.Message;
using microservice.mess.Interfaces;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly INotificationDispatcher _dispatcher;

        public NotifyController(ILogger<NotifyController> logger, INotificationDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] MessageRequest request)
        {
            try
            {
                var headers = await _dispatcher.DispatchMessageAsync(request);

                if (headers == null)
                    return BadRequest("Dữ liệu không hợp lệ.");

                return Ok(new
                {
                    message = "Message enqueued successfully.",
                    code = 200,
                    headers
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý message");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}

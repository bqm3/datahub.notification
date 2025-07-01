using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using microservice.mess.Services;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("slack")]
    public class SlackController : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendToSlack([FromBody] string msg, [FromServices] SlackService slack)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return BadRequest(new { success = false, message = "Message is empty." });

            await slack.SendMessageAsync(msg);
            return Ok(new { success = true, message = msg });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("api/signet")]
public class SgiWebhookController : ControllerBase
{
    [HttpPost("on-command")]
    public IActionResult TestWebhook([FromBody] JObject payload)
    {
        // // Log payload nếu cần
        // Console.WriteLine(" Webhook received: " + payload.ToString());

        // Trả về phản hồi 
        return Ok(new
        {
            status = "received",
            received_at = DateTime.UtcNow,
            payload = payload
        });
    }
}

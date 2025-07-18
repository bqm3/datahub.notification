using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using microservice.mess.Services;
using microservice.mess.Repositories;
using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Documents;

namespace microservice.mess.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ScheduledAllRepository _scheduledAllRepo;

        public ScheduleController(
            ILogger<ScheduleController> logger,
            IHttpClientFactory httpClientFactory,
            ScheduledAllRepository scheduledAllRepo)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scheduledAllRepo = scheduledAllRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduledAllModel model)
        {
            await _scheduledAllRepo.AddOrUpdateAsync(model);
            return Ok(ApiResponse<ScheduledAllModel>.SuccessResponse(model, "Tạo lịch thành công"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(string id, [FromBody] ScheduledAllModel model)
        {
            await _scheduledAllRepo.UpdateAsync(id, model);
            return Ok(ApiResponse<ScheduledAllModel>.SuccessResponse(model, "Cập nhật lịch thành công"));
        }
        
        [HttpGet("list")]
        public async Task<IActionResult> ListSchedule()
        {
            var scheduledEmails = await _scheduledAllRepo.GetAllScheduledAllAsync();
            return Ok(ApiResponse<List<ScheduledAllModel>>.SuccessResponse(scheduledEmails, "Danh sách lịch"));
        }
    }
}
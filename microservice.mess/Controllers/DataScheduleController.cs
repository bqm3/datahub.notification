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
    public class DataScheduleController : ControllerBase
    {
        private readonly ILogger<DataScheduleController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ScheduleQueryRepository _scheduledDataRepo;

        public DataScheduleController(
            ILogger<DataScheduleController> logger,
            IHttpClientFactory httpClientFactory,
            ScheduleQueryRepository scheduledAllRepo)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scheduledDataRepo = scheduledAllRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDataSchedule([FromBody] DataScheduleModel model)
        {
            await _scheduledDataRepo.CreateDataScheduleAsync(model);
            return Ok(ApiResponse<DataScheduleModel>.SuccessResponse(model, "Tạo data schedule thành công"));
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateSchedule(string id, [FromBody] DataScheduleModel model)
        // {
        //     await _scheduledDataRepo.UpdateAsync(id, model);
        //     return Ok(ApiResponse<DataScheduleModel>.SuccessResponse(model, "Cập nhật lịch thành công"));
        // }
        
        [HttpGet("list")]
        public async Task<IActionResult> ListSchedule()
        {
            var scheduledEmails = await _scheduledDataRepo.GetAllScheduledAsync();
            return Ok(ApiResponse<List<DataScheduleModel>>.SuccessResponse(scheduledEmails, "Danh sách data schedule"));
        }
    }
}
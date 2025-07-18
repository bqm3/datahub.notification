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
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<TemplateController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ScheduledAllRepository _scheduledAllRepo;
        private readonly SignetService _signetService;

        public TemplateController(ILogger<TemplateController> logger, ScheduledAllRepository scheduledAllRepository, IHttpClientFactory httpClientFactory, SignetService signetService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scheduledAllRepo = scheduledAllRepository;
            _signetService = signetService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] AllMessageTemplate request)
        {
            
             try
            {
                var result = await _signetService.SaveTemplateAsync(request);

                return Ok(ApiResponse<AllMessageTemplate>.SuccessResponse(request, "Upload file thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
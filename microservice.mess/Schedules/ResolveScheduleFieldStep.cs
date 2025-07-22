using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace microservice.mess.Schedules
{
    public class ResolveScheduleFieldStep : IMessageStep
    {
        private readonly ScheduleFieldQueryService _queryService;
        private readonly ILogger<ResolveScheduleFieldStep> _logger;

        public ResolveScheduleFieldStep(ScheduleFieldQueryService queryService, ILogger<ResolveScheduleFieldStep> logger)
        {
            _queryService = queryService;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            var result = await _queryService.ResolveQueryFieldsAndExecuteAsync(context.Schedule.Id, context);

            // Gán mergeFields vào context
            context.Items["mergeFields"] = result;
        }

    }
}

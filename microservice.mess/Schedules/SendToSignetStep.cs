
using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Services;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
// using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;


namespace microservice.mess.Schedules
{
    public class SendToSignetStep : IMessageStep
    {
        private readonly SignetService _signetService;
        private readonly ILogger<SendToSignetStep> _logger;

        public SendToSignetStep(SignetService signetService, ILogger<SendToSignetStep> logger)
        {
            _signetService = signetService;
            _logger = logger;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            var signetData = (Dictionary<string, string>)context.Items["signetData"];


            var fileHash = context.Items["fileHash"] as string;
            if (fileHash != null)
            {
                signetData["file_view"] = fileHash;
            }

            var result = await _signetService.SendTemplateMessageAsync(new SendTemplateMessageRequest
            {
                TemplateName = context.Schedule.Name,
                Data = signetData
            });



            if (!result.Success)
                throw new Exception("❌ Gửi thất bại: " + result.Message);

            _logger.LogInformation("✅ [SendToSignetStep] Message sent to Signet");
        }
    }

}
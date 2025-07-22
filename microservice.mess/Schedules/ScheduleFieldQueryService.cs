using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Services;
using microservice.mess.Repositories;
using microservice.mess.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Newtonsoft.Json;

namespace microservice.mess.Schedules
{
    public class ScheduleFieldQueryService
    {
        private readonly IScheduleFieldQueryRepository _repo;
        private readonly MongoQueryExecutor _mongoExecutor;
        private readonly ScheduleQueryRepository _allScheduled;
        private readonly MailRepository _mailRepository;
        private readonly SignetService _signetService;
        private readonly MailService _mailService;
        private readonly ILogger<ScheduleFieldQueryService> _logger;

        public ScheduleFieldQueryService(
            IScheduleFieldQueryRepository repo,
            MongoQueryExecutor mongoExecutor,
            MailRepository mailRepository,
            SignetService signetService,
            ScheduleQueryRepository scheduleQueryRepository,
            MailService mailService,
            ILogger<ScheduleFieldQueryService> logger)
        {
            _repo = repo;
            _mongoExecutor = mongoExecutor;
            _mailRepository = mailRepository;
            _allScheduled = scheduleQueryRepository;
            _signetService = signetService;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>> ResolveQueryFieldsAndExecuteAsync(string objId, ScheduleContext context)
        {
            var configs = await _repo.GetByObjIdAsync(objId);
            var result = new Dictionary<string, object>();

            Console.WriteLine("context.Schedule.Name" + context.Schedule.Name);
            var allTemplate = await _allScheduled.GetByNameAsync(context.Schedule.Name);

            _logger.LogInformation("Data temaplate", JsonConvert.SerializeObject(allTemplate));

            if (allTemplate?.Type == null)
            {
                _logger.LogWarning(" Template không có Type");
                return result;
            }

            var channelType = allTemplate.Type.ToLower();

            switch (channelType)
            {
                case "signet":
                case "zalo":
                case "email":
                    foreach (var config in configs)
                    {
                        _logger.LogInformation("→ Đang xử lý field: {key}, type: {type}, query: {query}, value: {value}",
                            config.Key, config.QueryType, config.Query, config.Value);

                        try
                        {
                            switch (config.QueryType?.ToLower())
                            {
                                case "system":
                                    result[config.Key] = HandleSystem(config.Query);
                                    break;

                                case "value":
                                    result[config.Key] = config.Value;
                                    break;

                                case "mongo":
                                    if (channelType is "signet" or "zalo")
                                    {
                                        var firstReceiver = allTemplate.Receivers?.FirstOrDefault();
                                        // if (!string.IsNullOrEmpty(firstReceiver))
                                        // {
                                        //     context.Schedule.To = new List<string> { firstReceiver };
                                        //     var value = await _mongoExecutor.ExecuteAsync(config.Query, context.Schedule.Data, context);
                                        //     result[config.Key] = value;
                                        // }
                                        // else
                                        // {
                                        //     _logger.LogWarning("Không có receiver nào cho mongo query của field {key}", config.Key);
                                        // }
                                    }
                                    else
                                    {
                                        _logger.LogInformation("Bỏ qua mongo query vì channel là {channel}", channelType);
                                    }
                                    break;

                                default:
                                    _logger.LogWarning("QueryType chưa được hỗ trợ: {type}", config.QueryType);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Lỗi xử lý field {key} với type {type}", config.Key, config.QueryType);
                        }
                    }
                    break;

                default:
                    _logger.LogWarning("Channel không được hỗ trợ: {channelType}", channelType);
                    break;
            }

            return result;
        }


        private object HandleSystem(string query)
        {
            return query.ToLower() switch
            {
                "today" => DateTime.Now.ToString("dd/MM/yyyy"),
                "now" => DateTime.Now,
                _ => query
            };
        }
    }
}

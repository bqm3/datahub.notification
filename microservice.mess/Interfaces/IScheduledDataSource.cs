using microservice.mess.Models.Message;
using microservice.mess.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace microservice.mess.Interfaces
{
    public interface IScheduledDataSource
    {
        Task<Dictionary<string, string>> GetDataAsync(string dataSourceKey);
        Task<Dictionary<string, string>> ResolveAsync(ScheduledAllModel model);
        Task ExecuteAsync(ScheduleContext context);
    }

}
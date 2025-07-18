using microservice.mess.Models.Message;
using microservice.mess.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace microservice.mess.Interfaces
{
    public interface IMessageStep
    {
        Task ExecuteAsync(ScheduleContext context);
    }
}
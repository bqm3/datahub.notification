using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace microservice.mess.Interfaces
{
     public interface INotificationChannelHandler
    {
        string ChannelName { get; }
        Task HandleAsync(string action, string payload, HubCallerContext context, IClientProxy caller);
    }
}
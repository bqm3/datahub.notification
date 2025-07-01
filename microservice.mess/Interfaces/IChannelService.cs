using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace microservice.mess.Interfaces
{
    public interface IChannelService
    {
        Task SendAsync(string payload);
    }
}
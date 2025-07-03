using Microsoft.AspNetCore.SignalR;
using microservice.mess.Models.Message;
using microservice.mess.Kafka;
using microservice.mess.Interfaces;
using System.Text.Json;

namespace microservice.mess.Hubs
{
    public class NotificationHub : Hub
{
    private readonly INotificationDispatcher _dispatcher;

    public NotificationHub(INotificationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task DispatchMessage(MessageRequest request)
    {
        await _dispatcher.DispatchMessageAsync(request, Clients.Caller);
    }

    public async Task SendToAll(string message)
    {
        await Clients.All.SendAsync("DispatchMessage", message);
    }

    public async Task SendToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync("DispatchMessage", message);
    }
}
}

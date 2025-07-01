using Microsoft.AspNetCore.SignalR;
using microservice.mess.Hubs;

namespace microservice.mess.Services
{
    public class SignalRService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<SignalRService> _logger;

        public SignalRService(IHubContext<NotificationHub> hubContext, ILogger<SignalRService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendToAllAsync(string message)
        {
            try
            {
                _logger.LogInformation("Sending SignalR message: {Message}", message);
                await _hubContext.Clients.All.SendAsync("DispatchMessage", message);
                _logger.LogInformation("SignalR message sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SignalR message.");
            }
        }
    }

}

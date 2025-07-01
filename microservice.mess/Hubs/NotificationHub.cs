using Microsoft.AspNetCore.SignalR;
using microservice.mess.Models.Message;
using microservice.mess.Kafka;
using microservice.mess.Interfaces;
using System.Text.Json;

namespace microservice.mess.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly KafkaProducerService _kafkaProducer;
        private readonly IEnumerable<INotificationChannelHandler> _channelHandlers;


        public NotificationHub(KafkaProducerService kafkaProducer, IEnumerable<INotificationChannelHandler> channelHandlers)
        {
            _kafkaProducer = kafkaProducer;
            _channelHandlers = channelHandlers;
        }

        public async Task SendToAll(string message)
        {
            await Clients.All.SendAsync("DispatchMessage", message);
        }

        public async Task SendToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("DispatchMessage", message);
        }

        public async Task DispatchMessage(MessageRequest request)
        {
            if (request?.Channels == null || string.IsNullOrWhiteSpace(request.Payload) || string.IsNullOrWhiteSpace(request.Action))
                return;

            var envelope = new
            {
                Action = request.Action,
                Payload = request.Payload
            };

            var serializedMessage = JsonSerializer.Serialize(envelope);

            foreach (var channel in request.Channels)
            {
                var handler = _channelHandlers.FirstOrDefault(h => h.ChannelName.Equals(channel, StringComparison.OrdinalIgnoreCase));

                string topic = channel.ToLower() switch
                {
                    "mail" => "topic-mail",
                    "zalo" => "topic-zalo",
                    "sms" => "topic-sms",
                    "slack" => "topic-slack",
                    "firebase" => "topic-firebase",
                    "telegram" => "topic-telegram",
                    "signet" => "topic-signet",
                    "sigr" => null,
                    _ => "topic-unknown"
                };

                if (channel.ToLower() == "sigr")
                {
                    await Clients.All.SendAsync("DispatchMessage", request.Payload);
                }
               

                else if (!string.IsNullOrWhiteSpace(topic))
                {
                    await _kafkaProducer.SendMessageAsync(topic, null, serializedMessage);
                }
            }

            await Clients.Caller.SendAsync("MessageSent", $"Đã gửi action `{request.Action}` đến {string.Join(", ", request.Channels)}");
        }
    }
}

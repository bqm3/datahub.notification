using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace microservice.mess.Services
{
    public class TelegramService
    {
        private readonly string _botToken = "7249001853:AAHzvHwEBHKKPy6dB81pPub0nCSXQPobZs0";
        private readonly HttpClient _httpClient;
        private readonly ILogger<TelegramService> _logger;

        public TelegramService(HttpClient httpClient, ILogger<TelegramService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SendMessageAsync(string message)
        {
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
            var data = new
            {
                chat_id = 1759811726,
                text = message,
                parse_mode = "HTML"
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Telegram API error: {StatusCode} - {Content}", response.StatusCode, responseContent);
            }
            response.EnsureSuccessStatusCode();

        }
    }
}

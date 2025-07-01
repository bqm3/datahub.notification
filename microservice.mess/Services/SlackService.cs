using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace microservice.mess.Services
{
    public class SlackService
{
    private readonly HttpClient _httpClient;
    private readonly string _webhookUrl;

    public SlackService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _webhookUrl = configuration["Slack:WebhookUrl"]; // lưu trong appsettings.json
    }

    public async Task SendMessageAsync(string message)
    {
        var payload = new
        {
            text = message
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_webhookUrl, content);
        response.EnsureSuccessStatusCode();
    }
}

}
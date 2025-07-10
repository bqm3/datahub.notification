using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;

namespace PROJECT.BASE.SERVICES
{
    public class SlackService : ISlackService
    {
        private HttpClient _httpClient;
        private SlackOption _slackOption;
        public SlackService(HttpClient httpClient, IOptions<SlackOption> options)
        {
            _slackOption = options.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri( _slackOption.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<string> SendMessageAsync(SendSlackMessageRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _slackOption.SendMessageEndpoint);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request,new JsonSerializerOptions(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase}), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode) return null;

            var responseText = await httpResponse.Content.ReadAsStringAsync(); 

            return responseText;
        }
    }
}
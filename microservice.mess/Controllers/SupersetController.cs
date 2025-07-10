using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("v1/custom")]
[Produces("application/json")]
public class SupersetController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SupersetController(IHttpClientFactory factory, IConfiguration configuration)
    {
        _httpClient = factory.CreateClient();
        _configuration = configuration;
    }

    [HttpPost("guest-token")]
    [Consumes("application/json")]
    public async Task<IActionResult> GetGuestToken([FromBody] Dictionary<string, object> payload)
    {
        try
        {
            var accessToken = await FetchAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized(new { error = "Unable to fetch access token" });
            }

            var guestToken = await FetchGuestToken(payload, accessToken);
            return Ok(new { token = guestToken });
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.ToString());

            return StatusCode(500, new { error = ex.Message });
        }
    }


    [HttpPost("screenshot")]
    public async Task<IActionResult> Screenshot()
    {
        var supersetApi = _configuration["SupersetAPI"]; 
        var accessToken = await FetchAccessToken();

        // Tạo body đúng định dạng JSON yêu cầu
        var body = new
        {
            url = "https://superset.apollo.dev/superset/dashboard/009c667a-9763-450a-b452-bf00cfad883a/",
            window_size = new { width = 1600, height = 800 },
            thumb_size = new { width = 800, height = 400 }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://superset.apollo.dev/api/v1/report/screenshot");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, responseBody);

        return Content(responseBody, "application/json");
    }



    private async Task<string> FetchAccessToken()
    {
        var supersetApi = _configuration["SupersetAPI"] ?? "https://superset.apollo.dev/";
        var url = $"{supersetApi.TrimEnd('/')}/api/v1/security/login";

        var body = new
        {
            username = "guest",
            password = "guest@123",
            provider = "db",
            refresh = true
        };

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var responseBody = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
        return result?["access_token"]?.ToString() ?? null;
    }

    private async Task<string> FetchGuestToken(Dictionary<string, object> payload, string accessToken)
    {
        var supersetApi = _configuration["SupersetAPI"] ?? "https://superset.apollo.dev/";
        var url = $"{supersetApi.TrimEnd('/')}/api/v1/security/guest_token/";

        var json = JsonConvert.SerializeObject(payload);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Superset error: {response.StatusCode} - {responseBody}");
        }

        var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

        if (result != null && result.TryGetValue("token", out var tokenObj))
        {
            return tokenObj?.ToString();
        }

        throw new Exception("The key 'token' was not present in the response from Superset.");
    }

}

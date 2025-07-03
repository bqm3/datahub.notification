using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using microservice.mess.Configurations;
using microservice.mess.Models;
using microservice.mess.Helpers;
using System;

namespace microservice.mess.Controllers
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }

    [ApiController]
    [Route("zalo-auth")]
    public class ZaloAuthorizeController : ControllerBase
    {
        private readonly ZaloSettings _zaloSettings;
        private readonly ILogger<ZaloAuthorizeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ZaloAuthorizeController(IHttpClientFactory httpClientFactory, ILogger<ZaloAuthorizeController> logger, IConfiguration configuration, IOptions<ZaloSettings> zaloOptions)
        {
            _zaloSettings = zaloOptions.Value;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AuthorizeZalo()
        {
            var state = _configuration["AppSettings:ZaloCodeState"];
            var codeVerifier = PkceHelper.GenerateCodeVerifier(); // tạo chuỗi ngẫu nhiên
            var codeChallenge = PkceHelper.GenerateCodeChallenge(codeVerifier); // SHA256 + base64url

            

            _logger.LogWarning("code Verifirer {Verifier}", codeVerifier);
            _logger.LogWarning("codeChallenge {Challenge}", codeChallenge);

            //  Lưu vào Session
            HttpContext.Session.SetString("zalo_code_verifier", codeVerifier);

            var redirectUri = Uri.EscapeDataString(_zaloSettings.redirect_uri);
            var url = $"https://oauth.zaloapp.com/v4/oa/permission?app_id={_zaloSettings.app_id}&redirect_uri={redirectUri}&state={state}&code_challenge={codeChallenge}&code_challenge_method=S256";

            return Redirect(url);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> AccessTokenOA([FromBody] RefreshTokenRequest request)

        {
            try
            {
                var url = "https://oauth.zaloapp.com/v4/oa/access_token";

                var client = _httpClientFactory.CreateClient();

                // Lấy từ cấu hình
                var secretKey = _configuration["Zalo:SecretKey"];
                var appId = _configuration["Zalo:AppId"];
                var grantType = "refresh_token";

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("refresh_token", request.RefreshToken),
                    new KeyValuePair<string, string>("app_id", appId),
                    new KeyValuePair<string, string>("grant_type", grantType),
                });

                client.DefaultRequestHeaders.Add("secret_key", secretKey);

                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                return Content(responseBody, "application/json");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Refresh token failed");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

    }
}

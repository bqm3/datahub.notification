using microservice.mess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Configurations;
using System.Threading.Tasks;
using System.Text.Json;

namespace microservice.mess.Repositories
{
    public class TokenRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TokenRepository> _logger;
        private readonly IMongoCollection<ZaloToken> _collection;
        private readonly IConfiguration _configuration;
        private readonly string _appId;

        public TokenRepository(
            IMongoClient mongoClient,
            ILogger<TokenRepository> logger,
            IOptions<MongoSettings> settings,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IOptions<AppSettings> appSettings)
        {
            var db = mongoClient.GetDatabase(settings.Value.ZaloDatabase);
            _collection = db.GetCollection<ZaloToken>("zalo_tokens");

            _httpClientFactory = httpClientFactory;
            _appId = appSettings.Value.AppId;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SaveOrUpdateToken(ZaloToken token)
        {
            var filter = Builders<ZaloToken>.Filter.Eq(t => t.OAID, token.OAID);
            var update = Builders<ZaloToken>.Update
                .Set(t => t.AccessToken, token.AccessToken)
                .Set(t => t.RefreshToken, token.RefreshToken)
                .Set(t => t.ExpiredAt, token.ExpiredAt);

            var options = new UpdateOptions { IsUpsert = true };
            await _collection.UpdateOneAsync(filter, update, options);
        }

        public async Task<ZaloToken?> GetByUserId(string userId)
        {
            return await _collection.Find(t => t.OAID == userId).FirstOrDefaultAsync();
        }

        public async Task<string> GetValidAccessTokenAsync(string oaId)
        {
            var token = await GetByUserId(oaId);
            if (token == null) throw new Exception("Chưa có token");

            if (token.ExpiredAt <= DateTime.UtcNow.AddMinutes(5))
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(
                    $"https://oauth.zalo.me/oauth/v4/refresh_token?app_id={_appId}&grant_type=refresh_token&refresh_token={token.RefreshToken}"
                );

                var content = await response.Content.ReadAsStringAsync();
                var newToken = JsonSerializer.Deserialize<ZaloTokenResponse>(content);

                if (!string.IsNullOrEmpty(newToken?.access_token))
                {
                    token.AccessToken = newToken.access_token;
                    token.RefreshToken = newToken.refresh_token;
                    token.ExpiredAt = DateTime.UtcNow.AddSeconds(double.Parse(newToken.expires_in));

                    await SaveOrUpdateToken(token);
                }
            }

            return token.AccessToken!;
        }

        public async Task<ZaloToken?> GetLatestTokenAsync()
        {
            return await _collection.Find(_ => true)
                .SortByDescending(t => t.ExpiredAt)
                .FirstOrDefaultAsync();
        }

        public async Task<ZaloToken?> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var url = "https://oauth.zaloapp.com/v4/oa/access_token";
                var client = _httpClientFactory.CreateClient();

                var secretKey = _configuration["Zalo:SecretKey"];
                var appId = _configuration["Zalo:AppId"];

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("app_id", appId),
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                });

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("secret_key", secretKey);

                var response = await client.PostAsync(url, content);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Zalo refresh failed: {Body}", body);
                    return null;
                }

                _logger.LogError(" Response từ Zalo khi gọi refresh: {Body}", body);


                var json = JsonSerializer.Deserialize<JsonElement>(body);
                var accessToken = json.GetProperty("access_token").GetString();
                var refreshTokenNew = json.GetProperty("refresh_token").GetString();
                var expiresIn = json.GetProperty("expires_in").GetInt32();

                var newToken = new ZaloToken
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshTokenNew,
                    ExpiredAt = DateTime.UtcNow.AddSeconds(expiresIn),
                    OAID = "4606516489169499369"
                };

                await _collection.InsertOneAsync(newToken);
                return newToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in RefreshTokenAsync");
                return null;
            }
        }

    }
}

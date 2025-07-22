using microservice.mess.Models;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Configurations;
using Lib.Setting;
using Lib.Utility;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Lib.Setting;
using Lib.Utility;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice.mess.Repositories
{
    public class ZaloRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private readonly ILogger<ZaloRepository> _logger;
        private readonly IMongoCollection<ZaloToken> _zaloTokenCollection;
        private readonly IMongoCollection<ZaloEvent> _zaloEventCollection;
        private readonly IMongoCollection<ZaloPromotionBson> _zaloPromotionCollection;
        private readonly IMongoCollection<ZaloMember> _zaloMemberCollection;
        private readonly IConfiguration _configuration;
        private readonly string _appId;

        public ZaloRepository(
            IMongoClient mongoClient,
            HttpClient httpClient,
            ILogger<ZaloRepository> logger,
            IOptions<MongoSettings> settings,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IOptions<AppSettings> appSettings)
        {
            var db = mongoClient.GetDatabase(settings.Value.ZaloDatabase);
            _zaloTokenCollection = db.GetCollection<ZaloToken>("zalo_tokens");
            _zaloPromotionCollection = db.GetCollection<ZaloPromotionBson>("zalo_promotions");
            _zaloMemberCollection = db.GetCollection<ZaloMember>("zalo_members");
            _zaloEventCollection = db.GetCollection<ZaloEvent>("zalo_events");

            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
            _appId = appSettings.Value.AppId;
            _logger = logger;
            _configuration = configuration;
        }

        #region Zalo Event
        public async Task InsertEventAsync(ZaloEvent ev)
        {
            await _zaloEventCollection.InsertOneAsync(ev);
        }

        #endregion

        #region Zalo Member
        public async Task UpsertUserAsync(string userId, string status, string eventName)
        {
            var filter = Builders<ZaloMember>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<ZaloMember>.Update
                .Set(u => u.Status, status)
                .Set(u => u.EventName, eventName)
                .Set(u => u.LastUpdated, DateTime.UtcNow);

            await _zaloMemberCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task<List<string>> ListUserAsync()
        {
            var filter = Builders<ZaloMember>.Filter.Empty; // Lấy tất cả
            var projection = Builders<ZaloMember>.Projection.Include(x => x.UserId).Exclude("_id");

            var users = await _zaloMemberCollection
                .Find(filter)
                .Project<ZaloMember>(projection)
                .ToListAsync();

            return users.Select(u => u.UserId).ToList();
        }
        #endregion


        #region Zalo Token
        public async Task SaveOrUpdateToken(ZaloToken token)
        {
            var filter = Builders<ZaloToken>.Filter.Eq(t => t.OAID, token.OAID);
            var update = Builders<ZaloToken>.Update
                .Set(t => t.AccessToken, token.AccessToken)
                .Set(t => t.RefreshToken, token.RefreshToken)
                .Set(t => t.ExpiredAt, token.ExpiredAt);

            var options = new UpdateOptions { IsUpsert = true };
            await _zaloTokenCollection.UpdateOneAsync(filter, update, options);
        }

        public async Task<ZaloToken?> GetByUserId(string userId)
        {
            return await _zaloTokenCollection.Find(t => t.OAID == userId).FirstOrDefaultAsync();
        }

        public async Task<string> GetValidAccessTokenAsync(string oaId)
        {
            var token = await GetByUserId(oaId);
            if (token == null) throw new Exception("Chưa có token");

            // Nếu token sắp hết hạn (trước 5 phút)
            if (token.ExpiredAt <= DateTime.UtcNow.AddMinutes(5))
            {
                var url = "https://oauth.zaloapp.com/v4/oa/access_token";

                var client = _httpClientFactory.CreateClient();

                // Lấy từ cấu hình
                var secretKey = _configuration["Zalo:SecretKey"];
                var appId = _configuration["Zalo:AppId"];
                var grantType = "refresh_token";

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("refresh_token", token.RefreshToken),
                    new KeyValuePair<string, string>("app_id", appId),
                    new KeyValuePair<string, string>("grant_type", grantType),
                });

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("secret_key", secretKey);

                var response = await client.PostAsync(url, formContent);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Refresh token failed: {responseBody}");

                var newToken = System.Text.Json.JsonSerializer.Deserialize<ZaloTokenResponse>(responseBody);
                if (!string.IsNullOrEmpty(newToken?.access_token))
                {
                    token.AccessToken = newToken.access_token;
                    token.RefreshToken = newToken.refresh_token;
                    if (int.TryParse(newToken.expires_in, out int seconds))
                    {
                        token.ExpiredAt = DateTime.UtcNow.AddSeconds(seconds);
                    }
                    else
                    {
                        throw new Exception("Invalid expires_in");
                    }

                    await SaveOrUpdateToken(token);
                }
                else
                {
                    throw new Exception("access_token trả về null");
                }
            }

            return token.AccessToken!;
        }

        public async Task<ZaloToken?> GetLatestTokenAsync()
        {
            return await _zaloTokenCollection.Find(_ => true)
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


                var json = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(body);
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

                await _zaloTokenCollection.InsertOneAsync(newToken);
                return newToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in RefreshTokenAsync");
                return null;
            }
        }
        #endregion


        #region Zalo Promotion

        public ZaloPromotionBson MapToBson(ZaloPromotionRequest model, string? existingId = null)
        {
            return new ZaloPromotionBson
            {
                Id = existingId ?? model.Id,
                Tag = model.Tag,
                Elements = model.Elements?.Select(e => new ZaloElementBson
                {
                    Type = e.Type,
                    AttachmentId = e.AttachmentId,
                    Align = e.Align,
                    Content = e.Content,
                    ContentTable = e.ContentTable?.Select(ct => new ZaloTableContent
                    {
                        Key = ct.Key,
                        Value = ct.Value
                    }).ToList()
                }).ToList(),
                Buttons = model.Buttons?.Select(b => new ZaloButtonBson
                {
                    Title = b.Title,
                    ImageIcon = b.ImageIcon,
                    Type = b.Type,
                    Payload = b.ToBsonPayload()
                }).ToList()
            };
        }

        public ZaloPromotionRequest MapToModel(ZaloPromotionBson bson)
        {
            return new ZaloPromotionRequest
            {
                Id = bson.Id,
                Tag = bson.Tag,
                Elements = bson.Elements?.Select(e => new ZaloElement
                {
                    Type = e.Type,
                    AttachmentId = e.AttachmentId,
                    Align = e.Align,
                    Content = e.Content,
                    ContentTable = e.ContentTable?.Select(ct => new ZaloTableRow
                    {
                        Key = ct.Key,
                        Value = ct.Value
                    }).ToList()
                }).ToList(),
                Buttons = bson.Buttons?.Select(b =>
                {
                    var btn = new ZaloButton
                    {
                        Title = b.Title,
                        ImageIcon = b.ImageIcon,
                        Type = b.Type
                    };
                    btn.LoadFromBsonPayload(b.Payload);
                    return btn;
                }).ToList()
            };
        }

        public async Task<bool> TagExistsAsync(string tag)
        {
            var count = await _zaloPromotionCollection.CountDocumentsAsync(x => x.Tag == tag);
            return count > 0;
        }

        public async Task<ZaloPromotionRequest?> GetPromotionByTagAsync(string tag)
        {
            var bson = await _zaloPromotionCollection.Find(x => x.Tag == tag).FirstOrDefaultAsync();
            return bson == null ? null : MapToModel(bson);
        }

        public async Task InsertOrUpdateByTagAsync(ZaloPromotionRequest promotion)
        {
            var existing = await _zaloPromotionCollection.Find(x => x.Tag == promotion.Tag).FirstOrDefaultAsync();

            var bson = MapToBson(promotion, existing?.Id);

            await _zaloPromotionCollection.ReplaceOneAsync(
                filter: Builders<ZaloPromotionBson>.Filter.Eq(x => x.Tag, bson.Tag),
                replacement: bson,
                options: new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task<bool> UpdatePromotionByTagAsync(string tag, ZaloPromotionRequest updatedPromotion)
        {
            var existing = await _zaloPromotionCollection.Find(x => x.Tag == tag).FirstOrDefaultAsync();
            if (existing == null) return false;

            var bson = MapToBson(updatedPromotion, existing.Id); // giữ nguyên _id cũ

            var result = await _zaloPromotionCollection.ReplaceOneAsync(x => x.Tag == tag, bson);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePromotionByTagAsync(string tag)
        {
            var result = await _zaloPromotionCollection.DeleteOneAsync(x => x.Tag == tag);
            return result.DeletedCount > 0;
        }

        public async Task<List<ZaloPromotionRequest>> GetAllAsync()
        {
            var all = await _zaloPromotionCollection.Find(_ => true).ToListAsync();
            return all.Select((ZaloPromotionBson x) => MapToModel(x)).ToList();
        }

        public async Task CreatePromotionTemplateAsync(ZaloPromotionRequest request)
        {
            var payload = new
            {
                recipient = new { user_id = request.UserId },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "promotion",
                            promotion = new
                            {
                                tag = request.Tag,
                                elements = request.Elements.Select(e => MapElement(e)).ToList(),
                                buttons = request.Buttons
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://openapi.zalo.me/v3.0/oa/message/promotion");
            httpRequest.Headers.Add("access_token", request.AccessToken);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Zalo API error: {result}");
        }

        private object MapElement(ZaloElement el)
        {
            if (el.Type == "table")
            {
                return new
                {
                    type = "table",
                    content = el.ContentTable?.Select(row => new
                    {
                        key = row.Key,
                        value = row.Value
                    }).ToList()
                };
            }

            var dict = new Dictionary<string, object>
            {
                ["type"] = el.Type
            };

            if (!string.IsNullOrEmpty(el.AttachmentId))
                dict["attachment_id"] = el.AttachmentId;

            if (!string.IsNullOrEmpty(el.Content))
                dict["content"] = el.Content;

            if (!string.IsNullOrEmpty(el.Align))
                dict["align"] = el.Align;

            return dict;
        }

        #endregion

    }
}

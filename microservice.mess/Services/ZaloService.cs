using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Lib.Setting;
using Lib.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Lib.Setting;
using Lib.Utility;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using microservice.mess.Models;
using microservice.mess.Repositories;
using microservice.mess.Configurations;

public class ZaloService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ZaloService> _logger;
    private readonly TokenRepository _tokenRepository;
    private readonly ZaloSettings _zaloSettings;
    private readonly ZaloPromotionRepository _zaloPromotionRepository;
    public ZaloService(
    IHttpClientFactory factory,
    ZaloPromotionRepository zaloPromotionRepository,
    IOptions<ZaloSettings> zaloOptions,
    ILogger<ZaloService> logger,
    TokenRepository tokenRepository)
    {
        _httpClientFactory = factory;
        _zaloPromotionRepository = zaloPromotionRepository;
        _logger = logger;
        _zaloSettings = zaloOptions.Value;
        _tokenRepository = tokenRepository;
    }

    // Xử lý zalo/callback
    public async Task ProcessCallback(ZaloCallbackRequest callback)
    {
        if (string.IsNullOrEmpty(callback.Code)) return;

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("secret_key", _zaloSettings.secret_key);

        var data = new Dictionary<string, string>
        {
            { "app_id", _zaloSettings.app_id },
            { "code_verifier", callback.CodeVerifier ?? "" },
            { "code", callback.Code },
            { "grant_type", "authorization_code" }
        };

        var response = await client.PostAsync("https://oauth.zaloapp.com/v4/oa/access_token", new FormUrlEncodedContent(data));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Zalo token error: {content}", content);
            return;
        }

        var tokenResponse = JsonConvert.DeserializeObject<ZaloTokenResponse>(content);
        if (tokenResponse.error != 0)
        {
            _logger.LogError("Zalo OAuth error: {desc}", tokenResponse.error_description);
            return;
        }

        var expiredAt = DateTime.UtcNow.AddSeconds(double.TryParse(tokenResponse.expires_in, out var sec) ? sec : 90000);

        await _tokenRepository.SaveOrUpdateToken(new ZaloToken
        {
            OAID = _zaloSettings.oa_id,
            AccessToken = tokenResponse.access_token,
            RefreshToken = tokenResponse.refresh_token,
            ExpiredAt = expiredAt
        });

        _logger.LogInformation("Zalo token saved: {user}", tokenResponse.oa_id ?? callback.State);
    }

    // Tạo gửi bài cho user
    public async Task HandleSendPromotionAsync(ZaloPromotionRequest payload)
    {
        // if (payload == null ||
        //     string.IsNullOrEmpty(payload.UserId) ||
        //     string.IsNullOrEmpty(payload.AccessToken) ||
        //     !payload.Elements.Any())
        // {
        //     _logger.LogWarning("Invalid or missing promotion data.");
        //     return;
        // }

        try
        {
            // await SendPromotionToUser(
            //     payload.UserId,
            //     payload.Header,
            //     payload.Elements,
            //     payload.AccessToken,
            //     payload.Buttons,
            //     payload.BannerAttachmentIds
            // );
            _logger.LogInformation("Promotion sent to user: {userId}", payload.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending promotion to user: {userId}", payload.UserId);
        }
    }

    public async Task SendZaloMessageAsync(string userId, string message, string accessToken)
    {
        var payload = new
        {
            recipient = new { user_id = userId },
            message = new { text = message }
        };

        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync($"https://openapi.zalo.me/v3.0/oa/message?access_token={accessToken}", payload);
        response.EnsureSuccessStatusCode();
    }


    public async Task<string> UploadImageToZaloAsync(string imagePath, string accessToken)
    {

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("access_token", accessToken);

        using var form = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(await File.ReadAllBytesAsync(imagePath));
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        form.Add(imageContent, "file", Path.GetFileName(imagePath));

        var response = await client.PostAsync("https://openapi.zalo.me/v2.0/oa/upload/image", form);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Upload failed: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("data").GetProperty("attachment_id").GetString();
    }
    public async Task CreatePromotionAsync(ZaloPromotionRequest promotion)
    {
        await _zaloPromotionRepository.InsertOneAsync(promotion);
    }

    public async Task SendPromotionToUser(
    string userId,
    string header,
    List<ZaloElement> elements,
    string accessToken,
    List<ZaloButton> buttons,
    List<string> bannerAttachmentIds)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("access_token", accessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var elementObjects = new List<object>();

        // Chèn các banner đầu tiên
        foreach (var banner in bannerAttachmentIds)
        {
            elementObjects.Add(new
            {
                type = "banner",
                attachment_id = banner
            });
        }

        // Chèn header sau banner
        elementObjects.Add(new
        {
            type = "header",
            content = header
        });

        // Chèn các phần tử gốc (text, table,...)
        var allowedTypes = new[] { "banner", "header", "text", "table" };

        foreach (var el in elements)
        {
            if (string.IsNullOrWhiteSpace(el.Type) || !allowedTypes.Contains(el.Type))
            {
                _logger.LogWarning("Zalo element has unsupported type: {type}", el.Type);
                continue;
            }

            if (el.Type == "text")
            {
                elementObjects.Add(new { type = "text", align = el.Align, content = el.Content });
            }
            else if (el.Type == "table" && el.ContentTable != null && el.ContentTable.Any())
            {
                elementObjects.Add(new
                {
                    type = "table",
                    content_table = el.ContentTable.Select(row => new { key = row.Key, value = row.Value }).ToList()
                });
            }
            else if (el.Type == "banner")
            {
                elementObjects.Add(new { type = "banner", attachment_id = el.AttachmentId });
            }
            else if (el.Type == "header")
            {
                elementObjects.Add(new { type = "header", content = el.Content });
            }
        }


        var buttonsObj = buttons.Select(btn => new
        {
            title = btn.Title,
            image_icon = btn.ImageIcon,
            type = btn.Type,
            payload = btn.Payload
        });

        var body = new
        {
            recipient = new { user_id = userId },
            message = new
            {
                attachment = new
                {
                    type = "template",
                    payload = new
                    {
                        template_type = "promotion",
                        elements = elementObjects,
                        buttons = buttonsObj
                    }
                }
            }
        };

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://openapi.zalo.me/v3.0/oa/message/promotion", content);
        var resultContent = await response.Content.ReadAsStringAsync();

        try
        {
            var zaloResponse = JsonConvert.DeserializeObject<ZaloResponseError>(resultContent);
            if (zaloResponse?.error == -218)
            {
                _logger.LogWarning("User {UserId} đã vượt quá quota nhận tin OA trong ngày.", userId);
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Lỗi khi parse JSON từ Zalo: {Error}", ex.Message);
        }

        Console.WriteLine($"Sent promotion to {userId} → Zalo response: {resultContent}");
    }



}

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
    private readonly ZaloTokenRepository _zaloTokenRepository;
    private readonly ZaloSettings _zaloSettings;
    private readonly ZaloPromotionRepository _zaloPromotionRepository;
    public ZaloService(
    IHttpClientFactory factory,
    ZaloPromotionRepository zaloPromotionRepository,
    IOptions<ZaloSettings> zaloOptions,
    ILogger<ZaloService> logger,
    ZaloTokenRepository ZaloTokenRepository)
    {
        _httpClientFactory = factory;
        _zaloPromotionRepository = zaloPromotionRepository;
        _logger = logger;
        _zaloSettings = zaloOptions.Value;
        _zaloTokenRepository = ZaloTokenRepository;
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

        await _zaloTokenRepository.SaveOrUpdateToken(new ZaloToken
        {
            OAID = _zaloSettings.oa_id,
            AccessToken = tokenResponse.access_token,
            RefreshToken = tokenResponse.refresh_token,
            ExpiredAt = expiredAt
        });

        _logger.LogInformation("Zalo token saved: {user}", tokenResponse.oa_id ?? callback.State);
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

        using var form = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(await File.ReadAllBytesAsync(imagePath));
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        form.Add(imageContent, "file", Path.GetFileName(imagePath));

        var request = new HttpRequestMessage(HttpMethod.Post, "https://openapi.zalo.me/v2.0/oa/upload/image")
        {
            Content = form
        };
        request.Headers.Add("access_token", accessToken);

        var response = await client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Upload failed: {json}");

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Debug JSON trả về
        Console.WriteLine(json);

        if (root.TryGetProperty("data", out var dataElement) &&
            dataElement.TryGetProperty("attachment_id", out var attachmentIdElement))
        {
            return attachmentIdElement.GetString();
        }

        throw new Exception("Zalo response does not contain attachment_id. Full response: " + json);
    }

    public async Task CreatePromotionAsync(ZaloPromotionRequest promotion)
    {
        await _zaloPromotionRepository.InsertOrUpdateByTagAsync(promotion);
    }

    public async Task SendPromotionToUser(
        string userId,
        string accessToken,
        string tag
    )
    {
        _logger.LogInformation("SendPromotionToUser: {Tag} {userId} {access}", tag, userId, accessToken);
        var promotion = await _zaloPromotionRepository.GetPromotionByTagAsync(tag);
        if (promotion == null)
        {
            _logger.LogWarning("Không tìm thấy nội dung ZaloPromotion với tag: {Tag}", tag);
            return;
        }

        var elements = promotion.Elements.Select(el => new ZaloElement
        {
            Type = el.Type,
            AttachmentId = el.AttachmentId,
            Content = el.Content,
            Align = el.Align,
            ContentTable = el.ContentTable?.Select(row => new ZaloTableRow
            {
                Key = row.Key ?? "",
                Value = row.Value ?? ""
            }).ToList()
        }).ToList();

        var buttons = promotion.Buttons.Select(btn => new ZaloButton
        {
            Title = btn.Title,
            ImageIcon = btn.ImageIcon,
            Type = btn.Type,
            Payload = btn.Payload
        }).ToList();

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("access_token", accessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var elementObjects = new List<object>();

        // Xử lý các phần tử từ MongoDB
        var allowedTypes = new[] { "banner", "header", "text", "table" };

        foreach (var el in elements)
        {
            if (string.IsNullOrWhiteSpace(el.Type) || !allowedTypes.Contains(el.Type))
            {
                _logger.LogWarning("Zalo element has unsupported type: {type}", el.Type);
                continue;
            }

            switch (el.Type)
            {
                case "banner":
                    if (!string.IsNullOrWhiteSpace(el.AttachmentId))
                    {
                        elementObjects.Add(new { type = "banner", attachment_id = el.AttachmentId });
                    }
                    break;

                case "header":
                    if (!string.IsNullOrWhiteSpace(el.Content))
                    {
                        elementObjects.Add(new { type = "header", content = el.Content });
                    }
                    break;

                case "text":
                    elementObjects.Add(new { type = "text", align = el.Align, content = el.Content });
                    break;

                case "table":
                    if (el.ContentTable == null || !el.ContentTable.Any())
                    {
                        _logger.LogWarning("Zalo table element is empty for tag {Tag}", tag);
                        break;
                    }

                    var validTableRows = el.ContentTable
                        .Where(row => !string.IsNullOrWhiteSpace(row.Key) || !string.IsNullOrWhiteSpace(row.Value))
                        .Select(row => new { key = row.Key ?? "", value = row.Value ?? "" })
                        .ToList();

                    if (validTableRows.Any())
                    {
                        _logger.LogInformation("Adding table element with rows: {@Rows}", validTableRows);
                        elementObjects.Add(new
                        {
                            type = "table",
                            content = validTableRows
                        });
                    }
                    else
                    {
                        _logger.LogWarning("Zalo table element has no valid rows for tag {Tag}", tag);
                    }

                    break;

            }
        }

        var buttonsObj = new List<object>();
        foreach (var btn in buttons)
        {
            if (btn.Type == "oa.open.url")
            {
                // Payload cần parse thành object có trường url
                try
                {
                    var urlPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(btn.Payload?.ToString() ?? "");
                    if (urlPayload != null && urlPayload.ContainsKey("url"))
                    {
                        buttonsObj.Add(new
                        {
                            title = btn.Title,
                            image_icon = btn.ImageIcon,
                            type = btn.Type,
                            payload = new
                            {
                                url = urlPayload["url"]
                            }
                        });
                    }
                    else
                    {
                        _logger.LogWarning("Invalid payload format for oa.open.url button: {Payload}", btn.Payload);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Lỗi parse payload url button: {Error}", ex.Message);
                }
            }
            else
            {
                // Trường hợp payload là chuỗi bình thường
                buttonsObj.Add(new
                {
                    title = btn.Title,
                    image_icon = btn.ImageIcon,
                    type = btn.Type,
                    payload = btn.Payload?.ToString()
                });
            }
        }


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
            var zaloResponse = JsonConvert.DeserializeObject<ZaloTokenResponse>(resultContent);
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

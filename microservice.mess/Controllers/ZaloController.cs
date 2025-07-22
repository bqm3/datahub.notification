using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Lib.Setting;
using Lib.Utility;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Repositories;
using microservice.mess.Services;
using microservice.mess.Configurations;
using microservice.mess.Kafka;

namespace microservice.mess.Controllers
{
    [ApiController]
    [Route("zalo")]
    public class ZaloController : ControllerBase
    {
        private readonly ILogger<ZaloController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ZaloService _zaloService;
        private readonly ZaloRepository _zaloRepository;
        private readonly ZaloSettings _zaloSettings;
        // private readonly KafkaProducerService _kafkaProducer;

        public ZaloController(IHttpClientFactory httpClientFactory,
                      ILogger<ZaloController> logger,
                      IOptions<ZaloSettings> zaloOptions,
                      ZaloRepository zaloRepository,
                      ZaloService zaloService
                      //   KafkaProducerService kafkaProducer
                      )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _zaloRepository = zaloRepository;
            _zaloSettings = zaloOptions.Value;
            _zaloService = zaloService;
            // _kafkaProducer = kafkaProducer;
        }

        [HttpGet("callback")]
        public async Task<IActionResult> ZaloCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Missing authorization code.");


            var codeVerifier = HttpContext.Session.GetString("zalo_code_verifier");
            _logger.LogInformation("=> zalo_code_verifier: {zalo_code_verifier}", codeVerifier);

            var callbackRequest = new ZaloCallbackRequest
            {
                Code = code,
                State = state,
                CodeVerifier = codeVerifier
            };

            await _zaloService.ProcessCallback(callbackRequest);

            // var envelope = new NotificationKafkaEnvelope
            // {
            //     Action = "callback",
            //     Payload = JsonConvert.SerializeObject(callbackRequest)
            // };

            // await _kafkaProducer.SendMessageAsync("topic-zalo", null, JsonConvert.SerializeObject(envelope));

            return Ok("Zalo callback received and sent to Kafka.");
        }

        [HttpPost("image/upload")]
        public async Task<IActionResult> UploadZaloImage(
            [FromForm] UploadFileDto dto,
            [FromHeader(Name = "access_token")] string accessToken,
            [FromServices] ZaloService zaloService)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var tempPath = Path.GetTempFileName();

            try
            {
                // Ghi file tạm
                using (var stream = System.IO.File.Create(tempPath))
                {
                    await dto.File.CopyToAsync(stream);
                }

                // Upload lên Zalo
                string attachmentId = await zaloService.UploadImageToZaloAsync(tempPath, accessToken);

                return Ok(new
                {
                    success = true,
                    attachment_id = attachmentId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
            finally
            {
                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);
            }
        }



        // POST: /zalo/create-promotion
        [HttpPost("create-promotion")]
        public async Task<IActionResult> CreatePromotion([FromBody] ZaloPromotionRequest promotion)
        {
            await _zaloService.CreatePromotionAsync(promotion);
            return Ok("Promotion created successfully");
        }

        [HttpPost("create-template")]
        public async Task<IActionResult> CreateTemplate([FromBody] ZaloPromotionRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.AccessToken))
                return BadRequest("Missing userId or accessToken");

            await _zaloRepository.CreatePromotionTemplateAsync(request);
            return Ok(new { message = "Zalo promotion template sent successfully" });
        }

        // GET: /zalo/get-promotion/{id}
        [HttpGet("get-promotion/{id}")]
        public async Task<IActionResult> GetPromotionById(string id)
        {
            var promotion = await _zaloRepository.GetPromotionByTagAsync(id);
            if (promotion == null)
            {
                return NotFound("Promotion not found");
            }

            return Ok(promotion);
        }

        // GET: zalo/promotions
        [HttpGet("promotions")]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _zaloRepository.GetAllAsync();
            return Ok(promotions);
        }

        // PUT: zalo/update-promotion/{id}
        [HttpPut("update-promotion/{id}")]
        public async Task<IActionResult> UpdatePromotion(string id, [FromBody] ZaloPromotionRequest updatedPromotion)
        {
            var existing = await _zaloRepository.GetPromotionByTagAsync(id);
            if (existing == null)
            {
                return NotFound("Promotion not found");
            }

            var updated = await _zaloRepository.UpdatePromotionByTagAsync(id, updatedPromotion);
            if (!updated)
            {
                return BadRequest("Update failed");
            }

            return Ok("Promotion updated successfully");
        }

        // DELETE: zalo/delete-promotion/{id}
        [HttpDelete("delete-promotion/{id}")]
        public async Task<IActionResult> DeletePromotion(string id)
        {
            var deleted = await _zaloRepository.DeletePromotionByTagAsync(id);
            if (!deleted)
            {
                return NotFound("Promotion not found or already deleted");
            }

            return Ok("Promotion deleted successfully");
        }



        [HttpPost("send-promotion")]
        public async Task<IActionResult> SendPromotion([FromBody] object body)
        {
            var accessToken = Request.Headers["access_token"].ToString().Trim();

            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("Thiếu access_token trong header.");

            var client = _httpClientFactory.CreateClient();

            // Thiết lập headers
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("access_token", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://openapi.zalo.me/v3.0/oa/message/promotion", content);
            var result = await response.Content.ReadAsStringAsync();

            return Content(result, "application/json");
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> ReceiveWebHook()
        {
            try
            {
                JsonElement payload;

                // 1. Kiểm tra Content-Type
                var contentType = Request.ContentType?.ToLower();

                if (contentType.Contains("application/json"))
                {
                    using var reader = new StreamReader(Request.Body);
                    var body = await reader.ReadToEndAsync();
                    using var doc = JsonDocument.Parse(body);
                    payload = doc.RootElement.Clone();
                }
                else if (contentType.Contains("application/x-www-form-urlencoded"))
                {
                    var form = await Request.ReadFormAsync();
                    if (!form.TryGetValue("data", out var dataRaw))
                    {
                        _logger.LogWarning("Missing 'data' field in form.");
                        return Ok();
                    }

                    using var doc = JsonDocument.Parse(dataRaw);
                    payload = doc.RootElement.Clone();
                }
                else
                {
                    _logger.LogWarning("Unsupported Content-Type: {ContentType}", contentType);
                    return Ok();
                }

                _logger.LogInformation("Zalo Webhook Payload: {Payload}", payload.ToString());

                var eventName = payload.GetProperty("event_name").GetString();
                _logger.LogInformation("Received event: {Event}", eventName);

                string userId = null;
                string oaId = "4606516489169499369";

                // 1. Ưu tiên lấy sender.id / recipient.id nếu có
                if (payload.TryGetProperty("sender", out var sender) &&
                    payload.TryGetProperty("recipient", out var recipient) &&
                    sender.TryGetProperty("id", out var senderIdElement) &&
                    recipient.TryGetProperty("id", out var recipientIdElement))
                {
                    var senderId = senderIdElement.GetString();
                    var recipientId = recipientIdElement.GetString();

                    var userEvents = new[] {
                        "follow", "unfollow",
                        "user_join_group", "user_request_join_group", "user_out_group",
                        "user_send_text"
                    };

                    if (userEvents.Contains(eventName))
                    {
                        userId = senderId;
                        oaId = recipientId;
                    }
                    else
                    {
                        userId = recipientId;
                        oaId = senderId;
                    }
                }
                else
                {
                    // 2. Nếu không có sender/recipient (đặc biệt với follow/unfollow)
                    if (payload.TryGetProperty("follower", out var follower) &&
                        follower.TryGetProperty("id", out var followerIdElement))
                    {
                        userId = followerIdElement.GetString();
                        oaId = "UNKNOWN"; // Hoặc lấy từ config nếu bạn chỉ có 1 OA
                        _logger.LogWarning("Used follower.id fallback for event: {event}", eventName);
                    }
                    else
                    {
                        _logger.LogWarning("Webhook payload missing sender, recipient, and follower ID for event: {event}", eventName);
                    }
                }

                var timestamp = DateTime.UtcNow;
                string messageType = null;
                string messageContent = null;

                if (payload.TryGetProperty("message", out var message))
                {
                    if (message.TryGetProperty("text", out var textElement))
                    {
                        messageType = "text";
                        messageContent = textElement.GetString();

                        var match = Regex.Match(messageContent, @"#(\w+)");
                        if (match.Success)
                        {
                            var tag = match.Groups[1].Value.ToLower();
                            _logger.LogInformation("Detected tag: {Tag}", tag);

                            if (!string.IsNullOrEmpty(tag))
                            {
                                var accessToken = await _zaloRepository.GetValidAccessTokenAsync(oaId);
                                _logger.LogInformation("AccessToken: {accessToken}", accessToken);

                                await _zaloService.SendPromotionToUser(
                                    userId: userId,
                                    accessToken: accessToken,
                                    tag: tag
                                );
                            }

                        }


                    }
                    else if (message.TryGetProperty("attachment", out var attachment))
                    {
                        messageType = attachment.GetProperty("type").GetString();
                        messageContent = attachment.GetRawText();
                    }
                    else
                    {
                        messageType = "unknown";
                        messageContent = message.GetRawText();
                    }
                }

                if (eventName == "user_join_group" ||
                    eventName == "user_request_join_group" ||
                    eventName == "user_out_group" ||
                    eventName == "follow" ||
                    eventName == "unfollow")
                {
                    string status = (eventName == "user_out_group" || eventName == "unfollow") ? "deactived" : "active";

                    await _zaloRepository.UpsertUserAsync(userId, status, eventName);
                    _logger.LogInformation("Zalo group event: {event} -> {userId} = {status}", eventName, userId, status);
                }


                var log = new ZaloEvent
                {
                    OAId = oaId,
                    UserId = userId,
                    Event = eventName,
                    MessageType = messageType,
                    MessageContent = messageContent,
                    Timestamp = timestamp
                };

                await _zaloRepository.InsertEventAsync(log);

                _logger.LogInformation("Zalo Webhook Event: {event} from user {userId}", eventName, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Webhook processing failed");
                return BadRequest();
            }
        }


        [HttpGet("list-users")]
        public async Task<IActionResult> GetListUserByIDs()
        {
            var userIds = await _zaloRepository.ListUserAsync();
            var result = userIds != null && userIds.Any() ? 1 : 0;

            return Ok(new
            {
                ResultCode = result > 0 ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                Message = result > 0 ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                Data = result > 0
                            ? userIds.Cast<object>().ToList()
                            : new List<object>()
                // hoặc new List<string>()
            });
        }

    }

}

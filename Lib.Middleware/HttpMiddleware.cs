using Lib.Consul.Configuration;
using Lib.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PROJECT.BASE.CORE;
using System.Reflection.Emit;

namespace Lib.Middleware
{
    public class HttpMiddleware
    {
        private readonly ILogger<HttpMiddleware> _logger;
        private readonly RequestDelegate _next;
        public HttpMiddleware(RequestDelegate next, ILogger<HttpMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                var request = await FormatRequest(context.Request);
                //context.Request.Headers.Add("client-ip", context.Connection.RemoteIpAddress.ToString());
                context.Request.Headers.TryGetValue("client-token", out var client_token);
                context.Request.Headers.TryGetValue("Authorization", out var access_token);
                if (string.IsNullOrEmpty(access_token))
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        ResultCode = Constant.RETURN_CODE_UNAUTHORIZED,
                        Message = $"{Constant.MESSAGE_UNAUTHORIZED}:{Constant.MESSAGE_NOT_FOUND}"
                    }));
                    return;
                }
                string secretKey = Config.CONFIGURATION_GLOBAL.Gateway.SecretKey;
                string issuer = Config.CONFIGURATION_GLOBAL.Gateway.Issuer;
                access_token = access_token.ToString().Replace("Bearer ", string.Empty);
                var dictTokenInfo = TokenJWT.VerifyJwtToken(access_token, secretKey, issuer);
                if (dictTokenInfo == null)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        ResultCode = Constant.RETURN_CODE_UNAUTHORIZED,
                        Message = $"{Constant.MESSAGE_UNAUTHORIZED}:{Constant.MESSAGE_SUCCESS_AUT_TOKEN_NOT}"
                    }));
                    return;
                }
                if (Utility.Utility.ConvertToUnixTime(DateTime.Now) > long.Parse(dictTokenInfo["EXPIRED"].ToString()))
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        ResultCode = Constant.RETURN_CODE_UNAUTHORIZED,
                        Message = $"{Constant.MESSAGE_UNAUTHORIZED}:{Constant.MESSAGE_AUT_ERROR_TOKEN}"
                    }));
                    return;
                }
                context.Items["HASH_TOKEN"] = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(access_token));
                context.Items["CORP_CODE"] = dictTokenInfo.ContainsKey("CORP_CODE") ? dictTokenInfo["CORP_CODE"].ToString() : string.Empty;
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next(context);
                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                }));
                return;
            }

        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            var formattedRequest = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString} {body}";
            request.Body.Position = 0;
            return formattedRequest;

        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;
using Lib.Consul.Configuration;
using System.Collections.Generic;

namespace PROJECT.BASE.SERVICES
{
    public class SmsService : ISmsService
    {
        HttpClient _httpClient;
        SmsOption _smsOption;
        SmsOtpOption _smsOtpOption;

        public SmsService(HttpClient httpClient, IOptions<SmsOption> options, IOptions<SmsOtpOption> smsOtpOptions)
        {
            _smsOption = options.Value;
            _smsOtpOption = smsOtpOptions.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri(_smsOption.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<GetAccountBalanceResponse> GetAccountBalanceAsync()
        {
            GetAccountBalanceRequest request = new GetAccountBalanceRequest()
            {
                ApiKey = _smsOption.ApiKey,
                SecretKey = _smsOption.SecretKey
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _smsOption.AccountBalanceEndpoint);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode) return null;

            var responseText = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetAccountBalanceResponse>(responseText);

        }
        


        public async Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request)
        {
            if (request == null) throw new Exception("Bad Request! Request is null");

            var smsContentName = SmsContent.GetSmsContentTypeName(request.SmsContentType);

            var listSmsOtps = BaseSettings.Get<List<string>>("ContentTypeOTPs");

            if(listSmsOtps.Contains(smsContentName))
             {
                request.ApiKey = _smsOtpOption.ApiKey;
                request.SecretKey = _smsOtpOption.SecretKey;
                request.Brandname = _smsOtpOption.BranchName;
                request.SmsType = _smsOtpOption.SmsType;
            }
            else
            {
                request.ApiKey = _smsOption.ApiKey;
                request.SecretKey = _smsOption.SecretKey;
                request.Brandname = _smsOption.BranchName;
                request.SmsType = _smsOption.SmsType;
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _smsOption.SendSmsEndpoint);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode) return null;

            var responseText = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SendSmsResponse>(responseText);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Lib.Utility
{
    public class HttpClientBase
    {
        private static HttpClient client;
        private static HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static HttpClient GetHTTPClient(string access_token,string userAgent)
        {
            HttpClient client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (!string.IsNullOrEmpty(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            if (!string.IsNullOrEmpty(access_token))
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {access_token}");             
            return client;
        }
        public static dynamic GET(string url, string access_token,string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpResponse = client.GetAsync(url).Result;
            if (httpResponse.Content == null)
                return default(dynamic);
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse.Result);
            if (response != null)
                return null;
            return response;
        }
        public static dynamic POST(object dataPost, string url, string access_token, string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpContent = dataPost != null ? new StringContent(JsonConvert.SerializeObject(dataPost), Encoding.UTF8, "application/json") : null;
            var httpResponse = client.PostAsync(url, httpContent).Result;
            if (httpResponse.Content == null)
                return default(dynamic);
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject<dynamic>(jsonResponse.Result);
            return response;
        }
        public static dynamic PUT(object dataPut, string url, string access_token, string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpContent = new StringContent(JsonConvert.SerializeObject(dataPut), Encoding.UTF8, "application/json");            
            var httpResponse = client.PutAsync(url, httpContent).Result;
            if (httpResponse.Content == null)
                return default(dynamic);
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject<dynamic>(jsonResponse.Result);
            return response;
        }
        public static dynamic DELETE(string url, string access_token, string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpResponse = client.DeleteAsync(url).Result;
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            dynamic response = JsonConvert.DeserializeObject<dynamic>(jsonResponse.Result);
            return response;
        }
        public static dynamic DELETE(object requestPayload, string url, string access_token,string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var requestUri = new Uri(url);
            var httpResponse = client.SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, requestUri)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(requestPayload), Encoding.UTF8, "application/json")
                }).Result;
            var jsonResponse = httpResponse.Content != null ? httpResponse.Content.ReadAsStringAsync() : null;
            dynamic response = JsonConvert.DeserializeObject<dynamic>(jsonResponse.Result);
            return response;
        }
        public static Dictionary<string, object> GET_TO_INFO(string url, string access_token, string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpResponse = client.GetAsync(url).Result;
            if (httpResponse.Content == null)
                return default(dynamic);
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse.Result);
            if (response != null && response.ContainsKey("error"))
                return null;
            return response;
        }

        public static List<Dictionary<string, object>> GET_TO_LIST(string url, string access_token, string userAgent)
        {
            client = GetHTTPClient(access_token, userAgent);
            var httpResponse = client.GetAsync(url).Result;
            if (httpResponse.Content == null)
                return default(dynamic);
            var jsonResponse = httpResponse.Content.ReadAsStringAsync();
            List<Dictionary<string, object>> response = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponse.Result);
            return response;
        }


    }
}

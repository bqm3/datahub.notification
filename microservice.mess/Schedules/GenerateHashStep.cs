using microservice.mess.Interfaces;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
// using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace microservice.mess.Schedules
{
    public class GenerateHashStep : IMessageStep
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GenerateHashStep> _logger;
        private readonly SignetRepository _signetRepository;

        private readonly string _gatewayUrl;

        public GenerateHashStep(IHttpClientFactory httpClientFactory,IConfiguration configuration, ILogger<GenerateHashStep> logger, SignetRepository signetRepository)
        {
            _httpClient = httpClientFactory.CreateClient("signet");
            _logger = logger;
            _signetRepository = signetRepository;
            _gatewayUrl = configuration["Signet:Gateway"];
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            if (!context.Items.TryGetValue("pdfPath", out var pathObj))
                throw new Exception("Missing 'pdfPath' in context");

            if (pathObj is not string pdfPath)
                throw new Exception("Invalid 'pdfPath' type in context");

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found for hash generation", pdfPath);


            _logger.LogInformation("🔐 [GenerateHashStep] Uploading PDF file for hash generation: {path}", pdfPath);

            using var stream = File.OpenRead(pdfPath);
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            content.Add(fileContent, "file", Path.GetFileName(pdfPath));

            var response = await _httpClient.PostAsync($"{_gatewayUrl}/files/upload/single", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Upload to Signet failed: {response.StatusCode} - {responseBody}");

            var json = JsonDocument.Parse(responseBody);
            var encodedValue = json.RootElement.GetProperty("data").GetProperty("value").GetString();
            var hashValue = PkceHelper.NormalizeFileValue(encodedValue!);

            var hashModel = new SGIUploadFileHashModel
            {
                FileName = Path.GetFileName(pdfPath),
                FileHash = hashValue,
                FileType = "application/pdf",
                CreatedAt = DateTime.UtcNow
            };

            await _signetRepository.UploadFileHashAsync(hashModel);

            context.Items["fileHash"] = hashValue;
            _logger.LogInformation("✅ [GenerateHashStep] File hash generated: {hash}", hashValue);
        }
    }
}

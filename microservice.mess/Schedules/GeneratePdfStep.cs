
using microservice.mess.Interfaces;
using microservice.mess.Documents;
using microservice.mess.Models.Message;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace microservice.mess.Schedules
{
    public class GeneratePdfStep : IMessageStep
    {
        // private readonly IChartPdfGenerator _pdfGenerator;

        private readonly SgiPdfChart _sgiPdfChart;

        public GeneratePdfStep(SgiPdfChart sgiPdfChart)
        {
            // _pdfGenerator = pdfGenerator;
            _sgiPdfChart = sgiPdfChart;
        }

        public async Task ExecuteAsync(ScheduleContext context)
        {
            var data = context.Items["signetDataPdf"];
            if (data is null)
                throw new Exception("signetDataPdf not found in context");

            var now = DateTime.UtcNow.AddHours(7);
            var json = JsonConvert.SerializeObject(data);
            var request = JsonConvert.DeserializeObject<ChartJsonRequest>(json)!;

            var outFileTemplate = context.Schedule.Data.OutFile ?? $"Bao_cao_chi_tiet_{now:dd_MM_yyyy}.pdf";

            var fixedFileName = outFileTemplate.Replace("{{date}}", now.ToString("dd_MM_yyyy"));

            var wordFileName = Path.ChangeExtension(fixedFileName, ".docx");

            var outputDir = "Exports";
            var wordPath = Path.Combine(outputDir, wordFileName);
            var pdfPath = Path.Combine(outputDir, fixedFileName);

            Directory.CreateDirectory(outputDir);

            // Ghi đè tên file trong request
            request.OutputFileName = fixedFileName;

            // Nếu file PDF đã tồn tại, không render lại
            if (!File.Exists(pdfPath))
            {
                await _sgiPdfChart.GenerateFromJson(request, request.TemplateName, wordPath, pdfPath);
            }

            // Cập nhật lại context
            context.Items["pdfPath"] = pdfPath;
            context.Items["fileUrl"] = pdfPath; // hoặc URL nếu có upload
        }


    }

}
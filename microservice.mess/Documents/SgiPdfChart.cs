using Aspose.Cells;
using Aspose.Cells.Charts;
using Aspose.Cells.Rendering;
using Aspose.Cells.Drawing;
using System.IO;
using System.Data;
using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Services.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace microservice.mess.Documents
{
    public class SgiPdfChart
    {
        private readonly IStorageService _storageService;

        public SgiPdfChart(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string> GenerateFromJson(ChartJsonRequest request, string templateName, string wordOutputPath, string pdfOutputPath)
        {
            if (request.Data.Count == 0)
                return "No data provided";

            var dataRows = request.Data.ToList();

            // Chỉ lấy cột từ bản ghi đầu tiên
            var columnNames = request.Data[0].Keys
                .Where(k => k != "Chủ đề")
                .Distinct()
                .OrderBy(k => k)
                .ToList();
            columnNames.Insert(0, "Chủ đề");

            var exportDir = "Exports";
            if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

            // Load cấu hình
            var configPath = Path.Combine("templates/configs", "social_ag.json");
            if (!File.Exists(configPath))
                throw new Exception("Không tìm thấy file cấu hình social_ag.json");

            var configJson = await File.ReadAllTextAsync(configPath);
            var fullConfig = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(configJson)!;

            var dataJsonMap = fullConfig.TryGetValue("DataJson", out var dataJsonToken)
                ? dataJsonToken.ToObject<Dictionary<string, string>>() ?? new()
                : new();

            string tableKey = request.DataJson?.FirstOrDefault()?.Category ?? "";
            if (string.IsNullOrWhiteSpace(tableKey) && !string.IsNullOrWhiteSpace(request.OutputFileName))
            {
                var fileName = Path.GetFileNameWithoutExtension(request.OutputFileName);
                tableKey = fullConfig.Keys
                    .Where(k => k != "DataJson")
                    .FirstOrDefault(k => fileName.StartsWith(k, StringComparison.OrdinalIgnoreCase)) ?? "";
            }

            if (string.IsNullOrWhiteSpace(tableKey) || !fullConfig.TryGetValue(tableKey, out var rawTemplateObj))
                throw new Exception($"Không tìm thấy cấu hình template cho table: {tableKey}");

            var templateConfig = rawTemplateObj.ToObject<TemplateConfig>() ?? new();
            templateConfig.DataJson = dataJsonMap;

            // Load Excel template
            var excelTemplatePath = Path.Combine("templates", templateConfig?.TemplateExcel);
            var workbook = File.Exists(excelTemplatePath)
                ? new Workbook(excelTemplatePath)
                : throw new Exception("Excel template file not found: " + excelTemplatePath);

            var sheetData = workbook.Worksheets["SheetData"] ?? workbook.Worksheets[0];
            sheetData.Name = "SheetData";
            sheetData.Cells.Clear(); // ✅ Xoá toàn bộ dữ liệu cũ trong SheetData

            // Ghi header
            for (int col = 0; col < columnNames.Count; col++)
                sheetData.Cells[0, col].PutValue(columnNames[col]);

            // Ghi dữ liệu
            for (int row = 0; row < dataRows.Count; row++)
            {
                for (int col = 0; col < columnNames.Count; col++)
                {
                    var colName = columnNames[col];
                    dataRows[row].TryGetValue(colName, out var value);

                    if (value is long l) sheetData.Cells[row + 1, col].PutValue((double)l);
                    else if (value is int i) sheetData.Cells[row + 1, col].PutValue((double)i);
                    else if (value is double d) sheetData.Cells[row + 1, col].PutValue(d);
                    else if (value is string s && double.TryParse(s, out double num)) sheetData.Cells[row + 1, col].PutValue(num);
                    else sheetData.Cells[row + 1, col].PutValue(value?.ToString() ?? "0");
                }
            }

            // Export charts
            var fieldToImageMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var sheetChart = workbook.Worksheets["SheetChart"];

            var fileNameOnly = Path.GetFileNameWithoutExtension(request.OutputFileName ?? "");
            string tableName = "unknown";
            string dateStr = DateTime.Now.ToString("yyyyMMdd");

            var parts = fileNameOnly.Split('_');
            if (parts.Length >= 2)
            {
                dateStr = parts[^1];
                tableName = string.Join("_", parts.Take(parts.Length - 1));
            }

            var validChartNames = new HashSet<string>(
                templateConfig.Charts.Select(c => c.MergeField),
                StringComparer.OrdinalIgnoreCase);

            // Console.WriteLine("== DEBUG: Danh sách MergeField hợp lệ từ config:");
            // foreach (var name in validChartNames)
            //     Console.WriteLine($" - MergeField = '{name}'");

            foreach (var chart in sheetChart.Charts)
            {
                var chartName = chart.Name?.Trim();
                if (string.IsNullOrWhiteSpace(chartName)) continue;

                if (!validChartNames.Contains(chartName))
                {
                    Console.WriteLine($"⚠️ Bỏ qua chart '{chartName}' vì không nằm trong cấu hình Charts.");
                    continue;
                }

                var imagePath = Path.Combine(exportDir, $"chart_{tableName}_{dateStr}_{chartName}.png");
                chart.ToImage(imagePath, new ImageOrPrintOptions
                {
                    ImageType = ImageType.Png,
                    Quality = 100,
                    HorizontalResolution = 96,
                    VerticalResolution = 96
                });

                fieldToImageMap[chartName] = imagePath;
                Console.WriteLine($"✅ Exported chart '{chartName}' to {imagePath}");
            }

            // Export Word
            string wordTemplatePath = Path.Combine("templates", templateConfig.TemplateWord);
            await new ChartWordExporter().InsertChartsAndMergeFields(
               wordTemplatePath,
               wordOutputPath,
               fieldToImageMap,
               request.MergeFields ?? new(),
               templateConfig.DataJson ?? new(),
               request.DataJson ?? new(),
               templateConfig
            );

            // Export PDF
            new ChartWordExporter().ExportToPdf(wordOutputPath, pdfOutputPath);

            if (!File.Exists(pdfOutputPath))
                throw new FileNotFoundException("PDF file not found after export.", pdfOutputPath);

            var fi = new FileInfo(pdfOutputPath);
            if (fi.Length == 0)
                throw new Exception("PDF file is empty — export may have failed.");

            string fileNameUpload = Path.GetFileName(pdfOutputPath);
            string fileUrl = await _storageService.UploadPdfAsync(pdfOutputPath, "reports", fileNameUpload);

            return fileUrl ?? pdfOutputPath;
        }

    }
}

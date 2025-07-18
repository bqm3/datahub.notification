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
// Add the correct namespace for IStorageService below if it exists, for example:
namespace microservice.mess.Documents
{
    // If IStorageService does not exist, define a minimal interface for compilation:
    public class SgiPdfChart
    {
        private readonly IStorageService _storageService;

        public SgiPdfChart(IStorageService storageService)
        {
            _storageService = storageService;
        }
        public void GenerateCharts(ChartInputConfig config)
        {
            var workbook = new Workbook();
            var sheet = workbook.Worksheets[0];
            sheet.Name = "Data";

            // Ghi dữ liệu vào bảng
            // Ghi header
            for (int col = 0; col < config.Data.Columns.Count; col++)
            {
                sheet.Cells[0, col].PutValue(config.Data.Columns[col].ColumnName);
            }

            // Ghi dữ liệu
            for (int row = 0; row < config.Data.Rows.Count; row++)
            {
                for (int col = 0; col < config.Data.Columns.Count; col++)
                {
                    sheet.Cells[row + 1, col].PutValue(config.Data.Rows[row][col]);
                }
            }


            int rowCount = config.Data.Rows.Count;

            // Xác định vị trí cột
            int topicCol = config.Data.Columns.IndexOf(config.CategoryColumn);
            int totalCol = config.Data.Columns.IndexOf(config.TotalColumn);
            int facebookCol = config.Data.Columns.IndexOf(config.FacebookColumn);
            int telegramCol = config.Data.Columns.IndexOf(config.TelegramColumn);

            string topicRange = $"{GetColLetter(topicCol)}2:{GetColLetter(topicCol)}{rowCount + 1}";
            string totalRange = $"{GetColLetter(totalCol)}2:{GetColLetter(totalCol)}{rowCount + 1}";
            string facebookRange = $"{GetColLetter(facebookCol)}2:{GetColLetter(facebookCol)}{rowCount + 1}";
            string telegramRange = $"{GetColLetter(telegramCol)}2:{GetColLetter(telegramCol)}{rowCount + 1}";

            // Biểu đồ Doughnut
            var chart1 = sheet.Charts[sheet.Charts.Add(ChartType.Doughnut, 7, 0, 22, 6)];
            chart1.Title.Text = config.DoughnutTitle;
            chart1.NSeries.Add($"=Data!{totalRange}", true);
            chart1.NSeries.CategoryData = $"Data!{topicRange}";

            // Biểu đồ Pie
            var chart2 = sheet.Charts[sheet.Charts.Add(ChartType.Pie, 7, 7, 22, 13)];
            chart2.Title.Text = config.PieTitle;
            chart2.NSeries.Add($"=Data!{facebookRange}", true);
            chart2.NSeries.CategoryData = $"Data!{topicRange}";

            // Biểu đồ Cột
            var chart3 = sheet.Charts[sheet.Charts.Add(ChartType.Column, 24, 0, 42, 10)];
            chart3.Title.Text = config.ColumnTitle;
            chart3.NSeries.Add($"=Data!{facebookRange}", true);
            chart3.NSeries[0].Name = config.FacebookColumn;
            chart3.NSeries.Add($"=Data!{telegramRange}", true);
            chart3.NSeries[1].Name = config.TelegramColumn;
            chart3.NSeries.CategoryData = $"Data!{topicRange}";

            var exportDir = "Exports";
            if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

            ExportChart(chart1, Path.Combine(exportDir, "chart1_doughnut.png"));
            ExportChart(chart2, Path.Combine(exportDir, "chart2_pie.png"));
            ExportChart(chart3, Path.Combine(exportDir, "chart3_column.png"));
        }
        private void ExportChart(Chart chart, string filePath)
        {
            var options = new ImageOrPrintOptions
            {
                ImageType = ImageType.Png,
                HorizontalResolution = 150,
                VerticalResolution = 200
            };

            using var stream = new FileStream(filePath, FileMode.Create);
            chart.ToImage(stream, options);
        }
        private List<string> ResolveSeriesAndEnsureTotal(
            List<string> configSeries,
            List<string> allColumns,
            List<Dictionary<string, object>> dataRows,
            string categoryCol,
            out bool totalWasGenerated
            )
        {
            totalWasGenerated = false;

            var excluded = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                categoryCol, "Tổng", "Total", "Sum"
            };

            bool autoDetect = configSeries == null || configSeries.Count == 0 || configSeries.Contains("__AUTO_PLATFORM__");
            List<string> resultSeries = autoDetect
                ? allColumns.Where(col => !excluded.Contains(col)).ToList()
                : configSeries;

            // Nếu biểu đồ yêu cầu dùng "Tổng" nhưng cột này không tồn tại => tạo thủ công
            if (allColumns.All(c => !string.Equals(c, "Tổng", StringComparison.OrdinalIgnoreCase)))
            {
                var sumColName = "Tổng";
                foreach (var row in dataRows)
                {
                    double sum = 0;
                    foreach (var col in resultSeries)
                    {
                        if (row.TryGetValue(col, out var val) && double.TryParse(val?.ToString(), out double num))
                            sum += num;
                    }
                    row[sumColName] = sum;
                }

                allColumns.Add(sumColName);
                totalWasGenerated = true;
            }

            return resultSeries;
        }

        public async Task<string> GenerateFromJson(ChartJsonRequest request, string templateName, string wordOutputPath, string pdfOutputPath)
        {
            if (request.Data.Count == 0)
                return "No data provided";

            var dataRows = request.Data.ToList();

            var columnNames = request.Data[0].Keys.ToList();
            var exportDir = "Exports";
            if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

            // Load config từ template
            string configPath = Path.Combine("templates/configs", Path.ChangeExtension(templateName, ".json"));
            if (!File.Exists(configPath))
                throw new Exception($"Không tìm thấy cấu hình cho template: {templateName}");

            var configJson = await File.ReadAllTextAsync(configPath);
            var templateConfig = JsonConvert.DeserializeObject<TemplateConfig>(configJson)!;

            // Load Excel template
            var excelTemplatePath = Path.Combine("templates", templateConfig?.TemplateExcel);
            var workbook = File.Exists(excelTemplatePath)
                ? new Workbook(excelTemplatePath)
                : throw new Exception("Excel template file not found: " + excelTemplatePath);

            var sheetData = workbook.Worksheets["SheetData"] ?? workbook.Worksheets[0];
            sheetData.Name = "SheetData";

            // Ghi header
            for (int col = 0; col < columnNames.Count; col++)
                sheetData.Cells[0, col].PutValue(columnNames[col]);

            // Ghi dữ liệu
            for (int row = 0; row < dataRows.Count; row++)
            {
                for (int col = 0; col < columnNames.Count; col++)
                {
                    var value = dataRows[row][columnNames[col]];
                    if (value is long l) sheetData.Cells[row + 1, col].PutValue((double)l);
                    else if (value is int i) sheetData.Cells[row + 1, col].PutValue((double)i);
                    else if (value is double d) sheetData.Cells[row + 1, col].PutValue(d);
                    else if (value is string s && double.TryParse(s, out double num)) sheetData.Cells[row + 1, col].PutValue(num);
                    else sheetData.Cells[row + 1, col].PutValue(value?.ToString() ?? "");
                }
            }

            // Export chart images based on chart name
            var fieldToImageMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var sheetChart = workbook.Worksheets["SheetChart"];

            foreach (var chart in sheetChart.Charts)
            {
                var chartName = chart.Name;
                if (string.IsNullOrWhiteSpace(chartName))
                {
                    Console.WriteLine("Chart without name skipped.");
                    continue;
                }

                var matchedConfig = templateConfig.Charts.FirstOrDefault(c => c.MergeField.Equals(chartName, StringComparison.OrdinalIgnoreCase));
                if (matchedConfig == null)
                {
                    Console.WriteLine($"Chart name '{chartName}' not found in config, skipped.");
                    continue;
                }

                var imagePath = Path.Combine(exportDir, $"chart_{chartName}.png");
                chart.ToImage(imagePath, new ImageOrPrintOptions
                {
                    ImageType = ImageType.Png,
                    Quality = 100,
                    HorizontalResolution = 96,
                    VerticalResolution = 96
                });

                fieldToImageMap[chartName] = imagePath;
                Console.WriteLine($"Exported chart '{chartName}' to {imagePath}");
            }


            // 1. Tạo file Word
            string templatePath = Path.Combine("templates", templateName);
            new ChartWordExporter().InsertChartsAndMergeFields(
                 templatePath,
                 wordOutputPath,
                 fieldToImageMap,
                 request.MergeFields ?? templateConfig.MergeFields ?? new(),
                 request.DataJson,
                 templateConfig
            );

            string pdfPath = pdfOutputPath; 

            // 1. Export PDF trước
            try
            {
                new ChartWordExporter().ExportToPdf(wordOutputPath, pdfPath);
                Console.WriteLine("pdfPath 2: " + pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ExportToPdf failed: " + ex.Message);
                throw;
            }


            // 2. Kiểm tra file tồn tại và dung lượng
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found after export.", pdfPath);

            FileInfo fi = new FileInfo(pdfPath);

            if (fi.Length == 0)
                throw new Exception("PDF file is empty — export may have failed.");

            // 3. Upload
            string fileName = Path.GetFileName(pdfPath);
            string fileUrl = await _storageService.UploadPdfAsync(pdfPath, "reports", fileName);

            // 4. Trả về kết quả
            return fileUrl ?? pdfPath;

        }

        private string GetColLetter(int colIndex)
        {
            var dividend = colIndex + 1;
            string colLetter = "";
            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                colLetter = Convert.ToChar(65 + modulo) + colLetter;
                dividend = (dividend - modulo) / 26;
            }
            return colLetter;
        }
    }
}
using Aspose.Cells;
using Aspose.Cells.Charts;
using Aspose.Cells.Rendering;
using Aspose.Cells.Drawing;
using System.IO;
using System.Data;
using microservice.mess.Models.Message;
using microservice.mess.Models;

namespace microservice.mess.Documents
{
    public class SgiPdfChart
    {
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

        // public void GenerateFromJson(ChartJsonRequest request)
        // {
        //     var workbook = new Workbook();
        //     var sheet = workbook.Worksheets[0];
        //     sheet.Name = "Data";

        //     if (request.Data.Count == 0)
        //     {
        //         Console.WriteLine("No data provided.");
        //         return;
        //     }

        //     // Skip the header row and get actual data
        //     var dataRows = request.Data.Skip(1).ToList();
        //     if (dataRows.Count == 0)
        //     {
        //         Console.WriteLine(" No valid data after filtering.");
        //         return;
        //     }

        //     Console.WriteLine($"Processing {dataRows.Count} data rows");

        //     // Get column names from first row
        //     var columnNames = request.Data[0].Keys.ToList();

        //     // Write headers to Excel
        //     for (int col = 0; col < columnNames.Count; col++)
        //     {
        //         sheet.Cells[0, col].PutValue(columnNames[col]);
        //     }

        //     // Write data to Excel (converting to proper types)
        //     for (int row = 0; row < dataRows.Count; row++)
        //     {
        //         var dataRow = dataRows[row];
        //         for (int col = 0; col < columnNames.Count; col++)
        //         {
        //             var columnName = columnNames[col];
        //             var value = dataRow[columnName];

        //             // Convert to appropriate type
        //             if (value is long l)
        //             {
        //                 sheet.Cells[row + 1, col].PutValue((double)l);
        //             }
        //             else if (value is int i)
        //             {
        //                 sheet.Cells[row + 1, col].PutValue((double)i);
        //             }
        //             else if (value is double d)
        //             {
        //                 sheet.Cells[row + 1, col].PutValue(d);
        //             }
        //             else if (value is string s && double.TryParse(s, out double num))
        //             {
        //                 sheet.Cells[row + 1, col].PutValue(num);
        //             }
        //             else
        //             {
        //                 sheet.Cells[row + 1, col].PutValue(value?.ToString() ?? "");
        //             }
        //         }
        //     }

        //     int dataRowCount = dataRows.Count;
        //     var exportDir = "Exports";
        //     if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

        //     int chartIndex = 1;
        //     foreach (var chartDef in request.Charts)
        //     {
        //         Console.WriteLine($"Creating chart: {chartDef.Title}");

        //         ChartType chartType;
        //         try
        //         {
        //             chartType = (ChartType)Enum.Parse(typeof(ChartType), chartDef.Type, ignoreCase: true);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($" Invalid chart type '{chartDef.Type}': {ex.Message}");
        //             continue;
        //         }

        //         // Add chart
        //         var chart = sheet.Charts[sheet.Charts.Add(chartType, chartIndex * 20, 0, chartIndex * 20 + 15, 10)];

        //         // Set chart title
        //         chart.Title.Text = chartDef.Title;
        //         chart.Title.Font.Name = "Arial Unicode MS";
        //         chart.Title.Font.Size = 12;

        //         // Clear any existing series
        //         chart.NSeries.Clear();

        //         // Find category column index
        //         int categoryColIndex = columnNames.IndexOf(chartDef.Category);
        //         if (categoryColIndex < 0)
        //         {
        //             Console.WriteLine($" Category column '{chartDef.Category}' not found.");
        //             continue;
        //         }

        //         // Add series
        //         bool hasValidSeries = false;
        //         foreach (var seriesName in chartDef.Series)
        //         {
        //             int seriesColIndex = columnNames.IndexOf(seriesName);
        //             if (seriesColIndex < 0)
        //             {
        //                 Console.WriteLine($" Series column '{seriesName}' not found.");
        //                 continue;
        //             }

        //             // Create the data range for this series (skip header row)
        //             string seriesRange = $"{GetColLetter(seriesColIndex)}2:{GetColLetter(seriesColIndex)}{dataRowCount + 1}";
        //             string categoryRange = $"{GetColLetter(categoryColIndex)}2:{GetColLetter(categoryColIndex)}{dataRowCount + 1}";

        //             Console.WriteLine($"Adding series '{seriesName}' with range: {seriesRange}");
        //             Console.WriteLine($"Category range: {categoryRange}");

        //             // Add series
        //             var seriesIndex = chart.NSeries.Add(seriesRange, true);
        //             chart.NSeries[seriesIndex].Name = seriesName;
        //             chart.NSeries[seriesIndex].XValues = categoryRange;

        //             hasValidSeries = true;
        //         }

        //         if (!hasValidSeries)
        //         {
        //             Console.WriteLine($" No valid series found for chart '{chartDef.Title}'");
        //             continue;
        //         }

        //         // Configure chart based on type
        //         if (chartType == ChartType.Pie || chartType == ChartType.Doughnut)
        //         {
        //             // For pie/doughnut charts, show data labels
        //             foreach (var series in chart.NSeries)
        //             {
        //                 series.DataLabels.ShowValue = true;
        //                 series.DataLabels.ShowPercentage = true;
        //                 series.DataLabels.ShowCategoryName = true;
        //             }
        //         }
        //         else if (chartType == ChartType.Column)
        //         {
        //             // For column charts, show values on bars
        //             foreach (var series in chart.NSeries)
        //             {
        //                 series.DataLabels.ShowValue = true;
        //             }
        //         }

        //         // Set chart area properties
        //         chart.ChartArea.Border.IsVisible = false;
        //         chart.PlotArea.Border.IsVisible = false;

        //         // Export chart
        //         try
        //         {
        //             // Force calculation
        //             workbook.CalculateFormula();
        //             chart.Calculate();

        //             // Create image options
        //             var imageOptions = new ImageOrPrintOptions();
        //             imageOptions.ImageType = ImageType.Png;
        //             imageOptions.Quality = 100;
        //             imageOptions.HorizontalResolution = 300;
        //             imageOptions.VerticalResolution = 300;

        //             string fileName = $"chart{chartIndex}_{chartDef.Type.ToLower()}.png";
        //             string filePath = Path.Combine(exportDir, fileName);

        //             chart.ToImage(filePath, imageOptions);
        //             Console.WriteLine($"=> Chart exported: {fileName}");
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($" Error exporting chart: {ex.Message}");
        //         }

        //         chartIndex++;
        //     }

        //     // Save workbook for debugging
        //     try
        //     {
        //         workbook.Save(Path.Combine(exportDir, "debug.xlsx"), SaveFormat.Xlsx);
        //         Console.WriteLine("=> Workbook saved: debug.xlsx");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($" Error saving workbook: {ex.Message}");
        //     }
        // }

        // // Helper method to convert column index to Excel column letter
        // private string GetColLetter(int colIndex)
        // {
        //     string result = "";
        //     while (colIndex >= 0)
        //     {
        //         result = (char)('A' + colIndex % 26) + result;
        //         colIndex = colIndex / 26 - 1;
        //     }
        //     return result;
        // }

        private void ExportChart(Chart chart, string filePath)
        {
            var options = new ImageOrPrintOptions
            {
                ImageType = ImageType.Png,
                HorizontalResolution = 200,
                VerticalResolution = 200
            };

            using var stream = new FileStream(filePath, FileMode.Create);
            chart.ToImage(stream, options);
        }

        public void GenerateFromJson(ChartJsonRequest request)
        {
            var workbook = new Workbook();
            var sheet = workbook.Worksheets[0];
            sheet.Name = "Data";

            if (request.Data.Count == 0)
            {
                Console.WriteLine(" No data provided.");
                return;
            }

            var dataRows = request.Data.Skip(1).ToList(); 
            if (dataRows.Count == 0)
            {
                Console.WriteLine(" No valid data after filtering.");
                return;
            }

            var columnNames = request.Data[0].Keys.ToList();

            // Write headers
            for (int col = 0; col < columnNames.Count; col++)
                sheet.Cells[0, col].PutValue(columnNames[col]);

            // Write data
            for (int row = 0; row < dataRows.Count; row++)
            {
                var dataRow = dataRows[row];
                for (int col = 0; col < columnNames.Count; col++)
                {
                    var columnName = columnNames[col];
                    var value = dataRow[columnName];

                    if (value is long l) sheet.Cells[row + 1, col].PutValue((double)l);
                    else if (value is int i) sheet.Cells[row + 1, col].PutValue((double)i);
                    else if (value is double d) sheet.Cells[row + 1, col].PutValue(d);
                    else if (value is string s && double.TryParse(s, out double num)) sheet.Cells[row + 1, col].PutValue(num);
                    else sheet.Cells[row + 1, col].PutValue(value?.ToString() ?? "");
                }
            }

            var exportDir = "Exports";
            if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

            int chartIndex = 1;
            List<string> exportedChartPaths = new();

            foreach (var chartDef in request.Charts)
            {
                Console.WriteLine($" Creating chart: {chartDef.Title}");

                ChartType chartType;
                try
                {
                    chartType = (ChartType)Enum.Parse(typeof(ChartType), chartDef.Type, true);
                }
                catch
                {
                    Console.WriteLine($" Invalid chart type: {chartDef.Type}");
                    continue;
                }

                var chart = sheet.Charts[sheet.Charts.Add(chartType, chartIndex * 20, 0, chartIndex * 20 + 15, 10)];
                chart.Title.Text = chartDef.Title;
                chart.Title.Font.Name = "Arial Unicode MS";
                chart.Title.Font.Size = 12;
                chart.NSeries.Clear();

                int categoryColIndex = columnNames.IndexOf(chartDef.Category);
                if (categoryColIndex < 0)
                {
                    Console.WriteLine($" Category column '{chartDef.Category}' not found.");
                    continue;
                }

                bool hasSeries = false;
                foreach (var seriesName in chartDef.Series)
                {
                    int seriesColIndex = columnNames.IndexOf(seriesName);
                    if (seriesColIndex < 0)
                    {
                        Console.WriteLine($" Series column '{seriesName}' not found.");
                        continue;
                    }

                    string seriesRange = $"=Data!{GetColLetter(seriesColIndex)}2:{GetColLetter(seriesColIndex)}{dataRows.Count + 1}";
                    string categoryRange = $"=Data!{GetColLetter(categoryColIndex)}2:{GetColLetter(categoryColIndex)}{dataRows.Count + 1}";

                    var index = chart.NSeries.Add(seriesRange, true);
                    chart.NSeries[index].Name = seriesName;
                    chart.NSeries[index].XValues = categoryRange;

                    hasSeries = true;
                }

                if (!hasSeries)
                {
                    Console.WriteLine($" No valid series for chart '{chartDef.Title}'");
                    continue;
                }

                if (chartType == ChartType.Pie || chartType == ChartType.Doughnut)
                {
                    foreach (Series series in chart.NSeries)
                    {
                        series.DataLabels.ShowValue = true;
                        series.DataLabels.ShowPercentage = true;
                        series.DataLabels.ShowCategoryName = true;
                    }
                }
                else
                {
                    foreach (Series series in chart.NSeries)
                    {
                        series.DataLabels.ShowValue = true;
                    }
                }

                chart.ChartArea.Border.IsVisible = false;
                chart.PlotArea.Border.IsVisible = false;

                workbook.CalculateFormula();
                chart.Calculate();

                var imagePath = Path.Combine(exportDir, $"chart{chartIndex}_{chartDef.Type.ToLower()}.png");
                chart.ToImage(imagePath, new ImageOrPrintOptions
                {
                    ImageType = ImageType.Png,
                    Quality = 100,
                    HorizontalResolution = 300,
                    VerticalResolution = 300
                });

                exportedChartPaths.Add(imagePath);
                Console.WriteLine($" Exported: {imagePath}");
                chartIndex++;
            }

            // workbook.Save(Path.Combine(exportDir, "debug.xlsx"), SaveFormat.Xlsx);

            // Export to Word template
            new ChartWordExporter().InsertChartsIntoTemplate("templates/temp.docx",
                    Path.Combine(exportDir, "final_report.docx"),
                    exportedChartPaths);
            string wordOutputPath = "Exports/final_report.docx";
            string pdfOutputPath = "Exports/final_report.pdf";

            // new ChartWordExporter().ExportToPdf(wordOutputPath, pdfOutputPath);
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
using Aspose.Words;
using Aspose.Words.Replacing;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using System.Drawing;
using Aspose.Words.Fields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using microservice.mess.Documents;
using microservice.mess.Models;
using microservice.mess.Helpers;
using microservice.mess.Models.Message;

public class ChartWordExporter
{
    private readonly ILogger _logger;

    public async Task InsertChartsAndMergeFields(
        string templatePath,
        string outputPath,
        Dictionary<string, string> imagePaths,
        Dictionary<string, string> mergeFields,
        Dictionary<string, string> dataJsonMap,
        List<DataJsonCategory> groupedArticles,
        TemplateConfig config
    )
    {
        var doc = new Document(templatePath);
        var builder = new DocumentBuilder(doc);

        // * MergeFields
        // Cấu hình style cho các field cụ thể
        var fieldStyles = new Dictionary<string, MergeFieldStyle>(StringComparer.OrdinalIgnoreCase)
        {
            ["TITLE"] = new MergeFieldStyle
            {
                FontSize = 16,
                FontColor = Color.White,
                Bold = true,
                FontName = "Arial"
            },
            ["LOCATION"] = new MergeFieldStyle
            {
                FontSize = 24,
                FontColor = Color.Yellow,
                FontName = "Arial"
            },
            ["BAN_TIN_NGAY"] = new MergeFieldStyle
            {
                FontSize = 12,
                FontColor = Color.Yellow,
                FontName = "Arial"
            }
        };

        var defaultStyle = new MergeFieldStyle
        {
            FontName = "Tahoma",
            FontSize = 12,
            FontColor = Color.FromArgb(33, 33, 33),
            Bold = false
        };

        // Xử lý placeholder ngày {{before}}, {{now}}
        var now = DateTime.Now;
        string nowDate = now.ToString("dd/MM/yyyy");
        string beforeDate = now.AddDays(-1).ToString("dd/MM/yyyy");

        var resolvedMergeFields = mergeFields.ToDictionary(
            kv => kv.Key,
            kv => kv.Value
                .Replace("{{now}}", nowDate)
                .Replace("{{before}}", beforeDate)
                .Replace("{{send_date}}", nowDate)
        );

        // Replace fields với style
        foreach (var field in resolvedMergeFields)
        {
            MergeFieldStyle style = fieldStyles.TryGetValue(field.Key, out var customStyle)
                ? customStyle
                : defaultStyle;

            var options = new FindReplaceOptions
            {
                Direction = FindReplaceDirection.Forward,
                ReplacingCallback = new StyledMergeFieldReplacer(
                    field.Value,
                    fontName: style.FontName,
                    fontSize: style.FontSize,
                    fontColor: style.FontColor,
                    isBold: style.Bold
                )
            };

            doc.Range.Replace($"«{field.Key}»", "", options);
        }

        // IMAGE
        var maxWidth = 1584.0;
        var maxHeight = 1584.0;

        // Tạo dictionary cho kích thước chart từ config
        var chartSizes = new Dictionary<string, (double Width, double Height)>(StringComparer.OrdinalIgnoreCase);

        if (config?.Charts != null)
        {
            foreach (var chart in config.Charts)
            {
                if (!string.IsNullOrWhiteSpace(chart.MergeField) &&
                    chart.Width.HasValue && chart.Height.HasValue)
                {
                    chartSizes[chart.MergeField] = (
                        Width: (double)chart.Width.Value,
                        Height: (double)chart.Height.Value
                    );
                    Console.WriteLine($"Loaded config size for {chart.MergeField}: {chart.Width}x{chart.Height}");
                }
            }
        }

        foreach (var kv in imagePaths)
        {
            string fieldName = kv.Key;
            string imagePath = kv.Value;

            // Kiểm tra file ảnh có tồn tại không
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Warning: Image file not found: {imagePath}");
                continue;
            }

            // Tìm và thay thế merge field với ảnh
            foreach (Table table in doc.GetChildNodes(NodeType.Table, true))
            {
                foreach (Row row in table.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        // Tìm merge field trong cell
                        var fieldsToRemove = new List<Field>();

                        foreach (Field field in cell.Range.Fields)
                        {
                            if (field.Type != FieldType.FieldMergeField) continue;

                            string fieldCode = field.GetFieldCode();
                            if (!fieldCode.Contains(fieldName)) continue;

                            // Di chuyển builder đến vị trí field
                            builder.MoveTo(field.Start);

                            try
                            {
                                using var imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                                var shape = builder.InsertImage(imgStream);

                                // Thiết lập kích thước ảnh
                                SetImageSize(shape, fieldName, chartSizes, cell, row, maxWidth, maxHeight, imagePath);

                                // Căn giữa ảnh trong cell
                                shape.RelativeHorizontalPosition = RelativeHorizontalPosition.Column;
                                shape.RelativeVerticalPosition = RelativeVerticalPosition.Paragraph;
                                shape.HorizontalAlignment = HorizontalAlignment.Center;
                                shape.VerticalAlignment = VerticalAlignment.Center;

                                fieldsToRemove.Add(field);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error inserting image {imagePath}: {ex.Message}");
                            }
                        }

                        // Xóa các field đã được thay thế
                        foreach (var field in fieldsToRemove)
                        {
                            field.Remove();
                        }
                    }
                }
            }
        }

        // DATA JSON
        // if (dataJsonMap != null && dataJsonMap.Any())
        // {
         await InsertArticleBlocksFromDataJson(doc, builder, dataJsonMap, groupedArticles);
        // }

        try
        {
            // Đảm bảo thư mục đích tồn tại
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save file Word
            doc.Save(outputPath, SaveFormat.Docx);
            Console.WriteLine($"=> Word file generated: {outputPath}");

            // Kiểm tra file được tạo thành công
            if (File.Exists(outputPath))
            {
                FileInfo fileInfo = new FileInfo(outputPath);
                Console.WriteLine($"Word file size: {fileInfo.Length} bytes");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving Word file: {ex.Message}");
            throw;
        }
    }


    public void ExportToPdf(string docxPath, string pdfPath)
    {
        if (!File.Exists(docxPath))
            throw new FileNotFoundException("Word file not found: " + docxPath);

        try
        {
            var doc = new Document(docxPath);
            doc.Save(pdfPath, SaveFormat.Pdf);
            Console.WriteLine($"=> PDF file generated: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("ExportToPdf failed: " + ex.Message);
            throw;
        }
    }


    public static string GetCategoryAbbreviation(string category)
    {
        return string.Concat(category
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => char.ToUpperInvariant(word[0])));
    }

    private void SetImageSize(
        Shape shape,
        string fieldName,
        Dictionary<string, (double Width, double Height)> chartSizes,
        Cell cell,
        Row row,
        double maxWidth,
        double maxHeight,
        string imagePath
    )
    {

        if (chartSizes.TryGetValue(fieldName, out var configSize))
        {
            try
            {
                using var img = System.Drawing.Image.FromFile(imagePath); //  dùng đúng file ảnh
                double imgWidthPx = img.Width;
                double imgHeightPx = img.Height;

                // Convert px -> pt (1 pt = 1/72 inch), assume 96 DPI
                double imgWidthPt = imgWidthPx * 72.0 / 96.0;
                double imgHeightPt = imgHeightPx * 72.0 / 96.0;

                // Tính tỉ lệ để resize ảnh đúng theo config pt
                double scaleX = configSize.Width / imgWidthPt;
                double scaleY = configSize.Height / imgHeightPt;
                double finalScale = Math.Min(scaleX, scaleY);

                shape.Width = imgWidthPt * finalScale;
                shape.Height = imgHeightPt * finalScale;

                Console.WriteLine($"Scaled image for {fieldName}: {shape.Width}x{shape.Height} from file {imagePath}");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read image for {fieldName}: {ex.Message}");
            }
        }

        // fallback auto-size
        double cellWidth = CalculateCellWidth(cell);
        double cellHeight = row.RowFormat.Height > 0 ? row.RowFormat.Height : shape.Height;

        double targetWidth = Math.Min(cellWidth * 0.9, maxWidth);
        double targetHeight = Math.Min(cellHeight * 0.9, maxHeight);

        double widthRatio = targetWidth / shape.Width;
        double heightRatio = targetHeight / shape.Height;
        double autoScale = Math.Min(widthRatio, heightRatio);

        shape.Width *= autoScale;
        shape.Height *= autoScale;

        Console.WriteLine($"Auto-sized {fieldName}: {shape.Width}x{shape.Height} (scale: {autoScale:F2})");
    }


    // Method helper để tính chiều rộng cell (bao gồm merged cells)
    private double CalculateCellWidth(Cell cell)
    {
        double totalWidth = cell.CellFormat.Width;

        // Xử lý merged cells
        if (cell.CellFormat.HorizontalMerge != CellMerge.None)
        {
            totalWidth = 0;
            var parentRow = cell.ParentRow;
            int cellIndex = parentRow.Cells.IndexOf(cell);

            for (int i = cellIndex; i < parentRow.Cells.Count; i++)
            {
                var currentCell = parentRow.Cells[i];
                totalWidth += currentCell.CellFormat.Width;

                // Dừng khi gặp cell không merge
                if (currentCell.CellFormat.HorizontalMerge == CellMerge.None)
                    break;
            }
        }

        return totalWidth;
    }
    public async Task InsertArticleBlocksFromDataJson(
    Document doc,
    DocumentBuilder builder,
    Dictionary<string, string> dataJsonMap, // key = field trong Word, value = chuyên mục hiển thị
    List<DataJsonCategory> allGroupedArticles) // data thực sự của các chuyên mục
    {
        foreach (var kv in dataJsonMap)
        {
            string fieldName = kv.Key;              // ví dụ: "SOCIAL_AGI_GITH"
            string displayCategory = kv.Value;      // ví dụ: "GIAO THÔNG"

            Console.WriteLine($"[DataJsonMap] Field: {fieldName}, DisplayCategory: {displayCategory}");

            var articleData = allGroupedArticles
                .FirstOrDefault(c => string.Equals(c.Category, displayCategory, StringComparison.OrdinalIgnoreCase));

            if (articleData == null)
            {
                Console.WriteLine($"[DataJsonMap] ❌ Không tìm thấy dữ liệu cho chuyên mục '{displayCategory}'");
                continue;
            }

            Console.WriteLine($"[DataJsonMap] ✅ Tìm thấy {articleData.Data?.Count ?? 0} bài viết cho chuyên mục '{displayCategory}'");

            foreach (Field field in doc.Range.Fields)
            {
                if (field.Type == FieldType.FieldMergeField)
                {
                    string fieldCode = field.GetFieldCode();
                    // Console.WriteLine($"[FieldCheck] Đang kiểm tra field code: {fieldCode}");

                    if (fieldCode.Contains(fieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[FieldMatch] ↪ Đã match field «{fieldName}» với merge field trong Word");

                        builder.MoveToField(field, true);

                        // Render với tiêu đề category
                       await HtmlArticleRenderer.RenderArticlesWithLayout(builder, displayCategory, articleData.Data);

                        field.Remove();
                    }
                }
            }
        }
    }
}

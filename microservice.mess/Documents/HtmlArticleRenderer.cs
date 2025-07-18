using System.Net.Http;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Replacing;
using Aspose.Words.Drawing;
using System.Drawing;
using SkiaSharp;
using Aspose.Words.Fields;
using microservice.mess.Models.Message;
using microservice.mess.Models;

namespace microservice.mess.Documents
{
    public static class HtmlArticleRenderer
    {
        public static List<string> downloadedImages;
        public static void RenderArticlesWithLayout(DocumentBuilder builder, string category, List<ArticleItem> articles)
        {
            // Render category header with custom layout
            RenderCategoryHeader(builder, category);

            foreach (var article in articles)
            {
                string title = article.Title ?? "";
                string content = article.Content ?? "";
                string author = article.Author ?? "";
                string createdAt = article.CreatedAt ?? "";
                string url = article.ArticleUrl ?? "";
                string image = article.Image ?? "";

                string shortTitle = title.Length > 120 ? title[..117] + "..." : title;
                string shortContent = content.Length > 400 ? content[..397] + "..." : content;
                string timeFormatted = DateTime.TryParse(createdAt, out var parsed)
                    ? parsed.ToString("dd/MM/yyyy HH:mm")
                    : createdAt;

                // Start table
                Table table = builder.StartTable();

                // Column: Image
                builder.InsertCell();
                builder.CellFormat.Width = 120;
                builder.RowFormat.Height = 150;
                builder.RowFormat.HeightRule = HeightRule.Exactly;
                builder.CellFormat.Borders.LineStyle = LineStyle.None;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Top;
                builder.CellFormat.Shading.BackgroundPatternColor = Color.Empty;

                if (!string.IsNullOrWhiteSpace(image))
                {
                    var imgStream = DownloadImageStream(image);
                    if (imgStream != null)
                    {
                        try
                        {
                            builder.InsertImage(imgStream, RelativeHorizontalPosition.Margin, 0,
                                    RelativeVerticalPosition.Margin, 0,
                                    120, 120, WrapType.None);

                        }
                        catch (Exception ex)
                        {
                            builder.Writeln("");
                        }
                    }
                    else
                    {
                        builder.Writeln("");
                    }
                }
                else
                {
                    builder.Writeln("");
                }

                // Column: Text content
                builder.InsertCell();
                builder.CellFormat.Width = 360;
                builder.CellFormat.Borders.LineStyle = LineStyle.None;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Top;
                builder.CellFormat.Shading.BackgroundPatternColor = Color.Empty;
                builder.ParagraphFormat.LeftIndent = 4;

                builder.Font.Bold = true;
                builder.Font.Size = 11;
                builder.Font.Color = Color.FromArgb(19, 135, 236); // #1387EC
                builder.Writeln(shortTitle);

                builder.Font.Bold = false;
                builder.Font.Size = 9;
                builder.Font.Color = Color.FromArgb(85, 85, 85); // #555
                builder.Writeln($"{timeFormatted} - {author}");

                builder.Font.Size = 10;
                builder.Font.Color = Color.Black;
                builder.Writeln(shortContent);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    builder.Font.Size = 9;
                    builder.Font.Color = Color.FromArgb(13, 71, 161); // #0d47a1
                    builder.InsertHyperlink("Link bài", url, false);
                }

                builder.RowFormat.AllowBreakAcrossPages = false;
                builder.EndRow();
                builder.EndTable();

                builder.Writeln(); // Space after article
            }

            builder.Writeln(); // Space after category
        }

        public static void RenderCategoryHeader(DocumentBuilder builder, string category)
        {
            Table table = builder.StartTable();

            builder.InsertCell();
            builder.CellFormat.PreferredWidth = PreferredWidth.Auto;
            builder.CellFormat.Borders.LineStyle = LineStyle.None;
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(18, 98, 168); // #1262A8
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.SpaceBefore = 4;
            builder.ParagraphFormat.SpaceAfter = 4;

            builder.Font.Name = "Arial";
            builder.Font.Size = 16;
            builder.Font.Color = Color.White;
            builder.Font.Bold = true;

            builder.Write(category.ToUpperInvariant());

            builder.EndRow();
            table.PreferredWidth = PreferredWidth.Auto;
            table.AllowAutoFit = true;
            builder.EndTable();

            builder.Writeln();
        }

        // Download url image => pdf
        private static Stream? DownloadImageStream(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) return null;

                var stream = response.Content.ReadAsStreamAsync().Result;
                var mem = new MemoryStream();
                stream.CopyTo(mem);
                mem.Position = 0;
                return mem;
            }
            catch
            {
                return null;
            }
        }


    }

}
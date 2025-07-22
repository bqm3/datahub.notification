using System.Net.Http;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Replacing;
using Aspose.Words.Drawing;
using System.Drawing;
using SkiaSharp;
using System.Text;
using Aspose.Words.Fields;
using microservice.mess.Models.Message;
using microservice.mess.Models;

namespace microservice.mess.Documents
{
    public static class HtmlArticleRenderer
    {
        public static List<string> downloadedImages;
        public static async Task RenderArticlesWithLayout(DocumentBuilder builder, string category, List<ArticleItem> articles)
        {
            // Render category header with custom layout
            RenderCategoryHeader(builder, category);

            // Tải ảnh song song
            var semaphore = new SemaphoreSlim(10); // giới hạn 5 ảnh một lúc
            var imageTasks = articles.Select(a =>
                string.IsNullOrWhiteSpace(a.Image)
                    ? Task.FromResult<Stream?>(null)
                    : SafeDownloadImage(a.Image, semaphore)
            ).ToArray();

            var imageStreams = await Task.WhenAll(imageTasks);

            for (int i = 0; i < articles.Count; i++)
            {
                var article = articles[i];
                var imgStream = imageStreams[i];
                var lightGray = Color.FromArgb(243, 246, 244);

                string title = article.Title ?? "";
                string content = article.Content ?? "";
                string author = article.Author ?? "";
                string createdAt = article.CreatedAt ?? "";
                string url = article.ArticleUrl ?? "";

                string shortTitle = title.Length > 120 ? title[..117] + "..." : title;
                string shortContent = content.Length > 400 ? content[..397] + "..." : content;
                string timeFormatted = DateTime.TryParse(createdAt, out var parsed)
                    ? parsed.ToString("dd/MM/yyyy HH:mm")
                    : createdAt;

                Table table = builder.StartTable();

                // --- Cột ảnh ---
                builder.InsertCell();
                builder.CellFormat.Width = 120;
                builder.CellFormat.Borders.LineStyle = LineStyle.None;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Top;
                builder.CellFormat.Shading.BackgroundPatternColor = lightGray;


                builder.RowFormat.HeightRule = HeightRule.Auto;
                builder.RowFormat.Height = 1; // bỏ Height cố định, để tự co giãn theo nội dung

                if (imgStream != null)
                {
                    try
                    {
                        using var roundedImageStream = ApplyRoundedCornersToImage(imgStream, 120, 120, 12);
                        builder.InsertImage(roundedImageStream, RelativeHorizontalPosition.Margin, 0,
                            RelativeVerticalPosition.Margin, 0, 120, 120, WrapType.None);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Không insert ảnh cho bài: {title} → {ex.Message}");
                        builder.Writeln("");
                    }
                }
                else
                {
                    builder.Writeln(""); // fallback nếu không có ảnh
                }

                // --- Cột nội dung ---
                builder.InsertCell();
                builder.CellFormat.Width = 360;
                builder.CellFormat.Borders.LineStyle = LineStyle.None;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Top;
                builder.CellFormat.Shading.BackgroundPatternColor = lightGray;
                builder.ParagraphFormat.LeftIndent = 4;

                builder.Font.Bold = true;
                builder.Font.Size = 11;
                builder.Font.Color = Color.FromArgb(19, 135, 236); // #0a7de2ff
                builder.ParagraphFormat.SpaceBefore = 4;
                builder.Writeln(shortTitle);

                builder.Font.Bold = false;
                builder.Font.Size = 9;
                builder.Font.Color = Color.FromArgb(85, 85, 85); // #555
                builder.Writeln($"{timeFormatted} - {author}");

                builder.Font.Size = 10;
                builder.Font.Color = Color.Black;
                builder.Writeln(InsertSoftWraps(shortContent));

                if (!string.IsNullOrWhiteSpace(url))
                {
                    builder.Font.Size = 8;
                    builder.Font.Color = Color.FromArgb(13, 71, 161); // #063a88ff
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

        private static async Task<Stream?> SafeDownloadImage(string url, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                return await DownloadImageStreamAsync(url);
            }
            finally
            {
                semaphore.Release();
            }
        }

        // Download url image => pdf
        private static async Task<Stream?> DownloadImageStreamAsync(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var stream = await response.Content.ReadAsStreamAsync();
                var mem = new MemoryStream();
                await stream.CopyToAsync(mem);
                mem.Position = 0;
                return mem;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi tải ảnh từ URL: {url} → {ex.Message}");
                return null;
            }
        }

        private static string InsertSoftWraps(string text, int maxChunkLength = 50)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            var result = new StringBuilder();
            int current = 0;

            while (current < text.Length)
            {
                int nextSpace = text.IndexOf(' ', current);
                int length = (nextSpace == -1 ? text.Length : nextSpace) - current;

                if (length > maxChunkLength)
                {
                    // Quá dài không ngắt, insert khoảng trắng mềm
                    result.Append(text.Substring(current, maxChunkLength) + "\u200B"); // zero-width space
                    current += maxChunkLength;
                }
                else
                {
                    int end = (nextSpace == -1) ? text.Length : nextSpace + 1;
                    result.Append(text.Substring(current, end - current));
                    current = end;
                }
            }

            return result.ToString();
        }

        private static MemoryStream ApplyRoundedCornersToImage(Stream originalImageStream, int width, int height, int cornerRadius)
        {
            using var original = SKBitmap.Decode(originalImageStream);
            using var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            using var paint = new SKPaint
            {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.High
            };

            var rect = new SKRect(0, 0, width, height);
            var path = new SKPath();
            path.AddRoundRect(rect, cornerRadius, cornerRadius);
            canvas.ClipPath(path, SKClipOperation.Intersect, true);
            canvas.DrawBitmap(resized, rect, paint);

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var ms = new MemoryStream();
            data.SaveTo(ms);
            ms.Position = 0;
            return ms;
        }

    }

}
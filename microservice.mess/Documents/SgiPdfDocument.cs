using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using microservice.mess.Models.Message;
using System.Net.Http;

namespace microservice.mess.Documents
{
    public class SgiPdfDocument : IDocument
    {
        private readonly List<SgiDataPdfTemplate> _records;

        public SgiPdfDocument(List<SgiDataPdfTemplate> records)
        {
            _records = records;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Content().Column(main =>
                {
                    // Group theo chuyên đề
                    foreach (var group in _records.GroupBy(r => r.Message.ChuyenDe))
                    {
                        main.Item()
                        .PaddingBottom(10)
                        .Text(group.Key.ToUpper())
                        .FontSize(14)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                        foreach (var record in group)
                        {
                            var props = record.Message.Properties;
                            string title = props.GetValueOrDefault("title_s_srcha")?.FirstOrDefault() ?? "";
                            string content = props.GetValueOrDefault("content_s_srcha")?.FirstOrDefault() ?? "";
                            string author = props.GetValueOrDefault("authorName_s_srcha")?.FirstOrDefault() ?? "";
                            string time = props.GetValueOrDefault("createdAt_dt_srcha")?.FirstOrDefault() ?? "";
                            string imageUrl = props.GetValueOrDefault("media_s_unsrcha")?.FirstOrDefault();
                            string linkUrl = props.GetValueOrDefault("articleUrl_s_srcha")?.FirstOrDefault();


                            main.Item().PaddingBottom(4).Element(container =>
                            container
                                .CornerRadius(4)
                                .Background("#f3f6f4")
                                .Padding(4)
                                .Column(column =>
                                {
                                    column.Item().Row(row =>
                                    {
                                        if (!string.IsNullOrEmpty(imageUrl))
                                        {
                                            byte[]? imgBytes = DownloadImage(imageUrl);
                                            if (imgBytes != null)
                                            {
                                                row.ConstantItem(90).Element(e =>
                                                     e.Padding(4)
                                                      .Height(80)
                                                      .Image(imgBytes)
                                                      .FitArea()
                                                 );
                                            }
                                        }

                                        row.RelativeItem().Column(c =>
                                        {
                                            string shortTitle = title.Length > 120 ? title.Substring(0, 117) + "..." : title;
                                            string shortContent = content.Length > 300 ? content.Substring(0, 297) + "..." : content;
                                            string timeFormatted = DateTime.TryParse(time, out var parsed)
                                                                        ? parsed.ToString("dd/MM/yyyy HH:mm")
                                                                        : time;

                                            c.Item().Text($"{shortTitle}")
                                                            .FontSize(11)
                                                            .SemiBold()
                                                            .FontColor(Colors.Blue.Medium);

                                            //  thời gian, tài khoản, nội dung, link
                                            c.Item().Element(inner =>
                                                inner.Container().Column(block =>
                                                {
                                                    block.Item().Text($"{timeFormatted} – {author}").FontSize(10);
                                                    block.Item().Text(shortContent).FontSize(10);
                                                    block.Item().Hyperlink(linkUrl).Text("Link bài").FontColor(Colors.Blue.Medium).FontSize(9);
                                                })
                                            );

                                        });
                                    });
                                })
                            );

                        }
                    }
                });
            });
        }

        private byte[]? DownloadImage(string url)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0"); // thêm UA      
                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var contentType = response.Content.Headers.ContentType?.MediaType;

                // Chỉ xử lý nếu là ảnh
                if (contentType != null && contentType.StartsWith("image"))
                {
                    return response.Content.ReadAsByteArrayAsync().Result;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // void AddItem(string label, string value)
        // {
        //     column.Item().Text(text =>
        //     {
        //         text.Span($"{label} ").Bold();
        //         text.Span(value);
        //     });
        // }

    }
}

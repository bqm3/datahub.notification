using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using System;
using System.Collections.Generic;
using System.IO;

public class ChartWordExporter
{
    public void InsertChartsIntoTemplate(string templatePath, string outputPath, List<string> chartImagePaths)
    {
        var doc = new Document(templatePath);
        var builder = new DocumentBuilder(doc);

        for (int i = 0; i < chartImagePaths.Count; i++)
        {
            string fieldName = $"ANH_{i + 1}";

            foreach (Field field in doc.Range.Fields)
            {
                if (field.Type == FieldType.FieldMergeField)
                {
                    var fieldCode = field.GetFieldCode();
                    if (fieldCode.Contains(fieldName))
                    {
                        builder.MoveToField(field, true);

                        // Chèn ảnh
                        builder.InsertImage(chartImagePaths[i], 200, 200);

                        field.Remove();
                        break;
                    }
                }
            }
        }

        doc.Save(outputPath);
        Console.WriteLine($"=> Word file generated: {outputPath}");
    }

    public void ExportToPdf(string docxPath, string pdfPath)
    {
        var doc = new Document(docxPath);
        doc.Save(pdfPath, SaveFormat.Pdf);
        Console.WriteLine($"=> PDF file generated: {pdfPath}");
    }
}

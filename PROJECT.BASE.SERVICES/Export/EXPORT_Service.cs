using Aspose.Cells;
using System;
using System.IO;

namespace PROJECT.BASE.SERVICES.Export
{
    public class ExportService
    {
        public static void SetAsposeLicense(string licenseFile)
        {
            string error = string.Empty;
            try
            {
                License license = new License();
                license.SetLicense(licenseFile);

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }


        }


        public static MemoryStream ExportToExcel(string templateFile, object data, out string error)
        {
            error = string.Empty;
            try
            {

                // Create a workbook from Smart Markers template file
                Workbook workbook = new Workbook(templateFile);

                // Instantiate a new WorkbookDesigner
                WorkbookDesigner designer = new WorkbookDesigner();

                // Specify the Workbook
                designer.Workbook = workbook;

                designer.SetDataSource("Data", data);

                // Process the smart markers
                designer.Process();

                // Save the Excel file
                return workbook.SaveToStream();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return null;
        }


    }
}

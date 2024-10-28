using background_job_queue.Interfaces;
using ClosedXML.Excel;

namespace background_job_queue.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<byte[]> GerarArquivoExcelAsync(CancellationToken token)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");

                // Setting the header
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Date";
                worksheet.Cell(1, 4).Value = "Value";

                // Adding sample data
                worksheet.Cell(2, 1).Value = 1;
                worksheet.Cell(2, 2).Value = "Product A";
                worksheet.Cell(2, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell(2, 4).Value = 100.50;

                worksheet.Cell(3, 1).Value = 2;
                worksheet.Cell(3, 2).Value = "Product B";
                worksheet.Cell(3, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell(3, 4).Value = 200.75;

                // Auto fit columns to content
                worksheet.Columns().AdjustToContents();

                using (var ms = new MemoryStream())
                {
                    workbook.SaveAs(ms); // Saves Excel content to MemoryStream
                    return ms.ToArray(); // Converts to a byte array and returns
                }
            }
        }
    }
}

using background_job_queue.Interfaces;
using ClosedXML.Excel;

namespace background_job_queue.Services
{
  public class ExcelService: IExcelService
  {
    public async Task<byte[]> GerarArquivoExcelAsync(CancellationToken token)
    {
      using (var workbook = new XLWorkbook())
      {
        var worksheet = workbook.Worksheets.Add("Relatório");

        // Definindo o cabeçalho
        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nome";
        worksheet.Cell(1, 3).Value = "Data";
        worksheet.Cell(1, 4).Value = "Valor";

        // Adicionando dados de exemplo
        worksheet.Cell(2, 1).Value = 1;
        worksheet.Cell(2, 2).Value = "Produto A";
        worksheet.Cell(2, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
        worksheet.Cell(2, 4).Value = 100.50;

        worksheet.Cell(3, 1).Value = 2;
        worksheet.Cell(3, 2).Value = "Produto B";
        worksheet.Cell(3, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
        worksheet.Cell(3, 4).Value = 200.75;

        // Auto ajustar colunas para conteúdo
        worksheet.Columns().AdjustToContents();

        using (var ms = new MemoryStream())
        {
          workbook.SaveAs(ms); // Salva o conteúdo do Excel no MemoryStream
          return ms.ToArray(); // Converte para um array de bytes e retorna
        }
      }
    }
  }
}

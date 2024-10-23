namespace background_job_queue.Interfaces
{
  public interface IExcelService
  {
    Task<byte[]> GerarArquivoExcelAsync(CancellationToken token);
  }
}

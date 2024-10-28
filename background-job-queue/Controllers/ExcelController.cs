using background_job_queue.Interfaces;
using background_job_queue.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace background_job_queue.Controllers
{
    public class ExcelController : ControllerBase
    {
        private readonly IBackgroundJobQueue _jobQueue;
        private readonly IFileStorage _fileStorage;
        private readonly IExcelService _excelService;
        private readonly IHubContext<ExcelNotificationHub> _hubContext;
        public ExcelController(IBackgroundJobQueue jobQueue, IFileStorage fileStorage, IExcelService excelService, IHubContext<ExcelNotificationHub> hubContext)
        {
            _jobQueue = jobQueue;
            _fileStorage = fileStorage;
            _excelService = excelService;
            _hubContext = hubContext;
        }

        [HttpPost("generate")]
        public IActionResult GenerateExcel()
        {
            var jobId = Guid.NewGuid().ToString();

            _jobQueue.QueueBackgroundWorkItem(async token =>
            {
                var excelFile = await _excelService.GerarArquivoExcelAsync(token);
                _fileStorage.SaveFile(jobId, excelFile);

                // Notifies client via WebSocket when file is ready
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", jobId);
            });

            return Ok(new { jobId });
        }

        [HttpGet("wait-for-file/{jobId}")]
        public async Task<IActionResult> WaitForFile(string jobId, CancellationToken cancellationToken)
        {
            // Polling: Wait until the file is ready
            while (!_fileStorage.FileExists(jobId))
            {
                await Task.Delay(1000, cancellationToken); //Checks every 1 second
            }

            // When ready, returns the contents of the file
            var fileBytes = _fileStorage.GetFile(jobId);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatorio.xlsx");
        }
    }
}

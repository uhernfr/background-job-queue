using background_job_queue.Interfaces;

namespace background_job_queue.Services
{
  public class QueuedHostedService : BackgroundService
  {
    private readonly IBackgroundJobQueue _jobQueue;
    private readonly ILogger<QueuedHostedService> _logger;

    public QueuedHostedService(IBackgroundJobQueue jobQueue, ILogger<QueuedHostedService> logger)
    {
      _jobQueue = jobQueue;
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Background service started.");

      while (!stoppingToken.IsCancellationRequested)
      {
        var workItem = await _jobQueue.DequeueAsync(stoppingToken);

        try
        {
          await workItem(stoppingToken);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Error occurred while executing background job.");
        }
      }

      _logger.LogInformation("Background service stopped.");
    }
  }
}

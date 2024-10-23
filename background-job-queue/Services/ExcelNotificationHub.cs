using Microsoft.AspNetCore.SignalR;

namespace background_job_queue.Services
{
  public class ExcelNotificationHub : Hub
  {
    public async Task NotifyFileReady(string jobId)
    {
      await Clients.Caller.SendAsync("ReceiveNotification", jobId);
    }
  }
}

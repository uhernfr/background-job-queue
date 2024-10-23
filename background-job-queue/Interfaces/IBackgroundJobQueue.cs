using System.Threading.Channels;

namespace background_job_queue.Interfaces
{
  public interface IBackgroundJobQueue
  {
    void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);
    Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);


  }

  public class BackgroundJobQueue : IBackgroundJobQueue
  {
    private readonly Channel<Func<CancellationToken, Task>> _queue;

    public BackgroundJobQueue()
    {
      _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>();
    }

    public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
    {
      _queue.Writer.TryWrite(workItem);
    }

    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
    {
      return await _queue.Reader.ReadAsync(cancellationToken);
    }
  }
}

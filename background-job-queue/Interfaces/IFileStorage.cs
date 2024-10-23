namespace background_job_queue.Interfaces
{
  public interface IFileStorage
  {
    void SaveFile(string jobId, byte[] file);
    byte[] GetFile(string jobId);
    bool FileExists(string jobId);
  }

  public class InMemoryFileStorage : IFileStorage
  {
    private readonly Dictionary<string, byte[]> _storage = new Dictionary<string, byte[]>();

    public void SaveFile(string jobId, byte[] file)
    {
      _storage[jobId] = file;
    }

    public byte[] GetFile(string jobId)
    {
      _storage.TryGetValue(jobId, out var file);
      return file;
    }

    public bool FileExists(string jobId)
    {
      return _storage.ContainsKey(jobId);
    }
  }
}

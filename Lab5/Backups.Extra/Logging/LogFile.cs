using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.Logging;

public class LogFile : RepositoryComponent
{
    private List<string> _loggers = new List<string>();
    public LogFile(string name)
        : base(name)
    {
    }

    public void AddLog(string log)
    {
        if (string.IsNullOrWhiteSpace(log))
            throw new BackupsException("null reference of lof");
        _loggers.Add(log);
    }

    public IReadOnlyCollection<string> GetLogFile()
    {
        return _loggers;
    }
}
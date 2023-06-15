using Backups.Exception;
using File = Backups.InMemoryRepository.File;

namespace Backups.Extra.Logging;

public class ReceiverFile : IReceiver
{
    private LogFile _logger = new LogFile("Logger.log");

    public void Message(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
                throw new BackupsException("Null reference");
        _logger.AddLog(message);
    }
}
using Backups.Exception;

namespace Backups.Extra.Logging;

public class Logger : ILogger
{
    private IReceiver _receiver;

    public void SetReceiver(IReceiver receiver)
    {
        if (receiver == null)
            throw new BackupsException("Null reference of receiver");
        _receiver = receiver;
    }

    public void Execute(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new BackupsException("Null reference of message");
        _receiver.Message(message);
    }
}
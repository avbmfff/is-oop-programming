using Backups.Exception;

namespace Backups.Extra.Logging;

public class ReceiverConsole : IReceiver
{
    public void Message(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new BackupsException("Null reference");
        Console.WriteLine(message);
    }
}
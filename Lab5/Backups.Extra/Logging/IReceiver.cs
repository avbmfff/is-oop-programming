namespace Backups.Extra.Logging;

public interface IReceiver
{
    void Message(string message);
}
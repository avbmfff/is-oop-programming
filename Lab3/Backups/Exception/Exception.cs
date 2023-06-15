namespace Backups.Exception;

public class BackupsException : System.Exception
{
    public BackupsException(string message)
        : base(message)
    { }
}
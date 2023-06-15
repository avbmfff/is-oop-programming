namespace Banks;

public class BanksException : System.Exception
{
    public BanksException(string message)
        : base(message)
    { }
}
namespace Isu.Exception;

public class IsuException : System.Exception
{
    public IsuException(string message)
        : base(message) // base Exception class constructor
    { }
}
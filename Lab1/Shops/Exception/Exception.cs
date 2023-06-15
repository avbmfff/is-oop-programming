namespace Shops.Exception;

public class ShopException : System.Exception
{
    public ShopException(string message)
        : base(message) // base Exception class constructor
    { }
}
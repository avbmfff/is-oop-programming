using Shops.Exception;

namespace Shops.Models;

public class Product
{
    private const int Limitdegree = 0;
    public Product(string name, int price, int amount)
    {
        if (name != null! && price >= Limitdegree && amount >= Limitdegree)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }
        else
        {
            throw new ShopException("Invalid data");
        }
    }

    public Product(string name, int amount)
    {
        if (name != null! && amount >= Limitdegree)
        {
            Name = name;
            Amount = amount;
        }
        else
        {
            throw new ShopException("Invalid data");
        }
    }

    public string Name { get; private set; }
    public int Price { get; private set; }
    public int Amount { get; private set; }

    public void SetName(string name)
    {
        if (name != null!)
        {
            Name = name;
        }
        else
        {
            throw new ShopException("Can't set a name");
        }
    }

    public void SetPrice(int price)
    {
        if (price >= Limitdegree)
        {
            Price = price;
        }
        else
        {
            throw new ShopException("Can't set a price");
        }
    }

    public void SetAmount(int amount)
    {
        if (amount >= Limitdegree)
        {
            Amount = amount;
        }
        else
        {
            throw new ShopException("Can't set amount");
        }
    }
}

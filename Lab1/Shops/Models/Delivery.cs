using Shops.Exception;
using Shops.Models;

namespace Shops.Entities;

public class Delivery
{
    private const int Limitdegree = 0;
    public Delivery(Buyer buyer, string address, int time, int amount, int price)
    {
        if (buyer != null! && !string.IsNullOrWhiteSpace(address) && time >= Limitdegree && amount >= Limitdegree &&
            price >= Limitdegree)
        {
            Buyer = buyer;
            Address = address;
            Time = time;
            Amount = amount;
            Price = price;
        }
        else
        {
            throw new ShopException("Invalid data");
        }
    }

    public Buyer Buyer { get; private set; }
    public string Address { get; private set; }
    public int Time { get; private set; }
    public int Amount { get; }
    public int Price { get; }

    public void SetBuyer(Buyer buyer)
    {
        if (buyer != null!)
        {
            Buyer = buyer;
        }
        else
        {
            throw new ShopException("Can't set data");
        }
    }

    public void SetAddress(string address)
    {
        if (address != null!)
        {
            Address = address;
        }
        else
        {
            throw new ShopException("Can't set data");
        }
    }

    public void SetTime(int time)
    {
        if (time >= Limitdegree)
        {
            Time = time;
        }
        else
        {
            throw new ShopException("Can't set data");
        }
    }
}
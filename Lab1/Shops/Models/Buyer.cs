using Shops.Entities;
using Shops.Exception;

namespace Shops.Models;

public class Buyer
{
    private const int Limitdegree = 0;
    private string _name;
    public Buyer(string name, int money)
    {
        if (money >= Limitdegree)
        {
            Money = money;
        }

        if (name != null)
        {
            _name = name;
        }
        else
        {
            throw new System.Exception("Invalid name");
        }
    }

    private int Money { get; set; }

    public void ToBuyTheProduct(int price)
    {
        if (Money >= price)
        {
            Money = Money - price;
        }

        if (Money < price)
        {
            throw new ShopException("Buyer can't buy");
        }
    }
}
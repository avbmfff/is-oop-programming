using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shops.Entities;
using Shops.Exception;
using Shops.Models;

namespace Shops.Service;

public class ShopService : IShopService
{
    public Shop AddShop(Shop newShop)
    {
        if (ShopsData.IsShopExist(newShop)) return null!;
        ShopsData.Shops.Add(newShop);
        return newShop;
    }

    public Product AddProduct(Shop shop, Product newProduct)
    {
        if (shop == null! || !ShopsData.IsShopExist(shop)) return null!;
        shop.AddNewProducts(newProduct);
        return newProduct;
    }

    public IReadOnlyCollection<Shop> GetShopsWithProducts(string product, int amount) // change name
    {
        var shopWithProduct = ShopsData.Shops.Where(shops => shops.CanToBuyProduct(product, amount)).ToList();

        if (shopWithProduct.Count == 0)
        {
            throw new ShopException("Can't buy a product");
        }

        return shopWithProduct;
    }

    public Product ToDoANewPrice(Shop shop, Product product, int price)
    {
        shop.ChangePrice(product, price);
        return product;
    }

    public Shop GetShopsWithCheaperPriceListOfProducts(List<Product> products)
    {
        int maxpriceconst = 1000000000;
        int minprice = 1000000000;
        Shop shopwithminprice = null!;
        foreach (var shops in ShopsData.Shops)
        {
            int fullprice = 0;
            foreach (var product in products)
            {
                if (shops.CanToBuyProduct(product.Name, product.Amount))
                {
                    fullprice += product.Price;
                }
                else
                {
                    fullprice += maxpriceconst;
                }
            }

            if (fullprice <= minprice)
            {
                minprice = fullprice;
                shopwithminprice = shops;
            }
        }

        return shopwithminprice;
    }

    public void BuyAProduct(string product, int amount, Buyer buyer, Shop shop, Delivery delivery)
    {
        if (!shop.CanToBuyProduct(product, amount)) return;
        shop.ChangeAmountBecauseBuy(product, amount);
        buyer.ToBuyTheProduct(shop.SetPrice(product, amount));
    }

    public void SupplyOfProducts(Shop shop, Product product, int amount, int price)
    {
        shop.OrderOfSupply(product, amount, price);
    }
}
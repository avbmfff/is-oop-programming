using Shops.Entities;
using Shops.Models;

namespace Shops.Service;

public interface IShopService
{
    Shop AddShop(Shop newShop);
    Product AddProduct(Shop shop, Product newProduct);
    IReadOnlyCollection<Shop> GetShopsWithProducts(string product, int amount);
    Product ToDoANewPrice(Shop shop, Product product, int price);
    Shop GetShopsWithCheaperPriceListOfProducts(List<Product> products);
    void BuyAProduct(string product, int amount, Buyer buyer, Shop shop, Delivery delivery);
    void SupplyOfProducts(Shop shop, Product product, int amount, int price);
}
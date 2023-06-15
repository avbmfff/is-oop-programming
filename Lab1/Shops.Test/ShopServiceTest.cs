using Shops.Entities;
using Shops.Models;
using Shops.Service;
using Xunit;

namespace Shops.Test;

public class IsuServiceTest
{
    [Fact]
    public void CreateShopAddProductsAndSupplyCheck()
    {
        var service = new ShopService();
        var shop1 = new Shop(1234, "shop1");
        service.AddShop(shop1);
        var product1 = new Product("balls", 200, 5);
        var product2 = new Product("cards", 50, 100);
        service.AddProduct(shop1, product1);
        service.AddProduct(shop1, product2);
        Assert.True(shop1.CanToBuyProduct("balls", 2));
        Assert.True(shop1.CanToBuyProduct("cards", 50));
    }

    [Fact]
    public void ChangePrice()
    {
        var service = new ShopService();
        var shop1 = new Shop(12345, "shop1");
        service.AddShop(shop1);
        var product1 = new Product("balls", 200, 5);
        service.AddProduct(shop1, product1);
        service.ToDoANewPrice(shop1, product1, 500);
        Assert.True(product1.Price == 500);
    }

    [Fact]
    public void SearchingTheMostCheaperShop()
    {
        var service = new ShopService();
        var shop1 = new Shop(12, "shop1");
        var shop2 = new Shop(123, "shop2");
        var shop3 = new Shop(1234, "shop3");
        service.AddShop(shop1);
        service.AddShop(shop2);
        service.AddShop(shop3);
        var products = new List<Product>();
        products.Add(new Product("balls", 5));
        products.Add(new Product("toys", 3));
        products.Add(new Product("doll", 1));
        var product1shop1 = new Product("balls", 150, 3);
        var product2shop1 = new Product("toys", 100, 5);
        var product3shop1 = new Product("doll", 100, 6);
        var product1shop2 = new Product("balls", 200, 5);
        var product2shop2 = new Product("toys", 80, 10);
        var product1shop3 = new Product("balls", 200, 6);
        var product2shop3 = new Product("toys", 110, 3);
        var product3shop3 = new Product("doll", 150, 5);
        service.AddProduct(shop1, product1shop1);
        service.AddProduct(shop1, product2shop1);
        service.AddProduct(shop1, product3shop1);
        service.AddProduct(shop2, product1shop2);
        service.AddProduct(shop2, product2shop2);
        service.AddProduct(shop3, product1shop3);
        service.AddProduct(shop3, product2shop3);
        service.AddProduct(shop3, product3shop3);
        service.GetShopsWithCheaperPriceListOfProducts(products);
        Assert.True(service.GetShopsWithCheaperPriceListOfProducts(products) == shop3);
    }

    [Fact]
    public void BuyingProducts()
    {
        var service = new ShopService();
        var shop1 = new Shop(2222, "shop1");
        var buyer = new Buyer("Anton", 5000);
        var delivery = new Delivery(buyer, "address", 17, 6, 200);
        var product = new Product("ball", 200, 6);
        service.AddShop(shop1);
        service.AddProduct(shop1, product);
        service.BuyAProduct("ball", 1, buyer, shop1, delivery);
        Assert.True(product.Amount == 5);
    }
}
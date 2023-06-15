using Shops.Exception;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private const int Limitdegree = 0;
    private int _id;
    private string _name;
    private List<Product> _products = new List<Product>();
    public Shop(int id, string name)
    {
        if (id >= Limitdegree)
        {
            _id = id;
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            _name = name;
        }
        else
        {
            throw new ShopException("Invalid name");
        }
    }

    public int Id => _id;
    public string Name => _name;
    public void AddNewProducts(Product product)
    {
        string productName = product.Name;
        if (!IsExistProduct(productName))
        {
            _products.Add(product);
        }
        else
        {
            throw new ShopException("Can't add to list");
        }
    }

    public bool CanToBuyProduct(string product, int amount)
    {
        if (!IsExistProduct(product))
        {
            return false;
        }

        return GetProduct(product).Amount - amount >= Limitdegree;
    }

    public void ReturnPrice(string nameProduct)
    {
        if (!IsExistProduct(nameProduct)) throw new InvalidOperationException();
        int price = _products.SingleOrDefault(product => product == GetProduct(nameProduct)) !.Price;
    }

    public bool IsEqual(Product product1, Product product2)
    {
        return product1.Amount == product2.Amount && product1.Name == product2.Name && product1.Price == product2.Price;
    }

    public void ChangePrice(Product oldProduct, int price)
    {
        if (!IsExistProduct(oldProduct)) throw new InvalidOperationException();
        if (!_products.Any(product => IsEqual(product, oldProduct))) throw new InvalidOperationException();
        oldProduct.SetPrice(price);
    }

    public void ChangeAmountBecauseBuy(string oldproduct, int amount)
    {
        if (!IsExistProduct(oldproduct)) return;
        if ((GetProduct(oldproduct).Amount - amount) > Limitdegree)
        {
            GetProduct(oldproduct).SetAmount(GetProduct(oldproduct).Amount - amount);
        }

        if ((GetProduct(oldproduct).Amount - amount) < Limitdegree)
        {
            throw new ShopException("Not enough quantity");
        }

        if ((GetProduct(oldproduct).Amount - amount) != Limitdegree) return;
        GetProduct(oldproduct).SetAmount(GetProduct(oldproduct).Amount - amount);
        throw new ShopException("Product is out of stock");
    }

    public int SetPrice(string product, int amount)
    {
        return GetProduct(product).Price * amount;
    }

    public void OrderOfSupply(Product newProduct, int amount, int price)
    {
        if (!IsExistProduct(newProduct))
        {
            GetProduct(newProduct.Name).SetAmount(amount);
            GetProduct(newProduct.Name).SetPrice(price);
        }

        if (!IsExistProduct(newProduct)) return;
        GetProduct(newProduct.Name).SetAmount(amount);
        GetProduct(newProduct.Name).SetPrice(price);
        AddNewProducts(newProduct);
    }

    public Product FindProduct(Product newProduct)
    {
        return _products.FirstOrDefault(product => product == newProduct) !;
    }

    private bool IsExistProduct(Product newProduct)
    {
        return _products.Any(product => IsEqual(product, newProduct));
    }

    private bool IsExistProduct(string newProduct)
    {
        return _products.Any(product => product.Name == newProduct);
    }

    private Product GetProduct(string newProduct)
    {
        return _products.FirstOrDefault(product => product.Name == newProduct) !;
    }
}
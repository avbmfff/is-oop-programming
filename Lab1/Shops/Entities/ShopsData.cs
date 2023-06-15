using Shops.Entities;

internal class ShopsData
{
    public static List<Shop> Shops { get; set; } = new List<Shop>();

    public static bool IsShopExist(Shop shop)
    {
        return Shops.Any(shops => shops == shop);
    }
}
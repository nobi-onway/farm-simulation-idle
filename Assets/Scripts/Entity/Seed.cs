public class Seed : IInventoryItem, IShopItem
{
    public string Name { get; private set; }
    public int Cost { get; private set; }
    public int YieldInterval { get; private set; }
    public int MaxYield { get; private set; }

    public int Quantity { get; set; }
    public int Id => Name.GetHashCode();

    public Seed(string name, int cost, int yieldInterval, int maxYield)
    {
        Name = name;
        Cost = cost;
        YieldInterval = yieldInterval;
        MaxYield = maxYield;
    }
}
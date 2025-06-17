public class Seed : IInventoryItem
{
    public string Name { get; private set; }
    public string ProductName { get; private set; }
    public int Cost { get; private set; }
    public int YieldInterval { get; private set; }
    public int MaxYield { get; private set; }

    public int Quantity { get; set; }

    public int Id => Name.GetHashCode();

    public Seed(string name, string productName, int cost, int yieldInterval, int maxYield)
    {
        Name = name;
        ProductName = productName;
        Cost = cost;
        YieldInterval = yieldInterval;
        MaxYield = maxYield;
    }
}
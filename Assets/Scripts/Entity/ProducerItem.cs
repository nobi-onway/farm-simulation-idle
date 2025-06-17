public class ProducerItem : IInventoryItem, IShopItem
{
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int YieldInterval { get; private set; }
    public int MaxYield { get; private set; }

    public int Quantity { get; set; }
    public string Id { get; private set; }
    public int DecayTimer { get; private set; }

    public ProducerItem(ProducerData data)
    {
        Id = data.Id;
        Name = data.Name;
        YieldInterval = data.YieldInterval;
        MaxYield = data.MaxYield;
        DecayTimer = data.DecayTimer;
    }

    public ProducerItem(ShopData data)
    {
        Id = data.Id;
        Name = data.Name;
        Price = data.Price;
    }
}
using System;

public class ProducerItem : IInventoryItem, IBuyableItem
{
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int YieldInterval { get; private set; }
    public int MaxYield { get; private set; }

    public int Quantity { get; set; }
    public string Id { get; private set; }
    public int DecayTimer { get; private set; }

    public Type StorageType => typeof (Inventory);

    public string TransactionLabel => $"Buy: {Price}";

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

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Inventory inventory) return false;
        if (!wallet.TryWithdraw(Price)) return false;

        return inventory.TryAddItem(Id);
    }
}
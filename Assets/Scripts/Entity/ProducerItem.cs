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

    public Type StorageType => typeof(Inventory);

    public string TransactionLabel => $"Buy: {Price}";

    public bool BulkOnly { get; private set; }

    public int BulkAmount { get; private set; }

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
        BulkOnly = data.BulkOnly;
        BulkAmount = data.BulkAmount;
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Inventory inventory) return false;
        if (!wallet.TryWithdraw(Price)) return false;

        return BulkOnly ? inventory.TryAddItem(this, BulkAmount) : inventory.TryAddItem(this);
    }
}
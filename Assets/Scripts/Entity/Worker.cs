using System;

public class Worker : IBuyableItem
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Type StorageType => typeof(Roster);

    public string TransactionLabel => $"Hire: {Price}";

    public Worker(WorkerData data)
    {
        Id = data.Id;
        Name = data.Name;
        Price = data.HireCost;
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Roster roster) return false;

        if (!wallet.TryWithdraw(Price)) return false;

        roster.AddWorker(this);

        return true;
    }
}
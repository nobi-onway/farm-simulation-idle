using System;

public interface IBuyableItem
{
    public string Id { get; }
    public string Name { get; }
    public int Price { get; }
    public Type StorageType { get; }
    public string TransactionLabel { get; }
    public bool TryBuy(Wallet wallet, object storage);
}
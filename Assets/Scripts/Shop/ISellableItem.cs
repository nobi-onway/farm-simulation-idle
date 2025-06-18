public interface ISellableItem
{
    public string Id { get; }
    public int SellPrice { get; }
    public void Sell(Wallet wallet, Inventory inventory);
}
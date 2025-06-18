public class Product : IInventoryItem, ISellableItem
{
    public string Name { get; private set; }
    public int Quantity { get; set; }
    public string Id { get; private set; }
    public string ProducerId { get; private set; }

    public int SellPrice { get; private set; }

    public Product(ProductData data)
    {
        Id = data.Id;
        ProducerId = data.ProducerId;
        Name = data.Name;
        SellPrice = data.SellPrice;
    }

    public void Sell(Wallet wallet, Inventory inventory)
    {
        wallet.Deposit(SellPrice * Quantity);
        inventory.RemoveItem(this, Quantity);
        Quantity = 0;
    }
}
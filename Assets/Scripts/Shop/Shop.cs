using System.Collections.Generic;

public class Shop
{
    public List<IBuyableItem> Items { get; private set; }

    public Shop()
    {
        Items = new();
    }

    public void AddItem(IBuyableItem item) => Items.Add(item);

    public void BuyItem(IBuyableItem item)
    {
        if (!GameManager.Instance.Wallet.TryWithdraw(item.Price)) return;

        if (GameManager.Instance.Inventory.TryAddItem(item.Id)) return;

        ProducerItem producerItem = new(GameManager.Instance.ProducerDataLookUp[item.Id]);
        GameManager.Instance.Inventory.AddItem(producerItem, 1);
    }
}
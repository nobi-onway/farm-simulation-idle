using System.Collections.Generic;

public class Shop
{
    public List<IShopItem> Items { get; private set; }

    public Shop()
    {
        Items = new();
    }

    public void AddItem(IShopItem item) => Items.Add(item);

    public void BuyItem(IShopItem item)
    {
        if (!GameManager.Instance.Wallet.TryWithdraw(item.Cost)) return;

        GameManager.Instance.Inventory.TryAddItem(item.Id);
    }
}
using System.Collections.Generic;

public class Shop
{
    public List<IBuyableItem> BuyableItems { get; private set; }

    public Shop()
    {
        BuyableItems = new();
    }

    public void AddItem(IBuyableItem item) => BuyableItems.Add(item);
}
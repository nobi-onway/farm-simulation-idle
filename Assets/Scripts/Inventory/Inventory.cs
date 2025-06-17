using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public List<Seed> Seeds => Items.OfType<Seed>().ToList();
    public List<IInventoryItem> Items { get; private set; }

    public event Action<IInventoryItem> OnAddItem;
    public event Action<IInventoryItem> OnUpdateItem;

    public Inventory()
    {
        Items = new();
    }

    public void AddItem(IInventoryItem item, int quantity)
    {
        IInventoryItem existingItem = Items.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            OnUpdateItem?.Invoke(existingItem);
            return;
        }

        item.Quantity = quantity;

        Items.Add(item);

        OnAddItem?.Invoke(item);
    }
}

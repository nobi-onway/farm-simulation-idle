using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public List<ProducerItem> ProducerItems => Items.OfType<ProducerItem>().ToList();
    public List<IInventoryItem> Items { get; private set; }

    public event Action<IInventoryItem> OnAddItem;
    public event Action<IInventoryItem> OnUpdateItem;

    public Inventory()
    {
        Items = new();
    }

    public void AddItem(IInventoryItem item, int quantity)
    {
        if (TryAddItem(item.Id)) return;

        item.Quantity = quantity;

        Items.Add(item);

        OnAddItem?.Invoke(item);
    }

    public bool TryAddItem(string id)
    {
        IInventoryItem existingItem = Items.FirstOrDefault(i => i.Id == id);

        if (existingItem == null) return false;

        existingItem.Quantity += 1;
        OnUpdateItem?.Invoke(existingItem);
        return true;
    }
}

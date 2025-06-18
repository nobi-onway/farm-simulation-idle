using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private bool TryAddItem(string id, int quantity = 1)
    {
        IInventoryItem existingItem = Items.FirstOrDefault(i => i.Id == id);

        if (existingItem == null) return false;

        existingItem.Quantity += quantity;
        OnUpdateItem?.Invoke(existingItem);
        return true;
    }

    public bool TryAddItem(IInventoryItem inventoryItem, int quantity = 1)
    {
        if (TryAddItem(inventoryItem.Id, quantity)) return true;

        AddItem(inventoryItem, quantity);
        return true;
    }

    public void RemoveItem(IInventoryItem item, int quantity)
    {
        item.Quantity -= quantity;
        OnUpdateItem?.Invoke(item);
    }

    public bool TryGetItem<T>(string Id, out T item) where T : IInventoryItem
    {
        item = Items.OfType<T>().FirstOrDefault(i => i.Id == Id);

        if (item == null) return false;
        if (item.Quantity == 0) return false;

        item.Quantity--;
        OnUpdateItem?.Invoke(item);
        return true;
    }
}

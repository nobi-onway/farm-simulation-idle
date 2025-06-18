using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI _shopItemUIPrefab;

    private Dictionary<Type, object> StorageLookUp;

    private void Start()
    {
        StorageLookUp = new()
        {
            { typeof(Inventory), FarmManager.Instance.Inventory },
            { typeof(Roster), FarmManager.Instance.Roster }
        };

        GenerateItems();
    }

    private void GenerateItems()
    {
        foreach (IBuyableItem buyableItem in GameManager.Instance.Shop.BuyableItems)
        {
            GenerateItem(buyableItem);
        }
    }

    private void GenerateItem(IBuyableItem buyableItem)
    {
        ShopItemUI shopItemUI = Instantiate(_shopItemUIPrefab, this.transform);
        shopItemUI.Initialize(buyableItem, () => buyableItem.TryBuy(GameManager.Instance.Wallet, StorageLookUp[buyableItem.StorageType]));
    }
}
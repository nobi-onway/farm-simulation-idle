using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI _shopItemUIPrefab;

    private void Start()
    {
        GenerateItems();
    }

    private void GenerateItems()
    {
        foreach (IShopItem shopItem in GameManager.Instance.Shop.Items)
        {
            GenerateItem(shopItem);
        }
    }

    private void GenerateItem(IShopItem shopItem)
    {
        ShopItemUI shopItemUI = Instantiate(_shopItemUIPrefab, this.transform);
        shopItemUI.Initialize(shopItem, () => GameManager.Instance.Shop.BuyItem(shopItem));
    }
}
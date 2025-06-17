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
        foreach (IBuyableItem shopItem in GameManager.Instance.Shop.Items)
        {
            GenerateItem(shopItem);
        }
    }

    private void GenerateItem(IBuyableItem shopItem)
    {
        ShopItemUI shopItemUI = Instantiate(_shopItemUIPrefab, this.transform);
        shopItemUI.Initialize(shopItem, () => GameManager.Instance.Shop.BuyItem(shopItem));
    }
}
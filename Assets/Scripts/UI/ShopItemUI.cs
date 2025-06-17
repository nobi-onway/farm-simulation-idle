using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleTMP;
    [SerializeField] private Button _buyButton;

    public void Initialize(IShopItem shopItem, UnityAction OnBuyItem)
    {
        _titleTMP.SetText(shopItem.Name);
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Buy: {shopItem.Cost}");
        _buyButton.onClick.AddListener(OnBuyItem);
    }
}
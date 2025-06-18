using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleTMP;
    [SerializeField] private Button _buyButton;

    public void Initialize(IBuyableItem buyableItem, UnityAction OnBuyItem)
    {
        _titleTMP.SetText(buyableItem.Name);
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buyableItem.TransactionLabel);
        _buyButton.onClick.AddListener(OnBuyItem);
    }
}
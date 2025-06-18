using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private IInventoryItem _item;
    private TextMeshProUGUI _descriptionTMP;
    [SerializeField] private Button _sellButton;

    private void OnDisable()
    {
        FarmManager.Instance.Inventory.OnUpdateItem -= UpdateUI;
    }

    private void Awake()
    {
        _descriptionTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(IInventoryItem item)
    {
        _item = item;

        UpdateUI(item);

        if (item is ISellableItem sellableItem)
        {
            _sellButton.gameObject.SetActive(true);
            _sellButton.onClick.AddListener(() => sellableItem.Sell(GameManager.Instance.Wallet, FarmManager.Instance.Inventory));
        }

        FarmManager.Instance.Inventory.OnUpdateItem += UpdateUI;
    }

    private void UpdateUI(IInventoryItem item)
    {
        if (item.Id != _item.Id) return;

        _descriptionTMP.SetText($"{item.Name} \n ({item.Quantity})");
    }
}
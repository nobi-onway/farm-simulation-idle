using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    private IInventoryItem _item;
    private TextMeshProUGUI _descriptionTMP;

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

        FarmManager.Instance.Inventory.OnUpdateItem += UpdateUI;
    }

    private void UpdateUI(IInventoryItem item)
    {
        if(item.Id != _item.Id) return;

        _descriptionTMP.SetText($"{item.Name} \n ({item.Quantity})");
    }
}
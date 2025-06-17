using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private RectTransform _itemUIPrefab;

    private void Start()
    {
        GenerateItems();
    }

    private void GenerateItems()
    {
        foreach (Seed seed in GameManager.Instance.Inventory.Seeds)
        {
            GenerateItem(seed.Name, seed.Quantity);
        }
    }

    private void GenerateItem(string name, int count)
    {
        RectTransform itemUIClone = Instantiate(_itemUIPrefab, this.transform);
        itemUIClone.GetComponentInChildren<TextMeshProUGUI>().SetText($"{name} ({count})");
    }
}
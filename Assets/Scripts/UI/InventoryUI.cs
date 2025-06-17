using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI _itemUIPrefab;

    private void OnDisable()
    {
        GameManager.Instance.Inventory.OnAddItem -= GenerateItem;
    }

    private void Start()
    {
        GameManager.Instance.Inventory.OnAddItem += GenerateItem;

        GenerateItems();
    }

    private void GenerateItems()
    {
        Clear();

        foreach (IInventoryItem item in GameManager.Instance.Inventory.Items)
        {
            GenerateItem(item);
        }
    }

    private void GenerateItem(IInventoryItem item)
    {
        ItemUI itemUIClone = Instantiate(_itemUIPrefab, this.transform);
        itemUIClone.Initialize(item);
    }

    private void Clear()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private Inventory _inventory;
    public Inventory Inventory => _inventory;

    private void Awake()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        _inventory = new();

        _inventory.AddItem(new Seed("Tomato Seed", "Tomato", 30, 10, 5), 10);
        _inventory.AddItem(new Seed("Blueberry Seed", "Blueberry", 50, 10, 5), 10);
        _inventory.AddItem(new Seed("Cow", "Gallon", 100, 10, 10), 5);
    }
}
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
        
        _inventory.AddSeed(new Seed("Tomato", 30, 10, 10), 10);
        _inventory.AddSeed(new Seed("Blueberry", 50, 10, 10), 10);
        _inventory.AddSeed(new Seed("Cow", 100, 10, 10), 10);
    }
}
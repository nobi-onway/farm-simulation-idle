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

    public Inventory Inventory { get; private set; }
    public Shop Shop { get; private set; }
    public Wallet Wallet { get; private set; }

    private void Awake()
    {
        InitializeInventory();
        InitializeShop();
        InitializeWallet();
    }

    private void InitializeInventory()
    {
        Inventory = new();

        Inventory.AddItem(new Seed("Tomato Seed", 30, 10, 5), 10);
        Inventory.AddItem(new Seed("Blueberry Seed", 50, 10, 5), 10);
        Inventory.AddItem(new Seed("Cow", 100, 10, 10), 5);
    }

    private void InitializeShop()
    {
        Shop = new();

        Shop.AddItem(new Seed("Tomato Seed", 30, 10, 5));
        Shop.AddItem(new Seed("Blueberry Seed", 50, 10, 5));
        Shop.AddItem(new Seed("Cow", 100, 10, 10));
    }

    private void InitializeWallet()
    {
        Wallet = new(1000);
    }
}
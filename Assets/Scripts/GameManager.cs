using System.Collections.Generic;
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
    public Roster Roster { get; private set; }
    public Shop Shop { get; private set; }
    public Wallet Wallet { get; private set; }

    public Dictionary<string, ProducerData> ProducerDataLookUp;
    private Dictionary<string, InventoryData> InventoryDataLookUp;
    private Dictionary<string, ShopData> ShopDataLookUp;
    public Dictionary<string, ProductData> ProductDataLookUp;
    private Dictionary<string, WorkerData> WorkerDataLookup;

    private void Awake()
    {
        InitialResource();

        InitializeInventory();
        InitializeRoster();
        InitializeShop();
        InitializeWallet();
    }

    private void InitialResource()
    {
        ProducerDataLookUp ??= LoaderUtils.LoadProducerData();
        InventoryDataLookUp ??= LoaderUtils.LoadInventoryData("Assets/Config/InitialInventoryData.csv");
        ShopDataLookUp ??= LoaderUtils.LoadShopData();
        ProductDataLookUp ??= LoaderUtils.LoadProductData();
        WorkerDataLookup ??= LoaderUtils.LoadWorkerData();
    }

    private void InitializeInventory()
    {
        Inventory = new();

        foreach (string key in InventoryDataLookUp.Keys)
        {
            ProducerItem producerItem = new(ProducerDataLookUp[key]);
            int quantity = InventoryDataLookUp[key].Quantity;

            Inventory.AddItem(producerItem, quantity);
        }
    }

    private void InitializeRoster()
    {
        Roster = new();
    }

    private void InitializeShop()
    {
        Shop = new();

        foreach (ShopData data in ShopDataLookUp.Values)
        {
            ProducerItem producerItem = new(data);

            Shop.AddItem(producerItem);
        }

        foreach (WorkerData data in WorkerDataLookup.Values)
        {
            Worker worker = new(data);

            Shop.AddItem(worker);
        }
    }

    private void InitializeWallet()
    {
        Wallet = new(1000);
    }
}
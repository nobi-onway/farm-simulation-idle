using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    public Shop Shop { get; private set; }
    public Wallet Wallet { get; private set; }

    private ResourceManager resourceManager => ResourceManager.Instance;

    private void Awake()
    {
        InitializeShop();
        InitializeWallet();
    }

    private void InitializeShop()
    {
        Shop = new();

        foreach (ShopData data in resourceManager.ShopDataLookUp.Values)
        {
            ProducerItem producerItem = new(data);

            Shop.AddItem(producerItem);
        }

        foreach (WorkerData data in resourceManager.WorkerDataLookup.Values)
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
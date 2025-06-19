using System;

public class GameManager : MonoSingleton<GameManager>
{
    public Shop Shop { get; private set; }
    public Wallet Wallet { get; private set; }

    private ResourceManager resourceManager => ResourceManager.Instance;

    public event Action OnEndGame;

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
        int startBalance = resourceManager.GameConfigDataLookUp["StartBalance"].Value;
        int targetBalance = resourceManager.GameConfigDataLookUp["TargetBalance"].Value;

        Wallet = new(startBalance, () => FloatingTextUI.Instance.ShowText("Not enough money."));

        Wallet.OnBalanceChange += (balance) =>
        {
            if(balance >= targetBalance) OnEndGame?.Invoke();
        };
    }
}
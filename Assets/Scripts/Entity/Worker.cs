using System;
using System.Collections;
using UnityEngine;

public class Worker : IBuyableItem
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Type StorageType => typeof(Roster);

    public string TransactionLabel => $"Hire: {Price}";

    public Worker(WorkerData data)
    {
        Id = data.Id;
        Name = data.Name;
        Price = data.HireCost;
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Roster roster) return false;

        if (!wallet.TryWithdraw(Price)) return false;

        roster.AddWorker(this);

        return true;
    }

    public IEnumerator IE_RunLifeCycle()
    {
        while (true)
        {
            Plot plot = null;
            yield return new WaitUntil(() => FarmManager.Instance.TryGetEmptyPlot(out plot));

            yield return new WaitForSeconds(3.0f);

            plot?.PlantSeed(GameManager.Instance.Inventory);
        }
    }
}
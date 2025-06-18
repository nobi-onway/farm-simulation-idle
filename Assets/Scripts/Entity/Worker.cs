using System;
using System.Collections;
using UnityEngine;

public class Worker : IBuyableItem
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int TaskDuration { get; private set; }

    public Type StorageType => typeof(Roster);

    public string TransactionLabel => $"Hire: {Price}";

    private Plot _targetPlot;
    private BTNode _root;
    private float _actionStartTime;

    public Worker(WorkerData data)
    {
        Id = data.Id;
        Name = data.Name;
        Price = data.HireCost;
        TaskDuration = data.TaskDuration;

        _root = BuildBehaviorTree();
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Roster roster) return false;

        if (!wallet.TryWithdraw(Price)) return false;

        roster.AddWorker(this);

        return true;
    }

    private BTNode BuildBehaviorTree()
    {
        return new SelectorNode
        (
            new SequenceNode
            (
                new ConditionNode(() => FarmManager.Instance.TryGetEmptyPlot(out _targetPlot)),
                new TimerActionNode(() => DoAction(PlantSeed), TaskDuration)
            ),
            new SequenceNode
            (
                new ConditionNode(() => FarmManager.Instance.TryGetCanHarvestPlot(out _targetPlot)),
                new TimerActionNode(() => DoAction(Harvest), TaskDuration)
            )
        );
    }

    private bool DoAction(Action action)
    {
        action?.Invoke();

        return true;
    }

    private void PlantSeed()
    {
        _targetPlot.PlantSeed(FarmManager.Instance.Inventory);
        _targetPlot = null;
    }

    private void Harvest()
    {
        _targetPlot.Harvest(FarmManager.Instance.Inventory);
        _targetPlot = null;
    }

    public IEnumerator IE_RunLifeCycle()
    {
        while (true)
        {
            _root.Execute();
            yield return null;
        }
    }
}
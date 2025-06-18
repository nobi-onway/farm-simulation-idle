using System;

public enum EWorkerState { IDLE, PLANTING, HARVESTING }
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

    public EWorkerState State { get; private set; }
    public event Action<EWorkerState> OnStateChange;

    private WorkerData _data;

    public Worker(WorkerData data)
    {
        Id = data.Id;
        Name = data.Name;
        Price = data.HireCost;
        TaskDuration = data.TaskDuration;

        _data = data;

        State = EWorkerState.IDLE;
        _root = BuildBehaviorTree();
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if (storage is not Roster roster) return false;

        if (!wallet.TryWithdraw(Price)) return false;

        roster.AddWorker(CreateInstance());

        return true;
    }

    private Worker CreateInstance() => new(_data);

    private BTNode BuildBehaviorTree()
    {
        return new SelectorNode
        (
            ExecuteCurrentState(),
            DecideNextState()
        );
    }

    private BTNode ExecuteCurrentState()
    {
        return new SelectorNode
        (
            new SequenceNode
            (
                new ConditionNode(() => State == EWorkerState.PLANTING),
                new TimerActionNode(() => DoAction(PlantSeed), TaskDuration)
            ),
            new SequenceNode
            (
                new ConditionNode(() => State == EWorkerState.HARVESTING),
                new TimerActionNode(() => DoAction(Harvest), TaskDuration)
            )
        );
    }

    private BTNode DecideNextState()
    {
        return new SelectorNode
        (
            new SequenceNode
            (
                new ConditionNode(() => FarmManager.Instance.TryGetPlotWithCondition(out _targetPlot, p => p.IsFreeForWorker)),
                new ActionNode(() => DoAction(() => SetState(EWorkerState.PLANTING)))
            ),
            new SequenceNode
            (
                new ConditionNode(() => FarmManager.Instance.TryGetPlotWithCondition(out _targetPlot, p => p.CanWorkerHarvest)),
                new ActionNode(() => DoAction(() => SetState(EWorkerState.HARVESTING)))
            )
        );
    }

    private bool DoAction(Action action)
    {
        action?.Invoke();

        return true;
    }

    private bool DoAction(Func<bool> func)
    {
        return func.Invoke();
    }

    private void PlantSeed()
    {
        _targetPlot.PlantSeed(FarmManager.Instance.Inventory);
        _targetPlot.IsReserved = false;
        _targetPlot = null;

        SetState(EWorkerState.IDLE);
    }

    private bool Harvest()
    {
        bool success = _targetPlot.TryHarvest(FarmManager.Instance.Inventory);
        _targetPlot.IsReserved = false;
        _targetPlot = null;

        SetState(EWorkerState.IDLE);

        return success;
    }

    private void SetState(EWorkerState state)
    {
        State = state;
        OnStateChange?.Invoke(state);
    }

    public void Execute()
    {
        _root.Execute();
    }
}
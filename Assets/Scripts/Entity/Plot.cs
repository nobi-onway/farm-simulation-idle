using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public enum EPlotState { LOCK, EMPTY, PLANTED, DECAY }
public class Plot : IBuyableItem
{
    private Producer _producer;
    private ProducerItem _producerItem;
    public bool CanWorkerHarvest => (State == EPlotState.PLANTED || State == EPlotState.DECAY) && _producer.Yield > 0 && !IsReserved;
    public bool IsFreeForWorker => State == EPlotState.EMPTY && !IsReserved;
    public bool IsReserved { get; set; }

    public string ProducerItemId { get; private set; }
    public string Name { get; private set; }
    public string Id { get; private set; }
    public int Price { get; private set; }

    private float _boost;

    private EPlotState _state;
    public EPlotState State
    {
        get => _state;
        private set
        {
            _state = value;
            OnStateChange?.Invoke(_state);
        }
    }

    public Type StorageType => throw new NotImplementedException();

    public string TransactionLabel => throw new NotImplementedException();

    public event Action<EPlotState> OnStateChange;
    public event Action<float> OnTimer;
    public event Action<int, int> OnYieldChange;
    public event Action OnDecay;

    public Plot(PlotData data)
    {
        ProducerItemId = data.ProducerId;
        Name = data.Name;
        Id = data.Id;
        Price = data.Price;
        IsReserved = false;

        State = EPlotState.LOCK;
    }

    public IEnumerator IE_RunLifeCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => State == EPlotState.PLANTED);
            yield return IE_Planting();
            yield return IE_Decaying(Time.time, OnDecay);
        }
    }

    private IEnumerator IE_Planting()
    {
        _producer.OnYieldChange += OnYieldChange;
        yield return _producer.IE_Producing(OnTimer);
        State = EPlotState.DECAY;
    }

    private IEnumerator IE_Decaying(float startTime, Action OnDecay)
    {
        while (Time.time - startTime < _producerItem.DecayTimer && _producer.RemainingYield > 0)
        {
            OnTimer?.Invoke(_producerItem.DecayTimer - (Time.time - startTime));
            yield return null;
        }

        _producerItem = null;
        _producer = null;
        State = EPlotState.EMPTY;
        OnDecay?.Invoke();
    }

    public void PlantSeed(Inventory inventory)
    {   
        if(!inventory.TryGetItem(ProducerItemId, out ProducerItem producerItem)) return;

        _producerItem = producerItem;
        _producer = new(producerItem.YieldInterval, producerItem.MaxYield, _boost);

        State = EPlotState.PLANTED;
    }

    public bool TryHarvest(Inventory inventory)
    {
        if (_producer == null) return false;
        if (!_producer.TryConsumeYield(out int yield)) return false;

        inventory.AddItem(new Product(ResourceManager.Instance.ProductDataLookUp[_producerItem.Id]), yield);
        return true;
    }

    public void Upgrade()
    {
        _boost += 0.1f;
        _producer.YieldBoost = _boost;
    }

    public bool TryBuy(Wallet wallet, object storage)
    {
        if(storage is not FarmManager farmManager) return false;
        if(!wallet.TryWithdraw(Price)) return false;

        farmManager.AddPlot(this);
        State = EPlotState.EMPTY;
        return true;
    }
}
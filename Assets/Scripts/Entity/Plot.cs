using System;
using System.Collections;
using UnityEngine;

public enum EPlotState { EMPTY, PLANTED, DECAY }
public class Plot
{
    private Producer _producer;
    private ProducerItem _producerItem;
    public bool CanHarvest => State == EPlotState.PLANTED && _producer.Yield > 0;
    public string ProducerItemId { get; private set; }

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
    public event Action<EPlotState> OnStateChange;
    public event Action<float> OnTimer;
    public event Action<int, int> OnYieldChange;
    public event Action<ProducerItem> OnPlant;
    public event Action OnDecay;

    public Plot(string producerItemId, MonoBehaviour runner)
    {
        ProducerItemId = producerItemId;
        State = EPlotState.EMPTY;

        runner.StartCoroutine(IE_RunLifeCycle());
    }

    private IEnumerator IE_RunLifeCycle()
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
        while (Time.time - startTime < _producerItem.DecayTimer) yield return null;

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
        OnPlant?.Invoke(_producerItem);

        State = EPlotState.PLANTED;
    }

    public void Harvest(Inventory inventory)
    {
        if (!_producer.TryConsumeYield(out int yield)) return;

        inventory.AddItem(new Product(ResourceManager.Instance.ProductDataLookUp[_producerItem.Id]), yield);
    }

    public void Upgrade()
    {
        _boost += 0.1f;
        _producer.YieldBoost = _boost;
    }
}
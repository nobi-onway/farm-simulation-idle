using System;
using System.Collections;
using UnityEngine;

public class Plot
{
    private Producer _producer;
    private ProducerItem _producerItem;

    private float _boost;

    public void PlantSeed(ProducerItem seed)
    {
        _producerItem = seed;
        _producer = new(seed.YieldInterval, seed.MaxYield, _boost);
    }

    public IEnumerator IE_Plant(Action<ProducerItem> OnPlant, Action<int, int> OnYieldChange, Action<float> OnTimer, Action OnDecay)
    {
        _producer.OnYieldChange += OnYieldChange;
        OnPlant?.Invoke(_producerItem);

        yield return _producer.IE_Producing(OnTimer);
        yield return IE_Decay(Time.time, OnDecay);
    }

    public IEnumerator IE_Decay(float startTime, Action OnDecay)
    {
        while (Time.time - startTime < _producerItem.DecayTimer) yield return null;

        _producerItem = null;
        _producer = null;
        OnDecay?.Invoke();
    }

    public void Harvest()
    {
        if (!_producer.TryConsumeYield(out int yield)) return;

        GameManager.Instance.Wallet.Deposit(yield * _producerItem.Price);
    }

    public void Upgrade()
    {
        _boost += 0.1f;
        _producer.YieldBoost = _boost;
    }
}
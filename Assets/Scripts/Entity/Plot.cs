using System;
using System.Collections;
using UnityEngine;

public class Plot
{
    private Producer _producer;
    public bool IsPlanting => _producer != null;
    private Seed _seed => GameManager.Instance.Inventory.Seeds[0];

    public IEnumerator IE_Plant(Action<Seed> OnPlant, Action<int, int> OnYieldChange, Action<float> OnTimer, Action OnDecay)
    {
        _producer = new Producer(_seed);
        _producer.OnYieldChange += OnYieldChange;
        OnPlant?.Invoke(_seed);

        yield return _producer.IE_Producing(OnTimer);
        yield return IE_Decay(Time.time, OnDecay);
    }

    public IEnumerator IE_Decay(float startTime, Action OnDecay)
    {
        while (Time.time - startTime < 10) yield return null;

        _producer = null;
        OnDecay?.Invoke();
    }

    public void Harvest()
    {
        if (!_producer.TryConsumeYield(out int yield)) return;

        GameManager.Instance.Inventory.AddItem(new Product(_seed.ProductName), yield);
    }
}
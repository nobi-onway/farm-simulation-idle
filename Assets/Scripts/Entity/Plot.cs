using System;
using System.Collections;
using UnityEngine;


public class Plot
{
    private Producer _producer;
    private Seed _seed;

    public Plot()
    {
        _producer = new();
    }

    public void DoSeed(Seed seed)
    {
        _seed = seed;
        _producer.Initialize(seed.YieldInterval, seed.MaxYield);
    }

    public IEnumerator IE_Plant(Action<Seed> OnPlant, Action<int, int> OnYieldChange, Action<float> OnTimer, Action OnDecay)
    {
        _producer.OnYieldChange += OnYieldChange;
        OnPlant?.Invoke(_seed);

        yield return _producer.IE_Producing(OnTimer);
        yield return IE_Decay(Time.time, OnDecay);
    }

    public IEnumerator IE_Decay(float startTime, Action OnDecay)
    {
        while (Time.time - startTime < 5) { Debug.Log(Utils.TimeFormatter(Time.time - startTime)); yield return null; }

        OnDecay?.Invoke();
    }

    public void Harvest()
    {
        if (!_producer.TryConsumeYield(out int yield)) return;

        GameManager.Instance.Inventory.AddItem(new Product(_seed.ProductName), yield);
    }
}
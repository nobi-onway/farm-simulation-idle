using System;
using System.Collections;
using UnityEngine;

public class Plot
{
    private Producer _producer;
    public bool IsPlanting => _producer != null;

    public IEnumerator IE_Plant(Action<Seed> OnPlant, Action<int, int> OnProduceYield, Action<float> OnTimer, Action OnDecay)
    {
        Seed seed = GameManager.Instance.Inventory.Seeds[0];

        _producer = new Producer(seed);
        OnPlant?.Invoke(seed);

        yield return _producer.IE_Producing(OnProduceYield, OnTimer);
        yield return IE_Decay(Time.time, OnDecay);
    }

    public IEnumerator IE_Decay(float startTime, Action OnDecay)
    {
        while (Time.time - startTime < 10) yield return null;

        _producer = null;
        OnDecay?.Invoke();
    }
}
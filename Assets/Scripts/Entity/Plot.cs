using System;
using System.Collections;

public class Plot
{
    private Producer _producer;

    public bool IsPlanting => _producer != null;

    public IEnumerator IE_Plant(Action<Seed> OnPlant, Action<int, int> OnProduceYield, Action<float> OnTimer)
    {
        Seed seed = GameManager.Instance.Inventory.Seeds[0];

        _producer = new Producer(seed);
        OnPlant?.Invoke(seed);

        yield return _producer.IE_Producing(OnProduceYield, OnTimer);
    }
}
using System;
using System.Collections;
using UnityEngine;

public class Plot
{
    private const float DECAY_TIMER = 5.0f;
    private Producer _producer;
    private Seed _seed;

    private float _boost;

    public void PlantSeed(Seed seed)
    {
        _seed = seed;
        _producer = new(seed.YieldInterval, seed.MaxYield, _boost);
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
        while (Time.time - startTime < DECAY_TIMER) { Debug.Log(Utils.TimeFormatter(Time.time - startTime)); yield return null; }

        _seed = null;
        _producer = null;
        OnDecay?.Invoke();
    }

    public void Harvest()
    {
        if (!_producer.TryConsumeYield(out int yield)) return;

        GameManager.Instance.Wallet.Deposit(yield * _seed.Cost);
    }

    public void Upgrade()
    {
        _boost += 0.1f;
        _producer.YieldBoost = _boost;
    }
}
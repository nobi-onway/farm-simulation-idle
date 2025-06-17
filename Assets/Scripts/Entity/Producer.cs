using System;
using System.Collections;
using UnityEngine;

public class Producer
{
    public int Yield { get; private set; }
    public int RemainingYield { get; private set; }
    public int YieldInterval { get; private set; }

    public event Action<int, int> OnYieldChange;

    public Producer(Seed seed)
    {
        YieldInterval = seed.YieldInterval;
        RemainingYield = seed.MaxYield;
    }

    public IEnumerator IE_Producing(Action<float> OnTimer)
    {
        float timer = 0f;
        SetYield(0);

        while (Yield < RemainingYield)
        {
            float timeLeft = Mathf.Max(YieldInterval - timer, 0);
            OnTimer?.Invoke(timeLeft);

            timer += Time.deltaTime;

            if (timer >= YieldInterval)
            {
                ProduceProduct();
                timer = 0;
            }

            yield return null;
        }

        OnYieldChange = null;
    }

    private void ProduceProduct()
    {
        int yield = Mathf.Clamp(Yield + 1, 0, RemainingYield);

        SetYield(yield);
    }

    private void SetYield(int yield)
    {
        Yield = yield;
        OnYieldChange?.Invoke(Yield, RemainingYield);
    }

    public bool TryConsumeYield(out int yield)
    {
        yield = Yield;

        if (Yield == 0) return false;

        RemainingYield = Mathf.Clamp(RemainingYield - Yield, 0, RemainingYield);

        SetYield(0);

        return true;
    }
}
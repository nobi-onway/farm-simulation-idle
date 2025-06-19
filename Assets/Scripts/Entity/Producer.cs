using System;
using System.Collections;
using UnityEngine;

public class Producer
{
    private int _yield;
    private int _remainingYield;
    private int _yieldInterval;
    public float YieldBoost { get; set; }
    public float Yield => _yield;
    public float RemainingYield => _remainingYield;
    private float YieldTimer => (float)_yieldInterval / (1 + YieldBoost);

    public event Action<int, int> OnYieldChange;

    public Producer(ProducerItem producerItem, float yieldBoost = 0)
    {
        _yieldInterval = producerItem.YieldInterval;
        _remainingYield = producerItem.MaxYield;
        YieldBoost = yieldBoost;

        SetYield(0);
    }

    public IEnumerator IE_Producing(Action<float> OnTimer)
    {
        float progress = 0f;

        while (_yield < _remainingYield)
        {
            while (progress < 1f)
            {
                float speed = 1f / YieldTimer;
                progress = Mathf.Clamp01(progress + Time.deltaTime * speed);

                OnTimer?.Invoke((1 - progress) * YieldTimer);
                yield return null;
            }

            ProduceProduct();
            progress = 0f;
        }

        OnYieldChange = null;
    }

    private void ProduceProduct()
    {
        int yield = Mathf.Clamp(_yield + 1, 0, _remainingYield);

        SetYield(yield);
    }

    private void SetYield(int yield)
    {
        _yield = yield;
        OnYieldChange?.Invoke(_yield, _remainingYield);
    }

    public bool TryConsumeYield(out int yield)
    {
        yield = _yield;

        if (_yield == 0) return false;

        _remainingYield = Mathf.Clamp(_remainingYield - _yield, 0, _remainingYield);

        SetYield(0);

        return true;
    }
}
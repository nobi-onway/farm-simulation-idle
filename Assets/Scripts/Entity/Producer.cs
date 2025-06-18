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

    private float YieldTimer => (float)_yieldInterval / (1 + YieldBoost);
    public event Action<int, int> OnYieldChange;

    public Producer(int yieldInterval, int remainingYield, float yieldBoost = 0)
    {
        _yieldInterval = yieldInterval;
        _remainingYield = remainingYield;
        YieldBoost = yieldBoost;

        SetYield(0);
    }

    public IEnumerator IE_Producing(Action<float> OnTimer)
    {
        float timer = 0f;

        while (_yield < _remainingYield)
        {
            float timeLeft = Mathf.Max(YieldTimer - timer, 0);
            OnTimer?.Invoke(timeLeft);

            timer += Time.deltaTime;

            if (timer >= YieldTimer)
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
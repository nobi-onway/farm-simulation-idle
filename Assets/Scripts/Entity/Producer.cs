using System;
using System.Collections;
using UnityEngine;

public class Producer
{
    private Seed _seed;
    private int _yield;

    public Producer(Seed seed)
    {
        _seed = seed;
    }

    public IEnumerator IE_Producing(Action<int,int> OnProduceYield, Action<float> OnTimer)
    {
        float timer = 0f;
        OnProduceYield?.Invoke(_yield, _seed.MaxYield);

        while (_yield < _seed.MaxYield)
        {
            float timeLeft = Mathf.Max(_seed.MaxYield - timer, 0);
            OnTimer?.Invoke(timeLeft);

            timer += Time.deltaTime;

            if (timer >= _seed.MaxYield)
            {
                ProduceYield(OnProduceYield);
                timer = 0;
            }

            yield return null;
        }
    }

    private void ProduceYield(Action<int, int> OnProduceYield)
    {
        _yield = Mathf.Clamp(_yield + 1, 0, _seed.MaxYield);
        OnProduceYield?.Invoke(_yield, _seed.MaxYield);
    }
}
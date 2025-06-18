using System;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    private static FarmManager _instance;
    public static FarmManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<FarmManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("FarmManager").AddComponent<FarmManager>();
                }
            }

            return _instance;
        }
    }
    public List<Plot> Plots;

    public event Action<Plot> OnAddItem;

    private void Start()
    {
        Plots = new();

        foreach (ProducerData data in GameManager.Instance.ProducerDataLookUp.Values)
        {
            AddPlot(new Plot(data.Id, this));
        }
    }

    public void AddPlot(Plot plot)
    {
        Plots.Add(plot);
        OnAddItem?.Invoke(plot);    
    }
}
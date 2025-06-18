using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmManager : MonoSingleton<FarmManager>
{
    public List<Plot> Plots;
    public Roster Roster { get; private set; }
    public Inventory Inventory { get; private set; }


    private ResourceManager resourceManager => ResourceManager.Instance;

    private void Awake()
    {
        InitializeRoster();
        InitializeInventory();
    }

    private void Start()
    {
        InitializePlots();
    }

    private void Update()
    {
        Roster.ExecuteAllWorker();
    }

    private void InitializeRoster()
    {
        Roster = new();
    }

    private void InitializeInventory()
    {
        Inventory = new();

        foreach (string key in resourceManager.InventoryDataLookUp.Keys)
        {
            ProducerItem producerItem = new(resourceManager.ProducerDataLookUp[key]);
            int quantity = resourceManager.InventoryDataLookUp[key].Quantity;

            Inventory.AddItem(producerItem, quantity);
        }
    }

    private void InitializePlots()
    {
        Plots = new();
    }

    public void AddPlot(Plot plot)
    {
        Plots.Add(plot);

        StartCoroutine(plot.IE_RunLifeCycle());
    }

    public bool TryGetPlotWithCondition(out Plot plot, Func<Plot, bool> condition)
    {
        plot = Plots.FirstOrDefault(condition);

        if (plot == null) return false;

        plot.IsReserved = true;

        Debug.Log($"Plot reserved: {plot.Name}");
        return true;
    }
}
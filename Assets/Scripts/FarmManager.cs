using System;
using System.Collections.Generic;

public class FarmManager : MonoSingleton<FarmManager>
{
    public List<Plot> Plots;
    public Roster Roster { get; private set; }
    public Inventory Inventory { get; private set; }

    public event Action<Plot> OnAddItem;

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

    private void InitializeRoster()
    {
        Roster = new(this);
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

        foreach (ProducerData data in resourceManager.ProducerDataLookUp.Values)
        {
            AddPlot(new Plot(data.Id, this));
        }
    }

    public void AddPlot(Plot plot)
    {
        Plots.Add(plot);
        OnAddItem?.Invoke(plot);
    }

    public bool TryGetEmptyPlot(out Plot plot) => (plot = Plots.Find(p => p.State == EPlotState.EMPTY)) != null;
    public bool TryGetCanHarvestPlot(out Plot plot) => (plot = Plots.Find(p => p.CanHarvest)) != null;
}
using System;
using System.Collections.Generic;
using System.Linq;

public class FarmManager : MonoSingleton<FarmManager>
{
    public List<Plot> Plots;
    public List<Plot> OwnedPlots;
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
        OwnedPlots = new();

        foreach (PlotData data in ResourceManager.Instance.PlotDataLookup.Values)
        {
            Product product = new(ResourceManager.Instance.ProductDataLookUp[data.ProducerId]);

            Plot plot = new(data, product);

            Plots.Add(plot);
        }
    }

    public void AddToOwnedPlots(Plot plot)
    {
        OwnedPlots.Add(plot);

        StartCoroutine(plot.IE_RunLifeCycle());
    }

    public bool TryGetPlotWithCondition(out Plot plot, Func<Plot, bool> condition)
    {
        plot = Plots.FirstOrDefault(condition);

        if (plot == null) return false;

        plot.IsReserved = true;

        return true;
    }
}
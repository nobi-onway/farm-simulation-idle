using System.Collections.Generic;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    private Dictionary<string, ProducerData> _producerDataLookUp;
    public Dictionary<string, ProducerData> ProducerDataLookUp => _producerDataLookUp ??= LoaderUtils.LoadProducerData();

    private Dictionary<string, InventoryData> _inventoryDataLookUp;
    public Dictionary<string, InventoryData> InventoryDataLookUp => _inventoryDataLookUp ??= LoaderUtils.LoadInventoryData("Assets/Config/InitialInventoryData.csv");

    private Dictionary<string, ShopData> _shopDataLookUp;
    public Dictionary<string, ShopData> ShopDataLookUp => _shopDataLookUp ??= LoaderUtils.LoadShopData();

    private Dictionary<string, ProductData> _productDataLookUp;
    public Dictionary<string, ProductData> ProductDataLookUp => _productDataLookUp ??= LoaderUtils.LoadProductData();

    private Dictionary<string, WorkerData> _workerDataLookup;
    public Dictionary<string, WorkerData> WorkerDataLookup => _workerDataLookup ??= LoaderUtils.LoadWorkerData();
    
    private Dictionary<string, PlotData> _plotDataLookUp;
    public Dictionary<string, PlotData> PlotDataLookup => _plotDataLookUp ??= LoaderUtils.LoadPlotData();
}
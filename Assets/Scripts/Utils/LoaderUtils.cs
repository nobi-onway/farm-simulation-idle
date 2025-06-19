using System.Collections.Generic;
using System.IO;

public static class LoaderUtils
{
    public static Dictionary<string, ProducerData> LoadProducerData()
    {
        string[] lines = File.ReadAllLines("Assets/Config/ProducerData.csv");
        Dictionary<string, ProducerData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            ProducerData data = new
            (
                cells[0],
                cells[1],
                int.Parse(cells[2]),
                int.Parse(cells[3]),
                int.Parse(cells[4])
            );

            lookUp[data.Id] = data;
        }

        return lookUp;
    }

    public static Dictionary<string, InventoryData> LoadInventoryData(string csvPath)
    {
        string[] lines = File.ReadAllLines(csvPath);
        Dictionary<string, InventoryData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            InventoryData data = new
            (
                cells[0],
                int.Parse(cells[1])
            );

            lookUp[data.Id] = data;
        }

        return lookUp;
    }

    public static Dictionary<string, ShopData> LoadShopData()
    {
        string[] lines = File.ReadAllLines("Assets/Config/ShopData.csv");
        Dictionary<string, ShopData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            ShopData data = new
            (
                cells[0],
                cells[1],
                int.Parse(cells[2]),
                bool.Parse(cells[3].ToLower()),
                int.Parse(cells[4])
            );

            lookUp[data.Id] = data;
        }

        return lookUp;
    }

    public static Dictionary<string, ProductData> LoadProductData()
    {
        string[] lines = File.ReadAllLines("Assets/Config/ProductData.csv");
        Dictionary<string, ProductData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            ProductData data = new
            (
                cells[0],
                cells[1],
                cells[2],
                int.Parse(cells[3])
            );

            lookUp[data.ProducerId] = data;
        }

        return lookUp;
    }

    public static Dictionary<string, WorkerData> LoadWorkerData()
    {
        string[] lines = File.ReadAllLines("Assets/Config/WorkerData.csv");
        Dictionary<string, WorkerData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            WorkerData data = new
            (
                cells[0],
                cells[1],
                int.Parse(cells[2]),
                int.Parse(cells[3])
            );

            lookUp[data.Id] = data;
        }

        return lookUp;
    }

    public static Dictionary<string, PlotData> LoadPlotData()
    {
        string[] lines = File.ReadAllLines("Assets/Config/PlotData.csv");
        Dictionary<string, PlotData> lookUp = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');

            PlotData data = new
            (
                cells[0],
                cells[1],
                cells[2],
                int.Parse(cells[3]),
                int.Parse(cells[4])
            );

            lookUp[data.Id] = data;
        }

        return lookUp;
    }
}
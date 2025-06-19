public class PlotData
{
    public string Id { get; private set; }
    public string ProducerId { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int UpgradeCost { get; private set; }

    public PlotData(string id, string producerId, string name, int price, int upgradeCost)
    {
        Id = id;
        ProducerId = producerId;
        Name = name;
        Price = price;
        UpgradeCost = upgradeCost;
    }
}
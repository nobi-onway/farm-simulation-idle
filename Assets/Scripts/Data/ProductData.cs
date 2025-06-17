public class ProductData
{
    public string Id { get; private set; }
    public string ProducerId { get; private set; }
    public string Name { get; private set; }
    public int SellPrice { get; private set; }

    public ProductData(string id, string producerId, string name, int sellPrice)
    {
        Id = id;
        ProducerId = producerId;
        Name = name;
        SellPrice = sellPrice;
    }
}
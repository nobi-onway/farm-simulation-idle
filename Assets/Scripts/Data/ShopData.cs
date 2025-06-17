public class ShopData
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public bool BulkOnly { get; private set; }
    public int BulkAmount { get; private set; }

    public ShopData(string id, string name, int price, bool bulkOnly, int bulkAmount)
    {
        Id = id;
        Name = name;
        Price = price;
        BulkOnly = bulkOnly;
        BulkAmount = bulkAmount;
    }
}
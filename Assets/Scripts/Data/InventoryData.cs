public class InventoryData
{
    public string Id { get; private set; }
    public int Quantity { get; private set; }

    public InventoryData(string id, int quantity) => (Id, Quantity) = (id, quantity);
}
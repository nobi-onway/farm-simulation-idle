public interface IInventoryItem
{
    public string Name { get; }
    public string Id { get; }
    public int Quantity { get; set; }
}
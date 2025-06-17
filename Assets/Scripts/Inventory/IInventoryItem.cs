public interface IInventoryItem
{
    public string Name { get; }
    public int Id { get; }
    public int Quantity { get; set; }
}
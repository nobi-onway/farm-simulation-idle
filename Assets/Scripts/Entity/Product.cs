public class Product : IInventoryItem
{
    public string Name { get; private set; }
    public int Quantity { get; set; }
    public int Id => Name.GetHashCode();

    string IInventoryItem.Id => throw new System.NotImplementedException();

    public Product(string name)
    {
        Name = name;
    }
}
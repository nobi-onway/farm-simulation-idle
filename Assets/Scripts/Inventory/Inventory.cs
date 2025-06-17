using System.Collections.Generic;

public class Inventory
{
    public List<Seed> Seeds { get; private set; }

    public Inventory()
    {
        Seeds = new();
    }

    public void AddSeed(Seed seed, int quantity)
    {
        seed.Quantity = quantity;

        Seeds.Add(seed);
    }
}

public class ProducerData
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int YieldInterval { get; private set; }
    public int MaxYield { get; private set; }
    public int DecayTimer { get; private set; }

    public ProducerData(string id, string name, int yieldInterval, int maxYield, int decayTimer)
    {
        Id = id;
        Name = name;
        YieldInterval = yieldInterval;
        MaxYield = maxYield;
        DecayTimer = decayTimer;
    }
}
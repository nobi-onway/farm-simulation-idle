public class GameConfigData
{
    public string Key { get; set; }
    public int Value { get; set; }

    public GameConfigData(string key, int value)
    {
        Key = key;
        Value = value;
    }
}
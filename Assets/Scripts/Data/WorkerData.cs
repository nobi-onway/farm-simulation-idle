public class WorkerData
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int HireCost { get; private set; }
    public int TaskDuration { get; private set; }

    public WorkerData(string id, string name, int hireCost, int taskDuration)
    {
        Id = id;
        Name = name;
        HireCost = hireCost;
        TaskDuration = taskDuration;
    }
}
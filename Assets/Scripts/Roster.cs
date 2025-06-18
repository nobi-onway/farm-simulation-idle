using System;
using System.Collections.Generic;

public class Roster
{
    public List<Worker> Workers;

    public event Action<Worker> OnAddWorker;

    public Roster()
    {
        Workers = new();
    }

    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
        OnAddWorker?.Invoke(worker);
    }
}
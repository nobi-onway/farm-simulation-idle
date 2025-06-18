using System;
using System.Collections.Generic;
using UnityEngine;

public class Roster
{
    public List<Worker> Workers;

    public event Action<Worker> OnAddWorker;

    private MonoBehaviour _runner;

    public Roster(MonoBehaviour runner)
    {
        Workers = new();

        _runner = runner;
    }

    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
        OnAddWorker?.Invoke(worker);

        _runner.StartCoroutine(worker.IE_RunLifeCycle());
    }
}
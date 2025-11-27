using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectorHub : MonoBehaviour
{
    private List<Collector> _collectors = new List<Collector>();

    public event Action<Collector> CollectorAvailabled;

    void OnDisable()
    {
        foreach (var collector in _collectors)
        {
            collector.JobFinished -= CollectorAvailabled;
        }
    }

    public void RegisterCollector(Collector collector)
    {
        if (!_collectors.Contains(collector))
        {
            collector.JobFinished += CollectorAvailabled;

            _collectors.Add(collector);
        }
    }

    public bool TryGetAvailableCollector(out Collector collector)
    {
        collector = GetAvailableCollector();
        return collector != null;
    }

    public CollectJob FindInActiveJobs(Predicate<CollectJob> predicate)
    {
        foreach (var collector in _collectors)
        {
            var currentJob = collector.CurrentJob;
            if (currentJob != null && predicate(currentJob))
            {
                return currentJob;
            }
        }
        return null;
    }

    public void AssignCollectJob(CollectJob job)
    {
        if (TryGetAvailableCollector(out var collector))
        {
            collector.BeginCollect(job);
        }
    }

    private Collector GetAvailableCollector()
    {
        foreach (var collector in _collectors)
        {
            if (!collector.IsBusy)
                return collector;
        }

        return null;
    }

    private void OnCollectorJobFinished(Collector collector, IJob job)
    {
        CollectorAvailabled?.Invoke(collector);
    }
}
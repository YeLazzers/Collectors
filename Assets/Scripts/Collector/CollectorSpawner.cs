using System.Collections.Generic;
using UnityEngine;

public class CollectorSpawner : PoolBase<Collector>
{

    private List<Collector> _activeCollectors = new();

    public List<Collector> ActiveCollectors => _activeCollectors;

    public void Spawn(Vector3 position, Vector3 direction)
    {
        _activeCollectors.Add(Get().Initialize(position, direction));
    }

    public bool TryGetAvailableCollector(out Collector collector)
    {
        collector = GetAvailableCollector();
        return collector != null;
    }

    private Collector GetAvailableCollector()
    {
        foreach (var collector in _activeCollectors)
        {
            if (!collector.IsBusy)
                return collector;
        }

        return null;
    }
}
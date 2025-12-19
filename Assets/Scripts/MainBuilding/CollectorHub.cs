using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectorHub : MonoBehaviour
{
    private readonly float _yPosition = 1f;

    [SerializeField] private Sprite _icon;
    [SerializeField] private CollectorSpawner _spawner;
    [SerializeField] private float _spawnRadius = 1f;

    private List<Collector> _collectors = new List<Collector>();

    public int CollectorsCount => _collectors.Count;
    public Sprite Icon => _icon;

    public event Action<Collector> CollectorAvailabled;
    public event Action<int> CollectorsCountChanged;

    void OnDisable()
    {
        foreach (var collector in _collectors)
        {
            collector.JobFinished -= CollectorAvailabled;
        }
    }

    public void TrainCollector(int count)
    {
        float randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);
        for (int i = 0; i < count; i++)
        {
            var collector = _spawner.Spawn(GetCollectorSpawnPosition(i, count, randomRotationOffset), transform.position);

            collector.JobFinished += CollectorAvailabled;

            _collectors.Add(collector);
            
            CollectorsCountChanged?.Invoke(_collectors.Count);
            CollectorAvailabled?.Invoke(collector);
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

    private Vector3 GetCollectorSpawnPosition(int index, int total, float radialOffset = 0f)
    {
        float range = 2 * Mathf.PI / total;
        float angle = index * range + radialOffset;
        float x = _spawnRadius * Mathf.Cos(angle);
        float z = _spawnRadius * Mathf.Sin(angle);

        return new Vector3(
            transform.position.x + x,
            _yPosition,
            transform.position.z + z
        );
    }
}
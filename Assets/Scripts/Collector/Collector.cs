using System;
using UnityEngine;

public class Collector : MonoBehaviour, IPoolable<Collector>, ICollectWorkable
{
    [SerializeField] private CollectorBrain _brain;

    [SerializeField] private float _movementSpeed;

    private SplinePath _splinePath;
    private CollectJob _collectJob;

    public event Action<Collector> Expired;

    public CollectJob CurrentJob => _collectJob;
    public bool IsBusy => _collectJob != null;
    public float Speed => _movementSpeed;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
    }

    public Collector Initialize(Vector3 position)
    {
        transform.position = position;
        ClearJob();

        return this;
    }

    public Collector Initialize(Vector3 position, Vector3 direction, SplinePath splinePath)
    {
        Initialize(position);

        transform.LookAt(direction);
        _splinePath = splinePath;

        _brain.Initialize(_splinePath);

        return this;
    }

    public void SetJob(IJob job)
    {
        job.ApplyTo(this);
    }

    public void ClearJob()
    {
        _collectJob = null;
    }

    public void BeginCollect(CollectJob job)
    {
        _collectJob = job;

        _brain.BeginCollect();
    }
}
using System;
using UnityEngine;

public class Collector : MonoBehaviour, IPoolable<Collector>
{
    [SerializeField] private JobRunner _jobRunner;
    [SerializeField] private CollectorBrain _brain;
    [SerializeField] private float _movementSpeed;

    private SplinePath _splinePath;

    public event Action<Collector> Expired;

    public float Speed => _movementSpeed;
    public JobRunner JobRunner => _jobRunner;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
    }

    public Collector Initialize(Vector3 position)
    {
        transform.position = position;

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
}
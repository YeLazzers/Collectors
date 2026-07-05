using System;
using UnityEngine;
using YeLazzers.Buildings;

[RequireComponent(typeof(JobRunner))]
public class Worker : MonoBehaviour, IPoolable<Worker>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Building _home;
    private JobRunner _jobRunner;

    public event Action<Worker> Expired;

    public float Speed => _speed;

    public float RotationSpeed => _rotationSpeed;

    public Building Home => _home;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
        _jobRunner = GetComponent<JobRunner>();
    }

    public Worker Initialize(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public Worker Initialize(Vector3 position, Vector3 direction)
    {
        transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction - position));
        return this;
    }

    public void AssignToStation(Building home, JobBoard jobBoard)
    {
        _home = home;

        _jobRunner.SetJobBoard(jobBoard);
        _jobRunner.Run();
    }
}

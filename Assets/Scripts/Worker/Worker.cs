using System;
using UnityEngine;

public class Worker : MonoBehaviour, IPoolable<Worker>
{
    [SerializeField] private float _speed;

    public event Action<Worker> Expired;

    public float Speed => _speed;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
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
}

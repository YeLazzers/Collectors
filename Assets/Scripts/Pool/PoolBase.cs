using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolBase<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] protected T Prefab;

    protected ObjectPool<T> Pool;

    private int _createdCount;
    private int _spawnedCount;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            CreateFunc,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy
        );
    }

    public event Action<T> Getted;
    public event Action<T> Created;
    public event Action<T> Released;

    public int CreatedCount => _createdCount;
    public int SpawnedCount => _spawnedCount;
    public int ActiveCount => Pool.CountActive;

    public T Get() =>
        Pool.Get();

    private T CreateFunc()
    {
        T poolable = GameObject.Instantiate(Prefab, transform);
        poolable.Expired += Pool.Release;

        _createdCount++;
        Created?.Invoke(poolable);

        return poolable;
    }

    private void ActionOnGet(T poolable)
    {
        poolable.gameObject.SetActive(true);

        _spawnedCount++;
        Getted?.Invoke(poolable);
    }

    private void ActionOnRelease(T poolable)
    {
        poolable.gameObject.SetActive(false);
        Released?.Invoke(poolable);
    }

    private void ActionOnDestroy(T poolable)
    {
        poolable.Expired -= Pool.Release;
        GameObject.Destroy(poolable.gameObject);
    }
}
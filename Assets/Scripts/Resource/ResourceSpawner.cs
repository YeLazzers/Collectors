using System.Collections;
using UnityEngine;

public class ResourceSpawner : PoolBase<Resource>
{
    [SerializeField] private int _maxResources = 10;
    [SerializeField] private float _spawnIntervalMin = 3f;
    [SerializeField] private float _spawnIntervalMax = 5f;
    [SerializeField] private bool _isBurstEnabled;

    private Bounds _spawnBounds;

    public void Initialize(Bounds spawnBounds)
    {
        _spawnBounds = spawnBounds;

        if (_isBurstEnabled)
        {
            for (int i = 0; i < _maxResources; i++)
            {
                Spawn();
            }
        }
        else
        {
            StartCoroutine(SpawnResources());
        }
    }

    public void Release(Resource resource)
    {
        resource.transform.SetParent(transform);
        Pool.Release(resource);
    }

    private Resource Spawn()
    {
        var point = new Vector3(
            Random.Range(_spawnBounds.min.x, _spawnBounds.max.x),
            transform.position.y,
            Random.Range(_spawnBounds.min.z, _spawnBounds.max.z));

        return Get().Initialize(point, transform);
    }

    private IEnumerator SpawnResources()
    {
        while (enabled)
        {
            if (ActiveCount < _maxResources)
            {
                Spawn();
            }

            yield return new WaitForSeconds(Random.Range(_spawnIntervalMin, _spawnIntervalMax));
        }
    }
}
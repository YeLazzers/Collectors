using System.Collections;
using UnityEngine;

public class ResourceSpawner : PoolBase<Resource>
{
    [SerializeField] private SpawnArea _spawnArea;
    [SerializeField] private int _maxResources = 10;
    [SerializeField] private float _spawnIntervalMin = 3f;
    [SerializeField] private float _spawnIntervalMax = 5f;
    [SerializeField] private bool _isBirstEnabled;

    private void OnEnable()
    {
        if (_isBirstEnabled)
        {
            for (int i = 0; i < _maxResources; i++)
                Spawn();
        }
        else
        {
            StartCoroutine(SpawnResources());
        }
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

    public Resource Spawn()
    {
        return Get().Initialize(_spawnArea.GetRandomPointInArea(transform.position), transform);
    }

    public void Release(Resource resource)
    {
        resource.transform.SetParent(transform);
        Pool.Release(resource);
    }
}
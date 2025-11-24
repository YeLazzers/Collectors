using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainBuilding : MonoBehaviour
{
    private readonly float _yPosition = 1f;

    [Header("Spawners")]
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    [Header("Scanner Params")]
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;
    [SerializeField] private float _landingRadius = 1f;

    [Header("Collector Spawner Params")]
    [SerializeField] private int _initialCollectorsCount = 3;
    [SerializeField] private float _spawnRadius = 1f;

    private WaitForSeconds _scanWait;
    private List<IScannable> _scannedResources = new List<IScannable>();
    private ResourceStorage _resourceStorage = new ResourceStorage();

    private void Awake()
    {
        _scanWait = new WaitForSeconds(_scanInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(Scanning());

        if (_scanner != null)
            _scanner.ScannableDetected += OnResourceScanned;
    }

    private void OnDisable()
    {
        if (_scanner != null)
            _scanner.ScannableDetected -= OnResourceScanned;
    }

    private void Start()
    {
        // _scanner.Scan();

        float randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);

        for (int i = 0; i < _initialCollectorsCount; i++)
        {
            _collectorSpawner.Spawn(GetCollectorSpawnPosition(i, _initialCollectorsCount, randomRotationOffset), transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _landingRadius);
    }

    public void TakeResource(Resource resource, Action onDone = null)
    {
        resource.Collect(transform, () =>
        {
            _resourceStorage.Add(resource);
            _resourceSpawner.Release(resource);
            _scannedResources.Remove(resource);
            onDone?.Invoke();
        });
    }

    public Resource GetNextResource()
    {
        return (Resource)_scannedResources.First();
    }

    public Vector3 GetLandingPoint(Vector3 originPos)
    {
        Vector3 dir = (transform.position - originPos).normalized;
        return transform.position - dir * _landingRadius;
    }

    private IEnumerator Scanning()
    {
        while (enabled)
        {
            yield return _scanWait;
            _scanner.Scan();
        }
    }

    private void OnResourceScanned(IScannable scannable)
    {
        if (!_scannedResources.Contains(scannable))
        {
            if (scannable is ICollectable collectable && !collectable.IsCollected && _collectorSpawner.TryGetAvailableCollector(out Collector collector))
            {
                collector.BeginCollect(new CollectJob(collectable, this));
                _scannedResources.Add(scannable);
            }
        }
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
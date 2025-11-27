using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainBuilding : MonoBehaviour
{
    private readonly float _yPosition = 1f;

    [Header("Spawners")]
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    [Header("Components")]
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private CollectorHub _hub;

    [Header("Scanner Params")]
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;
    [SerializeField] private float _landingRadius = 1f;

    [Header("Collector Spawner Params")]
    [SerializeField] private int _initialCollectorsCount = 3;
    [SerializeField] private float _spawnRadius = 1f;

    private WaitForSeconds _scanWait;
    private List<IScannable> _scannedResources = new List<IScannable>();

    private void Awake()
    {
        _scanWait = new WaitForSeconds(_scanInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(Scanning());

        _scanner.ScannableDetected += OnResourceScanned;
        _hub.CollectorAvailabled += OnCollectorAvailabled;
    }

    private void OnDisable()
    {
        _scanner.ScannableDetected -= OnResourceScanned;
        _hub.CollectorAvailabled -= OnCollectorAvailabled;
    }

    private void Start()
    {
        float randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);

        for (int i = 0; i < _initialCollectorsCount; i++)
        {
            var collector = _collectorSpawner.Spawn(GetCollectorSpawnPosition(i, _initialCollectorsCount, randomRotationOffset), transform.position);
            _hub.RegisterCollector(collector);
        }
    }

    public void TakeResource(Resource resource, Action onDone = null)
    {
        resource.Collect(transform, () =>
        {
            _resourceStorage.Add(resource.Amount);
            _scannedResources.Remove(resource);

            _resourceSpawner.Release(resource);
            onDone?.Invoke();
        });
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
            scannable.Scan();
            _scannedResources.Add(scannable);

            if (scannable is ICollectable collectable)
            {
                _hub.AssignCollectJob(new CollectJob(collectable, this));
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


    private void OnCollectorAvailabled(Collector collector)
    {
        foreach (var scannable in _scannedResources)
        {
            if (scannable is ICollectable collectable && !IsResourceAlreadyAssigned(collectable))
            {
                _hub.AssignCollectJob(new CollectJob(collectable, this));
                break;
            }
        }
    }

    private bool IsResourceAlreadyAssigned(ICollectable collectable)
    {
        return _hub.FindInActiveJobs(job => job.Collectable == collectable) != null;
    }
}
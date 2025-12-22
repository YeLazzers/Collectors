using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private ResourceSpawner _resourceSpawner;

    [Header("Components")]
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private CollectorHub _hub;
    [SerializeField] private BuildingView _view;

    [Header("Scanner Params")]
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;
    [SerializeField] private float _landingRadius = 1f;

    [Header("Collector Spawner Params")]
    [SerializeField] private int _initialCollectorsCount = 3;

    private WaitForSeconds _scanWait;
    private List<ICollectable> _scannedResources = new List<ICollectable>();
    private BuildingConfig _config;

    public BuildingConfig Config => _config;

    private void Awake()
    {
        _scanWait = new WaitForSeconds(_scanInterval);

        if (_config == null)
            _config = (BuildingConfig)Resources.Load("BuildingConfigs/Factory");
    }

    private void OnEnable()
    {
        StartCoroutine(Scanning());

        _scanner.CollectableDetected += OnResourceScanned;
        _hub.CollectorAvailabled += OnCollectorAvailabled;
    }

    private void OnDisable()
    {
        _scanner.CollectableDetected -= OnResourceScanned;
        _hub.CollectorAvailabled -= OnCollectorAvailabled;
    }

    private void Start()
    {
        _hub.TrainCollector(_initialCollectorsCount);
    }

    public void Initialize(BuildingConfig config, Vector3 position)
    {
        transform.position = position;

        _config = config;

        _view.RenderModel(config);
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

    private void OnResourceScanned(ICollectable collectable)
    {
        if (!_scannedResources.Contains(collectable))
        {
            _hub.AssignCollectJob(new CollectJob(collectable, this));
            _scannedResources.Add(collectable);

            if (collectable is IHighlightable highlightable)
            {
                highlightable.Highlight();
            }
        }
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
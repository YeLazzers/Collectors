using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private CollectorHub _hub;
    [SerializeField] private BuildingView _view;
    [SerializeField] private JobBoard _jobBoard;

    [Header("Scanner Params")]
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;
    [SerializeField] private float _landingRadius = 1f;

    [Header("Collector Spawner Params")]
    [SerializeField] private int _initialCollectorsCount = 3;

    private WaitForSeconds _scanWait;
    private List<ICollectable> _scannedResources = new List<ICollectable>();
    private BuildingConfig _config;
    private ResourceSpawner _resourceSpawner;

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
    }

    private void OnDisable()
    {
        _scanner.CollectableDetected -= OnResourceScanned;
    }

    private void Start()
    {
        _hub.TrainWorker(_initialCollectorsCount);
    }

    public void Initialize(ResourceSpawner resourceSpawner, BuildingConfig config, Vector3 position)
    {
        _resourceSpawner = resourceSpawner;
        transform.position = position;

        _config = config;

        _view.RenderModel(config);
    }

    public void Deposit(Resource resource, Action onDone = null)
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
            var jobContext = new GatheringJobContext((Resource)collectable, this);
            _jobBoard.Publish(new GatheringJob(jobContext, 1));

            _scannedResources.Add(collectable);

            if (collectable is IHighlightable highlightable)
            {
                highlightable.Highlight();
            }
        }
    }
}
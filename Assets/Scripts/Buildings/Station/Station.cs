using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YeLazzers.Buildings.Modules;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class Station : MonoBehaviour
    {
        [Header("Building")]
        [SerializeField] private BuildingConfig _config;

        [Header("Modules")]
        [SerializeField] private Interactable _interactable;

        [Header("Systems")]
        [SerializeField] private ResourceStorage _resourceStorage;
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private JobBoard _jobBoard;

        [Header("Scanner Params")]
        [SerializeField] private Scanner _scanner;
        [SerializeField] private float _scanInterval = 5f;
        [SerializeField] private float _landingRadius = 1f;

        [Header("Collector Spawner Params")]
        [SerializeField] private int _initialCollectorsCount = 3;

        private Building _building;
        private WaitForSeconds _scanWait;
        private List<ICollectable> _scannedResources = new List<ICollectable>();
        private ResourceSpawner _resourceSpawner;

        public BuildingConfig Config => _building.Config;

        private void Awake()
        {
            _building = GetComponent<Building>();
            _scanWait = new WaitForSeconds(_scanInterval);
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

        public void Initialize(ResourceSpawner resourceSpawner, Vector3 position)
        {
            _building.Initialize(_config, position);
            _interactable.Initialize(_building.View);
            _resourceSpawner = resourceSpawner;
        }

        public bool TryDeposit(Resource resource)
        {
            if (resource == null)
                return false;

            resource.Transform.DOMove(transform.position, 1f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    _resourceStorage.Add(resource.Amount);
                    _scannedResources.Remove(resource);
                    _resourceSpawner.Release(resource);
                });

            return true;
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
}

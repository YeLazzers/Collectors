using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeLazzers.Buildings.Modules;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class Station : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] private Interactable _interactable;

        [Header("Systems")]
        [SerializeField] private Scanner _scanner;
        [SerializeField] private JobBoard _jobBoard;
        [SerializeField] private WorkerHub _hub;

        [Header("Scanner Params")]
        [SerializeField] private float _scanInterval = 5f;

        private Building _building;
        private WaitForSeconds _scanWait;
        private List<ICollectable> _scannedResources = new List<ICollectable>();

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
            _interactable.Initialize(_building.View);
        }

        public void Initialize(int startWorkerCount)
        {
            _hub.Initialize(startWorkerCount);
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
                var jobContext = new GatheringJobContext((Resource)collectable, _building);
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

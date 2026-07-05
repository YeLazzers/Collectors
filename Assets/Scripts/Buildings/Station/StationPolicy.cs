using System;
using UnityEngine;
using YeLazzers.Jobs;

namespace YeLazzers.Buildings
{
    public enum StationPolicyType
    {
        Idle,
        Training,
        Construction,
    }

    public class StationPolicy : MonoBehaviour
    {
        private const int MinWorkersToBuild = 2;

        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private CustomLineRenderer _lineRenderer;
        [SerializeField] private JobBoard _jobBoard;

        private StationPolicyType _currentPolicy = StationPolicyType.Training;

        private ConstructionSite _activeSite;
        private BuildingJob _activeBuildingJob;

        public ConstructionSite ActiveSite => _activeSite;

        public void SetActiveSite(ConstructionSite site)
        {
            if (_activeSite != site)
            {
                UnsubscribeFromActiveSite();

                _activeSite = site;
                _activeSite.SiteCompleted += OnSiteCompleted;
                TryEnterConstruction();
            }

            _lineRenderer.DrawLine(new[] { transform.position, _activeSite.transform.position });
        }

        private void OnEnable()
        {
            _storage.AmountChanged += OnResourceChanged;
        }

        private void OnDisable()
        {
            _storage.AmountChanged -= OnResourceChanged;
            UnsubscribeFromActiveSite();
        }

        private void OnResourceChanged(int newAmount)
        {
            switch (_currentPolicy)
            {
                default:
                case StationPolicyType.Idle:
                    break;
                case StationPolicyType.Training:
                    _hub.TryTrainWorker();
                    TryEnterConstruction();
                    break;
                case StationPolicyType.Construction:
                    TryBuildConstruction(newAmount);
                    break;
            }
        }

        private void TryBuildConstruction(int availableResources)
        {
            if (_activeSite == null || _activeBuildingJob != null)
                return;

            var cost = _activeSite.Config.Cost;

            if (availableResources >= cost)
            {
                _activeBuildingJob = new BuildingJob(new BuildingJobContext(_activeSite));
                _jobBoard.Publish(_activeBuildingJob);
            }
        }

        private void OnSiteCompleted(ConstructionSite site, Action<Building> reportNewBuilding)
        {
            UnsubscribeFromActiveSite();

            _activeSite = null;
            _activeBuildingJob = null;

            _lineRenderer.ClearLine();
            SetPolicy(StationPolicyType.Training);
        }

        private void TryEnterConstruction()
        {
            if (_activeSite != null && _hub.WorkerCount >= MinWorkersToBuild)
            {
                SetPolicy(StationPolicyType.Construction);
            }
        }

        private void SetPolicy(StationPolicyType policy)
        {
            _currentPolicy = policy;
        }

        private void UnsubscribeFromActiveSite()
        {
            if (_activeSite != null)
            {
                _activeSite.SiteCompleted -= OnSiteCompleted;
            }
        }
    }
}

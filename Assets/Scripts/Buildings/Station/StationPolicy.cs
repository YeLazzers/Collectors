using System;
using UnityEngine;
using YeLazzers.Jobs;

namespace YeLazzers.Buildings
{
    public enum StationPolicyType
    {
        Idle,
        Production,
        Construction,
    }

    public class StationPolicy : MonoBehaviour
    {
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private CustomLineRenderer _lineRenderer;
        [SerializeField] private JobBoard _jobBoard;

        private StationPolicyType _currentPolicy = StationPolicyType.Production;

        private ConstructionSite _activeSite;
        private BuildingJob _activeBuildingJob;

        public ConstructionSite ActiveSite => _activeSite;

        public void SetActiveSite(ConstructionSite site)
        {
            if (_activeSite != site)
            {
                if (_activeSite != null)
                {
                    _activeSite.SiteCompleted -= OnSiteCompleted;
                }

                _activeSite = site;
                _activeSite.SiteCompleted += OnSiteCompleted;
            }

            if (_activeBuildingJob == null)
            {
                _currentPolicy = StationPolicyType.Construction;
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

            if (_activeSite != null)
            {
                _activeSite.SiteCompleted -= OnSiteCompleted;
            }
        }

        private void OnResourceChanged(int newAmount)
        {
            switch (_currentPolicy)
            {
                default:
                case StationPolicyType.Idle:
                    break;
                case StationPolicyType.Production:
                    _hub.TryTrainWorker();
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
                _activeBuildingJob = new BuildingJob(new BuildingJobContext(_activeSite), 2);
                _jobBoard.Publish(_activeBuildingJob);
            }
        }

        private void OnSiteCompleted(ConstructionSite site, Action<Building> reportNewBuilding)
        {
            site.SiteCompleted -= OnSiteCompleted;

            _activeSite = null;
            _activeBuildingJob = null;
            _currentPolicy = StationPolicyType.Production;
            _lineRenderer.ClearLine();
        }
    }
}

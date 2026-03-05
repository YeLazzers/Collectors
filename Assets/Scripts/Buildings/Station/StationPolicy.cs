using UnityEngine;

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
        [SerializeField] private WorkerTrainer _trainer;
        [SerializeField] private BuildingConstructor _builder;
        [SerializeField] private JobBoard _jobBoard;

        private StationPolicyType _currentPolicy = StationPolicyType.Production;

        private ConstructionSite _activeSite;
        private BuildingJob _activeBuildingJob;

        private void OnEnable()
        {
            _builder.SitePlaced += OnSitePlaced;
            _storage.AmountChanged += OnResourceChanged;
        }

        private void OnDisable()
        {
            _builder.SitePlaced -= OnSitePlaced;
            _storage.AmountChanged -= OnResourceChanged;
        }

        private void OnSitePlaced(ConstructionSite site)
        {
            _activeSite = site;
            _currentPolicy = StationPolicyType.Construction;
        }

        private void OnResourceChanged(int newAmount)
        {
            switch (_currentPolicy)
            {
                default:
                case StationPolicyType.Idle:
                    break;
                case StationPolicyType.Production:
                    _trainer.TryTrainWorker();
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
                _activeBuildingJob = new BuildingJob(new BuildingJobContext(_activeSite.Config, _activeSite.Position, _builder), 2);
                _jobBoard.Publish(_activeBuildingJob);
            }
        }
    }
}

using UnityEngine;

public enum BuildingPolicyType
{
    Idle,
    Production,
    Construction,
}

public class BuildingPolicy : MonoBehaviour
{
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private CollectorHub _hub;
    [SerializeField] private CollectorTrainer _trainer;
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private JobBoard _jobBoard;

    private BuildingPolicyType _currentPolicy = BuildingPolicyType.Production;

    private BuildingConstructionSite _activeSite;
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

    private void OnSitePlaced(BuildingConstructionSite site)
    {
        _activeSite = site;
        _currentPolicy = BuildingPolicyType.Construction;
    }

    private void OnResourceChanged(int newAmount)
    {
        switch (_currentPolicy)
        {
            default:
            case BuildingPolicyType.Idle:
                break;
            case BuildingPolicyType.Production:
                _trainer.TryTrainCollector();
                break;
            case BuildingPolicyType.Construction:
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
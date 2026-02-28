using System;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    [SerializeField] private MainBuilding _buildingPrefab;
    [SerializeField] private BuildingConstructionSite _constructionSitePrefab;
    [SerializeField] private CustomLineRenderer _lineRenderer;

    private IJob _buildingJob;
    private bool _isBuildingInProgress = false;
    private BuildingConstructionSite _currentConstructionSite;

    public bool IsBuildingInProgress => _isBuildingInProgress;

    public event Action<BuildingConstructionSite> SitePlaced;
    public event Action<BuildingConstructionSite> SiteMoved;
    public event Action SiteCompleted;

    public void InitBuilding(BuildingConfig config, Vector3 position)
    {
        _currentConstructionSite = Instantiate(_constructionSitePrefab, position, Quaternion.identity);
        _currentConstructionSite.Initialize(config);

        _isBuildingInProgress = true;

        _lineRenderer.DrawLine(new Vector3[] { transform.position, position });

        SitePlaced?.Invoke(_currentConstructionSite);
    }

    public void ReplaceConstructionSite(Vector3 newPosition)
    {
        if (_currentConstructionSite != null)
        {
            _currentConstructionSite.transform.position = newPosition;
            _lineRenderer.DrawLine(new Vector3[] { transform.position, newPosition });
        }
    }

    public void FinishBuilding()
    {
        if (_currentConstructionSite != null)
        {
            var building = Instantiate(_buildingPrefab);
            building.Initialize(null, _currentConstructionSite.Config, _currentConstructionSite.Position);
            
            _isBuildingInProgress = false;
            _lineRenderer.ClearLine();
        }
    }
}
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    [SerializeField] private BuildingConstructionSite _constructionSitePrefab;
    [SerializeField] private CustomLineRenderer _lineRenderer;

    private IJob _buildingJob;
    private bool _isBuildingInProgress = false;
    private BuildingConstructionSite _currentConstructionSite;

    public bool IsBuildingInProgress => _isBuildingInProgress;

    public void InitBuilding(BuildingConfig config, Vector3 position)
    {
        _currentConstructionSite = Instantiate(_constructionSitePrefab, position, Quaternion.identity);
        _currentConstructionSite.Initialize(config);

        _isBuildingInProgress = true;

        _lineRenderer.DrawLine(new Vector3[] { transform.position, position });
    }

    public void ReplaceConstructionSite(Vector3 newPosition)
    {
        if (_currentConstructionSite != null)
        {
            _currentConstructionSite.transform.position = newPosition;
            _lineRenderer.DrawLine(new Vector3[] { transform.position, newPosition });
        }
    }
}
using System;
using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingConstructor : MonoBehaviour
    {
        [SerializeField] private Building _buildingPrefab;
        [SerializeField] private ConstructionSite _constructionSitePrefab;
        [SerializeField] private CustomLineRenderer _lineRenderer;

        private IJob _buildingJob;
        private bool _isBuildingInProgress = false;
        private ConstructionSite _currentConstructionSite;

        public bool IsBuildingInProgress => _isBuildingInProgress;

        public event Action<ConstructionSite> SitePlaced;
        public event Action<ConstructionSite> SiteMoved;
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
                building.Initialize(_currentConstructionSite.Config, _currentConstructionSite.Position);

                _isBuildingInProgress = false;
                _lineRenderer.ClearLine();
            }
        }
    }
}

using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class PlacementPreview : MonoBehaviour
    {
        [SerializeField] private LayerMask _footprintMask;
        [SerializeField] private PlacementPreviewView _previewView;

        private Building _building;
        private bool _isValidPosition = false;

        public BuildingConfig Config => _building.Config;
        public bool IsValidPosition => _isValidPosition;

        private void Awake()
        {
            _building = GetComponent<Building>();
        }

        public void Initialize(BuildingConfig config, Vector3 position)
        {
            _building.Initialize(config, position);
            _building.View.Footprint.Show();
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;

            Validate();
        }

        private void Validate()
        {
            _isValidPosition = _building.View.Footprint.HasOverlapWithFootprint(_footprintMask) == false;

            _previewView.SetValid(_isValidPosition);
        }
    }
}

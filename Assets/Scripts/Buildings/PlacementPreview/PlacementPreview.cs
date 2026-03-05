using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class PlacementPreview : MonoBehaviour
    {
        [SerializeField] private LayerMask _footprintMask;

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
            _building.View.ShowFootprint();
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;

            Validate();
        }

        private void Validate()
        {
            _isValidPosition = _building.View.Footprint.HasOverlapWithFootprint(_footprintMask) == false;

            _building.View.Model.MeshView.SetColor(_isValidPosition ? Color.green : Color.red);
        }
    }
}

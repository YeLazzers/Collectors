using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class PlacementPreview : MonoBehaviour
    {
        [SerializeField] private LayerMask _footprintMask;
        [SerializeField] private PlacementPreviewView _previewView;

        private Building _building;

        public BuildingConfig Config => _building.Config;

        public PlacementFootprint Footprint => _building.View.Footprint;

        public LayerMask FootprintMask => _footprintMask;

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
        }

        public void SetValid(bool isValid)
        {
            _previewView.SetValid(isValid);
        }
    }
}

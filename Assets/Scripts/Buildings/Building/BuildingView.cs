using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Material _material;

        [Header("Components")]
        [SerializeField] private PlacementFootprint _footprint;

        private MeshView _meshView;

        public PlacementFootprint Footprint => _footprint;

        public MeshView MeshView => _meshView;

        public void RenderModel(BuildingConfig config)
        {
            if (_meshView == null)
            {
                _meshView = Instantiate(config.Model, transform);
                _meshView.gameObject.name = "Model";
            }

            if (_material != null)
                _meshView.SetMaterial(_material);

            _footprint?.Initialize(config.FootprintSize);
        }
    }
}

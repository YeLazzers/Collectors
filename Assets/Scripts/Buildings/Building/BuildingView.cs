using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Material _material;

        [Header("Components")]
        [SerializeField] private BuildingFootprint _footprint;

        private MeshView _meshView;

        public BuildingFootprint Footprint => _footprint;

        public MeshView MeshView => _meshView;

        public void RenderModel(BuildingConfig config, Material ownerMaterial = null)
        {
            if (_meshView == null)
            {
                _meshView = Instantiate(config.Model, transform);
                _meshView.gameObject.name = "Model";
            }

            var material = ownerMaterial != null ? ownerMaterial : _material;
            if (material != null)
                _meshView.SetMaterial(material);

            _footprint?.Initialize(config.FootprintSize);
        }
    }
}

using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Material _material;

        [Header("Components")]
        [SerializeField] private PlacementFootprint _footprint;

        private BuildingModelPresenter _modelPresenter;

        public PlacementFootprint Footprint => _footprint;
        public BuildingModelPresenter Model => _modelPresenter;

        public void ShowFootprint()
            => _footprint.Show();

        public void HideFootprint()
            => _footprint.Hide();

        public void RenderModel(BuildingConfig config)
        {
            _modelPresenter = Instantiate(config.Model, transform);
            _modelPresenter.gameObject.name = "Model";
            _modelPresenter.MeshView.SetMaterial(_material);

            _footprint?.Initialize(config.footprintSize);
        }
    }
}

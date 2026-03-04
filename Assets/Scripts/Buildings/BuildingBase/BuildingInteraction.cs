using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingInteraction : MonoBehaviour, IHoverable, ISelectable
    {
        [SerializeField] private BuildingInfo _model;
        [SerializeField] private BuildingView _view;

        public BuildingInfo Model => _model;
        public string Name => _model.BuildingName;

        public void Select()
        {
            _view.ShowSelectionRing();
            _view.ShowFootprint();
        }

        public void Deselect()
        {
            _view.HideSelectionRing();
            _view.HideFootprint();
        }

        public void OnHoverEnter()
        {
            _view.Highlight();
        }

        public void OnHoverExit()
        {
            _view.Unhighlight();
        }
    }
}

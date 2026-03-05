using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(BuildingView))]
    public class PlacementPreviewView : MonoBehaviour
    {
        [SerializeField] private Color _validColor;
        [SerializeField] private Color _invalidColor;

        private BuildingView _view;
        private bool? _isValid;

        private void Awake()
        {
            _view = GetComponent<BuildingView>();
        }

        public void SetValid(bool isValid)
        {
            if (_isValid == isValid)
                return;

            _isValid = isValid;
            _view.MeshView.SetColor(isValid ? _validColor : _invalidColor);
        }
    }
}

using UnityEngine;

namespace YeLazzers.Buildings.Modules
{
    public class Highlighter : MonoBehaviour
    {
        [SerializeField] private Color _highlightColor = Color.white;
        [SerializeField] private float _highlightIntensity = 1f;

        private MeshView _meshView;

        public void Initialize(MeshView meshView)
        {
            _meshView = meshView;
        }

        public void Highlight()
        {
            _meshView.SetEmission(_highlightColor * _highlightIntensity);
        }

        public void Unhighlight()
        {
            _meshView.SetEmission(Color.black);
        }
    }
}

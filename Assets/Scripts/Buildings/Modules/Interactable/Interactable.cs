using UnityEngine;

namespace YeLazzers.Buildings.Modules
{
    public class Interactable : MonoBehaviour, IHoverable, ISelectable
    {
        [SerializeField] private SelectionRing _ring;
        [SerializeField] private Highlighter _highlighter;

        public string Name => gameObject.name;

        public void Initialize(IMeshView meshView)
        {
            _highlighter.Initialize(meshView);
        }

        public void Select()
        {
            _ring.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _ring.gameObject.SetActive(false);
        }

        public void OnHoverEnter()
        {
            _highlighter.Highlight();
        }

        public void OnHoverExit()
        {
            _highlighter.Unhighlight();
        }
    }
}

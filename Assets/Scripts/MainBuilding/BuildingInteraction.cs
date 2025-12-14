using UnityEngine;

public class BuildingInteraction : MonoBehaviour, IHoverable, ISelectable
{
    [SerializeField] private BuildingModel _model;
    [SerializeField] private HoverHighlighter _hoverHighlighter;
    [SerializeField] private Renderer _selectionRingRenderer;

    public BuildingModel Model => _model;
    public string Name => _model.BuildingName;

    public void Select()
    {
        _selectionRingRenderer.enabled = true;
    }

    public void Deselect()
    {
        _selectionRingRenderer.enabled = false;
    }

    public void OnHoverEnter()
    {
        _hoverHighlighter.Highlight();
    }

    public void OnHoverExit()
    {
        _hoverHighlighter.Unhighlight();
    }
}
using UnityEngine;

public class BuildingInteraction : MonoBehaviour, IHoverable, ISelectable
{
    [SerializeField] private BuildingInfo _model;
    [SerializeField] private Highlighter _highlighter;
    [SerializeField] private Renderer _selectionRingRenderer;

    public BuildingInfo Model => _model;
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
        _highlighter.Highlight();
    }

    public void OnHoverExit()
    {
        _highlighter.Unhighlight();
    }
}
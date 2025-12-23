using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Material _material;

    [Header("Components")]
    [SerializeField] private SelectionRing _ring;
    [SerializeField] private PlacementFootprint _footprint;
    [SerializeField] private Highlighter _highlighter;

    private BuildingModelPresenter _modelPresenter;

    public PlacementFootprint Footprint => _footprint;
    public BuildingModelPresenter Model => _modelPresenter;

    public void ShowSelectionRing()
        => _ring.gameObject.SetActive(true);

    public void HideSelectionRing()
        => _ring.gameObject.SetActive(false);

    public void ShowFootprint()
        => _footprint.Show();

    public void HideFootprint()
        => _footprint.Hide();

    public void Highlight()
        => _highlighter.Highlight();

    public void Unhighlight()
        => _highlighter.Unhighlight();

    public void RenderModel(BuildingConfig config)
    {
        _modelPresenter = Instantiate(config.Model, transform);
        _modelPresenter.MeshView.SetMaterial(_material);

        _highlighter?.Initialize(_modelPresenter.MeshView);
        _footprint?.Initialize(config.footprintSize);
    }
}
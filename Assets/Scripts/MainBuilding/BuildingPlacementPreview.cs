using UnityEngine;

public class BuildingPlacementPreview : MonoBehaviour
{
    [SerializeField] private BuildingView _view;
    [SerializeField] private LayerMask _footprintMask;

    private BuildingConfig _config;
    private bool _isValidPosition = false;

    public BuildingConfig Config => _config;
    public bool IsValidPosition => _isValidPosition;

    public void Initialize(BuildingConfig config, Vector3 position)
    {
        _view.RenderModel(config);
        _view.ShowFootprint();

        UpdatePosition(position);
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;

        Validate();
    }

    private void Validate()
    {
        _isValidPosition = _view.Footprint.HasOverlapWithFootprint(_footprintMask) == false;

        _view.Model.MeshView.SetColor(_isValidPosition ? Color.green : Color.red);
    }
}
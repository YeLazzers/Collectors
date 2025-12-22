using UnityEngine;

public class BuildingPlacementPreview : MonoBehaviour
{
    [SerializeField] private BuildingView _view;
    [SerializeField] private LayerMask _footprintMask;

    private BuildingConfig _config;

    public BuildingConfig Config => _config;

    public void Initialize(BuildingConfig config, Vector3 position)
    {
        _view.RenderModel(config);

        UpdatePosition(position);
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;

        Validate();
    }

    public bool Validate()
    {
        bool isValid = _view.Footprint.HasOverlapWithMask(_footprintMask);

        _view.Model.MeshView.SetColor(isValid ? Color.red : Color.green);

        return isValid;
    }
}
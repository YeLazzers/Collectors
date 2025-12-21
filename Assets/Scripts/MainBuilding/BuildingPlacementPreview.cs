using UnityEngine;

public class BuildingPlacementPreview : MonoBehaviour
{
    [SerializeField] private Material _previewMaterial;
    [SerializeField] private PlacementFootprint _placementFootprint;
    [SerializeField] private LayerMask _footprintMask;

    private BuildingConfig _config;
    private BuildingModelPresenter _prefabInstance;

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        if (_config == null)
            _config = (BuildingConfig)Resources.Load("BuildingConfigs/Factory");

        _prefabInstance = Instantiate(_config.Model, transform);

        _prefabInstance.MeshView.SetMaterial(_previewMaterial);
        _placementFootprint.Initialize(_config.footprintSize);
    }

    public void Initialize(Vector3 position)
    {
        Initialize();
        UpdatePosition(position);

        Validate();
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;

        Validate();
    }

    public bool Validate()
    {
        bool isValid = _placementFootprint.HasOverlapWithMask(_footprintMask);

        _prefabInstance.MeshView.SetColor(isValid ? Color.red : Color.green);

        return isValid;
    }
}
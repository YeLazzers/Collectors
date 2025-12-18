using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingPlacementPreview _buildingPreviewPrefab;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raycastDistance = 1000f;

    private BuildingPlacementPreview _previewInstance;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdatePreviewPosition();
    }

    private void UpdatePreviewPosition()
    {
        if (_mainCamera == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance, _groundLayer))
        {
            Cursor.visible = false;
            if (_previewInstance == null)
                CreatePreviewBuilding();

            _previewInstance.UpdatePosition(hit.point);
        }
    }

    private void CreatePreviewBuilding()
    {
        _previewInstance = Instantiate(_buildingPreviewPrefab);
        _previewInstance.Initialize();
    }

    public void PlaceBuilding()
    {
        if (_previewInstance == null)
            return;

        _previewInstance = null;
    }

    public void CancelPlacement()
    {
        if (_previewInstance != null)
        {
            Destroy(_previewInstance.gameObject);
            _previewInstance = null;
        }
    }

    public void ShowPreview()
    {

    }
}
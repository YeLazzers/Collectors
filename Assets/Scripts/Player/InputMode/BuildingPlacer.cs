using UnityEngine;

public class BuildingPlacer : MonoBehaviour, IInputMode
{
    [SerializeField] private PlayerInputRouter _router;
    [SerializeField] private BuildingPlacementPreview _buildingPreviewPrefab;
    [SerializeField] private LayerMask _groundLayer;

    private BuildingPlacementPreview _previewInstance;

    public LayerMask RaycastLayer => _groundLayer;

    public void OnMouseMove(PointerContext context)
    {
        if (context.HitInfo.point != null)
        {
            _previewInstance.UpdatePosition(context.HitInfo.point);
        }
    }

    public void OnEnter(PointerContext context)
    {
        Cursor.visible = false;

        _previewInstance = Instantiate(_buildingPreviewPrefab);
        _previewInstance.Initialize(context.HitInfo.point);
    }

    public void OnExit()
    {
        Cursor.visible = true;
    }

    public void OnLmbDown(PointerContext context)
    {
        PlaceBuilding();
    }

    public void OnRmbDown(PointerContext context)
    {
        CancelPlacement();
    }

    private void PlaceBuilding()
    {
        if (_previewInstance == null)
            return;

        Debug.Log("PlaceBuilding");
        // _previewInstance = null;
    }

    private void CancelPlacement()
    {
        if (_previewInstance != null)
        {
            Destroy(_previewInstance.gameObject);
            _previewInstance = null;
            _router.ActivateSelector();
        }
    }
}
using System.Diagnostics;
using UnityEngine;

public readonly struct BuildingPlacementContext
{
    public readonly BuildingConfig Config;
    public readonly BuildingInfo Source;

    public BuildingPlacementContext(BuildingConfig config, BuildingInfo source)
    {
        Config = config;
        Source = source;
    }
}

public class BuildingPlacementMode : MonoBehaviour, IInputMode
{
    [SerializeField] private PlayerInputRouter _router;
    [SerializeField] private BuildingPlacementPreview _buildingPreviewPrefab;
    [SerializeField] private LayerMask _groundLayer;

    private BuildingPlacementPreview _previewInstance;
    private BuildingPlacementContext _context;

    public LayerMask RaycastLayer => _groundLayer;

    public void Configure(BuildingPlacementContext context)
    {
        _context = context;
    }

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
        _previewInstance.Initialize(_context.Config, context.HitInfo.point);
    }

    public void OnExit()
    {
        Cursor.visible = true;

        ClearPreviewInstance();
    }

    public void OnLmbDown(PointerContext context)
    {
        
        // ClearPreviewInstance();
    }

    public void OnRmbDown(PointerContext context)
    {
        _router.ActivateSelector();
    }

    private void ClearPreviewInstance()
    {
        if (_previewInstance != null)
        {
            Destroy(_previewInstance.gameObject);
            _previewInstance = null;
        }
    }
}
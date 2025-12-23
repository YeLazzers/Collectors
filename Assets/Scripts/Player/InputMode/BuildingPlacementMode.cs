using UnityEngine;

public readonly struct BuildingPlacementContext
{
    public readonly BuildingConfig Config;
    public readonly BuildingBuilder Source;

    public BuildingPlacementContext(BuildingConfig config, BuildingBuilder source)
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

        // if (_context.Source.IsBuildingInProgress)
        // {
        //     _context.Source.HideConstructionSite();
        // }

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
        if (_previewInstance.IsValidPosition)
        {
            if (_context.Source.IsBuildingInProgress)
            {
                _context.Source.ReplaceConstructionSite(_previewInstance.transform.position);
            }
            else
            {
                _context.Source.InitBuilding(_context.Config, _previewInstance.transform.position);
            }
            _router.ActivateSelector();
        }
    }

    public void OnRmbDown(PointerContext context)
    {
        // if (_context.Source.IsBuildingInProgress)
        // {
        //     _context.Source.ShowConstructionSite();
        // }

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
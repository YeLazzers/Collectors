using System;
using UnityEngine;
using YeLazzers.Buildings;
using YeLazzers.Buildings.Modules;

namespace YeLazzers.Game
{
    public class BuildingMode : IGameMode
    {
        private readonly Cursor _cursor;
        private readonly PlacementPreview _buildingPreviewPrefab;
        private readonly Level _level;
        private readonly LayerMask _groundLayer;

        private PlacementPreview _previewInstance;
        private BuildingModeContext _context;

        public BuildingMode(
            Cursor cursor,
            PlacementPreview buildingPreviewPrefab,
            Level level,
            LayerMask groundLayer)
        {
            _cursor = cursor;
            _buildingPreviewPrefab = buildingPreviewPrefab;
            _level = level;
            _groundLayer = groundLayer;
        }

        public event Action Completed;

        public event Action Cancelled;

        public LayerMask RaycastLayer => _groundLayer;

        public void Configure(BuildingModeContext context)
        {
            _context = context;
        }

        public void OnEnter(PointerContext context)
        {
            _cursor.HideCursor();

            _previewInstance = UnityEngine.Object.Instantiate(_buildingPreviewPrefab);
            _previewInstance.Initialize(_context.Config, context.HitInfo.point);

            ShowFootprints();
            _previewInstance.SetValid(IsValidPosition());
        }

        public void OnExit()
        {
            _cursor.SetDefaultCursor();

            if (_context.Source.TryGetModule<Interactable>(out var interactable))
            {
                interactable.Deselect();
            }

            ClearPreviewInstance();
            HideFootprints();
        }

        public void OnMouseMove(PointerContext context)
        {
            if (_previewInstance != null)
            {
                _previewInstance.UpdatePosition(context.HitInfo.point);
                _previewInstance.SetValid(IsValidPosition());
            }
        }

        public void OnLmbDown(PointerContext context)
        {
            if (_previewInstance == null || IsValidPosition() == false)
                return;

            if (_context.Source.TryGetModule<StationPolicy>(out var policy))
            {
                var existingSite = policy.ActiveSite;

                if (existingSite != null)
                {
                    _level.MoveConstructionSite(existingSite, _previewInstance.transform.position);
                    policy.SetActiveSite(existingSite);
                }
                else
                {
                    var site = _level.PlaceConstructionSite(_context.Config, _previewInstance.transform.position);
                    policy.SetActiveSite(site);
                }
            }
            else
            {
                return;
            }

            Completed?.Invoke();
        }

        public void OnRmbDown(PointerContext context)
        {
            Cancelled?.Invoke();
        }

        private bool IsValidPosition()
        {
            GameObject[] ignoreObjects = null;

            if (_context.Source.TryGetModule<StationPolicy>(out var policy) && policy.ActiveSite != null)
            {
                ignoreObjects = new[] { policy.ActiveSite.GetComponent<Building>().View.Footprint.gameObject };
            }

            return _level.CanPlace(_previewInstance.Footprint, _previewInstance.FootprintMask, ignoreObjects);
        }

        private void ShowFootprints()
        {
            foreach (var building in _level.GetAllBuildings())
            {
                building.View.Footprint.Show();
            }
        }

        private void HideFootprints()
        {
            foreach (var building in _level.GetAllBuildings())
            {
                building.View.Footprint.Hide();
            }
        }

        private void ClearPreviewInstance()
        {
            if (_previewInstance != null)
            {
                UnityEngine.Object.Destroy(_previewInstance.gameObject);
                _previewInstance = null;
            }
        }
    }
}

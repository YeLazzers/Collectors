using UnityEngine;
using YeLazzers.Buildings;
using YeLazzers.Buildings.Modules;
using YeLazzers.Core;

namespace YeLazzers.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private Player _player;
        [SerializeField] private RTSCamera _camera;
        [SerializeField] private BuildingConfig _startBuilding;
        [SerializeField] private Cursor _cursor;
        [SerializeField] private PlacementPreview _buildingPreviewPrefab;
        [SerializeField] private LayerMask _selectableLayer;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private int _startWorkerCount = 3;

        private GameModeChanger _modeChanger;

        public void Initialize()
        {
            _level.Initialize(_player.Material);
            _level.BuildingSpawned += OnBuildingSpawned;

            _camera.SetBounds(_level.Bounds);

            var selectingMode = new SelectingMode(_cursor, _selectableLayer);
            var buildingMode = new BuildingMode(_cursor, _buildingPreviewPrefab, _level, _groundLayer);
            _modeChanger = new GameModeChanger(_player.Router, selectingMode, buildingMode);

            var startBuilding = _level.SpawnBuilding(_startBuilding, Vector3.zero);
            _camera.CenterOn(startBuilding.transform.position);

            if (startBuilding.TryGetModule<Station>(out var station))
            {
                station.Initialize(_startWorkerCount);
            }
        }

        private void OnBuildingSpawned(Building building)
        {
            if (building.TryGetModule<ResourceStorage>(out var storage))
            {
                storage.ResourceDeposited += OnResourceDeposited;
            }
        }

        private void OnResourceDeposited(Resource resource)
        {
            _level.ResourceSpawner.Release(resource);
        }
    }
}

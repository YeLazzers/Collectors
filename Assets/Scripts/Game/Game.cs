using UnityEngine;
using YeLazzers.Buildings;

namespace YeLazzers.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private Player _player;
        [SerializeField] private BuildingConfig _startBuilding;
        [SerializeField] private Cursor _cursor;
        [SerializeField] private PlacementPreview _buildingPreviewPrefab;
        [SerializeField] private LayerMask _selectableLayer;
        [SerializeField] private LayerMask _groundLayer;

        private GameModeChanger _modeChanger;

        public void Initialize()
        {
            _level.Initialize(_player.Material);
            _level.BuildingSpawned += OnBuildingSpawned;

            var selectingMode = new SelectingMode(_cursor, _selectableLayer);
            var buildingMode = new BuildingMode(_cursor, _buildingPreviewPrefab, _level, _groundLayer);
            _modeChanger = new GameModeChanger(_player.Router, selectingMode, buildingMode);

            _level.SpawnBuilding(_startBuilding, Vector3.zero);
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

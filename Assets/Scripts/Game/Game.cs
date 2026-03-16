using UnityEngine;
using YeLazzers.Buildings;

namespace YeLazzers.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private BuildingConfig _startBuilding;

        public void Initialize()
        {
            _level.Initialize();
            _level.BuildingSpawned += OnBuildingSpawned;
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

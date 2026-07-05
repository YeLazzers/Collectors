using System;
using System.Collections.Generic;
using UnityEngine;
using YeLazzers.Buildings;

namespace YeLazzers.Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private ConstructionSite _constructionSitePrefab;
        [SerializeField] private ResourceSpawner _resourceSpawner;
        [SerializeField] private Terrain _terrain;

        private Material _buildingMaterial;
        private readonly List<Building> _buildings = new List<Building>();

        public event Action<Building> BuildingSpawned;

        public event Action<Building> BuildingDestroyed;

        public ResourceSpawner ResourceSpawner => _resourceSpawner;

        public Terrain Terrain => _terrain;

        public IReadOnlyList<Building> GetAllBuildings() => _buildings;

        public void Initialize(Material buildingMaterial)
        {
            _buildingMaterial = buildingMaterial;

            var terrainPos = _terrain.transform.position;
            var terrainSize = _terrain.terrainData.size;
            var spawnBounds = new Bounds(terrainPos + terrainSize / 2f, terrainSize);

            _resourceSpawner.Initialize(spawnBounds);
        }

        public Building SpawnBuilding(BuildingConfig config, Vector3 position)
        {
            var building = Instantiate(config.Prefab, position, Quaternion.identity);
            building.Initialize(config, position, _buildingMaterial);
            Register(building);
            BuildingSpawned?.Invoke(building);
            return building;
        }

        public void DestroyBuilding(Building building)
        {
            Unregister(building);
            BuildingDestroyed?.Invoke(building);
            Destroy(building.gameObject);
        }

        public ConstructionSite PlaceConstructionSite(BuildingConfig config, Vector3 position)
        {
            var site = Instantiate(_constructionSitePrefab, position, Quaternion.identity);
            site.Initialize(config, position);

            Register(site.GetComponent<Building>());

            return site;
        }

        public void MoveConstructionSite(ConstructionSite site, Vector3 newPosition)
        {
            site.transform.position = newPosition;
        }

        public bool CanPlace(PlacementFootprint footprint, LayerMask mask, GameObject[] ignoreObjects = null)
        {
            return footprint.HasOverlapWithFootprint(mask, ignoreObjects) == false;
        }

        private void Register(Building building)
        {
            _buildings.Add(building);

            if (building.TryGetComponent<ConstructionSite>(out var site))
            {
                site.SiteCompleted += OnSiteCompleted;
            }
        }

        private void Unregister(Building building)
        {
            _buildings.Remove(building);

            if (building.TryGetComponent<ConstructionSite>(out var site))
            {
                site.SiteCompleted -= OnSiteCompleted;
            }
        }

        private void OnSiteCompleted(ConstructionSite site)
        {
            var config = site.Config;
            var position = site.Position;

            var siteBuilding = site.GetComponent<Building>();
            DestroyBuilding(siteBuilding);

            SpawnBuilding(config, position);
        }
    }
}

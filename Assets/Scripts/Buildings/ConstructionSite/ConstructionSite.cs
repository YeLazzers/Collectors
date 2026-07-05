using System;
using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class ConstructionSite : MonoBehaviour
    {
        private Building _building;

        public event Action<ConstructionSite, Action<Building>> SiteCompleted;

        public BuildingConfig Config => _building.Config;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _building = GetComponent<Building>();
        }

        public void Initialize(BuildingConfig config, Vector3 position)
        {
            _building.Initialize(config, position);
        }

        public Building Complete()
        {
            Building newBuilding = null;
            SiteCompleted?.Invoke(this, building => newBuilding = building);
            return newBuilding;
        }
    }
}

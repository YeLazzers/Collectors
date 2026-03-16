using System;
using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class ConstructionSite : MonoBehaviour
    {
        private Building _building;

        public event Action<ConstructionSite> SiteCompleted;

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

        public void Complete()
        {
            SiteCompleted?.Invoke(this);
        }
    }
}

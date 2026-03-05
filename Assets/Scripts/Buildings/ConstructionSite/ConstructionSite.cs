using UnityEngine;

namespace YeLazzers.Buildings
{
    [RequireComponent(typeof(Building))]
    public class ConstructionSite : MonoBehaviour
    {
        private Building _building;

        public BuildingConfig Config => _building.Config;
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _building = GetComponent<Building>();
        }

        public void Initialize(BuildingConfig config)
        {
            _building.Initialize(config, transform.position);
        }

        public void Initialize(BuildingConfig config, Vector3 position)
        {
            _building.Initialize(config, position);
        }

        public void CompleteConstruction()
        {
            // _state = ConstructionSiteState.Completed;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace YeLazzers.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingView _view;
        [SerializeField] private float _landingRadius = 1f;

        private BuildingConfig _config;

        public BuildingConfig Config => _config;

        public BuildingView View => _view;

        public void Initialize(BuildingConfig config, Vector3 position)
        {
            _config = config;
            transform.position = position;
            _view.RenderModel(_config);
        }

        public Vector3 GetLandingPoint(Vector3 originPos)
        {
            Vector3 dir = (transform.position - originPos).normalized;
            return transform.position - dir * _landingRadius;
        }

        public bool TryGetModule<T>(out T module) where T : Component
        {
            module = GetComponentInChildren<T>();
            return module != null;
        }
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

namespace YeLazzers.Buildings
{
    public class WorkerHub : MonoBehaviour
    {
        private readonly float _yPosition = 1f;

        [SerializeField] private JobBoard _jobBoard;
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private Sprite _icon;
        [SerializeField] private WorkerSpawner _spawner;
        [SerializeField] private float _spawnRadius = 1f;
        [SerializeField] private int _cost;

        private Building _building;

        public Sprite Icon => _icon;

        private void Awake()
        {
            _building = GetComponentInParent<Building>();
        }

        public void Initialize(int count)
        {
            float randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);
            float angleStep = Mathf.PI * 2f / count;

            for (int i = 0; i < count; i++)
            {
                TrainWorker(GetSpawnPosition(i * angleStep + randomRotationOffset));
            }
        }

        public void TryTrainWorker()
        {
            if (_storage.Amount >= _cost)
            {
                TrainWorker(GetSpawnPosition(Random.Range(0f, Mathf.PI * 2f)));
                _storage.Spend(_cost);
            }
        }

        private void TrainWorker(Vector3 spawnPosition)
        {
            Worker worker = _spawner.Spawn(spawnPosition, transform.position);
            worker.AssignToStation(_building, _jobBoard);
        }

        private Vector3 GetSpawnPosition(float angle)
        {
            float x = _spawnRadius * Mathf.Cos(angle);
            float z = _spawnRadius * Mathf.Sin(angle);

            return new Vector3(
                transform.position.x + x,
                _yPosition,
                transform.position.z + z
            );
        }
    }
}

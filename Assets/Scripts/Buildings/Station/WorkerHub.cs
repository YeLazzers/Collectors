using UnityEngine;
using Random = UnityEngine.Random;
using YeLazzers.Jobs;

namespace YeLazzers.Buildings
{
    public class WorkerHub : MonoBehaviour
    {
        private readonly float _yPosition = 1f;

        [SerializeField] private JobBoard _jobBoard;
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private WorkerSpawner _spawner;
        [SerializeField] private float _spawnRadius = 1f;
        [SerializeField] private int _cost = 3;

        private Building _building;

        public Building Building => _building;

        public JobBoard JobBoard => _jobBoard;

        public ResourceStorage Storage => _storage;

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
            worker.AssignToWorkerHub(this);
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

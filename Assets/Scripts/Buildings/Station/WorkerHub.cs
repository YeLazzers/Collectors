using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YeLazzers.Buildings
{
    public class WorkerHub : MonoBehaviour
    {
        private readonly float _yPosition = 1f;

        [SerializeField] private JobBoard _jobBoard;
        [SerializeField] private Sprite _icon;
        [SerializeField] private WorkerSpawner _spawner;
        [SerializeField] private float _spawnRadius = 1f;

        private List<Worker> _workers = new List<Worker>();

        public int WorkersCount => _workers.Count;

        public Sprite Icon => _icon;

        public event Action<int> WorkersCountChanged;

        public void TrainWorker(int count)
        {
            float randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);

            for (int i = 0; i < count; i++)
            {
                Worker worker = _spawner.Spawn(GetSpawnPosition(i, count, randomRotationOffset), transform.position);

                worker.GetComponent<JobRunner>().SetJobBoard(_jobBoard);
                worker.GetComponent<JobRunner>().Run();

                _workers.Add(worker);

                WorkersCountChanged?.Invoke(_workers.Count);
            }
        }

        private Vector3 GetSpawnPosition(int index, int total, float radialOffset = 0f)
        {
            float range = 2 * Mathf.PI / total;
            float angle = index * range + radialOffset;
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

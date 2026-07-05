using System;
using System.Collections.Generic;
using UnityEngine;
using YeLazzers.Jobs;

using Random = UnityEngine.Random;

namespace YeLazzers.Buildings.Modules
{
    public class WorkerHub : MonoBehaviour
    {
        private readonly float _yPosition = 1f;

        [SerializeField] private JobBoard _jobBoard;
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private WorkerSpawner _spawner;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _spawnRadius = 1f;
        [SerializeField] private int _cost = 3;

        private readonly List<Worker> _workers = new List<Worker>();

        private Building _building;

        public event Action<int> WorkerCountChanged;

        public Building Building => _building;

        public JobBoard JobBoard => _jobBoard;

        public ResourceStorage Storage => _storage;

        public Sprite Icon => _icon;

        public int WorkerCount => _workers.Count;

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

        public void AddWorker(Worker worker)
        {
            _workers.Add(worker);
            worker.AssignToWorkerHub(this);
            WorkerCountChanged?.Invoke(_workers.Count);
        }

        public void RemoveWorker(Worker worker)
        {
            _workers.Remove(worker);
            WorkerCountChanged?.Invoke(_workers.Count);
        }

        private void TrainWorker(Vector3 spawnPosition)
        {
            Worker worker = _spawner.Spawn(spawnPosition, transform.position);
            AddWorker(worker);
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

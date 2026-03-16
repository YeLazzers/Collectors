using System;
using UnityEngine;

namespace YeLazzers.Buildings
{
    // [System.Obsolete("BuildingInfo заморожен. Используется как фасад для RTS-панели, будет удалён.")]
    public class BuildingInfo : MonoBehaviour, IStationReadModel, IStationCommands
    {
        [Header("Systems")]
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private Building _building;
        [SerializeField] private BuildingConstructor _builder;

        [Header("Settings")]
        [SerializeField] private string _name = "Factory";
        [SerializeField] private int _unitPrice = 3;
        [SerializeField] private int _basePrice = 5;

        public event Action<IStationReadModel> BuildingUpdated;

        public string BuildingName => _name;

        public int ResourcesCount => _storage.Amount;

        public int UnitsCount => _hub.WorkersCount;

        public bool CanBuildUnit => _storage.Amount >= _unitPrice;

        public bool CanPlaceFlag => _hub.WorkersCount > 1;

        public bool CanBuildNewBase => _storage.Amount >= _basePrice;

        public BuildingConfig Config => _building.Config;

        public BuildingConstructor Builder => _builder;

        private void OnEnable()
        {
            _storage.AmountChanged += OnResourcesChanged;
            _hub.WorkersCountChanged += OnWorkersCountChanged;
        }

        private void OnDisable()
        {
            _storage.AmountChanged -= OnResourcesChanged;
            _hub.WorkersCountChanged -= OnWorkersCountChanged;
        }

        public void BuildNewBuilding(BuildingConfig config, Vector3 position)
        {
            _builder.InitBuilding(config, position);
        }

        public void BuildUnit()
        {
            throw new System.NotImplementedException();
        }

        public void StartPlacingFlag()
        {
            throw new System.NotImplementedException();
        }

        private void OnResourcesChanged(int amount)
        {
            BuildingUpdated?.Invoke(this);
        }

        private void OnWorkersCountChanged(int count)
        {
            BuildingUpdated?.Invoke(this);
        }
    }
}

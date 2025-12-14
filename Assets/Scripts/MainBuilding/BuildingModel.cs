using System;
using UnityEngine;

public class BuildingModel : MonoBehaviour, IBuildingReadModel, IBuildingCommands
{
    [Header("Systems")]
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private CollectorHub _hub;

    [Header("Settings")]
    [SerializeField] private string _name = "Factory";
    [SerializeField] private int _unitPrice = 3;
    [SerializeField] private int _basePrice = 5;

    public string BuildingName => _name;
    public int ResourcesCount => _storage.Amount;
    public int UnitsCount => _hub.CollectorsCount;
    public bool CanBuildUnit => _storage.Amount >= _unitPrice;
    public bool CanPlaceFlag => _hub.CollectorsCount > 1;
    public bool CanBuildNewBase => _storage.Amount >= _basePrice;
    
    public event Action<IBuildingReadModel> BuildingUpdated;

    private void OnEnable()
    {
        _storage.AmountChanged += OnResourcesChanged;
        _hub.CollectorsCountChanged += OnCollectorsCountChanged;
    }

    private void OnDisable()
    {
        _storage.AmountChanged -= OnResourcesChanged;
        _hub.CollectorsCountChanged -= OnCollectorsCountChanged;
    }

    public void BuildNewBaseAt(Vector3 position)
    {
        throw new System.NotImplementedException();
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

    private void OnCollectorsCountChanged(int count)
    {
        BuildingUpdated?.Invoke(this);
    }
}
using UnityEngine;

public class CollectorTrainer : MonoBehaviour
{
    [SerializeField] private CollectorHub _hub;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private int _cost;

    private void OnEnable()
    {
        _storage.AmountChanged += OnResourcesUpdated;
    }

    private void OnDisable()
    {
        _storage.AmountChanged -= OnResourcesUpdated;
    }

    private void OnResourcesUpdated(int amount)
    {
        if (amount >= _cost)
        {
            _hub.TrainCollector(1);
            _storage.Spend(amount);
        }
    }
}
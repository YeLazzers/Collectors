using UnityEngine;

public class BuildingHudPresenter : MonoBehaviour
{
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private CollectorHub _hub;
    [SerializeField] private ValueRow _resources;

    private void Awake()
    {
        _resources.Initialize(_resourceStorage.Icon, _resourceStorage.Amount);
    }

    private void OnEnable()
    {
        _resourceStorage.AmountChanged += OnAmountChanged;
    }
    private void OnDisable()
    {
        _resourceStorage.AmountChanged -= OnAmountChanged;
    }

    private void OnAmountChanged(int newAmount)
    {
        _resources.SetAmount(newAmount);
    }
}
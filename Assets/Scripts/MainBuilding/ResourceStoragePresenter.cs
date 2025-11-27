using UnityEngine;

public class ResourceStoragePresenter : MonoBehaviour
{
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private ResourcePanel _resourcePanel;

    private void Awake()
    {
        _resourcePanel.Initialize(_resourceStorage.Icon, _resourceStorage.Amount);
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
        _resourcePanel.SetAmount(newAmount);
    }
}
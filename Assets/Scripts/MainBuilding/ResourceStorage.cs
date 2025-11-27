using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private int _initialResources = 0;
    [SerializeField] private Sprite _icon;

    private int _amount = 0;

    public int Amount => _amount;
    public Sprite Icon => _icon;

    public event Action<int> AmountChanged;

    public void Add(int amount)
    {
        _amount += amount;
        AmountChanged?.Invoke(_amount);
    }

    public bool TrySpend(int amount)
    {
        if (_amount < amount)
            return false;

        _amount -= amount;
        AmountChanged?.Invoke(_amount);
        return true;
    }
}

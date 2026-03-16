using System;
using UnityEngine;

namespace YeLazzers.Buildings
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;

        private int _amount = 4;

        public event Action<int> AmountChanged;

        public event Action<Resource> ResourceDeposited;

        public int Amount => _amount;

        public Sprite Icon => _icon;

        public void Deposit(Resource resource)
        {
            Add(resource.Amount);
            ResourceDeposited?.Invoke(resource);
        }

        public void Add(int amount)
        {
            _amount += amount;
            AmountChanged?.Invoke(_amount);
        }

        public bool TrySpend(int amount)
        {
            if (_amount < amount)
                return false;

            Spend(amount);
            return true;
        }

        public void Spend(int amount)
        {
            _amount -= amount;
            AmountChanged?.Invoke(_amount);
        }
    }
}

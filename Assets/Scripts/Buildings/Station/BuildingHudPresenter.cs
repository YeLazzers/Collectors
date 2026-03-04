using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingHudPresenter : MonoBehaviour
    {
        [SerializeField] private ResourceStorage _resourceStorage;
        [SerializeField] private ValueRow _resources;
        private bool _isDirty = false;

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

        private void LateUpdate()
        {
            if (!_isDirty) return;
            _isDirty = false;
            _resources.SetAmount(_resourceStorage.Amount);
        }

        private void OnAmountChanged(int _)
        {
            _isDirty = true;
        }
    }
}

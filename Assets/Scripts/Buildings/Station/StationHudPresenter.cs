using TMPro;
using UnityEngine;
using YeLazzers.Buildings.Modules;

namespace YeLazzers.Buildings
{
    public class BuildingHudPresenter : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private ResourceStorage _resourceStorage;
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private StationPolicy _policy;

        [Header("UI References")]
        [SerializeField] private ValueRow _resourcesUI;
        [SerializeField] private ValueRow _workersUI;
        [SerializeField] private TextMeshProUGUI _policyUI;

        private bool _isDirty = false;

        private void Awake()
        {
            _resourcesUI.Initialize(_resourceStorage.Icon, _resourceStorage.Amount);
            _workersUI.Initialize(_hub.Icon, _hub.WorkerCount);
            _policyUI.text = _policy.CurrentPolicy.ToString();
        }

        private void OnEnable()
        {
            _resourceStorage.AmountChanged += OnAmountChanged;
            _hub.WorkerCountChanged += OnWorkerCountChanged;
            _policy.PolicyChanged += OnPolicyChanged;
        }

        private void OnDisable()
        {
            _resourceStorage.AmountChanged -= OnAmountChanged;
            _hub.WorkerCountChanged -= OnWorkerCountChanged;
            _policy.PolicyChanged -= OnPolicyChanged;
        }

        private void LateUpdate()
        {
            if (!_isDirty) return;
            _isDirty = false;
            _resourcesUI.SetAmount(_resourceStorage.Amount);
        }

        private void OnAmountChanged(int _)
        {
            _isDirty = true;
        }

        private void OnWorkerCountChanged(int count)
        {
            _workersUI.SetAmount(count);
        }

        private void OnPolicyChanged(StationPolicyType policy)
        {
            _policyUI.text = policy.ToString();
        }
    }
}

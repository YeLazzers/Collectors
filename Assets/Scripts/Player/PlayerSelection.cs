using System;
using UnityEngine;
using YeLazzers.Buildings;
using YeLazzers.Buildings.Modules;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private SelectingMode _objectSelector;
    [SerializeField] private PlayerInputRouter _router;

    private ISelectable _currentSelectable;

    // public BuildingInfo SelectedBuilding => IsBuildingSelectable(_currentSelectable, out BuildingInfo buildingModel) ? buildingModel : null;

    public event Action<BuildingInfo> BuildingSelected;
    public event Action SelectionCleared;

    private void OnEnable()
    {
        _objectSelector.Selected += SetSelectable;
    }
    private void OnDisable()
    {
        _objectSelector.Selected -= SetSelectable;
    }

    public void SetSelectable(ISelectable selectable)
    {
        if (selectable != null && selectable != _currentSelectable)
        {
            _currentSelectable?.Deselect();
            _currentSelectable = selectable;
            _currentSelectable?.Select();

            if (IsSelectableBuilding(selectable, out BuildingInfo buildingModel))
            {
                BuildingSelected?.Invoke(buildingModel);
                _router.ActivateBuildingPlacementMode(new BuildingPlacementContext(buildingModel.Config, buildingModel.Builder));
            }
        }
        else
        {
            ClearSelection();
        }
    }

    public void ClearSelection()
    {
        _currentSelectable?.Deselect();
        _currentSelectable = null;

        SelectionCleared?.Invoke();
    }

    private bool IsSelectableBuilding(ISelectable selectable, out BuildingInfo buildingModel)
    {
        if (selectable is Interactable interactable)
        {
            buildingModel = interactable.GetComponentInParent<BuildingInfo>();
            return buildingModel != null;
        }

        buildingModel = null;
        return false;
    }
}

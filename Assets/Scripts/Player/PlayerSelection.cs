using System;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private ObjectSelector _objectSelector;

    private BuildingModel _selectedBuilding;

    private ISelectable _currentSelectable;

    public BuildingModel SelectedBuilding => IsBuildingSelectable(_currentSelectable, out BuildingModel buildingModel) ? buildingModel : null;

    public event Action<BuildingModel> BuildingSelected;
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

            if (IsBuildingSelectable(selectable, out BuildingModel buildingModel))
            {
                BuildingSelected?.Invoke(buildingModel);
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

    private bool IsBuildingSelectable(ISelectable selectable, out BuildingModel buildingModel)
    {
        if (selectable is BuildingInteraction buildingInteraction)
        {
            buildingModel = buildingInteraction.Model;
            return true;
        }

        buildingModel = null;
        return false;
    }
}

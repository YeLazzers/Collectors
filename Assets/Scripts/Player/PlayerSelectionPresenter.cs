using UnityEngine;

public class BuildingSelectionPresenter : MonoBehaviour
{
    [SerializeField] private PlayerSelection _selection;
    [SerializeField] private SelectionPanel _view;

    private IBuildingReadModel _readModel;
    private IBuildingCommands _commands;

    private void OnEnable()
    {
        _selection.BuildingSelected += OnBuildingSelected;
        _selection.SelectionCleared += OnSelectionCleared;
    }

    private void OnDisable()
    {
        _selection.BuildingSelected -= OnBuildingSelected;
        _selection.SelectionCleared -= OnSelectionCleared;
    }

    private void OnBuildingSelected(BuildingInfo building)
    {
        ConnectBuilding(building);
        _commands = building;

        _view.Show(building.BuildingName);
        _view.UpdateStats(building);
    }

    private void OnSelectionCleared()
    {
        DisconnectBuilding();
        _commands = null;

        _view.Hide();
    }

    public void OnBuildUnitClicked()
    {
        _commands?.BuildUnit();
    }

    public void OnPlaceFlagClicked()
    {
        _commands?.StartPlacingFlag();
    }

    private void ConnectBuilding(BuildingInfo building)
    {
        if (building == null)
            return;

        DisconnectBuilding();

        _readModel = building;
        building.BuildingUpdated += _view.UpdateStats;
    }

    private void DisconnectBuilding()
    {
        if (_readModel != null)
        {
            _readModel.BuildingUpdated -= _view.UpdateStats;
            _readModel = null;
        }

    }
}

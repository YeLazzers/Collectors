using YeLazzers.Buildings;
using YeLazzers.Buildings.Modules;

namespace YeLazzers.Game
{
    public class GameModeChanger
    {
        private readonly ModeStateMachine _modes;
        private readonly SelectingMode _selectingMode;
        private readonly BuildingMode _buildingMode;

        public GameModeChanger(PlayerInputRouter router, SelectingMode selectingMode, BuildingMode buildingMode)
        {
            _selectingMode = selectingMode;
            _buildingMode = buildingMode;
            _modes = new ModeStateMachine(router);

            _selectingMode.Selected += OnSelected;
            _buildingMode.Completed += ActivateSelectingMode;
            _buildingMode.Cancelled += ActivateSelectingMode;

            _modes.Activate(_selectingMode);
        }

        private void ActivateSelectingMode()
        {
            _modes.Activate(_selectingMode);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (selectable is Interactable interactable
                && interactable.GetComponentInParent<Building>() is Building building)
            {
                _buildingMode.Configure(new BuildingModeContext(building.Config, building));
                _modes.Activate(_buildingMode);
            }
        }
    }
}

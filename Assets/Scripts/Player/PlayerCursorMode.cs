using UnityEngine;

public enum PlayerCursorModes
{
    Select,
    Building,
}

public class PlayerCursorMode : MonoBehaviour
{
    private readonly PlayerCursorModes DefaultCursorMode = PlayerCursorModes.Select;

    [SerializeField] private CursorChanger _cursor;
    [SerializeField] private ObjectSelector _selector;
    [SerializeField] private BuildingPlacer _placer;

    private PlayerCursorModes _cursorMode;

    public void SetCursorMode(PlayerCursorModes mode)
    {
        _cursorMode = mode;

        switch (_cursorMode)
        {
            default:
            case PlayerCursorModes.Select:
                ActivateSelectMode();
                break;
            case PlayerCursorModes.Building:
                ActivateBuildingMode();
                break;
        }
    }

    private void ActivateSelectMode()
    {
        _cursor.SetDefaultCursor();

        _placer.gameObject.SetActive(false);
        _selector.gameObject.SetActive(true);
    }

    private void ActivateBuildingMode()
    {
        _placer.gameObject.SetActive(true);
        _selector.gameObject.SetActive(false);
    }
}
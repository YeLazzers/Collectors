using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerInputRouter : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private SelectingMode _selector;
    [SerializeField] private BuildingPlacementMode _placer;
    [SerializeField] private float _raycastDistance = 1000f;

    private Camera _camera;
    private IInputMode _active;
    private PointerContext _lastPointer;

    private void Awake()
    {
        _camera = Camera.main;

        SwitchTo(_selector);
    }

    private void OnEnable()
    {
        _reader.MouseMoved += OnMouseMoved;
        _reader.LmbClicked += OnLmbClicked;
        _reader.RmbClicked += OnRmbClicked;
    }

    private void OnDisable()
    {
        _reader.MouseMoved += OnMouseMoved;
        _reader.LmbClicked += OnLmbClicked;
        _reader.RmbClicked += OnRmbClicked;
    }

    public void ActivateSelector()
        => SwitchTo(_selector);

    public void ActivateBuildingPlacementMode(BuildingPlacementContext context)
    {
        _placer.Configure(context);
        SwitchTo(_placer);
    }

    private void SwitchTo(IInputMode next)
    {
        if (_active == next) return;

        _active?.OnExit();
        _active = next;


        _active?.OnEnter(BuildPointerContext(_lastPointer.ScreenPos));
    }

    private void OnMouseMoved(Vector3 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _active?.OnMouseMove(BuildPointerContext(position));
    }

    private void OnLmbClicked(Vector3 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _active?.OnLmbDown(BuildPointerContext(position));
    }

    private void OnRmbClicked(Vector3 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _active?.OnRmbDown(BuildPointerContext(position));
    }

    private PointerContext BuildPointerContext(Vector2 screenPos)
    {
        Physics.Raycast(_camera.ScreenPointToRay(screenPos), out RaycastHit hitInfo, _raycastDistance, _active.RaycastLayer);

        _lastPointer = new PointerContext(screenPos, hitInfo);
        return _lastPointer;
    }
}
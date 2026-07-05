using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputRouter : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private float _raycastDistance = 1000f;

    private Camera _camera;
    private LayerMask _raycastLayer;
    private PointerContext _lastPointer;

    public event Action<PointerContext> Moved;

    public event Action<PointerContext> LmbClicked;

    public event Action<PointerContext> RmbClicked;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _reader.MouseMoved += OnMouseMoved;
        _reader.LmbClicked += OnLmbDown;
        _reader.RmbClicked += OnRmbDown;
    }

    private void OnDisable()
    {
        _reader.MouseMoved -= OnMouseMoved;
        _reader.LmbClicked -= OnLmbDown;
        _reader.RmbClicked -= OnRmbDown;
    }

    public void SetRaycastLayer(LayerMask layer)
    {
        _raycastLayer = layer;
    }

    public PointerContext RaycastAtLastScreenPosition()
    {
        return BuildPointerContext(_lastPointer.ScreenPos);
    }

    private void OnMouseMoved(Vector3 position)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Moved?.Invoke(BuildPointerContext(position));
    }

    private void OnLmbDown(Vector3 position)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        LmbClicked?.Invoke(BuildPointerContext(position));
    }

    private void OnRmbDown(Vector3 position)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        RmbClicked?.Invoke(BuildPointerContext(position));
    }

    private PointerContext BuildPointerContext(Vector2 screenPos)
    {
        Physics.Raycast(_camera.ScreenPointToRay(screenPos), out RaycastHit hitInfo, _raycastDistance, _raycastLayer);

        _lastPointer = new PointerContext(screenPos, hitInfo);
        return _lastPointer;
    }
}

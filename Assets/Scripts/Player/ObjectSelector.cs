using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CursorChanger _cursor;
    [SerializeField] private Camera _сamera;
    [SerializeField] private LayerMask selectableMask;

    private RaycastHit _hitInfo;
    private IHoverable _currentHoverable;

    public event Action<IHoverable> Hovered;
    public event Action<ISelectable> Selected;

    private void OnEnable()
    {
        _inputReader.MouseMoved += OnMouseMoved;
        _inputReader.Clicked += OnClick;
    }

    private void OnDisable()
    {
        _inputReader.MouseMoved -= OnMouseMoved;
        _inputReader.Clicked -= OnClick;
    }

    private void OnMouseMoved(Vector3 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("IsPointerOverGameObject");
            return;
        }

        if (TryRaycast(position, out _hitInfo)
            && TryGetHoverable(_hitInfo, out IHoverable hoverable))
        {
            if (hoverable == _currentHoverable)
                return;

            _currentHoverable?.OnHoverExit();
            _currentHoverable = hoverable;
            _currentHoverable?.OnHoverEnter();

            Hovered?.Invoke(_currentHoverable);

            if (TryGetSelectable(_hitInfo, out ISelectable _))
            {
                _cursor.SetSelectCursor();
            }
        }
        else if (_currentHoverable != null)
        {
            _currentHoverable?.OnHoverExit();
            _currentHoverable = null;
            Hovered?.Invoke(_currentHoverable);

            _cursor.SetDefaultCursor();
        }
    }

    private void OnClick(Vector3 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        TryGetSelectable(_hitInfo, out ISelectable selectable);

        Selected?.Invoke(selectable);
    }

    private bool TryRaycast(Vector3 screenPos, out RaycastHit hit)
    {
        Ray ray = _сamera.ScreenPointToRay(screenPos);
        return Physics.Raycast(ray, out hit);
    }

    private bool TryGetHoverable(RaycastHit hitInfo, out IHoverable hoverable)
    {

        if (hitInfo.collider != null
            && hitInfo.collider.TryGetComponent(out SelectionCollider selectionCollider))
        {
            hoverable = selectionCollider.GetComponentInParent<IHoverable>();
            return hoverable != null;
        }

        hoverable = null;
        return false;
    }

    private bool TryGetSelectable(RaycastHit hitInfo, out ISelectable selectable)
    {
        if (hitInfo.collider != null
            && hitInfo.collider.TryGetComponent(out SelectionCollider selectionCollider))
        {
            selectable = selectionCollider.GetComponentInParent<ISelectable>();
            return selectable != null;
        }

        selectable = null;
        return false;
    }
}
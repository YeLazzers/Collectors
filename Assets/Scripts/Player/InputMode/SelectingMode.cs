using System;
using UnityEngine;

public class SelectingMode : MonoBehaviour, IInputMode
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CursorChanger _cursor;
    [SerializeField] private LayerMask _selectableLayer;

    private IHoverable _currentHoverable;

    public LayerMask RaycastLayer => _selectableLayer;

    public event Action<IHoverable> Hovered;
    public event Action<ISelectable> Selected;

    public void OnMouseMove(PointerContext context)
    {
        if (TryGetHoverable(context.HitInfo, out IHoverable hoverable))
        {
            if (hoverable == _currentHoverable)
                return;

            SetHoverable(hoverable);

            if (TryGetSelectable(context.HitInfo, out ISelectable _))
            {
                _cursor.SetSelectCursor();
            }
        }
        else if (_currentHoverable != null)
        {
            SetHoverable(null);

            _cursor.SetDefaultCursor();
        }
    }

    public void OnEnter(PointerContext context)
    {
        Selected?.Invoke(null);

        OnMouseMove(context);
        _cursor.SetDefaultCursor();
    }

    public void OnExit()
    {
        SetHoverable(null);
    }

    public void OnLmbDown(PointerContext context)
    {
        if (TryGetSelectable(context.HitInfo, out ISelectable selectable))
            Selected?.Invoke(selectable);
    }

    public void OnRmbDown(PointerContext context)
    {
        Selected?.Invoke(null);
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

    private void SetHoverable(IHoverable hoverable)
    {
        _currentHoverable?.OnHoverExit();
        _currentHoverable = hoverable;
        _currentHoverable?.OnHoverEnter();

        Hovered?.Invoke(_currentHoverable);
    }
}
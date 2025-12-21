using UnityEngine;

public interface IInputMode
{
    LayerMask RaycastLayer { get; }

    void OnEnter(PointerContext context);
    void OnExit();
    void OnMouseMove(PointerContext context);
    void OnLmbDown(PointerContext context);
    void OnRmbDown(PointerContext context);

}
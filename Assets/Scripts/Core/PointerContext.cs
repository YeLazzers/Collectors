using UnityEngine;

public readonly struct PointerContext
{
    public readonly Vector2 ScreenPos;
    public readonly RaycastHit HitInfo;

    public PointerContext(Vector2 screenPos, RaycastHit hit)
    {
        ScreenPos = screenPos;
        HitInfo = hit;
    }
}

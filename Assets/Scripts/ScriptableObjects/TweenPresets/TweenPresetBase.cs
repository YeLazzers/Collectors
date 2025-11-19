using DG.Tweening;
using UnityEngine;

public abstract class TweenPreset : ScriptableObject
{
    [Header("Base Settings")]
    public float Duration = 1f;
    public int Repeats = -1;
    public LoopType LoopType = LoopType.Restart;
    public Ease EaseType = Ease.Linear;

    public abstract Tween Create(Component target);

    protected Tween ConfigureTween(Tween tween)
    {
        return tween
            .SetEase(EaseType)
            .SetLoops(Repeats, LoopType);
    }
}
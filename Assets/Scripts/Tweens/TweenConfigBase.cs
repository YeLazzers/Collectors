using DG.Tweening;
using UnityEngine;

public abstract class TweenConfigBase
{
    public float Duration;
    public int Repeats = -1;
    public LoopType LoopType;
    public Ease EaseType;

    public Tweener ConfigureTween(Tweener tween)
    {
        return tween
            .SetLoops(Repeats, LoopType)
            .SetEase(EaseType);
    }
}
using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class AppearTweenConfig : ScaleTweenConfig
{
    public Tweener CreateTween(Transform target)
    {
        target.localScale = Vector3.zero;

        var tween = target.DOScale(To, Duration);
        return ConfigureTween(tween);
    }
}
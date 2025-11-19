using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class RotateTweenConfig : TweenConfigBase
{
    [Header("Rotation Settings")]
    public Vector3 Rotation;

    public Tweener CreateTween(Transform target)
    {
        var tween = target.DORotate(Rotation, Duration);
        return ConfigureTween(tween);
    }
}
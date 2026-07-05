using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class VacuumTweenConfig
{
    [SerializeField] private MoveTweenConfig _moveConfig;
    [SerializeField] private ScaleTweenConfig _scaleConfig;

    public Tweener CreateMoveTween(Transform target, Vector3 targetPosition)
    {
        var tween = target.DOMove(targetPosition, _moveConfig.Duration);
        return _moveConfig.ConfigureTween(tween);
    }

    public Tweener CreateScaleTween(Transform target)
    {
        var tween = target.DOScale(_scaleConfig.To, _scaleConfig.Duration);
        return _scaleConfig.ConfigureTween(tween);
    }
}
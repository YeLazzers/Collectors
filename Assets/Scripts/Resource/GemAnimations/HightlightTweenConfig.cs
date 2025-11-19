using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class HightlightTweenConfig : TweenConfigBase
{
    private readonly float _blinkDurationDivider = 2f;
    private readonly string _colorPropertyName = "_EmissionColor";

    [Header("Highlight Settings")]
    public Color To;
    public float ColorMultiply = 3f;
    public int BlinkPerLoop = 2;
    public float ScaleMultiplier = 1.2f;
    public float JumpPower = 0.5f;

    public Tween CreateFlashTween(Renderer renderer)
    {
        Color originalColor = renderer.material.GetColor(_colorPropertyName);

        float halfBlinkDuration = Duration / BlinkPerLoop / _blinkDurationDivider;

        var sequence = DOTween.Sequence();
        for (int i = 0; i < BlinkPerLoop; i++)
        {
            sequence.Append(renderer.material.DOColor(To * ColorMultiply, _colorPropertyName, halfBlinkDuration));
            sequence.Append(renderer.material.DOColor(originalColor, _colorPropertyName, halfBlinkDuration));
        }

        return sequence;
    }

    public Tween CreateScaleTween(Transform target, Vector3 to)
    {
        return target.DOScale(to, Duration);
    }

    public Tween CreateJumpTween(Transform target)
    {
        return target.DOJump(target.position, JumpPower, 1, Duration).SetEase(Ease.OutQuad);
    }

    public Tween CreateTween(Transform target, Renderer renderer)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(CreateJumpTween(target))
            .Join(CreateFlashTween(renderer));

        return sequence;
    }

}
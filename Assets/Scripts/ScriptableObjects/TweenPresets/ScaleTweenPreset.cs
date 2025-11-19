using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "Tween Presets/Scale", order = 52)]
public class ScaleTweenPreset : TweenPreset
{
    [Header("Scale Settings")]
    public Vector3 To;

    public override Tween Create(Component target)
    {
        Transform targetComponent = (Transform)target;

        return ConfigureTween(targetComponent.DOScale(To, Duration));
    }
}
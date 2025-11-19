using DG.Tweening;
using UnityEngine;

public class TweenPlayer
{
    // [SerializeField] private TweenPreset preset;
    // [SerializeField] private Component target;

    private Tween _tween;

    // private void Awake()
    // {
    //     _tween = preset.Create(target);

    //     _tween.Pause().SetAutoKill(false);
    // }

    public TweenPlayer(TweenPreset preset, Component target)
    {
        _tween = preset.Create(target);

        _tween.Pause().SetAutoKill(false);
    }

    public void Play() => _tween.Restart();
    public void Reverse() => _tween.PlayBackwards();
}

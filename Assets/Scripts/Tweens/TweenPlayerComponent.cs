using UnityEngine;

public class TweenPlayerComponent : MonoBehaviour
{
    [SerializeField] private TweenPreset _preset;
    [SerializeField] private Component target;

    private TweenPlayer _tweenPlayer;

    private void Awake()
    {
        _tweenPlayer = new TweenPlayer(_preset, target);
    }

    public void Play() => _tweenPlayer.Play();
    public void Reverse() => _tweenPlayer.Reverse();
}
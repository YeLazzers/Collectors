using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class GemAnimations : MonoBehaviour
{
    // [SerializeField] private float _rotateMultiply = 5f;

    [Header("Tween Configs")]
    [SerializeField] private AppearTweenConfig _appear;
    [SerializeField] private VacuumTweenConfig _vacuum;
    [SerializeField] private RotateTweenConfig _rotate;
    [SerializeField] private HightlightTweenConfig _highlight;


    private Tween _appearTween;
    private Tween _vacuumTween;
    private Tween _rotateTween;
    private Tween _highlightTween;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }


    public void PlayAppearAnimation()
    {
        _appearTween = _appear.CreateTween(transform);
        _appearTween.Play();
    }

    public void PlayVacuumAnimation(Vector3 targetPosition)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_vacuum.CreateMoveTween(transform, targetPosition));
        sequence.Join(_vacuum.CreateScaleTween(transform));

        _vacuumTween = sequence;
        _vacuumTween.Play();
    }

    public void PlayRotateAnimation()
    {
        _rotateTween = _rotate.CreateTween(transform);
        _rotateTween.Play();
    }

    public void PlayHighlightAnimation()
    {
        _highlight.CreateTween(transform, _renderer);
    }
}
using UnityEngine;
using DG.Tweening;
using System;

public class Scanner : MonoBehaviour
{
    [Header("VFX Params")]
    [SerializeField] private float _expansionDuration = 1f;
    [SerializeField] private float _maxScale = 10f;
    [SerializeField] private Ease _expansionEase = Ease.OutQuad;

    private Tween _scanTween;

    public event Action<IScannable> ScannableDetected;

    private void Awake()
    {
        _scanTween = CreateScanTween();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IScannable scannable))
        {
            ScannableDetected?.Invoke(scannable);
        }
    }

    [ContextMenu("Scan")]
    public void Scan()
    {
        _scanTween.Restart();
    }

    private Tween CreateScanTween()
    {
        var tween = transform.DOScale(Vector3.one * _maxScale, _expansionDuration)
            .SetEase(_expansionEase)
            .SetAutoKill(false)
            .Pause();

        tween.onComplete += OnScanComplete;

        return tween;
    }

    private void OnScanComplete()
    {
        transform.localScale = Vector3.zero;
    }
}
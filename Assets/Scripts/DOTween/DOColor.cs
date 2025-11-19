using DG.Tweening;
using UnityEngine;

namespace YeLazzers.Tweening
{
    [RequireComponent(typeof(Renderer))]
    public class DOColor : DOBase
    {
        [Header("Color Settings")]
        [SerializeField] private Color _targetColor;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public override void Play()
        {

            _renderer.material
                .DOColor(_targetColor * 3f, Duration)
                .SetLoops(Repeats, LoopType)
                .SetEase(EaseType);
        }
    }
}
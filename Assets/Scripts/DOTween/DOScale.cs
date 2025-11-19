using DG.Tweening;
using UnityEngine;

namespace YeLazzers.Tweening
{
    public class DOScale : DOBase
    {
        [Header("Scale Settings")]
        [SerializeField] private Vector3 _scale;

        public override void Play()
        {
            transform
                .DOScale(_scale, Duration)
                .SetLoops(Repeats, LoopType)
                .SetRelative()
                .SetEase(EaseType);
        }
    }
}
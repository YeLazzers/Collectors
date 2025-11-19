using DG.Tweening;
using UnityEngine;

namespace YeLazzers.Tweening
{
    public class DOMove : DOBase
    {
        [Header("Position Settings")]
        [SerializeField] private Vector3 _position;

        public override void Play()
        {
            transform
                .DOMove(_position, Duration)
                .SetLoops(Repeats, LoopType)
                .SetRelative()
                .SetEase(EaseType);
        }
    }
}
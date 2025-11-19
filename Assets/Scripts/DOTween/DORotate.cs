using DG.Tweening;
using UnityEngine;

namespace YeLazzers.Tweening
{
    public class DORotate : DOBase
    {
        [Header("Rotation Settings")]
        [SerializeField] private Vector3 _rotation;

        public override void Play()
        {
            transform
                .DORotate(_rotation, Duration)
                .SetLoops(Repeats, LoopType)
                .SetRelative()
                .SetEase(EaseType);
        }
    }
}
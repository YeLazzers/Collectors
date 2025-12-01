using UnityEngine;
using DG.Tweening;

public interface ICollectable
{
    Vector3 Position { get; }
    void Collect(Transform owner, TweenCallback onComplete = null);
}
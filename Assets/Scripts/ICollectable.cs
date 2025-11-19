using UnityEngine;
using DG.Tweening;

public interface ICollectable
{
    Transform Transform { get; }
    Vector3 Position { get; }
    void Collect(Transform owner, TweenCallback onComplete = null);
}
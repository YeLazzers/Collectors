using UnityEngine;
using DG.Tweening;

public interface ICollectable
{
    Vector3 Position { get; }
    bool IsCollected { get; }
    void Collect(Transform owner, TweenCallback onComplete = null);
}
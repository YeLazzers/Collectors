using DG.Tweening;
using UnityEngine;

public interface IPickable
{
    void Pick(Transform owner, TweenCallback onComplete = null);
}
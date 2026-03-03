using DG.Tweening;
using UnityEngine;

public class CollectableGrabber : MonoBehaviour, ICollector
{
    public void Collect(ICollectable collectable, TweenCallback onComplete = null)
    {
        collectable.Transform.SetParent(transform);
        collectable.Transform.DOLocalMove(Vector3.zero, 3f).SetEase(Ease.InOutQuart).onComplete += onComplete;
    }
}

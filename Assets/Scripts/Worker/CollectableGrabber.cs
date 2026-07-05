using DG.Tweening;
using UnityEngine;

public class CollectableGrabber : MonoBehaviour, ICollector
{
    [SerializeField] private float _grabDuration = 3f;
    public void Collect(ICollectable collectable, TweenCallback onComplete = null)
    {
        collectable.Transform.SetParent(transform);
        collectable.Transform.DOLocalMove(Vector3.zero, _grabDuration).SetEase(Ease.InOutQuart).onComplete += onComplete;
    }
}

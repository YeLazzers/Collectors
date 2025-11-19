using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable<Resource>, ICollectable, IScannable
{
    [SerializeField] private ResourceConfig _resourceConfig;

    private GemAnimations _gemAnimations;

    public event Action<Resource> Expired;

    public Transform Transform => transform;
    public Vector3 Position => transform.position;
    public ResourceType Type => _resourceConfig.ResourceType;
    public int Amount => _resourceConfig.Value;

    private void Awake()
    {
        _gemAnimations = GetComponent<GemAnimations>();


        Initialize(Vector3.zero);
    }

    public Resource Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _gemAnimations.PlayAppearAnimation();
        _gemAnimations.PlayRotateAnimation();

        return this;
    }

    public Resource Initialize(Vector3 position, Transform parent = null)
    {
        if (parent != null)
            transform.SetParent(parent);

        return Initialize(position);
    }

    public void Collect(Transform newParent, TweenCallback onComplete = null)
    {
        transform.SetParent(newParent);
        transform.DOLocalMove(Vector3.zero, 3f).SetEase(Ease.InOutQuart).onComplete += onComplete;
    }

    public void Scan()
    {
        _gemAnimations.PlayHighlightAnimation();
    }
}
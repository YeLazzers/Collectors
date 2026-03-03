using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable<Resource>, ICollectable, IHighlightable
{
    [SerializeField] private ResourceConfig _resourceConfig;

    private GemAnimations _gemAnimations;

    public event Action<Resource> Expired;
    public Transform Transform => transform;
    public ResourceType Type => _resourceConfig.ResourceType;
    public int Amount => _resourceConfig.Value;

    private void Awake()
    {
        _gemAnimations = GetComponent<GemAnimations>();
    }

    public Resource Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _gemAnimations.PlayAppearAnimation();
        _gemAnimations.PlayRotateAnimation();

        name = $"{_resourceConfig.ResourceType} {GetInstanceID()}";

        return this;
    }

    public Resource Initialize(Vector3 position, Transform parent = null)
    {
        if (parent != null)
            transform.SetParent(parent);

        return Initialize(position);
    }

    public void Highlight()
    {
        _gemAnimations.PlayHighlightAnimation();
    }
}
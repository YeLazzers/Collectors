using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private CollectorMover _mover;

    private ICollectable _currentCollectable;

    public void Move(ICollectable collectable)
    {
        _currentCollectable = collectable;

        _mover.Move(_currentCollectable.Position);
    }

    public void Grab()
    {
        _currentCollectable.Collect(_resourceHolder.transform, DeliverCollectable);
    }

    public void DeliverCollectable()
    {
        _mover.Move(_mainBuilding.GetLandingPoint(transform.position), () =>
        {
            _mainBuilding.TakeResource((Resource)_currentCollectable);
            _currentCollectable = null;
        });
    }

    public void SetCollectable(ICollectable collectable)
    {
        _currentCollectable = collectable;
    }

}
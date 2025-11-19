using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private Resource _resource;

    [SerializeField] private float _moveSpeed = 2f;

    [ContextMenu("Grab Resource")]
    public void GrabResource()
    {
        if (_resource == null)
            return;

        _resource.Pick(_resourceHolder.transform);
    }

    [ContextMenu("Deliver Resource")]
    public void DeliverResource()
    {
        if (_resource == null)
            return;

        _mainBuilding.DeliverResource(_resource);
    }
}
using UnityEngine;

[RequireComponent(typeof(Collector))]
public class CollectorContext : MonoBehaviour
{
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private MainBuilding _mainBuilding;

    private Collector _collector;
    private ICollectable _collectable;

    private void Awake()
    {
        _collector = GetComponent<Collector>();
    }

    [ContextMenu("Move To Resource")]
    public void MoveToResource()
    {
        _collectable = _mainBuilding.GetNextResource();

        _collector.Move(_collectable);
    }

    [ContextMenu("Grab Resource")]
    public void GrabResource()
    {
        _collector.Grab();
    }

    [ContextMenu("Deliver Resource")]
    public void DeliverResource()
    {
        _collector.DeliverCollectable();
    }

    private ICollectable GetResource()
    {
        return _collectable == null ? _mainBuilding.GetNextResource() : _collectable;
    }
}
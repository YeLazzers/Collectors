using UnityEngine;

[RequireComponent(typeof(Collector))]
public class CollectorContext : MonoBehaviour
{
    [SerializeField] private MainBuilding _mainBuilding;

    private Collector _collector;
    private ICollectable _collectable;

    private void Awake()
    {
        _collector = GetComponent<Collector>();
    }

    [ContextMenu("Create job")]
    private void CreateJob()
    {
        _collectable = _mainBuilding.GetNextResource();

        _collector.BeginCollect(new CollectJob(_collectable, _mainBuilding));
    }
}
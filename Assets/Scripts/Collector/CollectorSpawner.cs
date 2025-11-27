using UnityEngine;

public class CollectorSpawner : PoolBase<Collector>
{
    [SerializeField] private SplineContainer _splineContainer;


    public Collector Spawn(Vector3 position, Vector3 direction)
    {
        Collector collector = Get();
        collector.Initialize(position, direction, _splineContainer.CreateEmptySpline(collector.GetInstanceID()));

        return collector;
    }
}
using UnityEngine;

public class WorkerSpawner : PoolBase<Worker>
{
    [SerializeField] private SplineContainer _splineContainer;

    public Worker Spawn(Vector3 position, Vector3 direction)
    {
        Worker worker = Get();
        worker.Initialize(position, direction, _splineContainer.CreateEmptySpline(worker.GetInstanceID()));

        return worker;
    }
}

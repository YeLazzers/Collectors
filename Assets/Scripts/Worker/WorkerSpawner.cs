using UnityEngine;

public class WorkerSpawner : PoolBase<Worker>
{
    public Worker Spawn(Vector3 position, Vector3 direction)
    {
        Worker worker = Get();
        worker.Initialize(position, direction);

        return worker;
    }
}

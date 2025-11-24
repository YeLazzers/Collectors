using UnityEngine;

public class CollectJob : JobBase
{
    public ICollectable Collectable { get; private set; }
    public CollectJob(ICollectable collectable, MainBuilding building) : base(building)
    {
        Collectable = collectable;
    }
    public override void ApplyTo(IWorkable worker)
    {
        if (worker is ICollectWorkable collectoWorker)
            collectoWorker.BeginCollect(this);
        else
            Debug.LogError($"{worker} cannot handle CollectJob");
    }
}
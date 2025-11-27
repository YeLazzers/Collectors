using UnityEngine;

public abstract class JobBase : IJob
{
    public JobBase(MainBuilding building) { Building = building; }

    public MainBuilding Building { get; private set; }
    public IWorkable AssignedWorker { get; private set; }

    public virtual void ApplyTo(IWorkable worker)
    {
        AssignedWorker = worker;
    }
}
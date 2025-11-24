using UnityEngine;

public abstract class JobBase : IJob
{
    public MainBuilding Building { get; private set; }
    public JobBase(MainBuilding building) { Building = building; }

    public abstract void ApplyTo(IWorkable worker);
}
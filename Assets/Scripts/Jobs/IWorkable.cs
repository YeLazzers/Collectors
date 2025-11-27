using System;

public interface IWorkable
{
    event Action<Collector> JobAssigned;
    event Action<Collector> JobFinished;

    void SetJob(IJob job);
    void FinishJob();
}
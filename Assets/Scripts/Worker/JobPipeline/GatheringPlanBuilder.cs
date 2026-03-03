using UnityEngine;

public sealed class GatheringPlanBuilder
{
    public bool TryBuild(GatheringJob gatheringJob, Worker worker, ICollector collector, out IWorkerPlan plan)
    {
        if (gatheringJob == null)
        {
            Debug.LogError("GatheringPlanBuilder.TryBuild failed: gatheringJob is null.");
            plan = null;
            return false;
        }

        if (worker == null)
        {
            Debug.LogError("GatheringPlanBuilder.TryBuild failed: worker is null.");
            plan = null;
            return false;
        }

        if (collector == null)
        {
            Debug.LogError("GatheringPlanBuilder.TryBuild failed: collector is null.");
            plan = null;
            return false;
        }

        plan = new WorkerPlanBuilder()
            .Add(new MoveStep(worker, () => gatheringJob.Resource.Transform.position))
            .Add(new CollectStep(gatheringJob.Resource, collector))
            .Add(new MoveStep(worker, () => gatheringJob.Destination.GetLandingPoint(worker.transform.position)))
            .Add(new DepositStep(gatheringJob.Destination, gatheringJob.Resource))
            .Build();

        return true;
    }
}

using UnityEngine;

public sealed class GatheringPlanBuilder
{
    public bool TryBuild(GatheringJob gatheringJob, Worker worker, ResourceHolder resourceHolder, out IWorkerPlan plan)
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

        if (resourceHolder == null)
        {
            Debug.LogError("GatheringPlanBuilder.TryBuild failed: resourceHolder is null.");
            plan = null;
            return false;
        }

        plan = new WorkerPlanBuilder()
            .Add(new MoveStep(worker, () => gatheringJob.Resource.Position))
            .Add(new CollectStep(gatheringJob.Resource, resourceHolder.transform))
            .Add(new MoveStep(worker, () => gatheringJob.Destination.GetLandingPoint(worker.transform.position)))
            .Add(new DepositStep(gatheringJob.Destination, gatheringJob.Resource))
            .Build();

        return true;
    }
}

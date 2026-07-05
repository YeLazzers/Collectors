using YeLazzers.Buildings.Modules;
using YeLazzers.Jobs;

public sealed class GatheringPlanBuilder
{
    public bool TryBuild(GatheringJob gatheringJob, Worker worker, ICollector collector, out IWorkerPlan plan)
    {
        if (gatheringJob.Destination.TryGetModule(out ResourceStorage storage))
        {

            plan = new WorkerPlanBuilder()
                .Add(new MoveStep(worker, () => gatheringJob.Resource.Transform.position))
                .Add(new CollectStep(gatheringJob.Resource, collector))
                .Add(new MoveStep(worker, () => gatheringJob.Destination.GetLandingPoint(worker.transform.position)))
                .Add(new DepositStep(storage, gatheringJob.Resource))
                .Build();

            return true;
        }
        else
        {
            plan = null;
            return false;
        }

    }
}

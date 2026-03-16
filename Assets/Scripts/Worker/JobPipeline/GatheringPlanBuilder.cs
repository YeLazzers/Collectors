using UnityEngine;
using YeLazzers.Buildings;

public sealed class GatheringPlanBuilder
{
    public bool TryBuild(GatheringJob gatheringJob, Worker worker, ICollector collector, out IWorkerPlan plan)
    {
        plan = null;

        ResourceStorage storage = null;

        if (gatheringJob?.Destination != null)
        {
            gatheringJob.Destination.TryGetModule(out storage);
        }

        if (gatheringJob == null || worker == null || collector == null || gatheringJob.Destination == null || storage == null)
        {
            Debug.LogError($"GatheringPlanBuilder: gatheringJob={gatheringJob}, worker={worker}, collector={collector}, destination={gatheringJob?.Destination}, storage={storage}");
            return false;
        }

        plan = new WorkerPlanBuilder()
            .Add(new MoveStep(worker, () => gatheringJob.Resource.Transform.position))
            .Add(new CollectStep(gatheringJob.Resource, collector))
            .Add(new MoveStep(worker, () => gatheringJob.Destination.GetLandingPoint(worker.transform.position)))
            .Add(new DepositStep(storage, gatheringJob.Resource))
            .Build();

        return true;
    }
}

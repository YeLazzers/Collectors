using UnityEngine;
using YeLazzers.Buildings;

public sealed class BuildingPlanBuilder
{
    public bool TryBuild(BuildingJob buildingJob, Worker worker, out IWorkerPlan plan)
    {
        ResourceStorage storage = worker.Hub.Storage;
        ConstructionSite site = buildingJob.Site;

        plan = new WorkerPlanBuilder()
            .Add(new TakeResourcesStep(storage, site.Config.Cost))
            .Add(new MoveStep(worker, () => site.Position))
            .Add(new BuildStep(site, worker))
            .Build();

        return true;
    }
}

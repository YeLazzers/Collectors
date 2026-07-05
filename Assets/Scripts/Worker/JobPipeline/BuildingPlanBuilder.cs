using UnityEngine;
using YeLazzers.Buildings;

public sealed class BuildingPlanBuilder
{
    public bool TryBuild(BuildingJob buildingJob, Worker worker, out IWorkerPlan plan)
    {
        plan = null;

        ResourceStorage storage = null;

        if (worker != null && worker.Home != null)
        {
            worker.Home.TryGetModule(out storage);
        }

        if (buildingJob == null || worker == null || buildingJob.Site == null || storage == null)
        {
            Debug.LogError($"BuildingPlanBuilder: buildingJob={buildingJob}, worker={worker}, site={buildingJob?.Site}, storage={storage}");
            return false;
        }

        Debug.Log($"BuildingPlanBuilder");

        ConstructionSite site = buildingJob.Site;

        plan = new WorkerPlanBuilder()
            .Add(new TakeResourcesStep(storage, site.Config.Cost))
            .Add(new MoveStep(worker, () => site.Position))
            .Add(new BuildStep(site))
            .Build();

        return true;
    }
}

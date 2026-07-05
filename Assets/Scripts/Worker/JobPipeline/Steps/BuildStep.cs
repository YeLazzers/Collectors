using System.Collections;
using YeLazzers.Buildings;

public sealed class BuildStep : StepBase
{
    private readonly ConstructionSite _site;
    private readonly Worker _worker;

    public BuildStep(ConstructionSite site, Worker worker)
    {
        _site = site;
        _worker = worker;
    }

    protected override IEnumerator Run()
    {
        if (_site == null || _worker == null)
        {
            Fail();
            yield break;
        }

        var newBuilding = _site.Complete();

        if (newBuilding != null && newBuilding.TryGetModule(out WorkerHub hub))
        {
            hub.AddWorker(_worker);
        }

        Succeed();

        yield break;
    }
}

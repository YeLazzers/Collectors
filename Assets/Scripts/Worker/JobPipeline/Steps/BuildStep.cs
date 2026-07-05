using System.Collections;
using YeLazzers.Buildings;

public sealed class BuildStep : StepBase
{
    private readonly ConstructionSite _site;

    public BuildStep(ConstructionSite site)
    {
        _site = site;
    }

    protected override IEnumerator Run()
    {
        if (_site == null)
        {
            Fail();
            yield break;
        }

        _site.Complete();
        Succeed();

        yield break;
    }
}

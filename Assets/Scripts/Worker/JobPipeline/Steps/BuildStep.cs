using System.Collections;
using UnityEngine;

public sealed class BuildStep : StepBase
{
    private readonly BuildingBuilder _builder;

    public BuildStep(BuildingBuilder builder)
    {
        _builder = builder;
    }

    protected override IEnumerator Run()
    {
        if (_builder == null || _builder.IsBuildingInProgress == false)
        {
            Fail();
            yield break;
        }

        Debug.Log($"[JobPipeline] BuildStep started. Builder={_builder.name}");

        _builder.FinishBuilding();
        Succeed();

        yield break;
    }
}

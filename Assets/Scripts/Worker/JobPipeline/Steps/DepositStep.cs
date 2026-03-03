using System.Collections;
using UnityEngine;

public sealed class DepositStep : StepBase
{
    private readonly MainBuilding _building;
    private readonly Resource _resource;

    public DepositStep(MainBuilding building, Resource resource)
    {
        _building = building;
        _resource = resource;
    }

    protected override IEnumerator Run()
    {
        if (_building == null || _resource == null)
        {
            Fail();
            yield break;
        }

        Debug.Log($"[JobPipeline] DepositStep started. Resource={_resource.name}, Building={_building.name}");

        _building.Deposit(_resource, () =>
        {
            if (Result == StepResult.None)
            {
                Succeed();
            }
        });

        while (Result == StepResult.None)
        {
            yield return null;
        }
    }
}

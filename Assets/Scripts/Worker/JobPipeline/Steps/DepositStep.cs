using System.Collections;
using UnityEngine;
using YeLazzers.Buildings;

public sealed class DepositStep : StepBase
{
    private readonly Station _building;
    private readonly Resource _resource;

    public DepositStep(Station building, Resource resource)
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

        if (_building.TryDeposit(_resource))
        {
            Succeed();
        }
        else
        {
            Fail();
        }
    }
}

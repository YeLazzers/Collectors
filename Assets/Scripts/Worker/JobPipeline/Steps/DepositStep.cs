using System.Collections;
using DG.Tweening;
using UnityEngine;
using YeLazzers.Buildings;

public sealed class DepositStep : StepBase
{
    private readonly ResourceStorage _storage;
    private readonly Resource _resource;

    public DepositStep(ResourceStorage storage, Resource resource)
    {
        _storage = storage;
        _resource = resource;
    }

    protected override IEnumerator Run()
    {
        if (_storage == null || _resource == null)
        {
            Fail();
            yield break;
        }

        _resource.Transform.DOMove(_storage.transform.position, 1f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                _storage.Deposit(_resource);
                Succeed();
            });

        while (Result == StepResult.None)
        {
            yield return null;
        }
    }
}

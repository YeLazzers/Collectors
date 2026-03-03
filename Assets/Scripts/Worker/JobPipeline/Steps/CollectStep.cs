using System.Collections;
using UnityEngine;

public sealed class CollectStep : StepBase
{
    private readonly ICollectable _collectable;
    private readonly Transform _holder;

    public CollectStep(ICollectable collectable, Transform holder)
    {
        _collectable = collectable;
        _holder = holder;
    }

    protected override IEnumerator Run()
    {
        if (_collectable == null || _holder == null)
        {
            Fail();
            yield break;
        }

        string collectableName = _collectable is Component component ? component.name : _collectable.GetType().Name;
        Debug.Log($"[JobPipeline] CollectStep started. Collectable={collectableName}, Holder={_holder.name}");

        _collectable.Collect(_holder, () =>
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

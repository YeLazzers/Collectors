using System.Collections;
using YeLazzers.Buildings.Modules;

public sealed class TakeResourcesStep : StepBase
{
    private readonly ResourceStorage _storage;
    private readonly int _amount;

    public TakeResourcesStep(ResourceStorage storage, int amount)
    {
        _storage = storage;
        _amount = amount;
    }

    protected override IEnumerator Run()
    {
        if (_storage == null || _storage.TrySpend(_amount) == false)
        {
            Fail();
            yield break;
        }

        Succeed();
        yield break;
    }
}

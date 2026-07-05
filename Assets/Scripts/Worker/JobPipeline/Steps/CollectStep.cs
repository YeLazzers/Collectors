using System.Collections;

public sealed class CollectStep : StepBase
{
    private readonly ICollectable _collectable;
    private readonly ICollector _collector;

    public CollectStep(ICollectable collectable, ICollector collector)
    {
        _collectable = collectable;
        _collector = collector;
    }

    protected override IEnumerator Run()
    {
        if (_collectable == null || _collector == null)
        {
            Fail();
            yield break;
        }

        _collector.Collect(_collectable, () =>
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

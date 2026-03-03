using System.Collections;
using UnityEngine;

public abstract class StepBase : IWorkerStep
{
    private bool _isCancelled;

    protected bool IsCancelled => _isCancelled;

    public StepResult Result { get; private set; } = StepResult.None;

    public IEnumerator Execute()
    {
        Result = StepResult.None;
        _isCancelled = false;

        yield return Run();

        if (_isCancelled && Result == StepResult.None)
        {
            Result = StepResult.Cancelled;
        }

        if (Result == StepResult.None)
        {
            Result = StepResult.Failed;
        }
    }

    public void Cancel()
    {
        LogIfResultAlreadySet(nameof(Cancel));

        _isCancelled = true;
        Result = StepResult.Cancelled;
    }

    protected abstract IEnumerator Run();

    protected void Succeed()
    {
        LogIfResultAlreadySet(nameof(Succeed));

        Result = StepResult.Success;
    }

    protected void Fail()
    {
        LogIfResultAlreadySet(nameof(Fail));

        Result = StepResult.Failed;
    }

    private void LogIfResultAlreadySet(string methodName)
    {
        if (Result != StepResult.None)
        {
            Debug.LogError($"{GetType().Name}.{methodName} called when result is already set to {Result}.");
        }
    }
}

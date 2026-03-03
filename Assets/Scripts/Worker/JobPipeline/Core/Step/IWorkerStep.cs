using System.Collections;

public interface IWorkerStep
{
    StepResult Result { get; }

    IEnumerator Execute();

    void Cancel();
}

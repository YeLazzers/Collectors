using System;
using System.Collections.Generic;

public sealed class WorkerPlanBuilder
{
    private readonly List<IWorkerStep> _steps = new();

    public WorkerPlanBuilder Add(IWorkerStep step)
    {
        if (step == null)
        {
            throw new ArgumentNullException(nameof(step));
        }

        _steps.Add(step);
        return this;
    }

    public IWorkerPlan Build()
    {
        return new WorkerPlan(_steps.ToArray());
    }
}

using System;
using System.Collections.Generic;

public sealed class WorkerPlan : IWorkerPlan
{
    private readonly IReadOnlyList<IWorkerStep> _steps;

    public WorkerPlan(IReadOnlyList<IWorkerStep> steps)
    {
        _steps = steps ?? throw new ArgumentNullException(nameof(steps));
    }

    public IReadOnlyList<IWorkerStep> Steps => _steps;
}

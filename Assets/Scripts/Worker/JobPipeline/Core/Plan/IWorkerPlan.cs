using System.Collections.Generic;

public interface IWorkerPlan
{
    IReadOnlyList<IWorkerStep> Steps { get; }
}

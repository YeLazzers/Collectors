public interface IJobPlan
{
    WorkerState EntryState { get; }

    void Configure(WorkContext context, TransitionScheme scheme);
}

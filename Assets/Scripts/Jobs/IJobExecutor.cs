namespace YeLazzers.Jobs
{
    public interface IJobExecutor
    {
        bool IsIdle { get; }
    }
}
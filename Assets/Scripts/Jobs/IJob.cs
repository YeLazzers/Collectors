namespace YeLazzers.Jobs
{
    public enum JobType
    {
        Building,
        ResourceGathering,
    }

    public enum JobStatus
    {
        Pending,
        Running,
        Completed,
        Failed,
    }

    public interface IJob
    {
        string Name { get; }
        int Priority { get; }
        JobType Type { get; }
        JobStatus Status { get; }

        void SetStatus(JobStatus status);
    }
}
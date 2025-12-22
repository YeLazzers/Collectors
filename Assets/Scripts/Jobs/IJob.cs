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
    void ApplyTo(IWorkable worker);
}

public interface IJob2
{
    string Name { get; }
    int Priority { get; }
    JobType Type { get; }
    JobStatus Status { get; }
}
using UnityEngine;

public readonly struct BuildingJobContext
{
    public readonly BuildingConfig Config;
    public readonly Vector3 Position;
    public readonly BuildingBuilder Source;

    public BuildingJobContext(BuildingConfig config, Vector3 position, BuildingBuilder source)
    {
        Config = config;
        Position = position;
        Source = source;
    }
}

public class BuildingJob : IJob
{
    private readonly string _name = "Building Job";

    private int _priority;
    private JobStatus _status;
    private BuildingJobContext _context;

    public string Name => _name;
    public int Priority => _priority;
    public JobType Type => JobType.Building;
    public JobStatus Status => _status;

    public BuildingConfig Config => _context.Config;
    public Vector3 Position => _context.Position;
    public BuildingBuilder Source => _context.Source;

    public BuildingJob(BuildingJobContext context, int priority)
    {
        _context = context;
        _priority = priority;
        _status = JobStatus.Pending;
    }

    public void SetPriority(int priority)
    {
        _priority = priority;
    }

    public void SetStatus(JobStatus status)
    {
        _status = status;
    }
}
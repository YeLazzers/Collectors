using UnityEngine;

public readonly struct ResourceGatheringJobContext
{
    public readonly Resource Resource;
    public readonly MainBuilding Destination;

    public ResourceGatheringJobContext(Resource resource, MainBuilding destination)
    {
        Resource = resource;
        Destination = destination;
    }
}

public class ResourceGatheringJob : IJob2
{
    private readonly string _name = "Resource Gathering Job";

    private int _priority;
    private JobStatus _status;
    private ResourceGatheringJobContext _context;

    public string Name => _name;
    public int Priority => _priority;
    public JobType Type => JobType.ResourceGathering;
    public JobStatus Status => _status;

    public Resource Resource => _context.Resource;
    public MainBuilding Destination => _context.Destination;

    public ResourceGatheringJob(ResourceGatheringJobContext context, int priority)
    {
        _context = context;
        _priority = priority;
        _status = JobStatus.Pending;
    }

    public void SetPriority(int priority)
    {
        _priority = priority;
    }
}
public readonly struct GatheringJobContext
{
    public readonly Resource Resource;
    public readonly MainBuilding Destination;

    public GatheringJobContext(Resource resource, MainBuilding destination)
    {
        Resource = resource;
        Destination = destination;
    }
}

public class GatheringJob : IJob, IJobPlan
{
    private readonly string _name = "Resource Gathering Job";

    private int _priority;
    private JobStatus _status;
    private GatheringJobContext _context;

    public string Name => _name;
    public int Priority => _priority;
    public JobType Type => JobType.ResourceGathering;
    public JobStatus Status => _status;
    public WorkerState EntryState => WorkerState.MoveToResource;

    public Resource Resource => _context.Resource;
    public MainBuilding Destination => _context.Destination;

    public GatheringJob(GatheringJobContext context, int priority)
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

    public void Configure(WorkContext context, TransitionScheme scheme)
    {
        context.Resource = Resource;
        context.Building = Destination;

        scheme.Add(WorkerState.MoveToResource, WorkerSignal.Arrived,   WorkerState.Grab)
              .Add(WorkerState.Grab,            WorkerSignal.Collected, WorkerState.ReturnToBase)
              .Add(WorkerState.ReturnToBase,    WorkerSignal.Arrived,   WorkerState.Deliver)
              .Add(WorkerState.Deliver,         WorkerSignal.Delivered, WorkerState.Idle);
    }
}

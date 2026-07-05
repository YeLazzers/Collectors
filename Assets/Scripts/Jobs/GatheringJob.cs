using YeLazzers.Buildings;

namespace YeLazzers.Jobs
{
    public readonly struct GatheringJobContext
    {
        public readonly Resource Resource;
        public readonly Building Destination;

        public GatheringJobContext(Resource resource, Building destination)
        {
            Resource = resource;
            Destination = destination;
        }
    }

    public class GatheringJob : IJob
    {
        private readonly string _name = "Resource Gathering Job";

        private int _priority;
        private JobStatus _status;
        private GatheringJobContext _context;

        public GatheringJob(GatheringJobContext context, int priority)
        {
            _context = context;
            _priority = priority;
            _status = JobStatus.Pending;
        }

        public string Name => _name;

        public int Priority => _priority;

        public JobType Type => JobType.ResourceGathering;

        public JobStatus Status => _status;

        public Resource Resource => _context.Resource;

        public Building Destination => _context.Destination;

        public void SetPriority(int priority)
        {
            _priority = priority;
        }

        public void SetStatus(JobStatus status)
        {
            _status = status;
        }
    }
}
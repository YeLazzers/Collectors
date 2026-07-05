using YeLazzers.Buildings;

namespace YeLazzers.Jobs
{
    public readonly struct BuildingJobContext
    {
        public readonly ConstructionSite Site;

        public BuildingJobContext(ConstructionSite site)
        {
            Site = site;
        }
    }

    public class BuildingJob : IJob
    {
        private readonly string _name = "Building Job";
        private readonly int _priority = 2;

        private JobStatus _status;
        private BuildingJobContext _context;

        public string Name => _name;
        public int Priority => _priority;
        public JobType Type => JobType.Building;
        public JobStatus Status => _status;

        public ConstructionSite Site => _context.Site;

        public BuildingJob(BuildingJobContext context)
        {
            _context = context;
            _status = JobStatus.Pending;
        }

        public void SetStatus(JobStatus status)
        {
            _status = status;
        }
    }
}
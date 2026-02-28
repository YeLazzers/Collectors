using UnityEngine;

public sealed class JobRunner : MonoBehaviour, IJobExecutor
{
    [SerializeField] private CollectorBrain _brain;

    private IJob _currentJob;
    private JobBoard _jobBoard;
    private bool _isRunning = false;

    public bool IsIdle => _currentJob == null;
    public IJob CurrentJob => _currentJob;

    private void OnEnable()
    {
        _brain.BecameIdle += OnBecameIdle;
    }

    private void OnDisable()
    {
        _brain.BecameIdle -= OnBecameIdle;
    }

    public void SetJobBoard(JobBoard board)
    {
        if (_jobBoard != null)
            _jobBoard.Changed -= TryGetJob;

        _jobBoard = board;
        _jobBoard.Changed += TryGetJob;

        TryGetJob();
    }

    public void Run()
    {
        _isRunning = true;
        TryGetJob();
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void AssignJob(IJob job)
    {
        _currentJob = job;

        switch (job.Type)
        {
            case JobType.ResourceGathering:
                var resourceGatheringJob = (GatheringJob)job;
                _brain.BeginGathering(resourceGatheringJob);
                // _fsm.EnterCollect((CollectJob)job);
                break;

            case JobType.Building:
                // _fsm.EnterBuild((BuildJob)job);
                break;
        }
        Debug.Log($"Job assigned: {_currentJob.Name}");
    }

    public bool CanExecute(JobType jobType)
    {
        throw new System.NotImplementedException();
    }

    private void TryGetJob()
    {
        if (_isRunning && IsIdle && _jobBoard.TryGetJob(out var job))
        {
            AssignJob(job);
        }
    }

    private void OnBecameIdle()
    {
        if (_currentJob != null)
        {
            _currentJob = null;
        }

        TryGetJob();
    }
}
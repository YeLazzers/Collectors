using UnityEngine;

public sealed class JobRunner : MonoBehaviour, IJobExecutor
{
    [SerializeField] private Worker _worker;

    private IJob _currentJob;
    private JobBoard _jobBoard;
    private bool _isRunning = false;

    public bool IsIdle => _currentJob == null;
    public IJob CurrentJob => _currentJob;

    private void OnEnable()
    {
        _worker.BecameIdle += OnBecameIdle;
    }

    private void OnDisable()
    {
        _worker.BecameIdle -= OnBecameIdle;
    }

    public void SetJobBoard(JobBoard board)
    {
        if (_jobBoard != null)
        {
            _jobBoard.Changed -= TryGetJob;
        }

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

        if (job is IJobPlan plan)
        {
            _worker.ExecuteJob(plan);
        }

        Debug.Log($"Job assigned: {_currentJob.Name}");
    }

    private void TryGetJob()
    {
        if (_isRunning && IsIdle && _jobBoard.TryGetJob(out IJob job))
        {
            AssignJob(job);
        }
    }

    private void OnBecameIdle()
    {
        _currentJob = null;
        TryGetJob();
    }
}

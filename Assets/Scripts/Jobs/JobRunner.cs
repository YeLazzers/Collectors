using System.Collections;
using UnityEngine;

public sealed class JobRunner : MonoBehaviour, IJobExecutor
{
    [SerializeField] private Worker _worker;
    [SerializeField] private ResourceHolder _resourceHolder;

    private readonly GatheringPlanBuilder _gatheringPlanBuilder = new();

    private IJob _currentJob;
    private JobBoard _jobBoard;
    private Coroutine _activePlanRoutine;
    private IWorkerStep _activeStep;
    private bool _isRunning = false;

    public bool IsIdle => _currentJob == null;
    public IJob CurrentJob => _currentJob;

    private void Awake()
    {
        if (_resourceHolder == null && _worker != null)
        {
            _resourceHolder = _worker.GetComponentInChildren<ResourceHolder>();
        }
    }

    private void OnDisable()
    {
        CancelActivePlan(markFailed: false);
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
        CancelActivePlan(markFailed: true);
    }

    public void AssignJob(IJob job)
    {
        _currentJob = job;

        // TODO: сделать фабрику при росте числа типов задач
        switch (job)
        {
            case GatheringJob gatheringJob:
                if (_gatheringPlanBuilder.TryBuild(gatheringJob, _worker, _resourceHolder, out IWorkerPlan pipelinePlan))
                {
                    StartPlan(pipelinePlan);
                }
                else
                {
                    FailCurrentJobAndTryNext();
                }

                break;
            default:
                Debug.LogWarning($"Unsupported job type in JobRunner: {job.GetType().Name}");
                FailCurrentJobAndTryNext();
                break;
        }
    }

    private void TryGetJob()
    {
        if (_isRunning && IsIdle && _jobBoard.TryGetJob(out IJob job))
        {
            AssignJob(job);
        }
    }

    private void StartPlan(IWorkerPlan plan)
    {
        CancelActivePlan(markFailed: false);
        _activePlanRoutine = StartCoroutine(ExecutePlan(plan));
    }

    private IEnumerator ExecutePlan(IWorkerPlan plan)
    {
        JobStatus finalStatus = JobStatus.Completed;

        for (int i = 0; i < plan.Steps.Count; i++)
        {
            _activeStep = plan.Steps[i];
            yield return _activeStep.Execute();

            if (_activeStep.Result != StepResult.Success)
            {
                finalStatus = JobStatus.Failed;
                break;
            }
        }

        _activeStep = null;
        _activePlanRoutine = null;

        if (_currentJob != null)
        {
            _currentJob.SetStatus(finalStatus);
            _currentJob = null;
        }

        TryGetJob();
    }

    private void CancelActivePlan(bool markFailed)
    {
        if (_activeStep != null && _activeStep.Result == StepResult.None)
        {
            _activeStep.Cancel();
        }

        if (_activePlanRoutine != null)
        {
            StopCoroutine(_activePlanRoutine);
            _activePlanRoutine = null;
        }

        _activeStep = null;

        if (markFailed && _currentJob != null && _currentJob.Status == JobStatus.Running)
        {
            _currentJob.SetStatus(JobStatus.Failed);
            _currentJob = null;
        }
    }

    private void FailCurrentJobAndTryNext()
    {
        if (_currentJob != null)
        {
            _currentJob.SetStatus(JobStatus.Failed);
            _currentJob = null;
        }

        TryGetJob();
    }
}

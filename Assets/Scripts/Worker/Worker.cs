using System;
using UnityEngine;

public class Worker : MonoBehaviour, IPoolable<Worker>
{
    [SerializeField] private WorkerStateMachine _stateMachine;
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private JobRunner _jobRunner;
    [SerializeField] private float _speed;

    private readonly WorkContext _context = new();
    private readonly TransitionScheme _scheme = new();

    public event Action<Worker> Expired;
    public event Action BecameIdle;

    public float Speed => _speed;
    public JobRunner JobRunner => _jobRunner;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
    }

    private void OnEnable()
    {
        _stateMachine.StateEntered += OnStateEntered;
    }

    private void OnDisable()
    {
        _stateMachine.StateEntered -= OnStateEntered;
    }

    public Worker Initialize(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public Worker Initialize(Vector3 position, Vector3 direction, SplinePath splinePath)
    {
        transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction - position));
        _stateMachine.Initialize(this, splinePath, _resourceHolder.transform, _context);
        return this;
    }

    public void ExecuteJob(IJobPlan plan)
    {
        _scheme.Clear();
        plan.Configure(_context, _scheme);
        _stateMachine.LoadScheme(_scheme);
        _stateMachine.ChangeState(plan.EntryState);
    }

    public void MoveToPoint(Vector3 position)
    {
        _stateMachine.ClearScheme();
        _context.ManualTarget = position;
        _stateMachine.ChangeState(WorkerState.MoveToPoint);
    }

    public void GrabResource(ICollectable resource)
    {
        _stateMachine.ClearScheme();
        _context.Resource = resource;
        _stateMachine.ChangeState(WorkerState.Grab);
    }

    public void ReturnToBase()
    {
        _stateMachine.ClearScheme();
        _stateMachine.ChangeState(WorkerState.ReturnToBase);
    }

    private void OnStateEntered(Enum stateId)
    {
        if ((WorkerState)stateId == WorkerState.Idle)
        {
            BecameIdle?.Invoke();
        }
    }
}

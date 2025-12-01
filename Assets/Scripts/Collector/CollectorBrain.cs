using System;
using UnityEngine;

public class CollectorBrain : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private ResourceHolder _resourceHolder;

    [Header("State machine")]
    [SerializeField] private CollectorStateMachine _stateMachine;

    private bool _isAutoMode = false;
    private SplinePath _splinePath;

    public void Initialize(SplinePath splinePath)
    {
        _splinePath = splinePath;
        _stateMachine.Initialize(_collector, _splinePath, _resourceHolder.transform);
    }

    public void BeginCollect(Action onComplete = null)
    {
        _isAutoMode = true;
        MoveToResource();
    }

    [ContextMenu("Move To Resource")]
    public void MoveToResource()
    {
        var job = GetCurrentJob();

        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = job.Collectable.Position
        }, OnMoveCompleted);
    }

    private void OnMoveCompleted()
    {
        HandleCompletion(Grab);
    }

    [ContextMenu("Grab Resource")]
    public void Grab()
    {
        var job = GetCurrentJob();

        _stateMachine.ChangeState(CollectorStates.Grab, job.Collectable, OnGrabCompleted);
    }

    private void OnGrabCompleted()
    {
        HandleCompletion(ReturnToBuilding);
    }

    [ContextMenu("Return To Building")]
    private void ReturnToBuilding()
    {
        var job = GetCurrentJob();

        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = job.Building.GetLandingPoint(_collector.transform.position),
        }, OnReturnCompleted);
    }

    private void OnReturnCompleted()
    {
        HandleCompletion(DeliverCollectable);
    }

    private void HandleCompletion(Action nextStep)
    {
        if (_isAutoMode)
            nextStep?.Invoke();
        else
            _stateMachine.ChangeStateToDefault();
    }

    [ContextMenu("Deliver Collectable")]
    public void DeliverCollectable()
    {
        var job = GetCurrentJob();

        _stateMachine.ChangeState(CollectorStates.Deliver, new DeliverStateParams
        {
            Building = job.Building,
            Collectable = job.Collectable,
        }, OnDeliverCompleted);
    }

    private void OnDeliverCompleted()
    {
        HandleCompletion(FinishCollect);
    }

    private CollectJob GetCurrentJob()
    {
        var job = _collector.CurrentJob;
        if (job != null)
            return job;

        FinishCollect();
        throw new Exception($"There isn`t assigned job for {_collector.name} {_collector.GetInstanceID()}");
    }

    private void FinishCollect()
    {
        _stateMachine.ChangeStateToDefault();
        _isAutoMode = false;

        _collector.FinishJob();
    }
}
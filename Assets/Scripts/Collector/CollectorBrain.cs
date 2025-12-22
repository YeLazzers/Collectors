using System;
using UnityEngine;

public class CollectorBrain : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private CollectorStateMachine _stateMachine;

    private bool _isAutoMode = false;
    private SplinePath _splinePath;
    private ResourceGatheringJob _gatheringJob;

    public Action BecameIdle;

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

    public void BeginGathering(ResourceGatheringJob job)
    {
        _gatheringJob = job;
        _isAutoMode = true;
        MoveToResource();
    }

    [ContextMenu("Move To Resource")]
    public void MoveToResource()
    {
        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = _gatheringJob.Resource.Position
        }, OnMoveCompleted);
    }

    private void OnMoveCompleted()
    {
        HandleCompletion(Grab);
    }

    [ContextMenu("Grab Resource")]
    public void Grab()
    {
        _stateMachine.ChangeState(CollectorStates.Grab, (ICollectable)_gatheringJob.Resource, OnGrabCompleted);
    }

    private void OnGrabCompleted()
    {
        HandleCompletion(ReturnToBuilding);
    }

    [ContextMenu("Return To Building")]
    private void ReturnToBuilding()
    {
        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = _gatheringJob.Destination.GetLandingPoint(_collector.transform.position),
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
        _stateMachine.ChangeState(CollectorStates.Deliver, new DeliverStateParams
        {
            Building = _gatheringJob.Destination,
            Collectable = (ICollectable)_gatheringJob.Resource,
        }, OnDeliverCompleted);
    }

    private void OnDeliverCompleted()
    {
        HandleCompletion(FinishGathering);
    }


    private void FinishGathering()
    {
        Debug.Log("Collector finished gathering");
        
        _stateMachine.ChangeStateToDefault();
        _isAutoMode = false;


        BecameIdle?.Invoke();
    }
}
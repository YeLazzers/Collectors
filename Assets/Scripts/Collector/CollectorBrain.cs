using System;
using UnityEngine;

public class CollectorBrain : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private CollectorStateMachine _stateMachine;

    private bool _isAutoMode = false;
    private SplinePath _splinePath;
    private GatheringJob _gatheringJob;
    private BuildingJob _buildingJob;

    public Action BecameIdle;

    public void Initialize(SplinePath splinePath)
    {
        _splinePath = splinePath;
        _stateMachine.Initialize(_collector, _splinePath, _resourceHolder.transform);
    }

    public void BeginGathering(GatheringJob job)
    {
        _gatheringJob = job;
        _isAutoMode = true;
        MoveToResource();
    }

    public void BeginBuilding(BuildingJob job)
    {
        _buildingJob = job;
        _isAutoMode = true;
        MoveTo(_buildingJob.Position);
    }

    [ContextMenu("Move To Resource")]
    public void MoveToResource()
    {
        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = _gatheringJob.Resource.Position
        }, OnMoveCompleted);
    }

    public void MoveTo(Vector3 position)
    {
        Vector3 dir = (position - transform.position).normalized;
        Vector3 targetPos = transform.position - dir * 1f;

        _stateMachine.ChangeState(CollectorStates.Move, new MoveStateParams
        {
            TargetPosition = targetPos
        }, () => HandleCompletion(_buildingJob.Source.FinishBuilding));
    }

    // public void CompleteBuilding()
    // {
    //     _buildingJob.Source.FinishBuilding();
    // }

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
        HandleCompletion(FinishJob);
    }


    private void FinishJob()
    {
        Debug.Log("Collector finished a job");

        _stateMachine.ChangeStateToDefault();
        _isAutoMode = false;


        BecameIdle?.Invoke();
    }
}
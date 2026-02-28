using UnityEngine;

public class WorkerStateMachine : StateMachineBase
{
    public void Initialize(Worker worker, SplinePath spline, Transform resourceHolder, WorkContext context)
    {
        RegisterState(WorkerState.Idle, new WorkerIdleState(this));
        RegisterState(WorkerState.MoveToResource, new MoveToResourceState(this, worker, spline, context));
        RegisterState(WorkerState.ReturnToBase, new ReturnToBaseState(this, worker, spline, context));
        RegisterState(WorkerState.MoveToPoint, new MoveToPointState(this, worker, spline, context));
        RegisterState(WorkerState.Grab, new WorkerGrabState(this, resourceHolder, context));
        RegisterState(WorkerState.Deliver, new WorkerDeliverState(this, context));

        Init(defaultStateId: WorkerState.Idle);
    }
}

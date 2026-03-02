using UnityEngine;

public class WorkerStateMachine : StateMachineBase
{
    public void Initialize(Worker worker, SplinePath spline, Transform resourceHolder, WorkContext context)
    {
        RegisterState(WorkerState.Idle, new WorkerIdleState(this));
        RegisterState(WorkerState.MoveToResource, new MoveToState(this, worker, spline, () => context.Resource.Position));
        RegisterState(WorkerState.ReturnToBase, new MoveToState(this, worker, spline, () => context.Building.GetLandingPoint(worker.transform.position)));
        RegisterState(WorkerState.MoveToPoint, new MoveToState(this, worker, spline, () => context.ManualTarget));
        RegisterState(WorkerState.Grab, new WorkerGrabState(this, resourceHolder, context));
        RegisterState(WorkerState.Deliver, new WorkerDeliverState(this, context));

        Init(defaultStateId: WorkerState.Idle);
    }
}

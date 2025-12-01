using UnityEngine;

public class CollectorStateMachine : StateMachineBase
{

    public void Initialize(Collector collector, SplinePath spline, Transform collectableHolder)
    {
        RegisterState(CollectorStates.Idle, new CollectorIdleState(this, collector));
        RegisterState(CollectorStates.Move, new CollectorMoveState(this, collector, spline));
        RegisterState(CollectorStates.Grab, new CollectorGrabState(this, collector, collectableHolder));
        RegisterState(CollectorStates.Deliver, new CollectorDeliverState(this, collector));

        Init(defaultStateId: CollectorStates.Idle);
    }
}
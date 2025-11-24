using System;
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

    public void Move(MoveStateParams parameters, Action onComplete = null)
    {
        ChangeState(CollectorStates.Move, parameters, onComplete);
    }

    public void Grab(ICollectable collectable, Action onComplete = null)
    {
        ChangeState(CollectorStates.Grab, collectable, onComplete);
    }
    public void Deliver(DeliverStateParams parameters, Action onComplete = null)
    {
        ChangeState(CollectorStates.Deliver, parameters, onComplete);
    }
}
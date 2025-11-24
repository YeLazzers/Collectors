using System;
using UnityEngine;

public class CollectorIdleState : StateBase
{
    private Collector _collector;

    public CollectorIdleState(StateMachineBase machine, Collector collector) : base(machine)
    {
        _collector = collector;
    }

    public override void OnEnter(Action onComplete)
    {
    }
    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }
}
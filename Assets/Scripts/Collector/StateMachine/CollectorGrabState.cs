using System;
using UnityEngine;

public class CollectorGrabState : StateBase, IParameterizedState<ICollectable>
{
    private Collector _collector;
    private ICollectable _collectable;
    private Transform _holder;

    public CollectorGrabState(StateMachineBase machine, Collector collector, Transform holder) : base(machine)
    {
        _collector = collector;
        _holder = holder;
    }

    public void Inject(ICollectable collectable)
    {
        _collectable = collectable;
    }

    public override void OnEnter(Action onComplete)
    {
        _collectable.Collect(_holder, () =>
        {
            onComplete?.Invoke();
        });
    }
    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }

}
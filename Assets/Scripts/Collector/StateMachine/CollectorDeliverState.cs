using System;

public struct DeliverStateParams
{
    public MainBuilding Building;
    public ICollectable Collectable;
}

public class CollectorDeliverState : StateBase, IParameterizedState<DeliverStateParams>
{
    private Collector _collector;
    private DeliverStateParams _params;

    public CollectorDeliverState(StateMachineBase machine, Collector collector) : base(machine)
    {
        _collector = collector;
    }

    public void Inject(DeliverStateParams parameters)
    {
        _params = parameters;
    }

    public override void OnEnter(Action onComplete)
    {
        _params.Building.TakeResource((Resource)_params.Collectable, onComplete);
    }
    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }

}
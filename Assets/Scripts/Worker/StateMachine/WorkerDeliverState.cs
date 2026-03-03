public class WorkerDeliverState : StateBase
{
    private readonly WorkContext _context;

    public WorkerDeliverState(StateMachineBase machine, WorkContext context) : base(machine)
    {
        _context = context;
    }

    public override void OnEnter()
    {
        _context.Building.Deposit((Resource)_context.Resource, () => Machine.FireSignal(WorkerSignal.Delivered));
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    public override void OnExit()
    {
    }
}

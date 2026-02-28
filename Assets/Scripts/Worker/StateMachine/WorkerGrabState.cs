using UnityEngine;

public class WorkerGrabState : StateBase
{
    private readonly Transform _resourceHolder;
    private readonly WorkContext _context;

    public WorkerGrabState(StateMachineBase machine, Transform resourceHolder, WorkContext context)
        : base(machine)
    {
        _resourceHolder = resourceHolder;
        _context = context;
    }

    public override void OnEnter()
    {
        _context.Resource.Collect(_resourceHolder, () => Machine.FireSignal(WorkerSignal.Collected));
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    public override void OnExit()
    {
    }
}

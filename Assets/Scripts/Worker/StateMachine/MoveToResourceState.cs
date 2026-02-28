using UnityEngine;

public class MoveToResourceState : WorkerMoveStateBase
{
    private readonly WorkContext _context;

    public MoveToResourceState(StateMachineBase machine, Worker worker, SplinePath spline, WorkContext context)
        : base(machine, worker, spline)
    {
        _context = context;
    }

    protected override Vector3 GetTarget()
    {
        return _context.Resource.Position;
    }
}

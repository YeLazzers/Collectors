using UnityEngine;

public class MoveToPointState : WorkerMoveStateBase
{
    private readonly WorkContext _context;

    public MoveToPointState(StateMachineBase machine, Worker worker, SplinePath spline, WorkContext context)
        : base(machine, worker, spline)
    {
        _context = context;
    }

    protected override Vector3 GetTarget()
    {
        return _context.ManualTarget;
    }
}

using UnityEngine;

public class ReturnToBaseState : WorkerMoveStateBase
{
    private readonly Worker _worker;
    private readonly WorkContext _context;

    public ReturnToBaseState(StateMachineBase machine, Worker worker, SplinePath spline, WorkContext context)
        : base(machine, worker, spline)
    {
        _worker = worker;
        _context = context;
    }

    protected override Vector3 GetTarget()
    {
        return _context.Building.GetLandingPoint(_worker.transform.position);
    }
}

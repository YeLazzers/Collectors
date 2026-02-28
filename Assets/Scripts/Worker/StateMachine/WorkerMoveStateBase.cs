using SplineMesh;
using UnityEngine;

public abstract class WorkerMoveStateBase : StateBase
{
    private readonly Worker _worker;
    private readonly SplinePath _spline;

    private float _sampleRate;

    protected WorkerMoveStateBase(StateMachineBase machine, Worker worker, SplinePath spline)
        : base(machine)
    {
        _worker = worker;
        _spline = spline;
    }

    protected abstract Vector3 GetTarget();

    public override void OnEnter()
    {
        _sampleRate = 0f;
        _spline.Build(_worker.transform, GetTarget());
    }

    public override void OnUpdate(float deltaTime)
    {
        _sampleRate += _worker.Speed * deltaTime / _spline.GetCurve(_sampleRate).Length;

        if (_sampleRate > _spline.NodesCount - 1)
        {
            Machine.FireSignal(WorkerSignal.Arrived);
            return;
        }

        CurveSample sample = _spline.GetCurveSample(_sampleRate);
        _worker.transform.position = sample.location;
        _worker.transform.rotation = sample.Rotation;
    }

    public override void OnExit()
    {
        _sampleRate = 0f;
        _spline.Clear();
    }
}

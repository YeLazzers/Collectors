using System;
using SplineMesh;
using UnityEngine;

public sealed class MoveToState : StateBase
{
    private readonly Worker _worker;
    private readonly SplinePath _spline;
    private readonly Func<Vector3> _getTarget;

    private float _sampleRate;

    public MoveToState(StateMachineBase machine, Worker worker, SplinePath spline, Func<Vector3> getTarget)
        : base(machine)
    {
        _worker = worker;
        _spline = spline;
        _getTarget = getTarget;
    }

    public override void OnEnter()
    {
        _sampleRate = 0f;
        _spline.Build(_worker.transform, _getTarget());
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

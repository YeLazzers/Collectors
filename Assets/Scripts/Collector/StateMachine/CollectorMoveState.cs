using System;
using SplineMesh;
using UnityEngine;

[Serializable]

public struct MoveStateParams
{
    public Vector3 TargetPosition;
    public float Speed;
}

public class CollectorMoveState : StateBase, IParameterizedState<MoveStateParams>
{
    private Collector _collector;
    private MoveStateParams _params;
    private float _sampleRate = 0f;
    private SplinePath _spline;

    public CollectorMoveState(StateMachineBase machine, Collector collector, SplinePath spline)
        : base(machine)
    {
        _collector = collector;
        _spline = spline;
    }

    public void Inject(MoveStateParams parameters)
    {
        _params = parameters;
    }

    public override void OnEnter(Action onComplete)
    {
        _sampleRate = 0f;
        _spline.Build(_collector.transform, _params.TargetPosition);
    }

    public override void OnUpdate(float deltaTime)
    {
        _sampleRate += _params.Speed * Time.deltaTime / _spline.GetCurve(_sampleRate).Length;
        if (_sampleRate > _spline.NodesCount - 1)
            return;

        CurveSample sample = _spline.GetCurveSample(_sampleRate);
        Place(sample);
    }

    public override void OnExit()
    {
        _sampleRate = 0f;
        _spline.Clear();
    }

    private void Place(CurveSample sample)
    {
        _collector.transform.position = sample.location;
        _collector.transform.rotation = sample.Rotation;
    }
}
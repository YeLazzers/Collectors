using System;
using System.Collections;
using UnityEngine;

public sealed class MoveStep : StepBase
{
    private readonly Transform _actor;
    private readonly float _speed;
    private readonly Func<Vector3> _getTarget;
    private readonly float _stopDistance;

    public MoveStep(Transform actor, float speed, Func<Vector3> getTarget, float stopDistance = 0.1f)
    {
        _actor = actor;
        _speed = speed;
        _getTarget = getTarget;
        _stopDistance = stopDistance;
    }

    protected override IEnumerator Run()
    {
        if (_actor == null || _getTarget == null)
        {
            Fail();
            yield break;
        }

        Vector3 startTarget = _getTarget();
        Debug.Log($"[JobPipeline] MoveStep started. Actor={_actor.name}, Target={startTarget}");

        float stopDistanceSqr = _stopDistance * _stopDistance;

        while (IsCancelled == false)
        {
            Vector3 target = _getTarget();
            Vector3 delta = target - _actor.position;

            if (delta.sqrMagnitude <= stopDistanceSqr)
            {
                Succeed();
                yield break;
            }

            float distance = delta.magnitude;
            float stepDistance = _speed * Time.deltaTime;

            if (stepDistance >= distance)
            {
                _actor.position = target;
            }
            else
            {
                _actor.position += (delta / distance) * stepDistance;
            }

            yield return null;
        }
    }
}

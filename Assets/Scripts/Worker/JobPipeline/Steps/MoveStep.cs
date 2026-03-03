using System;
using System.Collections;
using UnityEngine;

public sealed class MoveStep : StepBase
{
    private readonly Worker _worker;
    private readonly Func<Vector3> _getTarget;
    private readonly float _stopDistance;

    public MoveStep(Worker worker, Func<Vector3> getTarget, float stopDistance = 0.1f)
    {
        _worker = worker;
        _getTarget = getTarget;
        _stopDistance = stopDistance;
    }

    protected override IEnumerator Run()
    {
        if (_worker == null || _getTarget == null)
        {
            Fail();
            yield break;
        }

        float stopDistanceSqr = _stopDistance * _stopDistance;

        while (IsCancelled == false)
        {
            Vector3 target = _getTarget();
            Vector3 horizontalDelta = target - _worker.transform.position;
            horizontalDelta.y = 0f;

            if (horizontalDelta.sqrMagnitude <= stopDistanceSqr)
            {
                Succeed();
                yield break;
            }

            float targetAngle = Vector3.SignedAngle(Vector3.forward, horizontalDelta, Vector3.up);
            float newAngle = Mathf.MoveTowardsAngle(_worker.transform.eulerAngles.y, targetAngle, _worker.RotationSpeed * Time.deltaTime);
            _worker.transform.rotation = Quaternion.Euler(0f, newAngle, 0f);

            _worker.transform.position += _worker.transform.forward * _worker.Speed * Time.deltaTime;

            yield return null;
        }
    }
}

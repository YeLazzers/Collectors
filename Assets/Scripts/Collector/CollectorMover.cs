using System.Collections;
using SplineMesh;
using UnityEngine;

public class CollectorMover : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private bool _isOnlyBuildPath = false;

    private SplinePath _splinePath;

    public delegate void MoveCallback();

    private void Awake()
    {
        _splinePath = _splineContainer.CreateEmptySpline(gameObject.GetInstanceID());
    }

    private IEnumerator Moving(MoveCallback OnCompleted = null)
    {
        float t = 0;

        do
        {
            t += _moveSpeed * Time.deltaTime / _splinePath.GetCurve(t).Length;
            CurveSample sample = _splinePath.GetCurveSample(t);
            Place(sample);
            yield return new WaitForEndOfFrame();
        } while (t <= _splinePath.NodesCount - 1);

        if (OnCompleted != null)
            OnCompleted();
    }

    private void Place(CurveSample sample)
    {
        transform.position = sample.location;
        transform.rotation = sample.Rotation;
    }

    public void Move(Vector3 target, MoveCallback OnCompleted = null)
    {
        _splinePath.Build(transform, target);
        if (_isOnlyBuildPath)
            return;

        StartCoroutine(Moving(OnCompleted));
    }
}
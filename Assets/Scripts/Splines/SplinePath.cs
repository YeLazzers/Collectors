using UnityEngine;
using SplineMesh;

[RequireComponent(typeof(Spline))]
public class SplinePath : MonoBehaviour
{
    private Spline _spline;

    public int NodesCount => _spline.nodes.Count;

    private void Awake()
    {
        _spline = GetComponent<Spline>();
        // _spline.gizmos = true;
    }

    public void Build(Transform origin, Vector3 target)
    {
        _spline.curves.Clear();
        _spline.nodes.Clear();

        Vector3 p0 = origin.position;
        Vector3 p2 = new Vector3(target.x, p0.y, target.z);

        // контрольная точка
        float dist = Vector3.Distance(p0, p2);
        Vector3 p1 = p0 + (p2 - p0).normalized * (dist * 0.5f);

        Vector3 dir0 = origin.forward;
        // Vector3 dir0 = (p1 - p0).normalized;
        Vector3 dir1 = ((p0 - p1).normalized + (p2 - p1).normalized).normalized;
        Vector3 dir2 = (p2 - p1).normalized;

        var dir = (p2 - p0).normalized;
        _spline.AddNode(new SplineNode(p0, p0 + dir0));
        // _spline.AddNode(new SplineNode(p1, p1 + dir1));
        _spline.AddNode(new SplineNode(p2, p2));
    }

    public CurveSample GetCurveSample(float t)
    {
        return _spline.GetSample(t);
    }
    public CubicBezierCurve GetCurve(float t)
    {
        return _spline.GetCurve(t);
    }
}
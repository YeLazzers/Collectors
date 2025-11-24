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
    }

    public void Build(Transform origin, Vector3 target)
    {
        Clear();

        Vector3 p0 = origin.position;
        Vector3 p1 = new Vector3(target.x, p0.y, target.z);

        _spline.AddNode(new SplineNode(p0, p0 + origin.forward));
        _spline.AddNode(new SplineNode(p1, p1));
    }

    public CurveSample GetCurveSample(float t)
    {
        return _spline.GetSample(t);
    }
    public CubicBezierCurve GetCurve(float t)
    {
        return _spline.GetCurve(t);
    }

    public void Clear()
    {
        _spline.curves.Clear();
        _spline.nodes.Clear();
    }
}
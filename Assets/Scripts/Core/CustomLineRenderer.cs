using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CustomLineRenderer : MonoBehaviour
{
    private readonly Vector3 v_positionOffset = new Vector3(0, 0.01f, 0);
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private float _width = 0.2f;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3[] points)
    {
        ClearLine();

        _lineRenderer.startWidth = _width;
        _lineRenderer.endWidth = _width;

        // Set the color
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;

        // Set the number of vertices
        _lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            _lineRenderer.SetPosition(i, points[i] + v_positionOffset);
        }
        _lineRenderer.enabled = true;
    }

    public void ClearLine()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.enabled = false;
    }
}
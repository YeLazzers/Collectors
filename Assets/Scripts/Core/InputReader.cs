using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;
    private readonly string Vertical;
    private readonly int MouseLeftButton = 0;

    public event Action<float> HorizontalMoving;
    public event Action<float> VerticalMoving;
    public event Action<Vector3> MouseMoved;
    public event Action<Vector3> Clicked;

    private Vector3 _lastMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseLeftButton))
            Clicked?.Invoke(Input.mousePosition);


        if (_lastMousePosition != Input.mousePosition)
        {
            _lastMousePosition = Input.mousePosition;
            MouseMoved?.Invoke(Input.mousePosition);
        }

        HorizontalMoving?.Invoke(Input.GetAxis(nameof(Horizontal)));
        VerticalMoving?.Invoke(Input.GetAxis(nameof(Vertical)));
    }
}

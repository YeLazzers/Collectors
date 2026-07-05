using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;
    private readonly string Vertical;
    private readonly int MouseLeftButton = 0;
    private readonly int MouseRightButton = 1;

    public event Action<float> HorizontalMoving;
    public event Action<float> VerticalMoving;
    public event Action<float> Scrolled;
    public event Action<Vector3> MouseMoved;
    public event Action<Vector3> LmbClicked;
    public event Action<Vector3> RmbClicked;

    private Vector3 _lastMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseLeftButton))
            LmbClicked?.Invoke(Input.mousePosition);

        if (Input.GetMouseButtonDown(MouseRightButton))
            RmbClicked?.Invoke(Input.mousePosition);


        if (_lastMousePosition != Input.mousePosition)
        {
            _lastMousePosition = Input.mousePosition;
            MouseMoved?.Invoke(Input.mousePosition);
        }

        HorizontalMoving?.Invoke(Input.GetAxis(nameof(Horizontal)));
        VerticalMoving?.Invoke(Input.GetAxis(nameof(Vertical)));
        Scrolled?.Invoke(Input.GetAxis("Mouse ScrollWheel"));
    }
}

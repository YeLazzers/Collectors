using UnityEngine;


public class Cursor : MonoBehaviour
{
    [SerializeField] private Texture2D _dotTexture;
    [SerializeField] private Vector2 _hotspot = Vector2.zero;
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Texture2D _selecterCursor;

    private void Awake()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.SetCursor(_defaultCursor, _hotspot, CursorMode.Auto);
    }

    public void SetSelectCursor()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.SetCursor(_selecterCursor, _hotspot, CursorMode.Auto);
    }

    public void HideCursor()
    {
        UnityEngine.Cursor.visible = false;
    }
}
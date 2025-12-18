using UnityEngine;


public class CursorChanger : MonoBehaviour
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
        Cursor.visible = true;
        Cursor.SetCursor(_defaultCursor, _hotspot, CursorMode.Auto);
    }

    public void SetSelectCursor()
    {
        Cursor.visible = true;
        Cursor.SetCursor(_selecterCursor, _hotspot, CursorMode.Auto);
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }
}
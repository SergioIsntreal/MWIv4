using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    public Texture2D grabCursor;

    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        SetDefault();
    }

    public void SetDefault() => Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    public void SetHover() => Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    public void SetGrab() => Cursor.SetCursor(grabCursor, hotspot, CursorMode.Auto);
}

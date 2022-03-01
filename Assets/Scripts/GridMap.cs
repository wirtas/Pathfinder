using System;
using UnityEngine;
public class GridMap<TGridObject>
{
    private readonly int _width;
    private readonly int _height;
    private readonly float _cellSize;
    private readonly TGridObject[,] _gridArray;
    private readonly Vector3 _originPosition;

    public GridMap(int width, int height, float cellSize, Vector3 originPosition, 
        Func<GridMap<TGridObject>, int, int, TGridObject> gridObject)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        
        _gridArray = new TGridObject[width, height];
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x, y] = gridObject(this, x, y);
            }
        }
    }

    public static TextMesh CreateText(string text, Vector3 localPosition = default(Vector3),
        int fontSize = 20, Color? color = null, TextAlignment textAlignment = TextAlignment.Left,
        TextAnchor textAnchor = TextAnchor.MiddleCenter)
    {
        color ??= Color.white;
        return CreateText(text, localPosition, fontSize, (Color)color, textAlignment, textAnchor);
    }
    private static TextMesh CreateText(string text, Vector3 localPosition, int fontSize, Color color, TextAlignment textAlignment, TextAnchor textAnchor) {
        GameObject gameObject = new GameObject("GridText", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.anchor = textAnchor;
        textMesh.color = color;
        return textMesh;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition-_originPosition).y / _cellSize);
    }

/*
    private void SetGridObject(int x, int y, TGridObject gridObject)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height) return;
        _gridArray[x, y] = gridObject;
        _debugTextArray[x, y].text = _gridArray[x, y]?.ToString();
    }
*/

/*
    public void SetGridObject(Vector3 worldPosition, TGridObject gridObject)
    {
        GetXY(worldPosition, out int x, out int y);
        SetGridObject(x,y,gridObject);
    }
*/

    public TGridObject GetGridObject(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height) return default(TGridObject);
        return _gridArray[x, y];
    }
    
/*
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        GetXY(worldPosition, out int x, out int y);
        return GetGridObject(x,y);
    }
*/

    public static Vector3 GetMousePosition()
    {
        if (Camera.main is { })
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }
        return Vector3.zero;
    }

    public int GetWidth()
    {
        return _width;
    }
    public int GetHeight()
    {
        return _height;
    }

    public float GetSize()
    {
        return _cellSize;
    }
}

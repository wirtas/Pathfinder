using System;
using UnityEngine;
public class GridMap<TGridObject>
{
    private bool _showDebug = true;

    private int _width, _height;
    private float _cellSize;
    private TGridObject[,] _gridArray;
    private TextMesh[,] _debugTextArray;
    private Vector3 _originPosition;

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


        if (_showDebug)
        {
            _debugTextArray = new TextMesh[width, height];
            
            Debug.Log(width + " " + height);
            
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _debugTextArray[x,y] = CreateText(_gridArray[x,y]?.ToString(), GetWorldPosition(x,y) + new Vector3(cellSize,cellSize) * 0.5f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
        }
    }

    private static TextMesh CreateText(string text, Vector3 localPosition = default(Vector3),
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

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition-_originPosition).y / _cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject gridObject)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height) return;
        _gridArray[x, y] = gridObject;
        _debugTextArray[x, y].text = _gridArray[x, y]?.ToString();
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject gridObject)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x,y,gridObject);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height) return default(TGridObject);
        return _gridArray[x, y];
    }
    
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x,y);
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;

    }

    public int GetWidth()
    {
        return _width;
    }
    public int GetHeight()
    {
        return _height;
    }
}

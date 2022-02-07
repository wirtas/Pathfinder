using UnityEngine;
public class GridMap
{
    private int width, height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Vector3 originPosition;

    public GridMap(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
        
        Debug.Log(width + " " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = CreateText(gridArray[x,y].ToString(), GetWorldPosition(x,y) + new Vector3(cellSize,cellSize) * 0.5f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
        
        SetValue(2,1, 37);
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
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return;
        gridArray[x, y] = value;
        debugTextArray[x, y].text = gridArray[x, y].ToString();
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x,y,value);
    }

    public int GetValue(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return -1;
        return gridArray[x, y];
    }
    
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x,y);
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;

    }
}

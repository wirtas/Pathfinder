using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathfindingVisual : MonoBehaviour
{
    private GridMap<PathNode> _gridMap;
    private float _cellSize;
    [SerializeField] private Transform bgPrefab;
    private Transform[,] _bg;
    private Color _defaultColor;
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private Color pathColor =  Color.blue;

    private void Awake()
    {
        _defaultColor = bgPrefab.GetComponent<SpriteRenderer>().color;
    }

    public void SetGridMap(GridMap<PathNode> gridMap)
    {
        _gridMap = gridMap;
        _bg = new Transform[_gridMap.GetWidth(),_gridMap.GetHeight()];
        InitialDrawPathfindingVisual();
    }

    private void InitialDrawPathfindingVisual()
    {
        _cellSize = _gridMap.GetSize();
        for (int x = 0; x < _gridMap.GetWidth(); x++)
        {
            for (int y = 0; y < _gridMap.GetHeight(); y++)
            {
                Transform current = Instantiate(bgPrefab, transform);
                current.position = _gridMap.GetWorldPosition(x, y) + new Vector3(_cellSize/2, _cellSize/2);
                current.localScale = new Vector3(_cellSize*0.9f, _cellSize*0.9f);
                _bg[x, y] = current;
            }
        }
        
        _bg[0,0].GetComponent<SpriteRenderer>().color = startColor;
    }

    public void DrawPath(List<PathNode> path, int x, int y)
    {
        ClearMap();
        for (int i = 1; i < path.Count - 1; i++)
        {
            _bg[path[i].X, path[i].Y].GetComponent<SpriteRenderer>().color = pathColor;
            _bg[x, y].GetComponent<SpriteRenderer>().color = endColor;
        }
    }

    private void ClearMap()
    {
        for (int x = 0; x < _gridMap.GetWidth(); x++)
        {
            for (int y = 0; y < _gridMap.GetHeight(); y++)
            {
                _bg[x, y].GetComponent<SpriteRenderer>().color = _defaultColor;
            }
        }
        _bg[0,0].GetComponent<SpriteRenderer>().color = startColor;
    }
}

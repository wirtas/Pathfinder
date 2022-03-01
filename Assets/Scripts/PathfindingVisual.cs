using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingVisual : MonoBehaviour
{
    private GridMap<PathNode> _gridMap;
    private Pathfinding _pathfinding;
    
    private float _cellSize;
    private Transform[,] _bg;
    private Color _defaultColor;
    private TextMesh[,] _fCostTextArray;
    
    [SerializeField] private Transform bgPrefab;
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private Color pathColor =  Color.blue;
    [SerializeField] private Color wallColor =  Color.black;



    public void SetGridMap(GridMap<PathNode> gridMap, Pathfinding pathfinding)
    {
        _defaultColor = bgPrefab.GetComponent<SpriteRenderer>().color;
        _gridMap = gridMap;
        _pathfinding = pathfinding;
        _cellSize = _gridMap.GetSize();
        _bg = new Transform[_gridMap.GetWidth(),_gridMap.GetHeight()];
        _fCostTextArray = new TextMesh[_gridMap.GetWidth(), _gridMap.GetHeight()];
        
        InitialDrawPathfindingVisual();
    }

    private void InitialDrawPathfindingVisual()
    {
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

        
        
        if(_fCostTextArray != null)
            for (int i = 0; i < _gridMap.GetWidth(); i++)
            {
                for (int j = 0; j < _gridMap.GetHeight(); j++)
                {
                    if (_fCostTextArray[i, j] != null)
                    {
                        Destroy(_fCostTextArray[i, j].gameObject);
                    }
                }
            }

        for (int i = 0; i < _gridMap.GetWidth(); i++)
        {
            for (int j = 0; j < _gridMap.GetHeight(); j++)
            {
                if (_gridMap.GetGridObject(i,j).FCost != Int32.MaxValue && _gridMap.GetGridObject(i,j).FCost >= 0)
                {
                    if (_fCostTextArray != null)
                        _fCostTextArray[i, j] = GridMap<PathNode>.CreateText(
                            _gridMap.GetGridObject(i, j).FCost.ToString(),
                            _gridMap.GetWorldPosition(i, j) + new Vector3(_cellSize, _cellSize) * 0.5f);
                }
            }
        }
        
    }

    private void ClearMap()
    {
        for (int x = 0; x < _gridMap.GetWidth(); x++)
        {
            for (int y = 0; y < _gridMap.GetHeight(); y++)
            {
                if(!_pathfinding.GetNode(x, y).IsWalkable) continue;
                _bg[x, y].GetComponent<SpriteRenderer>().color = _defaultColor;
            }
        }
        _bg[0,0].GetComponent<SpriteRenderer>().color = startColor;
    }

    public void DrawWall(int x, int y)
    {
        ClearMap();
        _bg[x, y].GetComponent<SpriteRenderer>().color =
             _pathfinding.GetNode(x, y).IsWalkable ? _defaultColor : wallColor;
    }
}

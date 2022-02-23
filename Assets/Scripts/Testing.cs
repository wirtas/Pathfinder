using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform originPosition;
    [SerializeField] [Range(1,48)] private int width = 3;
    [SerializeField] [Range(1,27)] private int height = 3;
    [SerializeField] [Range(3,40)] private float cellSize = 10f;
    
    private GridMap<bool> _gridMap;
    private Pathfinding _pathfinding;
    private void Start()
    { 
        _pathfinding = new Pathfinding(width, height, cellSize, originPosition.position);
        //_gridMap = new GridMap<bool>(width, height, cellSize, originPosition.position, (g, x, y) => false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // _gridMap.SetGridObject(GridMap<bool>.GetMousePosition(), true);
            _pathfinding.GetGridMap().GetXY(GridMap<PathNode>.GetMousePosition(), out int x, out int y);
            Debug.Log(x + " " + y);
            List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log(i + " " + path[i].ToString());
                    Debug.DrawLine(new Vector3(path[i].X, path[i].Y) * cellSize + originPosition.position + Vector3.one * cellSize/2,
                        new Vector3(path[i+1].X, path[i+1].Y) * cellSize + originPosition.position + Vector3.one * cellSize/2, Color.blue, 10f);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
           //
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform originPosition;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] [Range(1,142)] private int width = 20;
    [SerializeField] [Range(1,80)] private int height = 15;
    [SerializeField] [Range(1,20)] private float cellSize = 10f;
    
    
    private GridMap<PathNode> _gridMap;
    private Pathfinding _pathfinding;
    private void Start()
    { 
        _pathfinding = new Pathfinding(width, height, cellSize, originPosition.position);
        _gridMap = _pathfinding.GetGridMap();
        pathfindingVisual.SetGridMap(_gridMap, _pathfinding);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pathfinding.GetGridMap().GetXY(GridMap<PathNode>.GetMousePosition(), out int x, out int y);
            if (x >= width || y >= height || x < 0 || y < 0) return;
            
            List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                pathfindingVisual.DrawPath(path, x ,y);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _pathfinding.GetGridMap().GetXY(GridMap<PathNode>.GetMousePosition(), out int x, out int y);
            _pathfinding.GetNode(x, y).SetIsWalkable(!_pathfinding.GetNode(x, y).IsWalkable);
            pathfindingVisual.DrawWall(x,y);
        }
    }
}

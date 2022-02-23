using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Pathfinding
{
    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;
    
    private GridMap<PathNode> _gridMap;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;
    public Pathfinding(int width, int height, float cellSize, Vector3 originPosition)
    {
        _gridMap = new GridMap<PathNode>(width, height, cellSize, originPosition,
            (g, x, y) => new PathNode(g, x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _gridMap.GetGridObject(startX, startY);
        PathNode endNode = _gridMap.GetGridObject(endX, endY);
        
        _openList = new List<PathNode> {startNode};
        _closedList = new List<PathNode>();

        for (int x = 0; x < _gridMap.GetWidth(); x++)
        {
            for (int y = 0; y < _gridMap.GetHeight(); y++)
            {
                PathNode pathNode = _gridMap.GetGridObject(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFromNode = null;
            }
        }
        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);
            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neighbourNode)) continue;
                
                int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.CameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!_openList.Contains(neighbourNode))
                        {
                            _openList.Add(neighbourNode);
                        }
                    }
                
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.X - 1 >= 0)
        {
            //LEFT
            neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            //LEFT DOWN
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            }
            //LEFT UP
            if (currentNode.Y + 1 < _gridMap.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
            }
        }
        if (currentNode.X + 1 < _gridMap.GetWidth())
        {
            //RIGHT
            neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            //RIGHT DOWN
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
            }
            //RIGHT UP
            if (currentNode.Y + 1 < _gridMap.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
            }
        }
            //DOWN
        if (currentNode.Y - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X,currentNode.Y - 1));
        }
            //UP
        if (currentNode.Y + 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X,currentNode.Y + 1));
        }

        
        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return _gridMap.GetGridObject(x, y);
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.CameFromNode != null)
        {
            path.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
    }

    private PathNode GetLowestFCostNode(IReadOnlyList<PathNode> pathNodes)
    {
        PathNode lowestFCostNode = pathNodes[0];
        for(int i = 1; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }

        return lowestFCostNode;
    }

    public GridMap<PathNode> GetGridMap()
    {
        return _gridMap;
    }

}

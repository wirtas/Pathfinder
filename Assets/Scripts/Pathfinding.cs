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

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
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
        }

        return null;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        return null;
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
        foreach (PathNode node in pathNodes.Where(node => node.FCost < lowestFCostNode.FCost))
        {
            lowestFCostNode = node;
        }

        return lowestFCostNode;
    }
}

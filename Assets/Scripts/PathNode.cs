public class PathNode
{
    private GridMap<PathNode> _gridMap;

    public int X { get; }

    public int Y { get; }

    public int GCost, HCost, FCost;
    // G - Start -> Current
    // H - Current -> End
    // F = G+H
    public bool IsWalkable { get; set; }
    public PathNode CameFromNode;
    
    public PathNode(GridMap<PathNode> gridMap, int x, int y)
    {
        _gridMap = gridMap;
        X = x;
        Y = y;
        IsWalkable = true;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
    public override string ToString()
    {
        return X + "," + Y;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        IsWalkable = isWalkable;
    }
}

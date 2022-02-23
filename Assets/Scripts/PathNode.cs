public class PathNode
{
    private GridMap<PathNode> _gridMap;

    public int X { get; set; }

    public int Y { get; set; }

    public int GCost, HCost, FCost;
    public PathNode CameFromNode;
    
    

    public PathNode(GridMap<PathNode> gridMap, int x, int y)
    {
        _gridMap = gridMap;
        X = x;
        Y = y;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
    public override string ToString()
    {
        return X + "," + Y;
    }
}

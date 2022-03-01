public class PathNode
{
    public int X { get; }

    public int Y { get; }

    public int GCost, HCost, FCost;
    // G - Start -> Current
    // H - Current -> End
    // F = G+H
    public bool IsWalkable { get; private set; }
    public PathNode CameFromNode;
    
    public PathNode(int x, int y)
    {
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

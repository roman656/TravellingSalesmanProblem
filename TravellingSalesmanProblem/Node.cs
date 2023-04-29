namespace TravellingSalesmanProblem;

public struct Node
{
    public int Index { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public Node(int index, double x = 0.0, double y = 0.0)
    {
        Index = index;
        X = x;
        Y = y;
    }

    public override string ToString() => $"Node №{Index} (x: {X}; y: {Y})";
}
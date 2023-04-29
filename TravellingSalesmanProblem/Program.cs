namespace TravellingSalesmanProblem;

public static class Program
{
    private const int NodesAmount = 8;
    private static readonly double[,] Distances = new double[NodesAmount, NodesAmount];
    private static readonly List<Node> Nodes = new(NodesAmount)
    {
        new Node(1, 0.0, 0.0),
        new Node(2, 1.0, 5.0),
        new Node(3, 2.0, 3.0),
        new Node(4, 3.0, 6.0),
        new Node(5, 4.0, 5.0),
        new Node(6, 5.0, 8.0),
        new Node(7, 6.0, 9.0),
        new Node(8, 7.0, 1.0),
    };
    private static int[] _bestPath = null!;
    private static double _bestPathDistance;
    
    public static void Main(string[] args)
    {
        PrintNodes();
        CalculateDistances();
        PrintDistances();
        
        _bestPath = GetStartPath();
        _bestPathDistance = GetPathDistance(_bestPath);
        
        var pathPermutations = GetPermutations(GetStartPath());
        
        foreach (var currentPath in pathPermutations)
        {
            if (!CheckPath(currentPath)) continue;
            
            var currentPathDistance = GetPathDistance(currentPath);

            if (currentPathDistance < _bestPathDistance)
            {
                _bestPathDistance = currentPathDistance;
                _bestPath = currentPath;
            }
        }
        
        PrintResult();
    }
    
    private static List<int[]> GetPermutations(int[] array)
    {
        var permutations = new List<int[]>();

        Permute(array, 0, permutations);
        
        return permutations;
    }
    
    private static void Permute(int[] array, int index, List<int[]> permutations)
    {
        if (index == array.Length - 1)
        {
            permutations.Add((int[])array.Clone());
            return;
        }
        
        for (var j = index; j < array.Length; j++)
        {
            (array[index], array[j]) = (array[j], array[index]);
            Permute(array, index + 1, permutations);
            (array[index], array[j]) = (array[j], array[index]);
        }
    }

    private static bool CheckPath(int[] path) => (path[0] == 1 && path[^1] == 1);

    private static int[] GetStartPath()
    {
        const int arraySize = NodesAmount + 1;
        var path = new int[arraySize];

        for (var i = 0; i < arraySize; i++)
        {
            path[i] = i + 1;
        }

        path[^1] = 1;

        return path;
    }

    private static double GetPathDistance(int[] path)
    {
        var distance = 0.0;

        for (var i = 0; i < path.Length - 1; i++)
        {
            distance += Distances[path[i] - 1, path[i + 1] - 1];
        }
        
        return distance;
    }

    private static void PrintResult()
    {
        Console.WriteLine("Best path:");
        
        for (var i = 0; i < _bestPath.Length; i++)
        {
            if (i == _bestPath.Length - 1)
            {
                Console.Write($"{_bestPath[i]}\nDistance: {_bestPathDistance}");
            }
            else
            {
                Console.Write($"{_bestPath[i]} -> ");
            }
        }
        
        Console.WriteLine();
    }

    private static void PrintNodes()
    {
        Console.WriteLine("Nodes:");
        
        for (var i = 0; i < NodesAmount; i++)
        {
            Console.WriteLine(Nodes[i]);
        }
        
        Console.WriteLine();
    }

    private static void PrintDistances()
    {
        Console.WriteLine("Distances:");
        
        for (var i = 0; i < NodesAmount; i++)
        {
            Console.Write("|");
            
            for (var j = 0; j < NodesAmount; j++)
            {
                Console.Write($" {Distances[i, j], 20} |");
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }

    private static void CalculateDistances()
    {
        for (var i = 0; i < NodesAmount; i++)
        {
            for (var j = 0; j < NodesAmount; j++)
            {
                Distances[i, j] = GetEuclideanDistance(Nodes[i], Nodes[j]);
            }
        }
    }

    private static double GetEuclideanDistance(Node firstNode, Node secondNode) =>
            Math.Sqrt(Math.Pow(firstNode.X - secondNode.X, 2) + Math.Pow(firstNode.Y - secondNode.Y, 2));
}
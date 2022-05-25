using System.Collections;

namespace LearningLinq.Utils;

public class QueryUtils
{
    public static void DisplayQuery(IEnumerable query, string message)
    {
        Console.WriteLine($"{message}:");
        foreach (var x in query)
        {
            Console.WriteLine($"\t{x}");
        }
        Console.WriteLine("\n");
    }

    public static IEnumerable<int> CreateIntRange(int start = 0, int stop = 100, int increment = 1, bool inclusive = true)
    {
        var arr = new List<int>();
        if (inclusive) stop += increment;

        for (var i = start; i < stop; i += increment)
        {
            arr[i] = i;
        }

        return arr;
    }

    public static IEnumerable<double> CreateDoubleRange(double start = 0, double stop = 100, double increment = 1, bool inclusive = true)
    {
        var arr = new List<double>();
        if (inclusive) stop += increment;

        for (var i = start; i < stop; i += increment)
        {
            arr.Add(i);
        }

        return arr;
    }
}
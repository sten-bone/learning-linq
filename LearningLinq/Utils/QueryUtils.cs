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

    public static IEnumerable<int> CreateIntRange(int start = 0, int stop = 100, int increment = 1)
    {
        var arr = new int[stop - start];
        for (var i = start; i < arr.Length; i += increment)
        {
            arr[i] = i;
        }

        return arr;
    }
}
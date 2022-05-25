using System.Linq;
using System.Collections;

namespace LearningLinq;

internal class Program
{
    private static void Main()
    {
        BasicIntQueries();
    }

    private static void BasicIntQueries()
    {
        var scores = CreateIntRange();

        var greaterThanEightyQuery = scores.Where(x => x > 80);
        DisplayQuery(greaterThanEightyQuery, "Greater than 80");


    }

    private static void DisplayQuery(IEnumerable query, string message)
    {
        Console.Write($"{message}: ");
        foreach (var x in query)
        {
            Console.Write($"{x} ");
        }
        Console.WriteLine("\n");
    }

    private static IEnumerable<int> CreateIntRange(int start = 0, int stop = 100, int increment = 1)
    {
        var arr = new int[stop - start];
        for (var i = start; i < arr.Length; i += increment)
        {
            arr[i] = i;
        }

        return arr;
    }
}
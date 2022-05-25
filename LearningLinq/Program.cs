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
        var scores = new int[100];
        for (var i = 0; i < scores.Length; i++)
        {
            scores[i] = i;
        }

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
}
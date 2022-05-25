using LearningLinq.Utils;

namespace LearningLinq;

internal class Program
{
    private static void Main()
    {
        BasicIntQueries();
    }

    private static void BasicIntQueries()
    {
        var scores = QueryUtils.CreateIntRange();

        var greaterThanEightyQuery = scores.Where(x => x > 80);
        QueryUtils.DisplayQuery(greaterThanEightyQuery, "Greater than 80");

        var toSeven = QueryUtils.CreateIntRange(stop: 7);
        var queryEvens = toSeven.Where(x => x % 2 == 0);
        QueryUtils.DisplayQuery(queryEvens, "Even numbers");

    }
}
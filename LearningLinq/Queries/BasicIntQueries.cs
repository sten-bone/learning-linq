using LearningLinq.Utils;

namespace LearningLinq.Queries;

public class BasicIntQueries
{
    public static void Query()
    {
        var scores = QueryUtils.CreateIntRange();

        var greaterThanEightyQuery = scores.Where(x => x > 80);
        QueryUtils.DisplayQuery(greaterThanEightyQuery, "Greater than 80");

        var toSeven = QueryUtils.CreateIntRange(stop: 7);
        var queryEvens = toSeven.Where(x => x % 2 == 0);
        QueryUtils.DisplayQuery(queryEvens, "Even numbers");

    }
}
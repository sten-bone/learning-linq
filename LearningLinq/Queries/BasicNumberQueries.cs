using LearningLinq.Utils;

namespace LearningLinq.Queries;

public class BasicNumberQueries
{
    public static void BasicQuery()
    {
        var scores = QueryUtils.CreateIntRange();

        var greaterThanEightyQuery = scores.Where(x => x > 80);
        QueryUtils.DisplayQuery(greaterThanEightyQuery, "Greater than 80");

        var toSeven = QueryUtils.CreateIntRange(stop: 7);
        var queryEvens = toSeven.Where(x => x % 2 == 0);
        QueryUtils.DisplayQuery(queryEvens, "Even numbers");

    }

    public static void RadiusQuery()
    {
        var radii = QueryUtils.CreateDoubleRange(start: 1, stop: 3);
        var radiusQuery = radii.Select(x => new
            {
                Radius = x,
                Area = x * x * Math.PI
            })
            .OrderByDescending(x => x.Area)
            .Select(x => $"Area for circle with radius {x.Radius} = {x.Area:F2}");
        QueryUtils.DisplayQuery(radiusQuery, "Calculated radius");
    }
}
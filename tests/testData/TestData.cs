using System;
using System.Collections.Generic;
using ascender.Enum;

namespace Tests.testData;

public static class TestData
{
    private static readonly Random Random = new();

    public static string AMetricName(Direction direction)
    {
        var increaseActions = new List<string>
        {
            "improve", "reduce", "increase", "raise", "enhance", "promote", "recover", "upgrade", "develop"
        };

        var decreaseActions = new List<string>
        {
            "decrease", "cut back", "remove", "reduce", "trim", "cut down", "lessen", "scale down", "shorten"
        };

        var metrics = new List<string>
        {
            "stuff", "things", "foo", "bar", "baz", "glorp", "glurp", "frobnication", "frobulation", "frobfuscation", "something"
        };

        var action = direction == Direction.Increase
            ? PickRandom(increaseActions)
            : PickRandom(decreaseActions);

        var metric = PickRandom(metrics);

        var mangle = Random.Next().ToString();

        return $"{action} {metric} {mangle}";
    }

    private static T PickRandom<T>(List<T> list) => list[Random.Next(list.Count)];
}